using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPIService_NetFramework.Controllers
{
    public class ValuesController : ApiController
    {
        private const string WebApiServiceBUrl = "http://localhost:5001";
        private readonly HttpClient httpClient = new HttpClient();
        // GET api/values
        public async Task<IEnumerable<string>> Get()
        {
            var response = await httpClient.GetAsync($"{WebApiServiceBUrl}/user/getList");
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                return new string[] { str };
            }
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
