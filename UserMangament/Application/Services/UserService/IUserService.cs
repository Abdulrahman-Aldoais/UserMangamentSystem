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

        Task<string> AddNewUserAsync(User user);
        Task<string> UpdateInformationUser(User user);


    }
}
