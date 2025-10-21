using ConferenceApp.Domain.Common.ValueObjects;
using ConferenceApp.Domain.Interfaces.Repositories;
using ConferenceApp.Domain.Interfaces.Services;
using MediatR;

namespace ConferenceApp.Application.Participants.Queries.AuthenticateParticipant
{
    public class AuthenticateParticipantQueryHandler : IRequestHandler<AuthenticateParticipantQuery, AuthenticationResult>
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticateParticipantQueryHandler(IParticipantRepository participantRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
        {
            _participantRepository = participantRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthenticationResult> Handle(AuthenticateParticipantQuery request, CancellationToken cancellationToken)
        {
            var email = new Email(request.Email);
            var participant = await _participantRepository.GetByEmailAsync(email);

            if (participant == null)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    ErrorMessage = "Invalid email or password"
                };
            }

            var isPasswordValid = participant.Password.Verify(request.Password, _passwordHasher);

            if (!isPasswordValid)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    ErrorMessage = "Invalid email or password"
                };
            }

            var token = _jwtTokenGenerator.GenerateToken(
                participant.Email.Value,
                participant.Id.ToString(),
                participant.Role.ToString());

            return new AuthenticationResult
            {
                Success = true,
                Token = token,
                UserId = participant.Id.ToString(),
                Email = participant.Email.Value,
                Role = participant.Role.ToString()
            };
        }
    }
}
