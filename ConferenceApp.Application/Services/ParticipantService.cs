using ConferenceApp.Application.DTOs;
using ConferenceApp.Application.Interfaces;
using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces;
using System.Security.Claims;
using System.Text;

namespace ConferenceApp.Application.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepository _repository;

        public ParticipantService(IParticipantRepository repository)
        {
            _repository = repository;
        }

        public async Task<(string, string)> AuthenticateAsync(string email, string password)
        {
            var participant = await _repository.GetByCredentialsAsync(email);
            if (participant == null)
                return (string.Empty, string.Empty);

            if(BCrypt.Net.BCrypt.Verify(password, participant.Password))
            {
                return (participant.Email, participant.Id.ToString());
            }
            return (string.Empty, string.Empty);
        }

        public async Task<List<Participant>> GetParticipants()
        {
            return await _repository.GetAll();
        }

        public async Task<Participant?> GetParticipant(Guid id)
        {
            return await _repository.GetByCredentialsAsync(id);
        }
        public async Task<Participant?> GetParticipant(string email)
        {
            return await _repository.GetByCredentialsAsync(email);
        }
        public async Task<List<Participant?>> GetApprovedPar()
        {
            return await _repository.GetApprovedPar();
        }
        public async Task<Participant?> EditAsync(string email, ParticipantDto participantDto)
        {
            var part = await _repository.GetByCredentialsAsync(email);
            if(part == null)
                return null;

            if(!string.IsNullOrEmpty(participantDto.FullName))
                part.FullName = participantDto.FullName;
            if (!string.IsNullOrEmpty(participantDto.Email))
                part.Email = participantDto.Email;
            if (!string.IsNullOrEmpty(participantDto.Organization))
                part.Organization = participantDto.Organization;
            if (!string.IsNullOrEmpty(participantDto.TitleLecture))
                part.TitleLecture = participantDto.TitleLecture;
            if (participantDto.ApplicationContent != null)
                part.ArticleFile = participantDto.ApplicationContent;
            if (!string.IsNullOrEmpty(participantDto.ApplicationName))
                part.ArticleFileName = participantDto.ApplicationName;

            await _repository.UpdateAsync(part);
            return part;
        }

        public async Task<(bool, string)> SubmitAsync(ParticipantDto dto)
        {
            if (await _repository.GetByCredentialsAsync(dto.Email) is not null)
                return (false, string.Empty);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            Participant newPart = new Participant()
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Email = dto.Email,
                Organization = dto.Organization,
                TitleLecture = dto.TitleLecture,
                Password = hashedPassword,
                ArticleFile = dto.ApplicationContent,
                ArticleFileName = dto.ApplicationName,
                IsApproved = dto.IsApproved,
                Role = dto.Role,
                Section = dto.Section
            };

            await _repository.SaveAsync(newPart);
            return (true, newPart.Id.ToString());
        }

        public async Task UpdateAsync(string email, ParticipantDto updateDto)
        {
            Participant newPart = new Participant()
            {
                Id = Guid.NewGuid(),
                FullName = updateDto.FullName,
                Email = updateDto.Email,
                Organization = updateDto.Organization,
                TitleLecture = updateDto.TitleLecture,
                Password = new Random().Next().ToString(),
                ArticleFile = updateDto.ApplicationContent,
                ArticleFileName = updateDto.ApplicationName
            };

            await _repository.UpdateAsync(email, newPart);
        }

    }
}