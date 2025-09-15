using Microsoft.EntityFrameworkCore;
using VehicleCatalog.Domain.Entities;
using VehicleCatalog.Domain.Interfaces;
using VehicleCatalog.Infrastructure.Data;

namespace VehicleCatalog.Infrastructure.Repositories;

public class VehicleRepository(ApplicationDbContext context) : IVehicleRepository
{
    public async Task<Vehicle?> GetByIdAsync(Guid id)
    {
        return await context.Vehicles.FindAsync(id);
    }

    public async Task<Vehicle?> GetByPaymentCodeAsync(Guid vehicleId)
    {
        return await context.Vehicles
            .FirstOrDefaultAsync(v => v.Id == vehicleId);
    }

    public async Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync()
    {
        return await context.Vehicles
            .Where(v => !v.IsSold)
            .ToListAsync();
    }

    public async Task<IEnumerable<Vehicle>> GetSoldVehiclesAsync()
    {
        return await context.Vehicles
            .Where(v => v.IsSold == true)
            .ToListAsync();
    }

    public async Task<Vehicle> AddAsync(Vehicle Vehicle)
    {
        await context.Vehicles.AddAsync(Vehicle);
        return Vehicle;
    }

    public async Task UpdateAsync(Vehicle Vehicle)
    {
        context.Vehicles.Update(Vehicle);
        await Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await context.Vehicles.AnyAsync(v => v.Id == id);
    }
    
    public async Task DeleteAsync(Vehicle vehicle)
    {
        context.Vehicles.Remove(vehicle);
        await Task.CompletedTask;
    }
    
    public async Task<IEnumerable<Vehicle>> GetAllAsync()
    {
        return await context.Vehicles.ToListAsync();
    }

    public async Task<IEnumerable<Vehicle>> SearchAsync(
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
        var query = context.Vehicles.AsQueryable();

        if (!string.IsNullOrWhiteSpace(brand))
            query = query.Where(v => v.Brand.ToLower().Contains(brand.ToLower()));

        if (!string.IsNullOrWhiteSpace(model))
            query = query.Where(v => v.Model.ToLower().Contains(model.ToLower()));

        if (!string.IsNullOrWhiteSpace(color))
            query = query.Where(v => v.Color.ToLower().Contains(color.ToLower()));

        if (minPrice.HasValue)
            query = query.Where(v => v.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(v => v.Price <= maxPrice.Value);

        if (year.HasValue)
            query = query.Where(v => v.Year == year.Value);
        else
        {
            if (minYear.HasValue)
                query = query.Where(v => v.Year >= minYear.Value);

            if (maxYear.HasValue)
                query = query.Where(v => v.Year <= maxYear.Value);
        }

        if (isAvailable.HasValue)
            query = query.Where(v => v.IsSold != isAvailable.Value);

        query = query.OrderBy(v => v.IsSold).ThenBy(v => v.Price);

        return await query.ToListAsync();
    }
}