using Microsoft.Extensions.Diagnostics.HealthChecks;

using System.Threading;
using System.Threading.Tasks;

namespace ApiStorage.Mongo
{
    public class BaseHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return HealthCheckResult.Healthy("Health check success");
        }
    }
}
