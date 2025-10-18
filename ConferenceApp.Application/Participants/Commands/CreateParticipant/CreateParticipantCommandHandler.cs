using ConferenceApp.Domain.Interfaces.Repositories;
using ConferenceApp.Domain.Interfaces.Services;
using MediatR;
using ConferenceApp.Domain.Common.ValueObjects;
using ConferenceApp.Domain.Entities;

namespace ConferenceApp.Application.Participants.Commands.CreateParticipant
{
    public class CreateParticipantCommandHandler : IRequestHandler<CreateParticipantCommand, Guid>
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IPasswordHasher _passwordHasher;

        public CreateParticipantCommandHandler(IParticipantRepository participantRepository, IPasswordHasher passwordHasher)
        {
            _participantRepository = participantRepository;
            _passwordHasher = passwordHasher;
        }


        public async Task<Guid> Handle(CreateParticipantCommand request, CancellationToken cancellationToken)
        {
            var existingParticipant = await _participantRepository.GetByEmailAsync(new Email(request.Email));

            if(existingParticipant != null)
            {
                throw new InvalidOperationException($"Participant with email {request.Email} already exists.");
            }

            var email = new Email(request.Email);
            var password = Password.CreateFromPlainText(request.Password, _passwordHasher);

            var participant = new Participant(
                request.FullName,
                request.Organization,
                email,
                request.TitleLecture,
                password,
                request.Section);

            await _participantRepository.AddAsync(participant);

            //Domain Events

            return participant.Id;
        }
    }
}
