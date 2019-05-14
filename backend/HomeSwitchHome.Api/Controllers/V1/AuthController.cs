using HomeSwitchHome.API.Contracts.V1;
using HomeSwitchHome.API.Contracts.V1.Requests;
using HomeSwitchHome.API.Contracts.V1.Responses.Users;
using HomeSwitchHome.API.Infrastructure;
using HomeSwitchHome.Application;
using HomeSwitchHome.Application.Models.Users;
using HomeSwitchHome.Application.Services.Users;
using HomeSwitchHome.Domain.Entities;
using HomeSwitchHome.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HomeSwitchHome.API.Controllers.V1
{
    public class AuthController : ControllerBase
    {
        private AuthenticationConfig _config;
        private SigningCredentials _signingCredentials;
        private ITokenGenerator _tokenGenerator;
        private IUsersService _usersService;

        private static readonly Logger Logger = LogManager.GetLogger(typeof(AuthController).FullName);

        public AuthController(AuthenticationConfig config, SigningCredentials signingCredentials,
                              ITokenGenerator tokenGenerator, IUsersService usersService)
        {
            _config = config;
            _signingCredentials = signingCredentials;
            _tokenGenerator = tokenGenerator;
            _usersService = usersService;
        }

        [HttpPost(ApiRoutes.Auth.Authenticate)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult Authenticate([FromBody] AuthenticateUserRequest request)
        {
            if (ModelState.IsValid == false)
                return BadRequest("Invalid Request");

            var user = _usersService.Authenticate(request.UserName, request.Password);

            if (user != null)
                return Ok(GetTokenResponse(user));

            return Unauthorized();
        }

        [HttpPost(ApiRoutes.Auth.Refresh)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult Refresh([FromBody] RefreshTokenRequest request)
        {
            var user = _usersService.Get(request.UserId);
            if (user == null)
                throw new SecurityTokenException("User not found");

            var refreshToken = _usersService.GetRefresh(user);
            if (refreshToken == null)
                return NotFound();

            if (refreshToken.Token != request.RefreshToken)
                throw new SecurityTokenException("Invalid refresh token");
            if (refreshToken.Expiration <= DateTime.Now)
                throw new SecurityTokenException("Expired refresh token");

            return Ok(GetTokenResponse(user));
        }

        private TokenResponse GetTokenResponse(User user)
        {
            var accessTokenExpiry = DateTime.UtcNow.AddMinutes(_config.JWT_EXPIRE_MINUTES);

            var principal = ClaimsPrincipalHelper.Create<User>(user);
            var identity = (ClaimsIdentity)principal.Identity;

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = _signingCredentials,
                Subject = identity,
                Expires = accessTokenExpiry
            };
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(securityTokenDescriptor);
            var accessToken = handler.WriteToken(securityToken);

            Logger.Info("User login: {0}", user.UserName);

            var refreshToken = _tokenGenerator.GenerateRefreshToken();
            var expiration = DateTime.UtcNow.AddDays(_config.JWT_REFRESH_TOKEN_EXPIRE_IN_DAYS);

            _usersService.UpdateRefreshToken(user, refreshToken, expiration);

            return new TokenResponse()
            {
                access_token = accessToken,
                refresh_token = refreshToken,
                token_type = "Bearer",
                expires_in = (int)TimeSpan.FromMinutes(_config.JWT_EXPIRE_MINUTES).TotalSeconds,
                user_id = user.Id,
                user_name = user.Name
            };
        }
    }
}