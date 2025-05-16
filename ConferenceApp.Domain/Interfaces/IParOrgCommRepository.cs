using ConferenceApp.Domain.Entities;

namespace ConferenceApp.Domain.Interfaces
{
    public interface IParOrgCommRepository
    {
        List<ParOrgComm> GetAll();
    }
}
