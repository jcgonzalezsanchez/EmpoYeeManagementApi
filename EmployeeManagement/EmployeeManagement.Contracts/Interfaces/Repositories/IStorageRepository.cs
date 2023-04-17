namespace EmployeeManagement.Contracts.Interfaces.Repositories
{
    public interface IStorageRepository
    {
        Task<string?> UploadFileAsync(byte[] file, string fileName);

        Task<string?> UpdateFileAsync(byte[] file, string fileNameToDelete, string fileNameToUpload);

        Task DeleteFileAsync(string fileName);
    }
}
