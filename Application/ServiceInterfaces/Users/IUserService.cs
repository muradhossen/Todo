using Application.Common;
using Application.Common.Pagination;
using Application.DTOs.Users;
using Application.ServiceInterfaces.Base;
using Domain.Entities.Users;

namespace Application.ServiceInterfaces.Users;

public interface IUserService : IService<User>
{
    Task<PagedList<UserDto>> GetPagedUsersAsync(UserPageParams pageParam);
    Task<Result> Registration(RegistrationDTO request);
    Task<Result> Login(LoginDTO loginDto); 
}
