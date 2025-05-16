using AutoMapper;
using ConferenceApp.Application.DTOs;
using ConferenceApp.Domain.Entities;

namespace ConferenceApp.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Conference, ConferenceDto>();
            CreateMap<Participant, ParticipantDto>();
        }
    }
}
