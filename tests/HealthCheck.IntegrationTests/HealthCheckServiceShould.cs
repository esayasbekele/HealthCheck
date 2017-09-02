using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace HealthCheck.IntegrationTests
{

    [TestClass]
    [DeploymentItem(@"Testdb.mdf")]
    [DeploymentItem(@"Testdb_log.ldf")]
    public class HealthCheckServiceShould
    {

        [ClassInitialize]
        public static void ClassSetup(TestContext context)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(context.TestDeploymentDir, string.Empty));
        }


        [TestMethod]
        public void CheckUrl()
        {
            //arrange
            var builder = new HealthCheckBuilder()
                           .AddUrlCheck("http://www.google.com");
            var service = builder.Build();

            //act
            var results = service.Check();

            //assert
            Assert.IsTrue(results.All(r => r.Value.Status == Status.Healthy));
        }


        [TestMethod]
        public void CheckSqlServer()
        {
            //arrange
            var builder = new HealthCheckBuilder()
                           .AddSqlServerCheck("localdb", @"Data Source=(LocalDB)\v11.0;Integrated Security=True;MultipleActiveResultSets=True;AttachDBFilename=|DataDirectory|\Testdb.mdf");
            var service = builder.Build();

            //act
            var results = service.Check();

            //assert
            Assert.IsTrue(results.All(r => r.Value.Status == Status.Healthy));
        }


        [TestMethod]
        public void CheckUrlWithBasicAuth()
        {
            //arrange
            var builder = new HealthCheckBuilder()
                           .AddUrlCheckWithBasicAuth("http://httpbin.org/basic-auth/user/passwd", "user", "passwd");
            var service = builder.Build();

            //act
            var results = service.Check();

            //assert
            Assert.IsTrue(results.All(r => r.Value.Status == Status.Healthy));
        }

    }
}
