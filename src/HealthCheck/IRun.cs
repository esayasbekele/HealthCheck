using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthCheck
{
    public interface IRun
    {
        Dictionary<string, HealthCheckResult> Check(Dictionary<string, IHealthCheck> checks);
    }
}
