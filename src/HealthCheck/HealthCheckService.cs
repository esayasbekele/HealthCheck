using System.Collections.Generic;

namespace HealthCheck
{
    public class HealthCheckService : IHealthCheckService
    {
        private Dictionary<string, IHealthCheck> _checks;
        private IRun _strategy;

        public HealthCheckService(Dictionary<string, IHealthCheck> checks, IRun strategy)
        {
            this._checks = checks;
            this._strategy = strategy;
        }

        public Dictionary<string, HealthCheckResult> Check()
        {
            var results = new Dictionary<string, HealthCheckResult>();
            return _strategy.Check(_checks);
        }
    }
}