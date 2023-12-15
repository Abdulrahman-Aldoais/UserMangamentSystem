using Application.Features.Users.Dtos.Get;
using Application.Features.Users.Dtos.GetList;
using Application.Repositories.UserRepository;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.UserService
{
    public class UserService : IUserService
    {
        public string UserId { get; }
        private readonly IUserReadRepository _userReadRepository;

        private readonly IMapper _mapper;
        public UserService(
            IUserReadRepository userReadRepository,
            IMapper mapper
            )
        {
            _userReadRepository = userReadRepository;
            _mapper = mapper;
        }
        public async Task<List<GetListUserOutput>> GetAllUserAsync()
        {
            var allUsers = _userReadRepository.GetAll();
            return await allUsers.Select(x => new GetListUserOutput
            {
                Id = x.Id,
                Age = x.Age,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                IsActive = x.IsActive,
                ModifiedDate = x.ModifiedDate,
                Phone = x.Phone,
                Email = x.Email,
                UserName = x.UserName,
                ModifiedBy = x.ModifiedBy

            }).ToListAsync();

            //var mapp = _mapper.Map<GetUserListOutput>( allUsers );
            // return mapp;
        }

        public async Task<GetUserOutput> GetUserByIdAsync(int id)
        {
            var getInformationUserById = await _userReadRepository.GetAsync(x => x.Id.Equals(id));
            var mappUserFromEintityToDto = _mapper.Map<GetUserOutput>(getInformationUserById);
            return mappUserFromEintityToDto;
        }

        public Task<string> AddNewUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
