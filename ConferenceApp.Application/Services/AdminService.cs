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

        public async Task<bool> Remove(string email)
        {
            return await _adminRepository.RemovePar(email);
        }
    }
}
