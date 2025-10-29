using Common.Security.Identity;
using Data.BasicExtensions;
using Data.Repository;
using Service;
using Service.Business;
using Microsoft.EntityFrameworkCore;
using Data.Context;

namespace Api
{
   public static class ConfigContainer
   {
      public static void Config(IServiceCollection services, IConfiguration configuration)
      {
         // Registrar o DataContext (ESSENCIAL para os repositórios funcionarem)
         services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(configuration["ConfigConnectionString:Default"]));


         //Gerador Adicionar aqui
services.AddScoped<AlunoStatusService>();
services.AddScoped<AlunoStatusRepository>();
services.AddScoped<AlunoStatusService>();
services.AddScoped<AlunoStatusRepository>();
         
         services.AddScoped<AlunoStatusService>();
         services.AddScoped<AlunoStatusRepository>();

         RegisterOtherComponents(services);
      }

      public static void RegisterOtherComponents(IServiceCollection services)
      {
         // services.AddScoped<AuthService>();
         // services.AddScoped<EmailSMTP>();
         services.AddCurrentUserConfiguration();
         //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
         //services.AddScoped<ICache, RedisComponent>();
         //services.AddScoped<CurrentUser>();
         //services.AddScoped<HelperNotification>();
         //services.AddScoped<IEmail, Email>();
         //services.AddScoped<IStorage, Storage>();
         //services.AddScoped<ICripto, Cripto>();
      }
   }
}
