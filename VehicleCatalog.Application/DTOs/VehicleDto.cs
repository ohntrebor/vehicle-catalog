
using System.ComponentModel.DataAnnotations;

namespace VehicleCatalog.Application.DTOs;

/// <summary>
/// DTO para representação de veículo
/// </summary>
public class VehicleDto
{
    public Guid Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public bool IsSold { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class VehicleSaleDto : VehicleDto
{
    public string? BuyerCpf { get; set; }
    public DateTime? SaleDate { get; set; }
    public string? PaymentStatus { get; set; }
    public string? PaymentCode { get; set; }
    public DateTime? SoldAt { get; set; }
}

public class CreateVehicleDto
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
}

public class UpdateVehicleDto : CreateVehicleDto
{
    public Guid Id { get; set; }
}

public class UpdatePaymentStatusDto
{
    public Guid VehicleId { get; set; }
    public string PaymentCode { get; set; }
    public string Status { get; set; }
}


/// <summary>
/// DTO para filtros de busca de veículos
/// </summary>
public class SearchVehiclesDto
{
    /// <summary>
    /// Marca do veículo (busca parcial)
    /// </summary>
    /// <example>Toyota</example>
    public string? Brand { get; set; }

    /// <summary>
    /// Modelo do veículo (busca parcial)
    /// </summary>
    /// <example>Corolla</example>
    public string? Model { get; set; }

    /// <summary>
    /// Preço mínimo
    /// </summary>
    /// <example>20000</example>
    [Range(0, double.MaxValue, ErrorMessage = "Preço mínimo deve ser maior ou igual a zero")]
    public decimal? MinPrice { get; set; }

    /// <summary>
    /// Preço máximo
    /// </summary>
    /// <example>50000</example>
    [Range(0, double.MaxValue, ErrorMessage = "Preço máximo deve ser maior ou igual a zero")]
    public decimal? MaxPrice { get; set; }

    /// <summary>
    /// Ano específico do veículo
    /// </summary>
    /// <example>2020</example>
    [Range(1900, 2030, ErrorMessage = "Ano deve estar entre 1900 e 2030")]
    public int? Year { get; set; }

    /// <summary>
    /// Ano mínimo (usado apenas se Year não for especificado)
    /// </summary>
    /// <example>2018</example>
    [Range(1900, 2030, ErrorMessage = "Ano mínimo deve estar entre 1900 e 2030")]
    public int? MinYear { get; set; }

    /// <summary>
    /// Ano máximo (usado apenas se Year não for especificado)
    /// </summary>
    /// <example>2023</example>
    [Range(1900, 2030, ErrorMessage = "Ano máximo deve estar entre 1900 e 2030")]
    public int? MaxYear { get; set; }

    /// <summary>
    /// Cor do veículo (busca parcial)
    /// </summary>
    /// <example>Preto</example>
    public string? Color { get; set; }

    /// <summary>
    /// Filtrar por disponibilidade: true = apenas disponíveis, false = apenas vendidos, null = todos
    /// </summary>
    /// <example>true</example>
    public bool? IsAvailable { get; set; }
}