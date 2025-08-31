using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Taskflow.Domain.ModelsPortal;

namespace Taskflow.API.CustomHealthCheck
{
    public class DatabasePortalHealtCheck(PortalContext portalContext) : IHealthCheck
    {
        private readonly PortalContext _portalContext = portalContext;

        /// <summary>
        /// Verifica la salud de la base de datos del contexto portal.
        /// </summary>
        /// <param name="context">Parametro del contexto.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="Task"/> Representa el asincronismo del metodo.</returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await _portalContext.Database.OpenConnectionAsync(cancellationToken);
                await _portalContext.Database.CloseConnectionAsync();

                return HealthCheckResult.Healthy("PONG");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(ex.Message);
            }
        }
    }

}
