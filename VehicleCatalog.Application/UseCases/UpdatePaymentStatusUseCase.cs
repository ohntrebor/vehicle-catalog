using VehicleCatalog.Application.Gateways;

namespace VehicleCatalog.Application.UseCases;

public class UpdatePaymentStatusUseCase(IVehicleGateway gateway)
{
    public async Task<bool> ExecuteAsync(string paymentCode, string status)
    {
        var vehicle = await gateway.FindByPaymentCodeAsync(paymentCode);
        if (vehicle == null) return false;

        if (Enum.TryParse<Domain.Enums.PaymentStatus>(status, out var paymentStatus))
        {
            vehicle.UpdatePaymentStatus(paymentStatus);
            await gateway.UpdateAsync(vehicle);
            return true;
        }

        return false;
    }
}