using VehicleCatalog.Application.Gateways;
using VehicleCatalog.Domain.Entities;

namespace VehicleCatalog.Application.UseCases;

/// <summary>
/// Use case para buscar veículos por critérios específicos
/// </summary>
public class SearchVehiclesUseCase(IVehicleGateway gateway)
{
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
    /// <param name="isAvailable">Filtrar apenas disponíveis (true) ou vendidos (false). Se null, busca todos</param>
    /// <returns>Lista de veículos que atendem aos critérios</returns>
    public async Task<IEnumerable<Vehicle>> ExecuteAsync(
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
        // Validações básicas
        if (minPrice.HasValue && minPrice < 0)
            throw new ArgumentException("Preço mínimo não pode ser negativo");
        
        if (maxPrice.HasValue && maxPrice < 0)
            throw new ArgumentException("Preço máximo não pode ser negativo");
        
        if (minPrice.HasValue && maxPrice.HasValue && minPrice > maxPrice)
            throw new ArgumentException("Preço mínimo não pode ser maior que o máximo");
        
        if (year.HasValue && (year < 1900 || year > DateTime.Now.Year + 1))
            throw new ArgumentException("Ano inválido");
        
        if (minYear.HasValue && (minYear < 1900 || minYear > DateTime.Now.Year + 1))
            throw new ArgumentException("Ano mínimo inválido");
        
        if (maxYear.HasValue && (maxYear < 1900 || maxYear > DateTime.Now.Year + 1))
            throw new ArgumentException("Ano máximo inválido");
        
        if (minYear.HasValue && maxYear.HasValue && minYear > maxYear)
            throw new ArgumentException("Ano mínimo não pode ser maior que o máximo");

        return await gateway.SearchVehiclesAsync(
            brand, model, minPrice, maxPrice, year, minYear, maxYear, color, isAvailable);
    }
}