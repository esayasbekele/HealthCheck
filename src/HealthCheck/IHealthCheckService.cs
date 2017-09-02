using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthCheck
{
    public interface IHealthCheckService
    {
        Dictionary<string, HealthCheckResult> Check();
    }
}
