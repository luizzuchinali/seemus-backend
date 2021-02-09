using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Seemus.Api.Hubs;
using Seemus.Domain.Constants;
using Seemus.Domain.Dtos.User;
using Seemus.Domain.Entities;
using Seemus.Domain.Interfaces.Data;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seemus.Api.Controllers
{
    [Route("api/artists")]
    public class ArtistController : ApplicationController
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHubContext<ArtistHub> _artistHub;

        public ArtistController(IMapper mapper, UserManager<User> userManager, IRepository<Role> roleRepository, IUserRepository userRepository, IHubContext<ArtistHub> artistHub) : base(mapper)
        {
            _userManager = userManager;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _artistHub = artistHub;
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

        [HttpGet, AllowAnonymous]
        [SwaggerOperation("Endpoint para listar artitas ativos")]
        [ProducesResponseType(typeof(IEnumerable<ArtistDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(string search = null, bool online = true) => Ok(await _userRepository.GetAllArtists(search, online));

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
            await _userRepository.UnitOfWork.Commit();

            await _artistHub.Clients.All.SendAsync(WebSocketTopics.ArtistOnline, Mapper.Map<ArtistDto>(artist));
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
            await _userRepository.UnitOfWork.Commit();

            await _artistHub.Clients.All.SendAsync(WebSocketTopics.ArtistOffline, Mapper.Map<ArtistDto>(artist));
            return Ok();
        }
    }
}
