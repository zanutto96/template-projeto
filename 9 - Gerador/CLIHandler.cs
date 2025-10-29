using Gerador.Model;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Text;

namespace Gerador;

public class CLIHandler
{
    private readonly AppSettings _settings;
    private readonly Repository _repository;
    private List<TableToGenerate> _tableList = new();
    private string? _rootPath;
    private string? _spaUI;
    private string? _controllers;
    private string? _service;
    private string? _serviceBusiness;
    private string? _serviceValidation;
    private string? _domain;
    private string? _domainEntities;
    private string? _domainFilters;
    private string? _data;
    private string? _dataBasicExtension;
    private string? _dataConfiguration;
    private string? _dataRepository;
    private string? _backEndTemplatePath;
    private string? _frontEndTemplatePath;
    private List<string> _apiInjectionInstructions = new();
    private List<string> _routingInstructions = new();
    private List<string> _dataContextInstructions = new();

    public CLIHandler(AppSettings settings)
    {
        _settings = settings;
        _repository = new Repository(_settings.ConnectionString);
        _backEndTemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "BackEnd");
        _frontEndTemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "FrontEnd");
    }

    public async Task GenerateAsync(string? tableName = null)
    {
        Console.WriteLine("Conectando ao banco de dados...");
        _tableList = _repository.GetTableToGenerates();

        if (tableName != null)
        {
            var table = _tableList.FirstOrDefault(t => t.TableName.Equals(tableName, StringComparison.OrdinalIgnoreCase));
            if (table == null)
            {
                Console.WriteLine($"Tabela '{tableName}' não encontrada.");
                return;
            }
            _tableList = new List<TableToGenerate> { table };
        }

        // Extrai o nome do banco da connection string
        string databaseName = ExtractDatabaseName(_settings.ConnectionString);
        _rootPath = Path.Combine(_settings.BackPath, databaseName);

        SetupPaths();
        CreateDirectories();

        Console.WriteLine($"Gerando código para {_tableList.Count} tabela(s)...");

        foreach (var table in _tableList)
        {
            Console.WriteLine($"Gerando {table.TableName}...");
            GenerateBack(table);
            GenerateFront(table);
        }

        WriteInstructionsFile();
        Console.WriteLine("Geração concluída!");
        Console.WriteLine($"Arquivos gerados em: {_rootPath}");
    }

    private void SetupPaths()
    {
        _spaUI = Path.Combine(_settings.FrontPath, "src", "app");
        _controllers = Path.Combine(_settings.BackPath, "Api", "Controllers");
        _service = Path.Combine(_settings.BackPath, "Service");
        _serviceBusiness = Path.Combine(_settings.BackPath, "Service", "Business");
        _serviceValidation = Path.Combine(_settings.BackPath, "Service", "Validation");
        _domain = Path.Combine(_settings.BackPath, "Domain");
        _domainEntities = Path.Combine(_settings.BackPath, "Domain", "Entities");
        _domainFilters = Path.Combine(_settings.BackPath, "Domain", "Filters");
        _data = Path.Combine(_settings.BackPath, "Data");
        _dataBasicExtension = Path.Combine(_settings.BackPath, "Data", "BasicExtension");
        _dataConfiguration = Path.Combine(_settings.BackPath, "Data", "Configuration");
        _dataRepository = Path.Combine(_settings.BackPath, "Data", "Repository");
    }

    private void CreateDirectories()
    {
        var directories = new[]
        {
            _spaUI, _controllers, _service, _serviceBusiness, _serviceValidation,
            _domain, _domainEntities, _domainFilters, _data, _dataBasicExtension,
            _dataConfiguration, _dataRepository
        };

        foreach (var dir in directories)
        {
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }

    private void GenerateBack(TableToGenerate tb)
    {
        GenerateController(tb);
        GenerateService(tb);
        GenerateDomain(tb);
        GenerateRepository(tb);
    }

    private void GenerateFront(TableToGenerate tb)
    {
        _routingInstructions.Add($"{{ path: '{tb.TableName.ToLower()}', loadChildren: () => import('./{tb.TableName.ToLower()}/{tb.TableName.ToLower()}.module').then(m => m.{tb.TableName}Module) }},");

        string componentPath = Path.Combine(_spaUI!, tb.TableName.ToLower());
        string registerPath = Path.Combine(componentPath, $"{tb.TableName.ToLower()}-register");
        string listPath = Path.Combine(componentPath, $"{tb.TableName.ToLower()}-list");

        Directory.CreateDirectory(registerPath);
        Directory.CreateDirectory(listPath);

        Util.WriteDocument(Path.Combine(_frontEndTemplatePath!, "service.ts.tpl"), Path.Combine(componentPath, $"{tb.TableName.ToLower()}.service.ts"), tb);
        Util.WriteDocument(Path.Combine(_frontEndTemplatePath!, "module.ts.tpl"), Path.Combine(componentPath, $"{tb.TableName.ToLower()}.module.ts"), tb);
        Util.WriteDocument(Path.Combine(_frontEndTemplatePath!, "component.ts.tpl"), Path.Combine(componentPath, $"{tb.TableName.ToLower()}.component.ts"), tb);
        Util.WriteDocument(Path.Combine(_frontEndTemplatePath!, "component.html.tpl"), Path.Combine(componentPath, $"{tb.TableName.ToLower()}.component.html"), tb);
        Util.WriteDocument(Path.Combine(_frontEndTemplatePath!, "component.css.tpl"), Path.Combine(componentPath, $"{tb.TableName.ToLower()}.component.scss"), tb);
        Util.WriteDocument(Path.Combine(_frontEndTemplatePath!, "routing.module.ts.tpl"), Path.Combine(componentPath, $"{tb.TableName.ToLower()}-routing.module.ts"), tb);

        Util.WriteDocument(Path.Combine(_frontEndTemplatePath!, "register.component.ts.tpl"), Path.Combine(registerPath, $"{tb.TableName.ToLower()}-register.component.ts"), tb);
        Util.WriteDocument(Path.Combine(_frontEndTemplatePath!, "register.component.html.tpl"), Path.Combine(registerPath, $"{tb.TableName.ToLower()}-register.component.html"), tb);
        Util.WriteDocument(Path.Combine(_frontEndTemplatePath!, "register.component.css.tpl"), Path.Combine(registerPath, $"{tb.TableName.ToLower()}-register.component.scss"), tb);

        Util.WriteDocument(Path.Combine(_frontEndTemplatePath!, "list.component.ts.tpl"), Path.Combine(listPath, $"{tb.TableName.ToLower()}-list.component.ts"), tb);
        Util.WriteDocument(Path.Combine(_frontEndTemplatePath!, "list.component.html.tpl"), Path.Combine(listPath, $"{tb.TableName.ToLower()}-list.component.html"), tb);
        Util.WriteDocument(Path.Combine(_frontEndTemplatePath!, "list.component.css.tpl"), Path.Combine(listPath, $"{tb.TableName.ToLower()}-list.component.scss"), tb);
    }

    private void GenerateController(TableToGenerate tb)
    {
        _apiInjectionInstructions.Add($"\t\t\tservices.AddScoped<{tb.TableName}Service>();");
        _apiInjectionInstructions.Add($"\t\t\tservices.AddScoped<{tb.TableName}Repository>();");

        Util.WriteDocument(Path.Combine(_backEndTemplatePath!, "Controller.tpl"), Path.Combine(_controllers!, $"{tb.TableName}Controller.cs"), tb);
    }

    private void GenerateService(TableToGenerate tb)
    {
        Util.WriteDocument(Path.Combine(_backEndTemplatePath!, "Service.tpl"), Path.Combine(_serviceBusiness!, $"{tb.TableName}Service.cs"), tb);
        Util.WriteDocument(Path.Combine(_backEndTemplatePath!, "Validation.tpl"), Path.Combine(_serviceValidation!, $"{tb.TableName}Validation.cs"), tb);
    }

    private void GenerateDomain(TableToGenerate tb)
    {
        Util.WriteDocument(Path.Combine(_backEndTemplatePath!, "Entity.tpl"), Path.Combine(_domainEntities!, $"{tb.TableName}.cs"), tb);
        Util.WriteDocument(Path.Combine(_backEndTemplatePath!, "Filter.tpl"), Path.Combine(_domainFilters!, $"{tb.TableName}Filter.cs"), tb);
    }

    private void GenerateRepository(TableToGenerate tb)
    {
        _dataContextInstructions.Add($"public virtual DbSet<{tb.TableName}> {tb.TableName} {{ get; set; }}");

        Util.WriteDocument(Path.Combine(_backEndTemplatePath!, "FilterBasicExtension.tpl"), Path.Combine(_dataBasicExtension!, $"{tb.TableName}FilterBasicExtension.cs"), tb);
        Util.WriteDocument(Path.Combine(_backEndTemplatePath!, "Configuration.tpl"), Path.Combine(_dataConfiguration!, $"{tb.TableName}Configuration.cs"), tb);
        Util.WriteDocument(Path.Combine(_backEndTemplatePath!, "Repository.tpl"), Path.Combine(_dataRepository!, $"{tb.TableName}Repository.cs"), tb);
    }

    private void WriteInstructionsFile()
    {
        string instructionsPath = Path.Combine(_rootPath!, "Instructions.txt");
        using StreamWriter writer = new(instructionsPath, false, Encoding.UTF8);

        writer.WriteLine("Copy and paste the code below into the given files");
        writer.WriteLine("");
        writer.WriteLine("Front End");
        writer.WriteLine("src/app/app-routing.module.ts");
        foreach (var instruction in _routingInstructions)
        {
            writer.WriteLine(instruction);
        }
        writer.WriteLine("");
        writer.WriteLine("BackEnd > Api > Config > ConfigConteiner");
        foreach (var instruction in _apiInjectionInstructions)
        {
            writer.WriteLine(instruction);
        }
        writer.WriteLine("");
        writer.WriteLine("BackEnd > Data > Context > DataContext.cs");
        foreach (var instruction in _dataContextInstructions)
        {
            writer.WriteLine(instruction);
        }
        writer.WriteLine("");
        writer.WriteLine("To create a basic angular page follow the next steps");
        writer.WriteLine("1 - Open cmd into Main folder, create a new angular component for Client using AngularCLI");
        writer.WriteLine("   ng g m client --routing");
        writer.WriteLine("   ng g c client");
        writer.WriteLine("2 - into CMD change to component folder, for this example client and create the service component");
        writer.WriteLine("   ng g service client");
        writer.WriteLine("3 - into routing component on Route Key: { path: '', data : { title : \"Client\" }, component: ClientComponent },");
        writer.WriteLine("4 - then create a route into app.routing { path: 'client',  loadChildren: () => import('./main/client/client.module').then(m => m.ClientModule) },");
        writer.WriteLine("");
    }

    private string ExtractDatabaseName(string connectionString)
    {
        try
        {
            var parts = connectionString.Split(';');
            foreach (var part in parts)
            {
                var keyValue = part.Split('=');
                if (keyValue.Length == 2)
                {
                    var key = keyValue[0].Trim().ToLower();
                    if (key == "initial catalog" || key == "database")
                    {
                        return keyValue[1].Trim();
                    }
                }
            }
        }
        catch
        {
            // Se não conseguir extrair, usa um nome padrão
        }
        return "Database";
    }
}