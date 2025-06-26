using ConferenceApp.Application.DTOs;
using ConferenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceApp.Application.Interfaces
{
    public interface IParticipantService
    {
        Task<List<Participant>> GetParticipants();
        Task<List<Participant>> GetApprovedPar();
        Task<(bool, string)> SubmitAsync(ParticipantDto dto);
        Task<(string, string)> AuthenticateAsync(string email, string password);
        Task UpdateAsync(string email, ParticipantDto updateDto);
        Task<bool> SubmitApplicationAsync(string email, ParticipantDto participantDto);
        Task<bool> SubmitArticleAsync(string email, ParticipantDto participantDto);
        Task<bool> SubmitMaterialsAsync(string email, ParticipantDto participantDto);
    }
}
