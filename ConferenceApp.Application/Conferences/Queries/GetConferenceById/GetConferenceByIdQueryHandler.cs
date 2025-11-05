using AutoMapper;
using ConferenceApp.Application.Common.Exceptions;
using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces.Repositories;
using MediatR;

namespace ConferenceApp.Application.Conferences.Queries.GetConferenceById
{
    public class GetConferenceByIdQueryHandler : IRequestHandler<GetConferenceByIdQuery, ConferenceDetailVm>
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly IMapper _mapper;

        public GetConferenceByIdQueryHandler(IConferenceRepository conferenceRepository, IMapper mapper)
        {
            _conferenceRepository = conferenceRepository;
            _mapper = mapper;
        }

        public async Task<ConferenceDetailVm> Handle(GetConferenceByIdQuery request, CancellationToken cancellationToken)
        {
            var conference = await _conferenceRepository.GetByIdAsync(request.Id);

            if (conference == null)
                throw new NotFoundException(nameof(Conference), request.Id);

            var conferenceDetail = _mapper.Map<ConferenceDetailDto>(conference);

            return new ConferenceDetailVm { Conference = conferenceDetail };
        }
    }
}
