using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace HealthCheck
{
    public class UrlHealthCheck : IHealthCheck
    {

        private string _uri;

        public UrlHealthCheck(string uri)
        {
            _uri = uri;
        }

        public virtual HealthCheckResult Check()
        {
            var result = new HealthCheckResult();
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(_uri);
                request.Method = "HEAD";
                var response = (HttpWebResponse)request.GetResponse();
                result.Status = (response.StatusCode == HttpStatusCode.OK) ? Status.Healthy : Status.Unhealthy;
            }
            catch 
            {
                result.Status = Status.Unhealthy;
            }
            return result;
        }
    }
}