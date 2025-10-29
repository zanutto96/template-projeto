using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Common.Storage
{
    public class AzureStorage
    {
        private CloudBlobContainer _cloudBlobContainer;
        private readonly string _storageAzureConnectionStringBase;

        public AzureStorage(string storageAzureConnectionStringBase)
        {
            this._storageAzureConnectionStringBase = storageAzureConnectionStringBase;
        }

        public async Task Upload(byte[] imageBytes, string containerName, string filename)
        {
            if (CloudStorageAccount.TryParse(this._storageAzureConnectionStringBase, out CloudStorageAccount storageAccount))
            {
                await this.ConfigCloudBlobContainer(containerName, storageAccount);
                var blob = _cloudBlobContainer.GetBlockBlobReference(filename);
                await blob.UploadFromByteArrayAsync(imageBytes, 0, imageBytes.Length);
            }

        }

        public async Task<MemoryStream> Download(string containerName, string fileName)
        {
            if (CloudStorageAccount.TryParse(this._storageAzureConnectionStringBase, out CloudStorageAccount storageAccount))
            {
                await this.ConfigCloudBlobContainer(containerName, storageAccount);
                var cloudBlockBlob = _cloudBlobContainer.GetBlockBlobReference(fileName);
                using (MemoryStream ms = new MemoryStream())
                {
                    await cloudBlockBlob.DownloadToStreamAsync(ms);
                    return ms;
                }
            }

            throw new InvalidOperationException("Storage error connect");
        }

        public async Task Delete(string containerName, string fileName)
        {
            if (CloudStorageAccount.TryParse(this._storageAzureConnectionStringBase, out CloudStorageAccount storageAccount))
            {
                await this.ConfigCloudBlobContainer(containerName, storageAccount);
                var cloudBlockBlob = _cloudBlobContainer.GetBlockBlobReference(fileName);
                await cloudBlockBlob.DeleteAsync();
            }
        }

        private async Task ConfigCloudBlobContainer(string containerName, CloudStorageAccount storageAccount)
        {
            var cloudBlobClient = storageAccount.CreateCloudBlobClient();
            _cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName.ToLower());
            await _cloudBlobContainer.CreateIfNotExistsAsync();
            var permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            await _cloudBlobContainer.SetPermissionsAsync(permissions);
        }
        public async Task<MemoryStream> GetStream(string containerName, string fileName)
        {
            if (CloudStorageAccount.TryParse(this._storageAzureConnectionStringBase, out CloudStorageAccount storageAccount))
            {
                await this.ConfigCloudBlobContainer(containerName, storageAccount);
                var cloudBlockBlob = _cloudBlobContainer.GetBlockBlobReference(fileName);
                var ms = new MemoryStream();
                await cloudBlockBlob.DownloadToStreamAsync(ms);
                return ms;
            }

            throw new InvalidOperationException("Storage error connect");
        }
    }
}