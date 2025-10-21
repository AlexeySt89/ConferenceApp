using AutoMapper;
using ConferenceApp.Domain.Interfaces.Repositories;
using MediatR;

namespace ConferenceApp.Application.Committee.Queries.GetParOrgCommMembers
{
    public class GetParOrgCommMembersQueryHandler : IRequestHandler<GetParOrgCommMembersQuery, ParOrgCommMembersVm>
    {
        private readonly IParOrgCommRepository _parOrgCommRepository;
        private readonly IMapper _mapper;

        public GetParOrgCommMembersQueryHandler(IParOrgCommRepository parOrgCommRepository, IMapper mapper)
        {
            _parOrgCommRepository = parOrgCommRepository;
            _mapper = mapper;
        }

        public async Task<ParOrgCommMembersVm> Handle(GetParOrgCommMembersQuery request, CancellationToken cancellationToken)
        {
            var members = await _parOrgCommRepository.GetAllAsync();

            var memberDtos = _mapper.Map<List<ParOrgCommMemberDto>>(members);

            return new ParOrgCommMembersVm
            {
                Members = memberDtos,
                TotalCount = memberDtos.Count
            };
        }
    }
}
