using Common.Email;
using Common.NotaFiscal;
using Common.Security.Identity;
using Data.BasicExtensions;
using Data.Repository;
using Service;
using Service.Business;

namespace Api
{
   public static class ConfigContainer
   {
      public static void Config(IServiceCollection services)
      {
         //Gerador Adicionar aqui

			services.AddScoped<AlunoService>();
			services.AddScoped<AlunoRepository>();
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
