using ConferenceApp.Domain.Entities;

namespace ConferenceApp.Domain.Interfaces
{
    public interface IParProgramCommRepository
    {
        List<ParProgramComm> GetAll();
    }
}
