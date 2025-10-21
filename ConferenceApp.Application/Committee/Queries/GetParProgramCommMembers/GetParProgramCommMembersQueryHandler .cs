using AutoMapper;
using ConferenceApp.Domain.Interfaces.Repositories;
using MediatR;

namespace ConferenceApp.Application.Committee.Queries.GetParProgramCommMembers
{
    public class GetParProgramCommMembersQueryHandler : IRequestHandler<GetParProgramCommMembersQuery, ParProgramCommMembersVm>
    {
        private readonly IParProgramCommRepository _parProgramCommRepository;
        private readonly IMapper _mapper;

        public GetParProgramCommMembersQueryHandler(IParProgramCommRepository parProgramCommRepository, IMapper mapper)
        {
            _parProgramCommRepository = parProgramCommRepository;
            _mapper = mapper;
        }

        public async Task<ParProgramCommMembersVm> Handle(GetParProgramCommMembersQuery request, CancellationToken cancellationToken)
        {
            var members = await _parProgramCommRepository.GetAllAsync();

            var memberDtos = _mapper.Map<List<ParProgramCommMemberDto>>(members);

            return new ParProgramCommMembersVm
            {
                Members = memberDtos,
                TotalCount = memberDtos.Count
            };
        }
    }
}
