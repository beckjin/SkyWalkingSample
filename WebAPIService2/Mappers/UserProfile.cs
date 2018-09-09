using AutoMapper;
using WebAPIService2.Controllers;
using WebAPIService2.Models;

namespace WebAPIService2.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, User>();
        }
    }
}
