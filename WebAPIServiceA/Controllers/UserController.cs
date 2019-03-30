using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIService1.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private const string WebApiServiceBUrl = "http://localhost:5001";

        private readonly HttpClient _httpClient;
        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<List<UserModel>> GetList()
        {
            var response = await _httpClient.GetAsync($"{WebApiServiceBUrl}/user/getList");
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<UserModel>>(str);
            }
            return new List<UserModel>();
        }

        [HttpGet]
        public async Task<UserModel> Get(string userId)
        {
            var response = await _httpClient.GetAsync($"{WebApiServiceBUrl}/user/get?useId={userId}");
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
                UserId = "userId" + DateTime.Now,
                UserName = "userName" + DateTime.Now
            }), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{WebApiServiceBUrl}/user/add", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<bool>(result);
            }
            return false;
        }
    }


    public class UserModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
