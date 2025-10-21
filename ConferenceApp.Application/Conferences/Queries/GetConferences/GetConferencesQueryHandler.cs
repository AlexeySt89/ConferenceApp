using AutoMapper;
using ConferenceApp.Domain.Interfaces.Repositories;
using MediatR;

namespace ConferenceApp.Application.Conferences.Queries.GetConferences
{
    public class GetConferencesQueryHandler : IRequestHandler<GetConferencesQuery, ConferencesVm>
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly IMapper _mapper;

        public GetConferencesQueryHandler(IConferenceRepository conferenceRepository, IMapper mapper)
        {
            _conferenceRepository = conferenceRepository;
            _mapper = mapper;
        }

        public async Task<ConferencesVm> Handle(GetConferencesQuery request, CancellationToken cancellationToken)
        {
            var conferences = await _conferenceRepository.GetAllAsync();

            var conferenceDtos = _mapper.Map<List<ConferenceDto>>(conferences);

            return new ConferencesVm
            {
                Conferences = conferenceDtos,
                TotalCount = conferenceDtos.Count
            };
        }
    }
}
