using AutoMapper;
using MediatR;
using VehicleCatalog.Application.Commands;
using VehicleCatalog.Application.DTOs;
using VehicleCatalog.Domain.Entities;
using VehicleCatalog.Domain.Interfaces;

namespace VehicleCatalog.Application.Handlers;

public class CreateVehicleHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateVehicleCommand, VehicleDto>
{
    public async Task<VehicleDto> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = new Vehicle(
            request.Brand,
            request.Model,
            request.Year,
            request.Color,
            request.Price
        );

        await unitOfWork.Vehicles.AddAsync(vehicle);
        await unitOfWork.SaveChangesAsync();

        return mapper.Map<VehicleDto>(vehicle);
    }
}
