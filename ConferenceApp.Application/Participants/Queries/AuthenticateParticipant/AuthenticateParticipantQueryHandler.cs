using ConferenceApp.Domain.Common.ValueObjects;
using ConferenceApp.Domain.Interfaces.Repositories;
using ConferenceApp.Domain.Interfaces.Services;
using MediatR;

namespace ConferenceApp.Application.Participants.Queries.AuthenticateParticipant
{
    public class AuthenticateParticipantQueryHandler : IRequestHandler<AuthenticateParticipantQuery, AuthenticationResult>
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticateParticipantQueryHandler(IParticipantRepository participantRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator, IAdminRepository adminRepository)
        {
            _participantRepository = participantRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _adminRepository = adminRepository;
        }

        public async Task<AuthenticationResult> Handle(AuthenticateParticipantQuery request, CancellationToken cancellationToken)
        {
            var email = new Email(request.Email);

            var participant = await _participantRepository.GetByEmailAsync(email);
            if (participant != null)
            {
                var isPasswordValid = participant.Password.Verify(request.Password, _passwordHasher);
                if (isPasswordValid)
                {
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

            var admin = await _adminRepository.GetByEmailAsync(email);
            if (admin != null)
            {
                var isPasswordValid = admin.Password.Verify(request.Password, _passwordHasher);
                if (isPasswordValid)
                {
                    var token = _jwtTokenGenerator.GenerateToken(
                        admin.Email.Value,
                        admin.Id.ToString(),
                        "admin"); 

                    return new AuthenticationResult
                    {
                        Success = true,
                        Token = token,
                        UserId = admin.Id.ToString(),
                        Email = admin.Email.Value,
                        Role = "admin"
                    };
                }
            }

            return new AuthenticationResult
            {
                Success = false,
                ErrorMessage = "Invalid email or password"
            };
        }
    }
}
