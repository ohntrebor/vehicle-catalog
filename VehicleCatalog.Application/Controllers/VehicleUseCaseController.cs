using VehicleCatalog.Application.DTOs;
using VehicleCatalog.Application.Gateways;
using VehicleCatalog.Application.Presenters;
using VehicleCatalog.Application.UseCases;

namespace VehicleCatalog.Application.Controllers;

public class VehicleUseCaseController(IVehicleGateway gateway, IVehiclePresenter presenter)
{
    public async Task<VehicleDto> CreateVehicle(CreateVehicleDto dto)
    {
        var useCase = new CreateVehicleUseCase(gateway);
        var vehicle = await useCase.ExecuteAsync(dto.Brand, dto.Model, dto.Year, dto.Color, dto.Price);
        return presenter.PresentVehicle(vehicle);
    }

    public async Task<VehicleDto> UpdateVehicle(UpdateVehicleDto dto)
    {
        var useCase = new UpdateVehicleUseCase(gateway);
        var vehicle = await useCase.ExecuteAsync(dto.Id, dto.Brand, dto.Model, dto.Year, dto.Color, dto.Price);
        return presenter.PresentVehicle(vehicle);
    }

    public async Task<IEnumerable<VehicleDto>> GetAvailableVehicles()
    {
        var useCase = new GetAvailableVehiclesUseCase(gateway);
        var vehicles = await useCase.ExecuteAsync();
        return presenter.PresentVehicleList(vehicles);
    }

    public async Task<IEnumerable<VehicleSaleDto>> GetSoldVehicles()
    {
        var useCase = new GetSoldVehiclesUseCase(gateway);
        var vehicles = await useCase.ExecuteAsync();
        return presenter.PresentSoldVehicleList(vehicles);
    }

    public async Task<bool> UpdatePaymentStatus(UpdatePaymentStatusDto dto)
    {
        var useCase = new UpdatePaymentStatusUseCase(gateway);
        return await useCase.ExecuteAsync(dto.VehicleId, dto.PaymentCode, dto.Status);
    }
    
    public async Task<bool> DeleteVehicle(Guid id)
    {
        var useCase = new DeleteVehicleUseCase(gateway);
        return await useCase.ExecuteAsync(id);
    }

    public async Task<VehicleDto?> GetVehicleById(Guid vehicleId)
    {
        var useCase = new GetVehicleByIdUseCase(gateway);
        var vehicle = await useCase.ExecuteAsync(vehicleId);

        if (vehicle == null)
            return null;

        return presenter.PresentVehicle(vehicle);
    }
    
    /// <summary>
    /// Busca veículos aplicando filtros específicos
    /// </summary>
    /// <param name="searchDto">Filtros de busca</param>
    /// <returns>Lista de veículos filtrados</returns>
    public async Task<IEnumerable<VehicleDto>> SearchVehicles(SearchVehiclesDto searchDto)
    {
        var useCase = new SearchVehiclesUseCase(gateway);
        var vehicles = await useCase.ExecuteAsync(
            searchDto.Brand,
            searchDto.Model,
            searchDto.MinPrice,
            searchDto.MaxPrice,
            searchDto.Year,
            searchDto.MinYear,
            searchDto.MaxYear,
            searchDto.Color,
            searchDto.IsAvailable
        );
    
        return presenter.PresentVehicleList(vehicles);
    }
}