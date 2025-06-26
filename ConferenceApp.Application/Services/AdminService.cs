using ConferenceApp.Application.Interfaces;
using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces;

namespace ConferenceApp.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository) => _adminRepository = adminRepository;

        public async Task<List<Participant>> GetAllPar()
        { 
            return await _adminRepository.GetAllPar();
        }
        public async Task<List<Participant>> GetApprovePar()
        {
            return await _adminRepository.GetApprovePar();
        }

        public async Task<bool> UpdateParStatusAsync(string email, bool status)
        {
            return await _adminRepository.UpdateParStatusAsync(email, status);
        }

        public async Task<bool> Remove(string email)
        {
            return await _adminRepository.RemovePar(email);
        }

        public async Task<(Stream FileStream, string FileName, string ContentType)> GetParticipantFileAsync(string email, string fileType)
        {
            var participant = await _adminRepository.GetPar(email);

            if (participant == null)
                return (null, null, null);

            byte[] fileData;
            string fileName;

            if (fileType == "article")
            {
                fileData = participant.ArticleFile;
                fileName = participant.ArticleFileName;
            }
            else if (fileType == "application")
            {
                fileData = participant.ApplicationFile;
                fileName = participant.ApplicationFileName;
            }
            else
            {
                throw new ArgumentException("Неизвестный тип файла");
            }

            if (fileData == null || fileData.Length == 0)
                return (null, null, null);

            // Определяем ContentType аналогично предыдущему примеру
            string contentType = GetContentType(fileName);

            return (new MemoryStream(fileData), fileName, contentType);
        }

        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();

            return extension switch
            {
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                _ => "application/octet-stream"
            };
        }
    }
}
