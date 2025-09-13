using AutoMapper;
using VehicleCatalog.Application.DTOs;
using VehicleCatalog.Domain.Entities;

namespace VehicleCatalog.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Vehicle, VehicleDto>();
            CreateMap<Vehicle, VehicleSaleDto>()
                .ForMember(dest => dest.PaymentStatus, 
                    opt => opt.MapFrom(src => src.PaymentStatus.ToString()));
        }
    }
}