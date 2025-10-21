using AutoMapper;
using ConferenceApp.Application.Committee.Queries.GetParOrgCommMembers;
using ConferenceApp.Application.Committee.Queries.GetParProgramCommMembers;
using ConferenceApp.Application.Conferences.Queries.GetConferenceById;
using ConferenceApp.Application.Conferences.Queries.GetConferences;
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

            CreateMap<Conference, ConferenceDto>()
           .ForMember(dest => dest.ParticipantsCount,
                      opt => opt.MapFrom(src => src.Participants.Count));

            CreateMap<Conference, ConferenceDetailDto>()
                .ForMember(dest => dest.Participants,
                           opt => opt.MapFrom(src => src.Participants));

            CreateMap<Participant, ConferenceParticipantDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value));

            CreateMap<ParOrgComm, ParOrgCommMemberDto>();
            CreateMap<ParProgramComm, ParProgramCommMemberDto>();
        }
    }
}
