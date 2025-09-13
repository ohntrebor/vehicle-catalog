using VehicleCatalog.Application.Gateways;
using VehicleCatalog.Domain.Entities;

namespace VehicleCatalog.Application.UseCases;

public class GetAvailableVehiclesUseCase(IVehicleGateway gateway)
{
    public async Task<IEnumerable<Vehicle>> ExecuteAsync()
    {
        return await gateway.FindAvailableVehiclesAsync();
    }
}