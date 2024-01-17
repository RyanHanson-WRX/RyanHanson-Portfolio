using System.Linq;
using Microsoft.EntityFrameworkCore;
using JustWatchStreams.DAL.Abstract;
using JustWatchStreams.Models;

// And the associated implementation, stubbed out for you.
// Put this in folder DAL/Concrete

namespace JustWatchStreams.DAL.Concrete;

public class ShowRepository : Repository<Show>, IShowRepository
{
    private DbSet<Show> _shows;

    public ShowRepository(JustWatchDbContext context) : base(context)
    {
        _shows = context.Shows;
    }

    public (int show, int movie, int tv) NumberOfShowsByType()
    {
        // Use _shows to get what you need.  We purposefully don't have access to other dbSets.
        int show = _shows.Count();
        int movie = _shows.Where(s => s.ShowType.ShowTypeIdentifier == "Movie").Count();
        int tv = _shows.Where(s => s.ShowType.ShowTypeIdentifier == "Show").Count();
        return (show,movie,tv);
    }

    public Show ShowWithHighestTMDBPopularity()
    {
        Show show = _shows.OrderByDescending(s => s.TmdbPopularity).First();
        return show;
    }

    public Show ShowWithMostIMDBVotes()
    {
        Show show = _shows.OrderByDescending(s => s.ImdbVotes).First();
        return show;
    }

    public List<Show> GetAllActorShows(Person actor)
    {
        var shows = _shows.Select(s => new {Show = s, Credits = s.Credits.Where(c => c.Person.Id == actor.Id && c.Role.RoleName == "Actor").Count()})
                          .Distinct()
                          .Where(n => n.Credits > 0)
                          .OrderByDescending(n => n.Credits)
                          .Select(n => n.Show)
                          .ToList();
        return shows;
    }

}