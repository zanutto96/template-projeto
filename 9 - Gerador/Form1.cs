using Gerador.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.DirectoryServices.ActiveDirectory;
using System;
using System.Data;
using System.Text;
using System.Diagnostics;

namespace Gerador;

public partial class Form1 : Form
{
   #region Variavel#
   private List<TableToGenerate> TableList = new List<TableToGenerate>();
   protected string? RootPath;
   string? SpaUI;
   string? SpaMain;
   string? SpaMainList;
   string? SpaMainRegister;
   string? Controllers;
   string? Service;
   string? ServiceBusiness;
   string? ServiceValidation;
   string? Domain;
   string? DomainEntities;
   string? DomainFilters;
   string? Data;
   string? DataBasicExtension;
   string? DataConfiguration;
   string? DataRepository;
   string? BackEndTemplatePath;
   string? FrontEndTemplatePath;
   List<string> ApiInjectionInstructions = new List<string>();
   List<string> RoutingInstructions = new List<string>();
   List<string> DataContextInstructions = new List<string>();

   #endregion#
   public Form1()
   {
      InitializeComponent();
   }

   private void Form1_Load(object sender, EventArgs e)
   {
      BackEndTemplatePath = System.IO.Directory.GetCurrentDirectory() + "\\Templates\\BackEnd\\";
      FrontEndTemplatePath = System.IO.Directory.GetCurrentDirectory() + "\\Templates\\FrontEnd\\";
      
      // Exemplo para SQL Server (padrão)
      txtConnectionString.Text = "Data Source=.\\SQLEXPRESS;Initial Catalog=Hoffman-DEV;user id=api;password=api;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;MultipleActiveResultSets=True";
      
      // Para usar MySQL, substitua pela connection string do MySQL:
      // txtConnectionString.Text = "Server=localhost;Database=Hoffman-DEV;Uid=api;Pwd=api;";
      
      // Tooltip com ajuda
      ToolTip tooltip = new ToolTip();
      tooltip.SetToolTip(txtConnectionString, 
         "SQL Server: Data Source=servidor;Initial Catalog=banco;user id=usuario;password=senha;\n" +
         "MySQL: Server=servidor;Database=banco;Uid=usuario;Pwd=senha;");
   }

   private void button1_Click(object sender, EventArgs e)
   {
      FolderBrowserDialog f = new FolderBrowserDialog();
      f.ShowDialog();
      if (!string.IsNullOrEmpty(f.SelectedPath))
      {
         txtDestino.Text = f.SelectedPath;
      }
   }

   protected void button2_Click(object sender, EventArgs e)
   {
      if (string.IsNullOrEmpty(txtDestino.Text))
      {
         MessageBox.Show("Informe o local de destino!");
         return;
      }

      if (string.IsNullOrEmpty(txtConnectionString.Text))
      {
         MessageBox.Show("Informe a connection string!");
         return;
      }

      Repository rep = new Repository(txtConnectionString.Text);
      TableList = rep.GetTableToGenerates();
      dataGridView1.AutoGenerateColumns = false;
      dataGridView1.DataSource = TableList;

      // Extrai o nome do banco da connection string para usar no diretório
      string databaseName = ExtractDatabaseName(txtConnectionString.Text);
      RootPath = @"" + txtDestino.Text + "\\" + databaseName;
      txtDestino.Enabled = false;
      txtConnectionString.Enabled = false;
   }

