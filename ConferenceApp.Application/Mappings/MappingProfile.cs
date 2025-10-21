using AutoMapper;
using ConferenceApp.Application.Conferences.DTOs;
using ConferenceApp.Application.Participants.Queries.GetApprovedParticipants;
using ConferenceApp.Application.Participants.Queries.GetParticipants;
using ConferenceApp.Application.Participants.Queries.Shared;
using ConferenceApp.Domain.Entities;

namespace ConferenceApp.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Participant, ParticipantDetailDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
                .ForMember(dest => dest.HasApplicationFile, opt => opt.MapFrom(src => src.GetApplicationFile() != null))
                .ForMember(dest => dest.HasArticleFile, opt => opt.MapFrom(src => src.GetArticleFile() != null));

            CreateMap<Participant, ParticipantDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value));

            CreateMap<Participant, ApprovedParticipantDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value));
        }
    }
}
