using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthCheck.Tests
{
    [TestClass]
    public class HealthCheckBuilderShould
    {
        [TestMethod]
        public void BuildHealthCheckServiceWithUrlCheckAndParallelRunStrategy()
        {
            //arrange
            var builder = new HealthCheckBuilder()
                           .AddUrlCheck("http://www.google.com")
                           .RunInParallel();
            //act
            var service = builder.Build();

            //assert
            Assert.IsInstanceOfType(service, typeof(IHealthCheckService));
        }


        [TestMethod]
        public void BuildHealthCheckServiceWithSqlServerCheck()
        {
            //arrange
            var builder = new HealthCheckBuilder()
                           .AddSqlServerCheck("localdb", @"Data Source=.\SQLEXPRESS; Initial Catalog=master; Integrated Security=True")
                           .RunInParallel();


            //act
            var service = builder.Build();

            //assert
            Assert.IsInstanceOfType(service, typeof(IHealthCheckService));
        }

        [TestMethod]
        public void BuildHealthCheckServiceWithUrlCheckBasicAuth()
        {
            //arrange
            var builder = new HealthCheckBuilder()
                           .AddUrlCheckWithBasicAuth("http://httpbin.org/basic-auth/user/passwd", "user", "passwd");


            //act
            var service = builder.Build();

            //assert
            Assert.IsInstanceOfType(service, typeof(IHealthCheckService));
        }

    }
}
