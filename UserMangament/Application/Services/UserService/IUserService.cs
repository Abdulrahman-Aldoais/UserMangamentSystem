

using Application.Features.Users.Dtos.Get;
using Application.Features.Users.Dtos.GetList;
using Domain.Entities;

namespace Application.Services.UserService
{
    public interface IUserService
    {
        public string UserId { get; }
        Task<List<GetListUserOutput>> GetAllUserAsync();
        Task<GetUserOutput> GetUserByIdAsync(int id);
        //public Task<BaseCommandResponse<GetUserOutput>> AddUserAsync(User user);
        public Task<string> AddNewUserAsync(User user);

    }
}
