using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Services;
using Google.Apis.Storage.v1;
using Google.Apis.Upload;
using Google.Cloud.Storage.V1;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common.Storage
{
   public class FirebaseStorage
   {
      private readonly string BUCKET_NAME = "hoffman-novoch.appspot.com";
      public FirebaseStorage()
      {
      }

      public async Task Upload(byte[] imageBytes, string containerName, string fileName)
      {
         var client = StorageClient.Create(GoogleCredential.FromFile("hoffman-novoch.json")
                        .CreateScoped(StorageService.ScopeConstants.CloudPlatform));
         var filePath = "";

         if (containerName is not null && fileName is not null)
         {
            filePath = string.Format("{0}/{1}", containerName, fileName);
         }
         else filePath = fileName;

         var obj = client.CreateObjectUploader(
            BUCKET_NAME,
            filePath,
            retornaMimeType(
               Path.GetExtension(filePath)),
            new MemoryStream(imageBytes));

          await obj.UploadAsync();

      }


      public async Task<FileResult> Download(string containerName, string fileName)
      {
         var client = StorageClient.Create(GoogleCredential.FromFile("hoffman-novoch.json")
                        .CreateScoped(StorageService.ScopeConstants.CloudPlatform));
         var stream = new MemoryStream();
         var filePath = "";

         if (containerName is not null && fileName is not null)
         {
            filePath = string.Format("{0}/{1}", containerName, fileName);
         }
         else filePath = fileName;

         var obj = await client.DownloadObjectAsync("hoffman-novoch.appspot.com", filePath, stream);

         stream.Position = 0;
         return new FileResult
         {
            FileStream = stream,
            FileName = fileName,
            MimeType = obj.ContentType
         };

      }


      public string retornaMimeType(string sExtensao)
      {
         sExtensao = sExtensao.ToLower().Replace(".", "");

         switch (sExtensao)
         {
            case "png":
               return "image/png";
            case "jpg":
               return "image/jpeg";
            case "jpeg":
               return "image/jpeg";
            case "gif":
               return "image/gif";
            case "svg":
               return "image/svg+xml";
            case "webp":
               return "image/webp";
            case "webm":
               return "audio/webm";
            case "pdf":
               return "application/pdf";
            case "doc":
               return "application/msword";
            case "docx":
               return "application/msword";
            case "xls":
               return "application/msexcel";
            case "xlsx":
               return "application/msexcel";
            case "ppt":
               return "application/vnd.ms-powerpoint";
            case "pptx":
               return "application/vnd.ms-powerpoint";
            case "pps":
               return "application/vnd.ms-powerpoint";
            case "ppsx":
               return "application/vnd.ms-powerpoint";
            case "zip":
               return "application/zip";
            default:
               return "text/plain";
         }
      }
   }

   public class FileResult
   {
      public string? FileName;
      public MemoryStream FileStream;
      public string MimeType;
   }
}