using System;
using System.Net;
using System.Text;

namespace HealthCheck
{
    public class UrlHealthCheckBasicAuth : UrlHealthCheck
    {

        private readonly string _username;
        private readonly string _password;
        private readonly string _uri;

        public UrlHealthCheckBasicAuth(string uri, string username, string password) : base(uri)
        {
            _uri = uri;
            _username = username;
            _password = password;
        }


        public override HealthCheckResult Check()
        {
            var result = new HealthCheckResult();
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(_uri);
                request.Method = "HEAD";
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(_username + ":" + _password)));
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