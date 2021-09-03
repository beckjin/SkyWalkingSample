using AutoMapper;
using GrpcService1;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrpcService.Repositories.EF
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _dbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<UserModel>> GetListAsync()
        {
            await Task.CompletedTask;
            var users = _mapper.Map<List<UserModel>>(_dbContext.Users);
            return users;
        }

        public async Task<UserModel> GetAsync(string userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            return _mapper.Map<UserModel>(user);
        }

        public async Task<bool> RemoveAsync(string userId)
        {
            var user = new Models.User { UserId = userId };
            _dbContext.Entry(user).State = EntityState.Deleted;
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> AddAsync(string userId, string userName)
        {
            var user = new Models.User
            {
                UserId = userId,
                UserName = userName
            };

            await _dbContext.Users.AddAsync(user);
            var count = await _dbContext.SaveChangesAsync();
            return count > 0;
        }
    }
}
