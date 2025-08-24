using Application.Common;
using Application.Common.Pagination;
using Application.DTOs.Users;
using Application.RepositoryInterfaces;
using Application.RepositoryInterfaces.Users;
using Application.ServiceInterfaces.Token;
using Application.ServiceInterfaces.Users;
using Application.Services.Base;
using Domain.Entities.Users;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;


namespace Application.Services.Users
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ITokenService _tokenService;
        private readonly ITeamRepository _teamRepository;

        public UserService(IUserRepository repository,
            ITokenService tokenService
            , ITeamRepository teamRepository) : base(repository)
        {
            this._repository = repository;
            this._tokenService = tokenService;
            this._teamRepository = teamRepository;
        }


        public async Task<PagedList<UserDto>> GetPagedUsersAsync(UserPageParams pageParam)
        {
            var query = _repository.TableNoTracking.AsQueryable();


            if (!string.IsNullOrWhiteSpace(pageParam.SearchKey))
            {
                string searchKey = pageParam.SearchKey.ToLower().Trim();

                query = query
                    .Where(c => c.FullName.ToLower().Contains(searchKey));
            }

            query = query.OrderByDescending(c => c.Id);

            if (pageParam.Order == -1)
            {
                query = query.OrderByDescending(c => c.Id);
            }

            var mappedDate = query.Select(x => new UserDto(x.Id, x.FullName, x.Email, x.Role, x.TeamId));

            return await PagedList<UserDto>.CreateAsync(mappedDate, pageParam.PageSize, pageParam.PageNumber);
        }
        public async Task<Result> Registration(RegistrationDTO request)
        {

            if (await _repository.IsExistAsync(request.Email))
            {
                return Result.Failure("User already exist!");
            }

            using var hmac = new HMACSHA512();

            var user = new User
            {
                Email = request.Email,
                FullName = request.FullName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
                PasswordSalt = hmac.Key,
                Role = (int)RoleEnum.Employee
            };

            if (await _repository.AddAsync(user))
            {
                var response = new RegistrationResponseDTO
                {
                    Email = request.Email,
                    FullName = request.FullName,
                    Token = _tokenService.CreateToken(user)
                };

                return Result<RegistrationResponseDTO>.Success(response);
            }

            return Result.Failure("Failed to add new user!");
        }

        public async Task<Result> Login(LoginDTO loginDto)
        {
            var user = await _repository.GetFirstOrDefaultAsync(c => c.Email == loginDto.Email);

            if (user == null) return Result.Failure("Invalid email address!");

            var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Result.Failure("Invalid password!");
            }

            var response = new RegistrationResponseDTO
            {
                FullName = user.FullName,
                Email = loginDto.Email,
                Token = _tokenService.CreateToken(user)
            };

            return Result<RegistrationResponseDTO>.Success(response);

        }


        public async Task<Result> CreateUserAsync(UserCreateDTO request)
        {
            if (await _repository.IsExistAsync(request.Email))
            {
                return Result.Failure("User already exist!");
            }

            if (request.TeamId != null)
            {
                var isTeamExist = await _teamRepository.TableNoTracking.AnyAsync(c => c.Id == request.TeamId);

                if (!isTeamExist)
                {
                    return Result.Failure("Team dosen't exist!");
                }
            }

            using var hmac = new HMACSHA512();

            var user = new User
            {
                Email = request.Email,
                FullName = request.FullName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
                PasswordSalt = hmac.Key,
                Role = request.Role,
                TeamId = request.TeamId
            };

            if (await _repository.AddAsync(user))
            {
                var response = new UserDto(user.Id, user.FullName, user.Email, user.Role, user.TeamId);

                return Result<UserDto>.Success(response);
            }

            return Result.Failure("Failed to add new user!");
        }

        public async Task<Result> UpdateUserAsync(int id, UserUpdateDTO request)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                return Result<UserDto>.Failure("User dose not exist!");
            }

            if (request.TeamId != null)
            {
                var isTeamExist = await _teamRepository.TableNoTracking.AnyAsync(c => c.Id == request.TeamId);

                if (!isTeamExist)
                {
                    return Result.Failure("Team dosen't exist!");
                }
            }


            user.FullName = request.FullName;
            user.TeamId = request.TeamId;
            user.Role = request.Role;

            if (await _repository.UpdateAsync(user))
            {
                var response = new UserDto(user.Id, user.FullName, user.Email, user.Role, user.TeamId);

                return Result<UserDto>.Success(response);
            }

            return Result.Failure($"Failed to update {user.FullName} user!");
        }
    }
}
