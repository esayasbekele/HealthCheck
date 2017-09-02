# HealthCheck


Steps to adding HealthCheck to a asp.net web api 2

1.Build and register the healthcheck service

```var healthCheckService = new HealthCheckBuilder()
                            .AddUrlCheck("http://www.google.com", "http://example.com", "http://infoq.com")
                            .AddUrlCheckWithBasicAuth("http://httpbin.org/basic-auth/user/passwd", "user", "passwd")
                            .AddSqlServerCheck("localdb", @"Data Source=.\SQLEXPRESS; Initial Catalog=master; Integrated Security=True")
                            .RunInParallel()
                            .Build(); 
                            ```

2.Enable Route Mapping 

``` config.MapHttpAttributeRoutes(); 
```


3.Add a controller that leverages the healthcheck lib

```
[RoutePrefix("")]
    public class HealthController : ApiController
    {
        private readonly IHealthCheckService _healthCheckService;

        public HealthController(IHealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }


        [Route("healthcheck")]
        [Route("hc")]
        public HttpResponseMessage Get()
        {
            var results = _healthCheckService.Check();
            HttpConfiguration config = new HttpConfiguration();
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add
                (new Newtonsoft.Json.Converters.StringEnumConverter());
            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));
            return Request.CreateResponse(results.Any(r => r.Value.Status == Status.Unhealthy) ? HttpStatusCode.ServiceUnavailable : HttpStatusCode.OK, results, config);
        }
    } 
 ```
