using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthCheck
{
    public class SequentialRun : IRun
    {
        public Dictionary<string, HealthCheckResult> Check(Dictionary<string, IHealthCheck> checks)
        {
            var results = new Dictionary<string, HealthCheckResult>();
            foreach (var check in checks)
                results.Add(check.Key, check.Value.Check());
            return results;
        }
    }
}
