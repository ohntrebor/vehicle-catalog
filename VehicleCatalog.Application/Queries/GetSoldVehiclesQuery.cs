using MediatR;
using VehicleCatalog.Application.DTOs;

namespace VehicleCatalog.Application.Queries
{
    public class GetSoldVehiclesQuery : IRequest<IEnumerable<VehicleSaleDto>>
    {
    }
}