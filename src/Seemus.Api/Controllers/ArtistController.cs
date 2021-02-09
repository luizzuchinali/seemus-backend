using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Seemus.Domain.Constants;
using Seemus.Domain.Dtos.User;
using Seemus.Domain.Entities;
using Seemus.Domain.Interfaces.Data;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Seemus.Api.Controllers
{
    [Route("api/artists")]
    public class ArtistController : ApplicationController
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IUserRepository _userRepository;
        public ArtistController(IMapper mapper, UserManager<User> userManager, IRepository<Role> roleRepository, IUserRepository userRepository) : base(mapper)
        {
            _userManager = userManager;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        [HttpPost, AllowAnonymous]
        [SwaggerOperation("Endpoint para registrar um artista")]
        [ProducesResponseType(typeof(AppProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Register(RegisterArtistDto dto)
        {
            var artistRole = await _roleRepository.Get(x => x.Name.Equals(Roles.Artist));
            var artist = new User(dto.Name, dto.Email);
            artist.AddRole(artistRole);
            artist.CreateArtistData();

            var result = await _userManager.CreateAsync(artist, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(Mapper.Map<UserDto>(artist));
        }

        [HttpPatch("online"), Authorize(Roles = Roles.Artist)]
        [SwaggerOperation("Endpoint para um artista modificar o status para online")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatusToOnline()
        {
            var artist = await _userRepository.GetArtistByUserId(GetCurrentUserId());
            if (artist is null)
                return NotFound();

            artist.ChangeOnlineStatus(true);

            //TODO: Implementar propagação para o hub de online/offline listagem

            await _userRepository.UnitOfWork.Commit();
            return Ok();
        }


        [HttpPatch("offline"), Authorize(Roles = Roles.Artist)]
        [SwaggerOperation("Endpoint para um artista modificar o status para offline")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatusToOffline()
        {
            var artist = await _userRepository.GetArtistByUserId(GetCurrentUserId());
            if (artist is null)
                return NotFound();

            artist.ChangeOnlineStatus(false);

            //TODO: Implementar propagação para o hub de online/offline listagem

            await _userRepository.UnitOfWork.Commit();
            return Ok();
        }
    }
}
