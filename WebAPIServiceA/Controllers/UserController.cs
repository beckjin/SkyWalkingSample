using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebAPIServiceA.Models;

namespace WebAPIService1.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private const string WebApiServiceBUrl = "http://localhost:5001";

        private readonly IHttpClientFactory _httpClientFactory;
        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<List<UserModel>> GetList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{WebApiServiceBUrl}/user/getList");
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<UserModel>>(str);
            }
            return new List<UserModel>();
        }

        [HttpGet]
        public async Task<UserModel> Get([FromQuery]string userId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{WebApiServiceBUrl}/user/get?userId={userId}");
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserModel>(str);
            }
            return null;
        }

        [HttpPost]
        public async Task<bool> Add([FromBody]UserModel request)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                UserId = request.UserId,
                UserName = request.UserName
            }), Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync($"{WebApiServiceBUrl}/user/add", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<bool>(result);
            }
            return false;
        }
    }
}
