using VehicleCatalog.Domain.Entities;

namespace VehicleCatalog.Application.Gateways;

public interface IVehicleGateway
{
    Task<Vehicle> SaveAsync(Vehicle vehicle);
    Task<Vehicle> UpdateAsync(Vehicle vehicle);
    Task<Vehicle?> FindByIdAsync(Guid id);
    Task<Vehicle?> FindByPaymentCodeAsync(string paymentCode);
    Task<IEnumerable<Vehicle>> FindAvailableVehiclesAsync();
    Task<IEnumerable<Vehicle>> FindSoldVehiclesAsync();
    Task<bool> DeleteAsync(Guid id);

}