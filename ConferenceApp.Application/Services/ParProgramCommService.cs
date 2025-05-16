using ConferenceApp.Application.Interfaces;
using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces;

namespace ConferenceApp.Application.Services
{
    public class ParProgramCommService : IParProgramCommService
    {
        private readonly IParProgramCommRepository _parProgramCommRepository;
        public ParProgramCommService(IParProgramCommRepository parProgramCommRepository)
        {
            _parProgramCommRepository = parProgramCommRepository;
        }
        public List<ParProgramComm> GetAllParProgramComm()
        {
            return _parProgramCommRepository.GetAll();
        }
    }
}
