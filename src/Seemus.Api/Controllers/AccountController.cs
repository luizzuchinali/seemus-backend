﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Seemus.Api.Authentication;
using Seemus.Api.Dtos.Account;
using Seemus.Api.Dtos.User;
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

				return Ok(new AuthResultDto("Bearer", token, refreshToken, expiration, Mapper.Map<UserDto>(user)));
			}

			return BadRequest("Usuário ou senha inválidos");
		}

		[HttpGet, Authorize]
		[SwaggerOperation("Endpoint para obter os dados do token enviado")]
		[ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetCurrentUser()
		{
			var user = await _userManager.FindByIdAsync(GetCurrentUserId().ToString());
			if (user is null)
				return NotFound();

			return Ok(Mapper.Map<UserDto>(user));
		}

	}
}
