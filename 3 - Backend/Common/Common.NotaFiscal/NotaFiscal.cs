using System.Net;
using Common.Domain;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Crmf;
using RestSharp;
using static IdentityModel.OidcConstants;

namespace Common.NotaFiscal
{
   public class NotaFiscal
   {
      private readonly IOptions<NotaFiscalConfig> _nfConfig;
      private readonly string BASE_URL = "https://api.bling.com.br";
      private readonly string NFE_ENDPOINT = "/Api/v3/nfe";
      private readonly string NFSE_ENDPOINT = "/Api/v3/nfse";

      public NotaFiscal(
         IOptions<NotaFiscalConfig> notaConfig
         )
      {
         _nfConfig = notaConfig;
      }

      public async Task<NotaFiscalResponse> EnviaNFSe(NotaFiscalServicoRequest notaFiscalServicoRequest, string access_token)
      {
         RestClient client;
         RestRequest request;
         ConfiguraRequest<NotaFiscalServicoRequest>(notaFiscalServicoRequest, access_token, NFSE_ENDPOINT, Method.Post, out client, out request);

         RestResponse response = await client.ExecuteAsync(request);
         if (response.IsSuccessStatusCode)
         {
            return JsonConvert.DeserializeObject<NotaFiscalResponse>(response.Content);
         }
         return JsonConvert.DeserializeObject<NotaFiscalResponse>(response.Content);
      }

      public async Task<NotaFiscalResponse> EnviaNFe(NotaFiscalProdutoRequest notaFiscalProdutoRequest, string access_token)
      {
         RestClient client;
         RestRequest request;
         ConfiguraRequest<NotaFiscalProdutoRequest>(notaFiscalProdutoRequest, access_token, NFE_ENDPOINT, Method.Post, out client, out request);

         RestResponse response = await client.ExecuteAsync(request);

         if (response.IsSuccessStatusCode)
         {
            return JsonConvert.DeserializeObject<NotaFiscalResponse>(response.Content);
         }
         return JsonConvert.DeserializeObject<NotaFiscalResponse>(response.Content);
      }

      public async Task<NotaFiscalResponse> EmiteNFe(string idExterno, string access_token)
      {
         RestClient client;
         RestRequest request;
         string url = $"{NFE_ENDPOINT}/{idExterno}/enviar";
         ConfiguraRequest<NotaFiscalProdutoRequest>(null, access_token, url, Method.Post, out client, out request);

         RestResponse response = await client.ExecuteAsync(request);

         if (response.IsSuccessStatusCode)
         {
            return JsonConvert.DeserializeObject<NotaFiscalResponse>(response.Content);
         }
         return JsonConvert.DeserializeObject<NotaFiscalResponse>(response.Content);
      }
      public async Task<NotaFiscalResponse> EmiteNFSe(string idExterno, string access_token)
      {
         RestClient client;
         RestRequest request;
         string url = $"{NFSE_ENDPOINT}/{idExterno}/enviar";
         ConfiguraRequest<NotaFiscalProdutoRequest>(null, access_token, url, Method.Post, out client, out request);

         RestResponse response = await client.ExecuteAsync(request);

         if (response.IsSuccessStatusCode)
         {
            return JsonConvert.DeserializeObject<NotaFiscalResponse>(response.Content);
         }
         return JsonConvert.DeserializeObject<NotaFiscalResponse>(response.Content);
      }
      public async Task<NotaFiscalResponse> StatusNFe(string idExterno, string access_token)
      {
         RestClient client;
         RestRequest request;
         string url = $"{NFE_ENDPOINT}/{idExterno}";
         ConfiguraRequest<NotaFiscalProdutoRequest>(null, access_token, url, Method.Get, out client, out request);

         RestResponse response = await client.ExecuteAsync(request);

         if (response.IsSuccessStatusCode)
         {
            return JsonConvert.DeserializeObject<NotaFiscalResponse>(response.Content);
         }
         return JsonConvert.DeserializeObject<NotaFiscalResponse>(response.Content);
      }
      public async Task<NotaFiscalResponse> StatusNFSe(string idExterno, string access_token)
      {
         RestClient client;
         RestRequest request;
         string url = $"{NFSE_ENDPOINT}/{idExterno}";
         ConfiguraRequest<NotaFiscalProdutoRequest>(null, access_token, url, Method.Get, out client, out request);

         RestResponse response = await client.ExecuteAsync(request);

         if (response.IsSuccessStatusCode)
         {
            return JsonConvert.DeserializeObject<NotaFiscalResponse>(response.Content);
         }
         return JsonConvert.DeserializeObject<NotaFiscalResponse>(response.Content);
      }


      private void ConfiguraRequest<T>(T notaFiscalRequest, string access_token, string endpoint, RestSharp.Method method, out RestClient client, out RestRequest request)
      {
         var options = new RestClientOptions(BASE_URL)
         {
            MaxTimeout = -1,
         };

         client = new RestClient(options);
         request = new RestRequest(endpoint, method);

         request.AddHeader("Content-Type", "application/json");
         request.AddHeader("Authorization", $"Bearer {access_token}");
         if (notaFiscalRequest is not null)
         {
            var body = JsonConvert.SerializeObject(notaFiscalRequest, Formatting.Indented);
            request.AddStringBody(body, DataFormat.Json);
         }
      }

      public string BuildAuthorizationUrl(string client_id)
      {
         string authorizationEndpoint = "https://bling.com.br/Api/v3/oauth/authorize";
         string state = Guid.NewGuid().ToString();
         string responseType = "code";

         return $"{authorizationEndpoint}?response_type={responseType}&client_id={client_id}&state={state}";
      }

      public async Task<AccessTokenResponse> ExchangeAuthorizationCodeForToken(string client_id, string client_secret, string code, bool isRefresh = false)
      {
         return await GetAccessToken(client_id, client_secret, code, false);
      }
      public async Task<AccessTokenResponse> RefreshToken(string client_id, string client_secret, string code, bool isRefresh = false)
      {
         return await GetAccessToken(client_id, client_secret, code, true);
      }

      private async Task<AccessTokenResponse> GetAccessToken(string client_id, string client_secret, string code, bool isRefresh = false)
      {
         string tokenEndpoint = "https://bling.com.br/Api/v3/oauth/token";

         var options = new RestClientOptions(BASE_URL)
         {
            MaxTimeout = -1,
         };
         var client = new RestClient(options);
         var request = new RestRequest(tokenEndpoint, Method.Post);

         byte[] clientSecrets = System.Text.Encoding.UTF8.GetBytes($"{client_id}:{client_secret}");

         request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
         request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(clientSecrets)}");
         request.AddParameter("grant_type", isRefresh ? "refresh_token" : "authorization_code");
         request.AddParameter(isRefresh ? "refresh_token" : "code", code);

         var response = await client.ExecuteAsync<AccessTokenResponse>(request);

         if (response.IsSuccessful && response.Data != null)
         {
            Console.WriteLine("Token recebido com sucesso!");
            return response.Data;
         }
         else
         {
            Console.WriteLine("Erro ao trocar código por token: " + response.ErrorMessage);
            return null;
         }
      }

      public class AccessTokenResponse
      {

         public string access_token { get; set; }
         public int expires_in { get; set; }
         public string token_type { get; set; }
         public string scope { get; set; }
         public string refresh_token { get; set; }

      }

   }
}