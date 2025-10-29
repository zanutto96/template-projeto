using Microsoft.Extensions.Configuration;
using System.CommandLine;

namespace Gerador;

static class Program
{
    static async Task<int> Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var appSettings = configuration.Get<AppSettings>();
        if (appSettings == null)
        {
            Console.Error.WriteLine("Erro ao carregar configurações do appsettings.json");
            return 1;
        }

        var cliHandler = new CLIHandler(appSettings);

        var tableOption = new Option<string?>(
            name: "--table",
            description: "Nome da tabela específica para gerar código (opcional - gera todas se não especificado)");

        var rootCommand = new RootCommand("Gerador de código CRUD para projetos .NET e Angular")
        {
            tableOption
        };

        rootCommand.SetHandler(async (tableName) =>
        {
            try
            {
                await cliHandler.GenerateAsync(tableName);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a geração: {ex.Message}");
                Environment.ExitCode = 1;
            }
        }, tableOption);

        return await rootCommand.InvokeAsync(args);
    }
}