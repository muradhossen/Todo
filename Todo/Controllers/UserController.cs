using Application.Common;
using Application.DTOs.Todos;
using Application.DTOs.Users;
using Application.ServiceInterfaces.Users;
using Domain.Entities.Users;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Todo.Controllers.Base;

namespace Todo.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserCreateDTO> _createUserValidator;
        private readonly IValidator<UserUpdateDTO> _updateUserValidator;

        public UserController(IUserService userService, IValidator<UserCreateDTO> validator
, IValidator<UserUpdateDTO> updateUserValidator)
        {
            this._userService = userService;
            _createUserValidator = validator;
            _updateUserValidator = updateUserValidator;
        }


        [HttpGet]
        public async Task<IActionResult> GetPaginatedUsers([FromQuery] UserPageParams pageParam)
        { 
            var result =await _userService.GetPagedUsersAsync(pageParam);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(Result.Failure("User not found!"));
            }

            return Ok(Result<UserDto>.Success((new UserDto(result.Id, result.FullName, result.Email, result.Role, result.TeamId))));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDTO request)
        {

            var validation = await _createUserValidator.ValidateAsync(request);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            var result = await _userService.CreateUserAsync(request);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDTO request)
        {

            var validation = await _updateUserValidator.ValidateAsync(request);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            var result = await _userService.UpdateUserAsync(id, request);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

             var user = await _userService.GetByIdAsync(id);

            if (user == null) return NotFound(Result.Failure("User not found!"));

            var isDeleted = await _userService.RemoveAsync(user);

            if (isDeleted)
            {
                return Ok(Result.Success());
            }

            return BadRequest(Result.Failure("Failed to delete user!"));
        }

    }
}
