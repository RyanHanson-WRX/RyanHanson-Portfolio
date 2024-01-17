using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JustWatchStreams.Models;
using JustWatchStreams.DAL.Abstract;
using JustWatchStreams.ExtensionMethods;

namespace JustWatchStreams.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ShowApiController : ControllerBase
    {
        private readonly IShowRepository _showRepo;
        private readonly IPersonRepository _personRepo;

        public ShowApiController(IShowRepository showRepo, IPersonRepository personRepo)
        {
            _personRepo = personRepo;
            _showRepo = showRepo;
        }

        // GET: api/actor/shows/5
        [HttpGet("actor/shows/{actorId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<JustWatchStreams.Models.DTO.Show>))]
        public ActionResult<IEnumerable<JustWatchStreams.Models.DTO.Show>> GetShows(int actorId)
        {
            var actor = _personRepo.FindActorById(actorId);
            var shows = _showRepo.GetAllActorShows(actor)
                            .Select(s => s.ToShowDTO())
                            .OrderBy(s => s.ReleaseYear)
                            .ToList();
            return shows;
        }
    }
}
