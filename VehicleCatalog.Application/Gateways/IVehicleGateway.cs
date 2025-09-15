using VehicleCatalog.Domain.Entities;

namespace VehicleCatalog.Application.Gateways;

public interface IVehicleGateway
{
    Task<Vehicle> SaveAsync(Vehicle vehicle);
    Task<Vehicle> UpdateAsync(Vehicle vehicle);
    Task<Vehicle?> FindByIdAsync(Guid id);
    Task<IEnumerable<Vehicle>> FindAvailableVehiclesAsync();
    Task<IEnumerable<Vehicle>> FindSoldVehiclesAsync();
    Task<bool> DeleteAsync(Guid id);
    
    /// <summary>
    /// Busca veículos aplicando filtros específicos
    /// </summary>
    /// <param name="brand">Marca do veículo (busca parcial)</param>
    /// <param name="model">Modelo do veículo (busca parcial)</param>
    /// <param name="minPrice">Preço mínimo</param>
    /// <param name="maxPrice">Preço máximo</param>
    /// <param name="year">Ano específico</param>
    /// <param name="minYear">Ano mínimo</param>
    /// <param name="maxYear">Ano máximo</param>
    /// <param name="color">Cor do veículo (busca parcial)</param>
    /// <param name="isAvailable">Filtrar por disponibilidade (null = todos)</param>
    /// <returns>Lista de veículos filtrados</returns>
    Task<IEnumerable<Vehicle>> SearchVehiclesAsync(
        string? brand = null,
        string? model = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        int? year = null,
        int? minYear = null,
        int? maxYear = null,
        string? color = null,
        bool? isAvailable = null);

}