using HomeSwitchHome.API.Contracts.V1;
using HomeSwitchHome.Application.Models.Users;
using HomeSwitchHome.Application.Services.Users;
using HomeSwitchHome.Infrastructure;
using HomeSwitchHome.Infrastructure.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Linq;

namespace HomeSwitchHome.API.Controllers.V1
{
    public class UsersController : ControllerBase
    {
        private IUsersService _usersService;

        private static readonly Logger Logger = LogManager.GetLogger(typeof(UsersController).FullName);

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost(ApiRoutes.Users.Register)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public IActionResult Register([FromBody] CreateUserRequest request)
        {
            if (ModelState.IsValid == false)
                return BadRequest("Invalid Request");

            Logger.Info("User creation with username: " + request.UserName);

            var userId = _usersService.Create(request.UserName, request.Name, request.Password, request.Role);

            Logger.Info("User created with username: " + request.UserName + " -> id: " + userId);

            return Ok(userId);
        }


        [Authorize(Roles = UserRole.Admin)]
        [HttpGet(ApiRoutes.Users.GetAll)]
        public IActionResult GetAll(GetUsersFilter filter)
        {
            var users = _usersService.GetAll(filter);
            var result = users.Select(x => new UserModel()
            {
                Id = x.Id,
                Name = x.Name,
                UserName = x.UserName,
                IsActive = x.IsActive,
                Role = x.Role,
                CreatedAt = x.CreatedAt.GetValueOrDefault(new DateTime(2019, 01, 01)),
                CreatedBy = x.CreatedBy,
                UpdatedAt = x.UpdatedAt,
                UpdatedBy = x.UpdatedBy
            }).ToArray();

            return Ok(result);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpGet(ApiRoutes.Users.Get)]
        public IActionResult GetById(int id)
        {
            var user = _usersService.Get(id);
            if (user == null)
                return NotFound();

            var result = new UserEditModel()
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                IsActive = user.IsActive,
                Role = user.Role,
                CreatedAt = user.CreatedAt.GetValueOrDefault(new DateTime(2019, 01, 01)),
                CreatedBy = user.CreatedBy,
                UpdatedAt = user.UpdatedAt,
                UpdatedBy = user.UpdatedBy
            };

            return Ok(result);
        }

        [Authorize(Roles = UserRole.GetAll)]
        [HttpGet(ApiRoutes.Users.Profile)]
        public IActionResult Profile()
        {
            var id = User.GetId().Value;
            var filter = new GetUsersFilter()
            {
                Id = id
            };
            var user = _usersService.Get(filter);
            if (user == null)
                return NotFound();

            var result = new UserEditModel()
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                IsActive = user.IsActive,
                Role = user.Role,
                CreatedAt = user.CreatedAt.GetValueOrDefault(new DateTime(2019, 01, 01)),
                CreatedBy = user.CreatedBy,
                UpdatedAt = user.UpdatedAt,
                UpdatedBy = user.UpdatedBy
            };

            return Ok(result);
        }

        [Authorize]
        [HttpPut(ApiRoutes.Users.Update)]
        public IActionResult Update(int id, [FromBody] UserEditModel userDto)
        {
            //If user is not an admin, he can only edit his own profile
            if (User.GetRole() != "admin" && id != User.GetId())
                return Unauthorized();

            // map dto to entity and set id
            if (_usersService.Exist(id) == false)
                return NotFound();

            _usersService.Update(id, userDto.Name, userDto.IsActive, userDto.Role, userDto.Password);

            return Ok();
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpDelete(ApiRoutes.Users.Delete)]
        public IActionResult Delete(int id)
        {
            if (_usersService.Exist(id) == false)
                return NotFound();

            _usersService.Delete(id);
            return Ok();
        }
    }
}