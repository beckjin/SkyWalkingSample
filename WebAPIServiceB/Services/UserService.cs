using AutoMapper;
using Grpc.Core;
using Grpc.Core.Interceptors;
using GrpcService1;
using SkyApm.Diagnostics.Grpc.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using static GrpcService1.User;

namespace WebAPIServiceB.Services
{
    public class UserService
    {
        private readonly UserClient _client;
        private readonly IMapper _mapper;

        public UserService(ClientDiagnosticInterceptor interceptor, IMapper mapper)
        {
            var target = "127.0.0.1:5051";
            var channel = new Channel(target, ChannelCredentials.Insecure);
            var invoker = channel.Intercept(interceptor);
            _client = new UserClient(invoker).WithHost(target);
            _mapper = mapper;
        }

        public async Task<List<Models.UserModel>> GetListAsync()
        {
            var response = await _client.GetListAsync(new GetListRequest());
            return response.Code == 1000 ? _mapper.Map<List<Models.UserModel>>(response.Data) : new List<Models.UserModel>();
        }

        public async Task<Models.UserModel> GetAsync(string userId)
        {
            var request = new GetRequest()
            {
                UserId = userId
            };
            var response = await _client.GetAsync(request);
            return response.Code == 1000 ? _mapper.Map<Models.UserModel>(response.Data) : null;
        }


        public async Task<bool> AddAsync(string userId, string name)
        {
            var request = new AddRequest()
            {
                UserId = userId,
                UserName = name
            };
            var response = await _client.AddAsync(request);
            return response.Data;
        }

        public async Task<bool> RemoveAsync(string userId)
        {
            var request = new RemoveRequest()
            {
                UserId = userId
            };
            var response = await _client.RemoveAsync(request);
            return response.Data;
        }
    }
}
