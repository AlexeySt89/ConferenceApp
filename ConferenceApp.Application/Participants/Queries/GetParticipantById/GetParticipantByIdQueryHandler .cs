using AutoMapper;
using ConferenceApp.Application.Common.Exceptions;
using ConferenceApp.Application.Participants.Queries.Shared;
using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces.Repositories;
using MediatR;

namespace ConferenceApp.Application.Participants.Queries.GetParticipantById
{
    public class GetParticipantByIdQueryHandler : IRequestHandler<GetParticipantByIdQuery, ParticipantDetailDto>
    {
        public readonly IParticipantRepository _participantRepository;
        public readonly IMapper _mapper;

        public GetParticipantByIdQueryHandler(IParticipantRepository participantRepository, IMapper mapper)
        {
            _participantRepository = participantRepository;
            _mapper = mapper;
        }

        public async Task<ParticipantDetailDto> Handle(GetParticipantByIdQuery request, CancellationToken cancellationToken)
        {
            var participant = await _participantRepository.GetByIdAsync(request.Id);

            if (participant == null)
                throw new NotFoundException(nameof(Participant), request.Id);

            return _mapper.Map<ParticipantDetailDto>(participant);
        }
    }
}
