using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Seemus.Api.Authentication;
using Seemus.Domain.Dtos.Account;
using Seemus.Domain.Dtos.User;
using Seemus.Domain.Entities;
using Seemus.Domain.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace Seemus.Api.Controllers
{
    [Route("api/accounts")]
    public class AccountController : ApplicationController
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenFactory _tokenFactory;

        public AccountController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, ITokenFactory tokenFactory) : base(mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _tokenFactory = tokenFactory;
        }

        [HttpPost("auth"), AllowAnonymous]
        [SwaggerOperation("Endpoint para autenticar um usuário")]
        [ProducesResponseType(typeof(AuthResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AppProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Auth(AuthDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return BadRequest();

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (result.Succeeded)
            {
                var expiration = DateTime.Now.AddDays(7);
                var token = JwtHelper.GenerateToken(
                    user,
                    _configuration["JwtTokenConfig:Key"],
                    _configuration["JwtTokenConfig:Issuer"],
                    _configuration["JwtTokenConfig:Audience"], expiration);

                var refreshToken = _tokenFactory.GenerateRefreshToken();
                user.AddRefreshToken(refreshToken, DateTime.Now.AddDays(30));

                await _userManager.UpdateAsync(user);

                return Ok(new AuthResultDto("Bearer", token, refreshToken, expiration, Mapper.Map<UserDto>(user)));
            }

            return BadRequest("Usuário ou senha inválidos");
        }

        [HttpGet, Authorize]
        [SwaggerOperation("Endpoint para obter os dados do usuário autenticado")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userManager.FindByIdAsync(GetCurrentUserId().ToString());
            if (user is null)
                return NotFound();

            return Ok(Mapper.Map<UserDto>(user));
        }

        [HttpPatch, Authorize]
        [SwaggerOperation("Endpoint para atualizar a senha do usuário autenticado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AppProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeAccountPassword(ChangeAccountPasswordDto dto)
        {
            var user = await _userManager.FindByIdAsync(GetCurrentUserId().ToString());
            if (user is null)
                return NotFound();

            var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        [HttpPatch("profile"), Authorize]
        [SwaggerOperation("Endpoint para alterar o perfil do usuário logado")]
        [ProducesResponseType(typeof(AppProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserProfile(UpdateUserProfileDto dto)
        {
            var user = await _userManager.FindByIdAsync(GetCurrentUserId().ToString());
            if (user is null)
                return NotFound();

            user.UpdateProfile(dto);

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(Mapper.Map<UserDto>(user));
        }

    }
}
