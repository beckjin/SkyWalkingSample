using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIServiceB.Models;
using WebAPIServiceB.Services;

namespace WebAPIService2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<List<UserModel>> GetList()
        {
            return await _userService.GetListAsync();
        }

        [HttpGet]
        public async Task<UserModel> Get([FromQuery]string userId)
        {
            return await _userService.GetAsync(userId);
        }

        [HttpPost]
        public async Task<bool> Add([FromBody] UserModel request)
        {
            return await _userService.AddAsync(request.UserId, request.UserName);
        }

        [HttpPost]
        public async Task<bool> Remove([FromBody] string userId)
        {
            return await _userService.RemoveAsync(userId);
        }
    }
}
