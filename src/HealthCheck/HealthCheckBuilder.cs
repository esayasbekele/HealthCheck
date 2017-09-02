using System.Collections.Generic;

namespace HealthCheck
{
    public class HealthCheckBuilder
    {
        private Dictionary<string, IHealthCheck> _checks;
        private IRun _strategy = new SequentialRun();


        public HealthCheckBuilder()
        {
            _checks = new Dictionary<string, IHealthCheck>();
        }


        public HealthCheckBuilder AddCheck(string name, IHealthCheck healthCheck)
        {
            if (!_checks.ContainsKey(name))
            {
                _checks.Add(name, healthCheck);
            }
            return this;
        }


        public HealthCheckBuilder AddUrlCheck(params string[] urls)
        {
            foreach (var url in urls)
            {
                if (!_checks.ContainsKey(url))
                {
                    _checks.Add(url, new UrlHealthCheck(url));
                }
            }
            return this;

        }

        public HealthCheckBuilder AddUrlCheckWithBasicAuth(string url, string username, string password)
        {
            if (!_checks.ContainsKey(url))
            {
                _checks.Add(url, new UrlHealthCheckBasicAuth(url, username, password));
            }
            return this;
        }

        public HealthCheckBuilder AddSqlServerCheck(string name, string connectionString)
        {
            if (!_checks.ContainsKey(name))
            {
                _checks.Add(name, new SqlServerHealthCheck(connectionString));
            }
            return this;
        }


        public HealthCheckBuilder RunInParallel()
        {
            _strategy = new ParallelRun();
            return this;
        }

        public HealthCheckService Build()
        {
            return new HealthCheckService(this._checks, this._strategy);
        }
    }
}