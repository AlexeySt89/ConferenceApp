using ConferenceApp.Application.DTOs;
using ConferenceApp.Application.Interfaces;
using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces;

namespace ConferenceApp.Application.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepository _repository;

        public ParticipantService(IParticipantRepository repository)
        {
            _repository = repository;
        }

        public async Task<ParticipantDto?> AuthenticateAsync(string email, string password)
        {
            var participant = await _repository.GetByCredentialsAsync(email);
            if (participant == null)
                return null;

            if(BCrypt.Net.BCrypt.Verify(password, participant.Password))
            {
                return new ParticipantDto
                {
                    FullName = participant.FullName,
                    Email = participant.Email,
                    Organization = participant.Organization,
                    TitleLecture = participant.TitleLecture,
                    FilePath = participant.FilePath
                };
            }
            return null;
        }

        public List<Participant> GetParticipants()
        {
            return _repository.GetAll();
        }

        public async Task<bool> SubmitAsync(ParticipantDto dto)
        {
            if (await _repository.GetByCredentialsAsync(dto.Email) is not null)
                return false;

            Participant newPart = new Participant()
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Email = dto.Email,
                Organization = dto.Organization,
                TitleLecture = dto.TitleLecture,
                FilePath = dto.FilePath,
                Password = new Random().Next().ToString() //Сделать норм пароли и реализацию хэша
            };

            await _repository.SaveAsync(newPart);
            return true;
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
                FilePath = updateDto.FilePath,
                Password = new Random().Next().ToString() //Сделать норм пароли и реализацию хэша
            };

            await _repository.UpdateAsync(email, newPart);
        }
    }
}