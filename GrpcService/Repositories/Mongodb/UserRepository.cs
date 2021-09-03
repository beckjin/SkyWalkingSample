using AutoMapper;
using GrpcService1;
using MD.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrpcService.Repositories.Mongodb
{
    public class UserRepository : MongoBaseRepository<Models.User>, IUserRepository
    {
        private readonly IMapper _mapper;

        public UserRepository(IMapper mapper) : base("mongodb://127.0.0.1:27017/skywalking", "skywalking", "user")
        {
            _mapper = mapper;
        }

        public async Task<List<UserModel>> GetListAsync()
        {
            var users = await FindAsync(new BsonDocument());
            return _mapper.Map<List<UserModel>>(users);
        }


        public async Task<UserModel> GetAsync(string userId)
        {
            var filter = Builders<Models.User>.Filter.Eq(m => m.UserId, userId);
            var user = await FindOneAsync(filter);
            return _mapper.Map<UserModel>(user);
        }

        public async Task<bool> AddAsync(string userId, string userName)
        {
            var user = new Models.User
            {
                UserId = userId,
                UserName = userName
            };
            return await InsertAsync(user);
        }

        public async Task<bool> RemoveAsync(string userId)
        {
            var filter = Builders<Models.User>.Filter.Eq(m => m.UserId, userId);
            var result = await DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
    }
}
