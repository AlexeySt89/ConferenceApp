using AutoMapper;
using ConferenceApp.Domain.Interfaces.Repositories;
using MediatR;

namespace ConferenceApp.Application.Participants.Queries.GetApprovedParticipants
{
    public class GetApprovedParticipantsQueryHandler : IRequestHandler<GetApprovedParticipantsQuery, ApprovedParticipantsVm>
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IMapper _mapper;

        public GetApprovedParticipantsQueryHandler(IParticipantRepository participantRepository, IMapper mapper)
        {
            _participantRepository = participantRepository;
            _mapper = mapper;
        }

        public async Task<ApprovedParticipantsVm> Handle(GetApprovedParticipantsQuery request, CancellationToken cancellationToken)
        {
            var approvedParticipants = await _participantRepository.GetApprovedAsync();

            var participantDtos = _mapper.Map<List<ApprovedParticipantDto>>(approvedParticipants);

            return new ApprovedParticipantsVm
            {
                Participants = participantDtos,
                TotalCount = participantDtos.Count
            };
        }
    }
}
