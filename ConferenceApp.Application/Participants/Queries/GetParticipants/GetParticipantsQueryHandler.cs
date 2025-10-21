using AutoMapper;
using ConferenceApp.Domain.Interfaces.Repositories;
using MediatR;

namespace ConferenceApp.Application.Participants.Queries.GetParticipants
{
    public class GetParticipantsQueryHandler : IRequestHandler<GetParticipantsQuery, ParticipantsVm>
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IMapper _mapper;

        public GetParticipantsQueryHandler(IParticipantRepository participantRepository, IMapper mapper)
        {
            _participantRepository = participantRepository;
            _mapper = mapper;
        }

        public async Task<ParticipantsVm> Handle(GetParticipantsQuery request, CancellationToken cancellationToken)
        {
            var participants = await _participantRepository.GetAllAsync();

            var participantDtos = _mapper.Map<List<ParticipantDto>>(participants);

            return new ParticipantsVm
            {
                Participants = participantDtos,
                TotalCount = participantDtos.Count
            };
        }
    }
}
