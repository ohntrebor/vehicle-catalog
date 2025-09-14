using VehicleCatalog.Application.Gateways;
using VehicleCatalog.Domain.Entities;

namespace VehicleCatalog.Application.UseCases;

public class GetVehicleByIdUseCase(IVehicleGateway gateway)
{
    public async Task<Vehicle?> ExecuteAsync(Guid vehicleId)
    {
        if (vehicleId == Guid.Empty)
            return null;

        return await gateway.FindByIdAsync(vehicleId);
    }
}