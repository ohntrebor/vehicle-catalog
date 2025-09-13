using MediatR;
using VehicleCatalog.Application.DTOs;

namespace VehicleCatalog.Application.Commands;

public class UpdatePaymentStatusCommand(PaymentWebhookDto dto) : IRequest<bool>
{
    public string PaymentCode { get; set; } = dto.PaymentCode;
    public string Status { get; set; } = dto.Status;
}