   private void button3_Click(object sender, EventArgs e)
   {
      dataGridView1.EndEdit();
      foreach (DataGridViewRow row in dataGridView1.Rows)
      {
         var table = TableList.Where(_ => _.TableName == row.Cells["TableName"].Value.ToString()).First();
         table.Selected = Convert.ToBoolean(row.Cells["Selected"].Value);
         table.GenerateFront = Convert.ToBoolean(row.Cells["Front"].Value);
         table.GenerateBack = Convert.ToBoolean(row.Cells["Back"].Value);

      }

      GerarPastas();
      progressBar1.Visible = true;
      progressBar1.Maximum = TableList.Count();
      progressBar1.Value = 0;

      foreach (var tb in TableList)
      {
         progressBar1.Value = progressBar1.Value + 1;

         if (tb.Selected)
         {

            if (tb.GenerateBack) GenerateBack(tb);
            if (tb.GenerateFront) GenerateFront(tb);
         }
      }

      //write instructions file
      StreamWriter xfile = new StreamWriter(RootPath + "\\Instructions.txt", false, Encoding.UTF8);
      xfile.WriteLine("Copy and paste the code below into the given files");
      xfile.WriteLine("");
      xfile.WriteLine("Front End");
      xfile.WriteLine("SPAUI > App > app.routing.module.ts into Main root");
      foreach (var s in RoutingInstructions)
      {
         xfile.WriteLine(s);
      }
      xfile.WriteLine("");
      xfile.WriteLine("BackEnd > Api > Config > ConfigConteiner");
      foreach (var s in ApiInjectionInstructions)
      {
         xfile.WriteLine(s);
      }
      xfile.WriteLine("");
      xfile.WriteLine("BackEnd > Data > Context > DataContext.cs");
      
      foreach (var s in DataContextInstructions)
      {
         xfile.WriteLine(s);
      }

      xfile.WriteLine("");
      xfile.WriteLine("To create a basic angular page follow the next steps");
      xfile.WriteLine("1 - Open cmd into Main folder, create a new angular component for Client using AngularCLI");
      xfile.WriteLine("   ng g m client --routing");
      xfile.WriteLine("   ng g c client");
      xfile.WriteLine("2 - into CMD change to component folder, for this example client and create the service component");
      xfile.WriteLine("   ng g service client");
      xfile.WriteLine("3 - into routing component on Route Key: { path: '', data : { title : \"Client\" }, component: ClientComponent },");
      xfile.WriteLine("4 - then create a route into app.routing { path: 'client',  loadChildren: () => import('./main/client/client.module').then(m => m.ClientModule) },");
      xfile.WriteLine("");

      xfile.Close();
      if (!string.IsNullOrEmpty(RootPath))
         Process.Start("explorer.exe", RootPath);

      MessageBox.Show("Arquivos gerados com sucesso! \r\nAbra o arquivo Instructions.txt para copiar o c�digo de Routing, Api Injection e DbSet");
      progressBar1.Visible = false;

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
      //generate instructions
      RoutingInstructions.Add($"{"{"} path: '{tb.TableName.ToLower()}', loadChildren: () => import('./main/{tb.TableName.ToLower()}/{tb.TableName.ToLower()}.module').then(m => m.{tb.TableName}Module){"},"}");

      string componentPath = SpaUI + "\\" + tb.TableName.ToLower();
      string registerPath = componentPath + "\\" + tb.TableName.ToLower() + "-register";
      string listPath = componentPath + "\\" + tb.TableName.ToLower() + "-list";
      Directory.CreateDirectory(registerPath);
      Directory.CreateDirectory(listPath);

      Util.WriteDocument(FrontEndTemplatePath + "service.ts.tpl", componentPath + "\\" + tb.TableName.ToLower() + ".service.ts", tb);
      Util.WriteDocument(FrontEndTemplatePath + "module.ts.tpl", componentPath + "\\" + tb.TableName.ToLower() + ".module.ts", tb);
      Util.WriteDocument(FrontEndTemplatePath + "component.ts.tpl", componentPath + "\\" + tb.TableName.ToLower() + ".component.ts", tb);
      Util.WriteDocument(FrontEndTemplatePath + "component.html.tpl", componentPath + "\\" + tb.TableName.ToLower() + ".component.html", tb);
      Util.WriteDocument(FrontEndTemplatePath + "component.css.tpl", componentPath + "\\" + tb.TableName.ToLower() + ".component.scss", tb);
      Util.WriteDocument(FrontEndTemplatePath + "routing.module.ts.tpl", componentPath + "\\" + tb.TableName.ToLower() + "-routing.module.ts", tb);

      Util.WriteDocument(FrontEndTemplatePath + "register.component.ts.tpl", registerPath + "\\" + tb.TableName.ToLower() + "-register.component.ts", tb);
      Util.WriteDocument(FrontEndTemplatePath + "register.component.html.tpl", registerPath + "\\" + tb.TableName.ToLower() + "-register.component.html", tb);
      Util.WriteDocument(FrontEndTemplatePath + "register.component.css.tpl", registerPath + "\\" + tb.TableName.ToLower() + "-register.component.scss", tb);

      Util.WriteDocument(FrontEndTemplatePath + "list.component.ts.tpl", listPath + "\\" + tb.TableName.ToLower() + "-list.component.ts", tb);
      Util.WriteDocument(FrontEndTemplatePath + "list.component.html.tpl", listPath + "\\" + tb.TableName.ToLower() + "-list.component.html", tb);
      Util.WriteDocument(FrontEndTemplatePath + "list.component.css.tpl", listPath + "\\" + tb.TableName.ToLower() + "-list.component.scss", tb);
   }

