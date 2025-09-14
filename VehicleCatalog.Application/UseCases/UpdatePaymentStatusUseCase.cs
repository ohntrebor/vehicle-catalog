using VehicleCatalog.Application.Gateways;
using VehicleCatalog.Domain.Enums;

namespace VehicleCatalog.Application.UseCases;

public class UpdatePaymentStatusUseCase(IVehicleGateway gateway)
{
    public async Task<bool> ExecuteAsync(Guid vehicleId, string paymentCode, string status)
    {
        var vehicle = await gateway.FindByIdAsync(vehicleId);
        if (vehicle == null) return false;

        // Mapear status recebido para PaymentStatus
        var paymentStatus = MapStringToPaymentStatus(status);
        if (paymentStatus == null) return false;

        vehicle.UpdatePaymentStatus(paymentCode, paymentStatus.Value);
        await gateway.UpdateAsync(vehicle);
        return true;
    }

    private PaymentStatus? MapStringToPaymentStatus(string status)
    {
        return status?.ToLowerInvariant() switch
        {
            "0" or "pending" or "processing" => PaymentStatus.Pending,
            "1" or "confirmed" or "paid" or "approved" => PaymentStatus.Paid,
            "2" or "cancelled" or "canceled" => PaymentStatus.Cancelled,
            "3" or "failed" or "rejected" or "declined" => PaymentStatus.Failed,
            _ => null
        };
    }
}