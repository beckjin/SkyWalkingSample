using AutoMapper;
using WebAPIServiceB.Models;

namespace WebAPIService2.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<GrpcService1.UserModel, UserModel>();
        }
    }
}