   private void GenerateController(TableToGenerate tb)
   {
      //generate instructions
      ApiInjectionInstructions.Add($"\t\t\tservices.AddScoped<{tb.TableName}Service>();");
      ApiInjectionInstructions.Add($"\t\t\tservices.AddScoped<{tb.TableName}Repository>();");

      Util.WriteDocument(BackEndTemplatePath + "Controller.tpl", Controllers + "\\" + tb.TableName + "Controller.cs", tb);
   }

   private void GenerateService(TableToGenerate tb)
   {
      Util.WriteDocument(BackEndTemplatePath + "Service.tpl", ServiceBusiness + "\\" + tb.TableName + "Service.cs", tb);
      Util.WriteDocument(BackEndTemplatePath + "Validation.tpl", ServiceValidation + "\\" + tb.TableName + "Validation.cs", tb);
   }

   private void GenerateDomain(TableToGenerate tb)
   {
      Util.WriteDocument(BackEndTemplatePath + "Entity.tpl", DomainEntities + "\\" + tb.TableName + ".cs", tb);
      Util.WriteDocument(BackEndTemplatePath + "Filter.tpl", DomainFilters + "\\" + tb.TableName + "Filter.cs", tb);
   }

   private void GenerateRepository(TableToGenerate tb)
   {
      //generate instructions
      DataContextInstructions.Add("public virtual DbSet<" + tb.TableName + "> " + tb.TableName + " { get; set; }");

      Util.WriteDocument(BackEndTemplatePath + "FilterBasicExtension.tpl", DataBasicExtension + "\\" + tb.TableName + "FilterBasicExtension.cs", tb);
      Util.WriteDocument(BackEndTemplatePath + "Configuration.tpl", DataConfiguration + "\\" + tb.TableName + "Configuration.cs", tb);
      Util.WriteDocument(BackEndTemplatePath + "Repository.tpl", DataRepository + "\\" + tb.TableName + "Repository.cs", tb);
   }

   private void ApagarFolder()
   {
      if (!string.IsNullOrEmpty(RootPath) && Directory.Exists(RootPath))
         Directory.Delete(RootPath, true);
   }

   private void GerarPastas()
   {
      try
      {
         if (string.IsNullOrEmpty(RootPath))
         {
            MessageBox.Show("Caminho de destino não definido!");
            return;
         }

         ApagarFolder();
         Directory.CreateDirectory(RootPath);
         if (rbGrupo.Checked)
         {
            SpaUI = RootPath + "\\SPAUI\\src\\app\\main";
            Controllers = RootPath + "\\Controllers";
            Service = RootPath + "\\Service";
            ServiceBusiness = RootPath + "\\Service\\Business";
            ServiceValidation = RootPath + "\\Service\\Validation";
            Domain = RootPath + "\\Domain";
            DomainEntities = RootPath + "\\Domain\\Entities";
            DomainFilters = RootPath + "\\Domain\\Filters";
            Data = RootPath + "\\Data";
            DataBasicExtension = RootPath + "\\Data\\BasicExtension";
            DataConfiguration = RootPath + "\\Data\\Configuration";
            DataRepository = RootPath + "\\Data\\Repository";
            Directory.CreateDirectory(SpaUI);
            Directory.CreateDirectory(Controllers);
            Directory.CreateDirectory(Service);
            Directory.CreateDirectory(ServiceBusiness);
            Directory.CreateDirectory(ServiceValidation);
            Directory.CreateDirectory(Domain);
            Directory.CreateDirectory(DomainEntities);
            Directory.CreateDirectory(DomainFilters);
            Directory.CreateDirectory(Data);
            Directory.CreateDirectory(DataBasicExtension);
            Directory.CreateDirectory(DataConfiguration);
            Directory.CreateDirectory(DataRepository);
         }
         else
         {
            foreach (var i in TableList)
            {
               Directory.CreateDirectory(RootPath + $"\\{i.TableName}");
            }
         }
      }
      catch (Exception ex)
      {
         MessageBox.Show("N�o foi poss�vel gerar os arquivos: " + ex.Message);
      }
   }

   private void GenerateSPAUI(TableToGenerate tb, string path)
   {

   }

   private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
   {

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
               // Suporte para SQL Server e MySQL
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
