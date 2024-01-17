using JustWatchStreams.Models;

namespace JustWatchStreams.DAL.Abstract;

public interface ICreditRepository : IRepository<Credit>
{
    /// <summary>
    /// Returns a Person which is the Director with the most shows and a list of the Director's Shows
    /// </summary>
    (Person Director, List<Show> DirectorShows) DirectorWithMostShowsAndShowList();

}