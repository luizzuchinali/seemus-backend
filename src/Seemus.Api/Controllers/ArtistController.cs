using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Seemus.Api.Dtos.User;
using Seemus.Domain.Constants;
using Seemus.Domain.Entities;
using Seemus.Domain.Interfaces.Data;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seemus.Api.Controllers
{
	[Route("api/artists")]
	public class ArtistController : ApplicationController
	{
		private readonly UserManager<User> _userManager;
		private readonly IRepository<Role> _roleRepository;
		public ArtistController(IMapper mapper, UserManager<User> userManager, IRepository<Role> roleRepository) : base(mapper)
		{
			_userManager = userManager;
			_roleRepository = roleRepository;
		}

		[HttpPost]
		[AllowAnonymous]
		[SwaggerOperation("Endpoint para registrar um artista")]
		[ProducesResponseType(typeof(AppProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Register(RegisterArtistDto dto)
		{
			var artistRole = await _roleRepository.Get(x => x.Name.Equals(Roles.Artist));
			var artist = new User(dto.Name, dto.Email);
			artist.AddRole(artistRole);
			var result = await _userManager.CreateAsync(artist, dto.Password);
			if (!result.Succeeded)
				return BadRequest(result.Errors);

			return Ok(Mapper.Map<UserDto>(artist));
		}
	}
}
