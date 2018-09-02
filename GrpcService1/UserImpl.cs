using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace GrpcService1
{
    public class UserImpl: User.UserBase
    {
        public override Task<GetListResponse> GetList(GetListRequest request, ServerCallContext context)
        {
            var users = new Google.Protobuf.Collections.RepeatedField<UserModel>()
            {
                new UserModel()
                {
                    UserId = Guid.NewGuid().ToString(),
                    UserName = "James"
                },
                new UserModel()
                {
                    UserId = Guid.NewGuid().ToString(),
                    UserName = "Beck"
                }
            };

            var response = new GetListResponse();
            response.Code = 1000;
            response.Data.AddRange(users);

            return Task.FromResult(response);
        }

        public override Task<GetResponse> Get(GetRequest request, ServerCallContext context)
        {
            var response = new GetResponse();
            response.Code = 1000;
            response.Data = new UserModel()
            {
                UserId = Guid.NewGuid().ToString(),
                UserName = "Mark"
            };
            return Task.FromResult(response);
        }

        public override Task<RemoveResponse> Remove(RemoveRequest request, ServerCallContext context)
        {
            var response = new RemoveResponse();
            response.Code = 1000;
            response.Data = true;
            return Task.FromResult(response);
        }
    }
}
