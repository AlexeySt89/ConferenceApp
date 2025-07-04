﻿using ConferenceApp.Domain.Entities;

namespace ConferenceApp.Domain.Interfaces
{
    public interface IAdminRepository
    {
        Task<Participant> GetPar(string email);
        Task<List<Participant>> GetAllPar();
        Task<List<Participant>> GetApprovePar();
        Task<bool> UpdateParStatusAsync(string email, bool status);
        Task<bool> RemovePar(string email);
    }
}
