using HealthCheck;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace API
{
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
}