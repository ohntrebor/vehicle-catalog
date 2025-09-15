using Microsoft.EntityFrameworkCore;
using VehicleCatalog.Application.Gateways;
using VehicleCatalog.Domain.Entities;
using VehicleCatalog.Domain.Interfaces;

namespace VehicleCatalog.Infrastructure.Gateways;

public class VehicleGateway(IUnitOfWork unitOfWork) : IVehicleGateway
{
    public async Task<Vehicle> SaveAsync(Vehicle vehicle)
    {
        await unitOfWork.Vehicles.AddAsync(vehicle);
        await unitOfWork.SaveChangesAsync();
        return vehicle;
    }

    public async Task<Vehicle> UpdateAsync(Vehicle vehicle)
    {
        await unitOfWork.Vehicles.UpdateAsync(vehicle);
        await unitOfWork.SaveChangesAsync();
        return vehicle;
    }

    public async Task<Vehicle?> FindByIdAsync(Guid id)
    {
        return await unitOfWork.Vehicles.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Vehicle>> FindAvailableVehiclesAsync()
    {
        var vehicles = await unitOfWork.Vehicles.GetAvailableVehiclesAsync();
        return vehicles.OrderBy(v => v.Price);
    }

    public async Task<IEnumerable<Vehicle>> FindSoldVehiclesAsync()
    {
        var vehicles = await unitOfWork.Vehicles.GetSoldVehiclesAsync();
        return vehicles.OrderBy(v => v.Price);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var vehicle = await unitOfWork.Vehicles.GetByIdAsync(id);
        if (vehicle == null) return false;

        // Regra de negócio: Não permite deletar veículo vendido
        if (vehicle.IsSold)
            throw new InvalidOperationException("Cannot delete a sold vehicle");

        await unitOfWork.Vehicles.DeleteAsync(vehicle);
        await unitOfWork.SaveChangesAsync();
        return true;
        
    }
    
    public async Task<IEnumerable<Vehicle>> SearchVehiclesAsync(
        string? brand = null,
        string? model = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        int? year = null,
        int? minYear = null,
        int? maxYear = null,
        string? color = null,
        bool? isAvailable = null)
    {
        // delegamos para o repository
        return await unitOfWork.Vehicles.SearchAsync(
            brand, model, minPrice, maxPrice, year, minYear, maxYear, color, isAvailable);
    }
}