using Microsoft.Extensions.Diagnostics.HealthChecks;
using VehicleCatalog.Infrastructure.Data;
using VehicleCatalog.Infrastructure.Data;

namespace VehicleCatalog.API.Health
{
    /// <summary>
    /// Health check para verificar a conectividade com o banco de dados
    /// </summary>
    public class DatabaseHealthCheck(ApplicationDbContext context) : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context1,
            CancellationToken cancellationToken = default)
        {
            try
            {
                // Tenta executar uma query simples no banco
                await context.Database.CanConnectAsync(cancellationToken);
                
                return HealthCheckResult.Healthy("Database is accessible");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(
                    "Database is not accessible",
                    exception: ex);
            }
        }
    }
}