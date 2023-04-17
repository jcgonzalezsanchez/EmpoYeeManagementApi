using EmployeeManagement.Contracts.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace EmployeeManagement.Repository.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly IConfiguration _configuration;

        public StorageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string?> UploadFileAsync(byte[] file, string fileName)
        {
            CloudBlockBlob blockBlob = await StorageConnection(fileName);

            await blockBlob.UploadFromByteArrayAsync(file, 0, file.Length);
            return blockBlob?.Uri.ToString();
        }

        public async Task<string?> UpdateFileAsync(byte[] file, string fileNameToDelete, string fileNameToUpload)
        {
            await DeleteFileAsync(fileNameToDelete);

            CloudBlockBlob blockBlob = await StorageConnection(fileNameToUpload);

            await blockBlob.UploadFromByteArrayAsync(file, 0, file.Length);
            return blockBlob?.Uri.ToString();
        }

        public async Task DeleteFileAsync(string fileName)
        {
            CloudBlockBlob blockBlob = await StorageConnection(fileName);
            await blockBlob.DeleteIfExistsAsync();
        }

        private async Task<CloudBlockBlob> StorageConnection(string filename)
        {
            var storageAccount = CloudStorageAccount.Parse(_configuration["StorageAccount:ConnectionString"]);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(_configuration["StorageAccount:ContainerName"]);
            await container.CreateIfNotExistsAsync();
            var blockBlob = container.GetBlockBlobReference(filename);
            return blockBlob;
        }
    }
}
