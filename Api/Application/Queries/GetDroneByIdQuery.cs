using API.Application.ApiModels;
using Domain.AggregatesModel.DroneAggregate;
using MediatR;

namespace API.Application.Queries
{
    public class GetDroneByIdQuery : IRequest<ApiDrone>
    {
        public GetDroneByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public class Handler : IRequestHandler<GetDroneByIdQuery, ApiDrone>
        {
            private readonly IDroneRepository _repo;

            public Handler(IDroneRepository repo)
            {
                _repo = repo;
            }

            public async Task<ApiDrone> Handle(GetDroneByIdQuery request, CancellationToken cancellationToken)
            {

                var drone = await _repo.GetById(request.Id);
                if(drone == null)
                    return null;

                return new ApiDrone(drone);

            }
        }
    }
}
