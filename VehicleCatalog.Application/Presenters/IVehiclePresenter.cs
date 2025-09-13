using VehicleCatalog.Application.DTOs;
using VehicleCatalog.Domain.Entities;

namespace VehicleCatalog.Application.Presenters;

public interface IVehiclePresenter
{
    VehicleDto PresentVehicle(Vehicle vehicle);
    IEnumerable<VehicleDto> PresentVehicleList(IEnumerable<Vehicle> vehicles);
    VehicleSaleDto PresentSoldVehicle(Vehicle vehicle);
    IEnumerable<VehicleSaleDto> PresentSoldVehicleList(IEnumerable<Vehicle> vehicles);
}