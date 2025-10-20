using AutoMapper;
using ConferenceApp.Application.Common.Exceptions;
using ConferenceApp.Application.Participants.Queries.Shared;
using ConferenceApp.Domain.Common.ValueObjects;
using ConferenceApp.Domain.Interfaces.Repositories;
using MediatR;

namespace ConferenceApp.Application.Participants.Queries.GetParticipantByEmail
{
    public class GetParticipantByEmailQueryHandler : IRequestHandler<GetParticipantByEmailQuery, ParticipantDetailDto>
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IMapper _mapper;

        public GetParticipantByEmailQueryHandler(IParticipantRepository participantRepository, IMapper mapper)
        {
            _participantRepository = participantRepository;
            _mapper = mapper;
        }

        public async Task<ParticipantDetailDto> Handle(GetParticipantByEmailQuery request, CancellationToken cancellationToken)
        {
            var email = new Email(request.Email);
            var participant = await _participantRepository.GetByEmailAsync(email);

            if (participant == null)
                throw new NotFoundException($"Participant with email {request.Email} not found");

            return _mapper.Map<ParticipantDetailDto>(participant);
        }
    }
}
