using AutoMapper;
using GrpcService1;

namespace GrpcService.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Repositories.EF.Models.User, UserModel>();
            CreateMap<Repositories.Mongodb.Models.User, UserModel>();
        }
    }
}
