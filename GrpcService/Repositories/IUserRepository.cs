using GrpcService1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrpcService.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetListAsync();

        Task<UserModel> GetAsync(string userId);

        Task<bool> RemoveAsync(string userId);

        Task<bool> AddAsync(string userId, string userName);
    }
}
