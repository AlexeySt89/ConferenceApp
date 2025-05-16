using ConferenceApp.Domain.Entities;

namespace ConferenceApp.Application.Interfaces
{
    public interface IParOrgCommService
    {
        List<ParOrgComm> GetAllParOrgComm();
    }
}
