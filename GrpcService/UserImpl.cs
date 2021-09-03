using AutoMapper;
using Grpc.Core;
using GrpcService.Repositories;
using GrpcService.Repositories.EF;
using System.Threading.Tasks;

namespace GrpcService1
{
    public class UserImpl : User.UserBase
    {
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserImpl(IUserRepository userRepository, AppDbContext appDbContext, IMapper mapper)
        {
            _userRepository = userRepository;
            _dbContext = appDbContext;
            _mapper = mapper;
        }

        public override async Task<GetListResponse> GetList(GetListRequest request, ServerCallContext context)
        {
            await new UserRepository(_dbContext, _mapper).GetListAsync();

            var users = await _userRepository.GetListAsync();
            var response = new GetListResponse
            {
                Code = 1000
            };
            response.Data.AddRange(users);
            return response;
        }

        public override async Task<GetResponse> Get(GetRequest request, ServerCallContext context)
        {
            var user = await _userRepository.GetAsync(request.UserId);

            return new GetResponse
            {
                Code = 1000,
                Data = user
            };
        }

        public override async Task<boolResponse> Remove(RemoveRequest request, ServerCallContext context)
        {
            var result = await _userRepository.RemoveAsync(request.UserId);

            return new boolResponse
            {
                Code = 1000,
                Data = result
            };
        }

        public override async Task<boolResponse> Add(AddRequest request, ServerCallContext context)
        {
            var result = await _userRepository.AddAsync(request.UserId, request.UserName);

            return new boolResponse
            {
                Code = 1000,
                Data = result
            };
        }
    }
}
