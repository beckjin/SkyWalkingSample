using AutoMapper;
using Grpc.Core;
using GrpcService1;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static GrpcService1.User;

namespace WebAPIService2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserClient _userClient;
        private readonly IMapper _mapper;

        public UserController(IMapper mapper)
        {
            _mapper = mapper;
            _userClient = new UserClient(new Channel("127.0.0.1:5051", ChannelCredentials.Insecure)); // 实际上Channel不能每次都创建
        }

        [HttpGet]
        public async Task<List<UserModel>> GetList()
        {
            var response = await _userClient.GetListAsync(new GetListRequest());
            if (response.Code == 1000)
            {
                var list = _mapper.Map<List<UserModel>>(response.Data);
                return list;
            }
            return new List<UserModel>();
        }

        [HttpGet]
        public async Task<UserModel> Get(string userId)
        {
            var request = new GetRequest()
            {
                UserId = userId
            };
            var response = await _userClient.GetAsync(request);
            if (response.Code == 1000)
            {
                var user = _mapper.Map<UserModel>(response.Data);
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
