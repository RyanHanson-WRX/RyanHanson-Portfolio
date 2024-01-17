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
using System.Text.RegularExpressions;

namespace JustWatchStreams.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ActorApiController : ControllerBase
    {
        private readonly IPersonRepository _actorRepo;

        public ActorApiController(IPersonRepository actorRepo)
        {
            _actorRepo = actorRepo;
        }

        // GET: api/search/actor/Joe
        [HttpGet("search/actor/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<JustWatchStreams.Models.DTO.Person>))]
        public ActionResult<IEnumerable<JustWatchStreams.Models.DTO.Person>> SearchActors(string name)
        {
            if (name.Contains("$"))
            {
                name = name.Replace("$", "\\$");
            }
            var actors = _actorRepo.GetAllActors()
                                   .Select(a => a.ToPersonDTO())
                                   .ToList();
            var actorMatches = actors.Where(a => Regex.IsMatch(a.FullName, name, RegexOptions.IgnoreCase))
                                     .ToList();
            return actorMatches;
        }

        //GET: api/search/5
        [HttpGet("search/{id}")]
        public ActionResult<JustWatchStreams.Models.DTO.Person> GetPerson(int id)
        {
          if (_actorRepo.GetAllActors() == null)
          {
              return NotFound();
          }
            var actor = _actorRepo.FindById(id).ToPersonDTO();

            if (actor == null)
            {
                return NotFound();
            }

            return actor;
        }
        // PUT: api/actor/0
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("actor/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK),  ProducesResponseType(StatusCodes.Status201Created), ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PutPerson(int id, JustWatchStreams.Models.DTO.Person person)
        {
            if (id != person.Id)
            {
                return Problem(detail: "Invalid ID", statusCode: 400);
            }
            Person personEntity;
            if (person.Id == 0)
            {
                if (_actorRepo.GetAllActors().Any(p => p.JustWatchPersonId == person.JustWatchPersonId))
                {
                    return Problem(detail: "The JustWatchPersonId entered is invalid.", statusCode: 400);
                }
                personEntity = person.ToPerson();
            }
            else
            {
                personEntity = _actorRepo.FindById(person.Id);
                if (personEntity == null)
                {
                    return Problem(detail: "Invalid ID", statusCode: 400);
                }
                if(person.JustWatchPersonId != personEntity.JustWatchPersonId)
                {
                    return Problem(detail: "The JustWatchPersonId entered is invalid.", statusCode: 400);
                }
                personEntity.FullName = person.FullName;
                
            }
            try
            {
                _actorRepo.AddOrUpdate(personEntity);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Problem(detail: "The server experienced a problem.  Please try again.", statusCode: 500);
            }
            catch (DbUpdateException)
            {
                return Problem(detail: "The server experienced a problem.  Please try again.", statusCode: 500);
            }
            if (id == 0)
            {
                return CreatedAtAction("GetPerson", new { id = personEntity.Id }, personEntity);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
