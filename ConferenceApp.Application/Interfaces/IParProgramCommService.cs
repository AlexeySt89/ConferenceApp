using ConferenceApp.Domain.Entities;

namespace ConferenceApp.Application.Interfaces
{
    public interface IParProgramCommService
    {
        List<ParProgramComm> GetAllParProgramComm();
    }
}
