using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JustWatchStreams.Models;
using JustWatchStreams.DAL.Abstract;

namespace JustWatchStreams.Controllers
{
    [Route("admin")]
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepo;

        public PersonController(IPersonRepository personRepo)
        {
            _personRepo = personRepo;
        }

        // GET: admin/actor/
        [HttpGet("actor/")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("actor/")]
        public IActionResult Index(int editId)
        {
            if (!_personRepo.GetAll().Any(p => p.JustWatchPersonId == editId))
                {
                    return View(new AdminError { ErrorMessage = "The JustWatchPersonId is invalid." });
                }
            return RedirectToAction("Edit", new { justWatchId = editId });
        }

        // GET: admin/actor/create
        [HttpGet("actor/create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/actor/create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("actor/create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("JustWatchPersonId,FullName")] Person person)
        {
            if (ModelState.IsValid)
            {
                if (_personRepo.GetAll().Any(p => p.JustWatchPersonId == person.JustWatchPersonId && p.Id != person.Id))
                {
                    ModelState.AddModelError("JustWatchPersonId", "The JustWatchPersonId is invalid.");
                    return View(person);
                }
                try
                {
                    person.Id = default(int);
                    _personRepo.AddOrUpdate(person);
                }
                catch(DbUpdateConcurrencyException)
                {
                    ViewBag.Message = "A concurrency error occurred while trying to add a new Actor.  Please try again.";
                    return View(person);
                }
                catch(DbUpdateException)
                {
                    ViewBag.Message = "An unknown database error occurred while trying to add a new Actor.  Please try again.";
                    return View(person);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: admin/actor/edit/5
        [HttpGet("actor/edit/{justWatchId}")]
        public IActionResult Edit(int? justWatchId)
        {
            if (justWatchId == null)
            {
                return NotFound();
            }
            var actor = _personRepo.FindByJustWatchId(justWatchId.Value);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        // POST: admin/actor/edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("actor/edit/{justWatchId}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int justWatchId, [Bind("JustWatchPersonId,FullName")] JustWatchStreams.Models.DTO.Person person)
        {
            if (justWatchId != person.JustWatchPersonId || _personRepo.Exists(person.Id))
            {
                ModelState.AddModelError("JustWatchPersonId", "You cannot change the JustWatchPersonId.");
                return View();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingPerson = _personRepo.FindByJustWatchId(person.JustWatchPersonId);
                    existingPerson.FullName = person.FullName;
                    existingPerson.JustWatchPersonId = person.JustWatchPersonId;
                    _personRepo.AddOrUpdate(existingPerson);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_personRepo.Exists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw new DbUpdateConcurrencyException("A concurrency error occurred while trying to update an Actor.  Please try again.");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }
    }
}
