using AutoMapper;
using MediatR;
using VehicleCatalog.Application.DTOs;
using VehicleCatalog.Application.Queries;
using VehicleCatalog.Domain.Interfaces;

namespace VehicleCatalog.Application.Handlers;

public class GetAvailableVehiclesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAvailableVehiclesQuery, IEnumerable<VehicleDto>>
{
    public async Task<IEnumerable<VehicleDto>> Handle(GetAvailableVehiclesQuery request, CancellationToken cancellationToken)
    {
        var vehicles = await unitOfWork.Vehicles.GetAvailableVehiclesAsync();
        var orderedVehicles = vehicles.OrderBy(v => v.Price);
        return mapper.Map<IEnumerable<VehicleDto>>(orderedVehicles);
    }
}
