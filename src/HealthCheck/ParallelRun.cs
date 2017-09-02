using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCheck
{
    public class ParallelRun : IRun
    {
        public Dictionary<string, HealthCheckResult> Check(Dictionary<string, IHealthCheck> checks)
        {
            var results = new ConcurrentDictionary<string, HealthCheckResult>();
            Parallel.ForEach(checks, kv =>
            {
                results.AddOrUpdate(kv.Key, kv.Value.Check(), (key, existingValue) => kv.Value.Check());
            });
            return results.ToDictionary(entry => entry.Key, entry => entry.Value);
        }
    }
}
