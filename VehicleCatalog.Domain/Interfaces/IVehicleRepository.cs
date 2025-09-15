using VehicleCatalog.Domain.Entities;

namespace VehicleCatalog.Domain.Interfaces;

/// <summary>
/// Interface do repositório de veículos
/// </summary>
public interface IVehicleRepository
{
    Task<Vehicle?> GetByIdAsync(Guid id);
    Task<Vehicle?> GetByPaymentCodeAsync(Guid vehicleId);
    Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync();
    Task<IEnumerable<Vehicle>> GetSoldVehiclesAsync();
    Task<Vehicle> AddAsync(Vehicle vehicle);
    Task UpdateAsync(Vehicle vehicle);
    Task DeleteAsync(Vehicle vehicle);

    /// <summary>
    /// Busca todos os veículos (para aplicar filtros)
    /// </summary>
    Task<IEnumerable<Vehicle>> GetAllAsync();

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
    /// <param name="isAvailable">Filtrar por disponibilidade</param>
    /// <returns>Lista de veículos filtrados</returns>
    Task<IEnumerable<Vehicle>> SearchAsync(
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