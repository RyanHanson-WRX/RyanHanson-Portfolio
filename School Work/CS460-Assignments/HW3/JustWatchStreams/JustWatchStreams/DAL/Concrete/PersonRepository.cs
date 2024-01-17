using System.Linq;
using Microsoft.EntityFrameworkCore;
using JustWatchStreams.DAL.Abstract;
using JustWatchStreams.Models;
using Microsoft.IdentityModel.Tokens;

namespace JustWatchStreams.DAL.Concrete;

public class PersonRepository : Repository<Person>, IPersonRepository
{
    private DbSet<Person> _people;

    public PersonRepository(JustWatchDbContext context) : base(context)
    {
        _people = context.People;
    }

    public IQueryable<Person> GetAllActors()
    {
        var actors = _people.Select(p => new {Actor = p, Credits = p.Credits.Where(c => c.Role.RoleName == "Actor").Count(), NoCredits = p.Credits.Count()})
                            .Distinct()
                            .Where(n => n.Credits > 0 || n.NoCredits == 0) // NoCredits == 0 is to make sure we get newly created actors who have no credits at all, and to not include directors who would also have a n.Credit equal to 0.
                            .OrderBy(n => n.Actor.FullName)
                            .Select(n => n.Actor);
        return actors;
    }

    public Person FindActorById(int id)
    {
        var actors = _people.Select(p => new {Actor = p, Credits = p.Credits.Where(c => c.Role.RoleName == "Actor").Count(), NoCredits = p.Credits.Count()})
                            .Distinct()
                            .Where(n => n.Credits > 0 || n.NoCredits == 0) // NoCredits == 0 is to make sure we get newly created actors who have no credits at all, and to not include directors who would also have a n.Credit equal to 0.
                            .OrderBy(n => n.Actor.FullName)
                            .Select(n => n.Actor)
                            .ToList();
        var actor = actors.Where(a => a.Id == id)
                            .Select(a => a)
                            .FirstOrDefault();
        if (actor == null)
        {
            return null!;
        }
        return actor;
    }
    public Person FindByJustWatchId(int id)
    {
        var person = _people.Where(p => p.JustWatchPersonId == id)
                            .Select(p => p)
                            .FirstOrDefault();
        return person;
    }
}