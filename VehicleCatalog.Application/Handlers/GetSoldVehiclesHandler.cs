using AutoMapper;
using MediatR;
using VehicleCatalog.Application.DTOs;
using VehicleCatalog.Application.Queries;
using VehicleCatalog.Domain.Interfaces;

namespace VehicleCatalog.Application.Handlers;

public class GetSoldVehiclesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetSoldVehiclesQuery, IEnumerable<VehicleSaleDto>>
{
    public async Task<IEnumerable<VehicleSaleDto>> Handle(GetSoldVehiclesQuery request, CancellationToken cancellationToken)
    {
        var vehicles = await unitOfWork.Vehicles.GetSoldVehiclesAsync();
        var orderedVehicles = vehicles.OrderBy(v => v.Price);
        return mapper.Map<IEnumerable<VehicleSaleDto>>(orderedVehicles);
    }
}