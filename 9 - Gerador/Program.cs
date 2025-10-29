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

        var backendOnlyOption = new Option<bool>(
            name: "--backend-only",
            description: "Gera apenas o código do backend (.NET)");

        var frontendOnlyOption = new Option<bool>(
            name: "--frontend-only",
            description: "Gera apenas o código do frontend (Angular)");

        var rootCommand = new RootCommand("Gerador de código CRUD para projetos .NET e Angular")
        {
            tableOption,
            backendOnlyOption,
            frontendOnlyOption
        };

        rootCommand.SetHandler(async (tableName, backendOnly, frontendOnly) =>
        {
            try
            {
                // Validar opções mutuamente exclusivas
                if (backendOnly && frontendOnly)
                {
                    Console.Error.WriteLine("Erro: --backend-only e --frontend-only não podem ser usados juntos.");
                    Environment.ExitCode = 1;
                    return;
                }

                await cliHandler.GenerateAsync(tableName, backendOnly, frontendOnly);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a geração: {ex.Message}");
                Environment.ExitCode = 1;
            }
        }, tableOption, backendOnlyOption, frontendOnlyOption);

        return await rootCommand.InvokeAsync(args);
    }
}