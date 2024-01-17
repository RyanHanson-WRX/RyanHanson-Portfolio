using System.Linq;
using Microsoft.EntityFrameworkCore;
using JustWatchStreams.DAL.Abstract;
using JustWatchStreams.Models;

namespace JustWatchStreams.DAL.Concrete;

public class CreditRepository : Repository<Credit>, ICreditRepository
{
    private DbSet<Credit> _credits;

    public CreditRepository(JustWatchDbContext context) : base(context)
    {
        _credits = context.Credits;
    }

    public (Person Director, List<Show> DirectorShows) DirectorWithMostShowsAndShowList()
    {
        var mostShowDirector = _credits.Where(c => c.Role.RoleName == "Director")
                                        .Select(c => new {MostShowDirector = c.Person, Credits = c.Person.Credits.Where(c => c.Role.RoleName == "Director").Count()})
                                        .Distinct()
                                        .OrderByDescending(n => n.Credits)
                                        .First()
                                        .MostShowDirector;
        var directorShows = _credits.Where(c => c.Role.RoleName == "Director" && c.Person.FullName == mostShowDirector.FullName)
                            .Select(c => c.Show)
                            .OrderBy(s => s.ReleaseYear)
                            .ToList();
        return (mostShowDirector, directorShows);
    }
}