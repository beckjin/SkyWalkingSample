using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebAPIService1.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private const string WebApiService2Url = "http://localhost:5001";

        [HttpGet]
        public async Task<List<UserModel>> GetList()
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{WebApiService2Url}/user/getList");
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<UserModel>>(str);
                return users;
            }
            return new List<UserModel>();
        }

        [HttpGet]
        public async Task<UserModel> Get(string userId)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{WebApiService2Url}/user/get?useId={userId}");
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserModel>(str);
                return user;
            }
            return null;
        }
    }


    public class UserModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
