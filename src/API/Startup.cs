using API;
using HealthCheck;
using Microsoft.Owin;
using Microsoft.Practices.Unity;
using Owin;
using System.Web.Http;

[assembly:OwinStartup(typeof(Startup),"Configure")]
namespace API
{
    public class Startup
    {
        public static void Configure(IAppBuilder appBuilder)
        {
           HttpConfiguration httpConfiguration = new HttpConfiguration();
           var container = new UnityContainer();
           
            //build and register a health check service 
           var healthCheckService = new HealthCheckBuilder()
                            .AddUrlCheck("http://www.google.com", "http://example.com", "http://infoq.com")
                            .AddUrlCheckWithBasicAuth("http://httpbin.org/basic-auth/user/passwd", "user", "passwd")
                            .AddSqlServerCheck("localdb", @"Data Source=.\SQLEXPRESS; Initial Catalog=master; Integrated Security=True")
                            .RunInParallel()
                            .Build();
           container.RegisterInstance<IHealthCheckService>(healthCheckService);

            httpConfiguration.DependencyResolver = new UnityResolver(container);
            WebApiConfig.Register(httpConfiguration);
            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}