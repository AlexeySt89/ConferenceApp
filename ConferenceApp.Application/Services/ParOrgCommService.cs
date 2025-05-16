using ConferenceApp.Application.Interfaces;
using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces;

namespace ConferenceApp.Application.Services
{
    public class ParOrgCommService : IParOrgCommService
    {
        private readonly IParOrgCommRepository _parOrgCommRepository;
        public ParOrgCommService(IParOrgCommRepository parOrgCommRepository)
        {
            _parOrgCommRepository = parOrgCommRepository;
        }
        public List<ParOrgComm> GetAllParOrgComm()
        {
            return _parOrgCommRepository.GetAll();
        }
    }
}
