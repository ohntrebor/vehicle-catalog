using MediatR;
using VehicleCatalog.Application.DTOs;

namespace VehicleCatalog.Application.Queries
{
    public class GetAvailableVehiclesQuery : IRequest<IEnumerable<VehicleDto>>
    {
    }
}