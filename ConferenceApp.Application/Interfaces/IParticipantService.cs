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
        List<Participant> GetParticipants();
        Task SubmitAsync(ParticipantDto dto);
        Task<ParticipantDto?> AuthenticateAsync(string email, string password);
        Task UpdateAsync(string email, ParticipantDto updateDto);
    }
}
