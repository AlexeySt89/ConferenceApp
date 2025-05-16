using ConferenceApp.Application.DTOs;
using ConferenceApp.Application.Interfaces;
using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces;
using AutoMapper;

namespace ConferenceApp.Application.Services
{
    public class ConferenceService : IConferenceService
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly IMapper _mapper;

        public ConferenceService(IConferenceRepository conferenceRepository, IMapper mapper)
        {
            _conferenceRepository = conferenceRepository;
            _mapper = mapper;
        }

        public async Task<List<ConferenceDto>> GetConferencesAsync()
        {
            var conferences = await _conferenceRepository.GetAllAsync();
            return _mapper.Map<List<ConferenceDto>>(conferences);
        }

        public async Task<ConferenceDto> GetConferenceByIdAsync(Guid id)
        {
            var conference = await _conferenceRepository.GetByIdAsync(id);
            return _mapper.Map<ConferenceDto>(conference);
        }
    }
}
