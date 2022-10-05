using System.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DK_Project.HealthChecks
{
    public class CustomHealthCheck : IHealthCheck
    {
        private readonly IConfiguration _configuration;

        public CustomHealthCheck(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                //throw new NotImplementedException();
            }
            catch (NotImplementedException ex)
            {
                return HealthCheckResult.Unhealthy("ITS NOT IMPLEMENTED");
            }
            return HealthCheckResult.Healthy("Its ALLRIGHT");
        }
    }
}
