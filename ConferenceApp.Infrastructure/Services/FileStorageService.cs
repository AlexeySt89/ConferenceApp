using ConferenceApp.Domain.Interfaces.Services;

namespace ConferenceApp.Infrastructure.Services
{
    public class FileStorageService : IFileStorageService
    {
        public Task<bool> DeleteFileAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> DownloadFileAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
        {
            throw new NotImplementedException();
        }
    }
}