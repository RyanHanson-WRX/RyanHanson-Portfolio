using JustWatchStreams.Models;

namespace JustWatchStreams.ViewModels
{
    public class InfoVM
    {
        public int NumberOfShows { get; set; }
        public int NumberOfMovies { get; set; }
        public int NumberOfTVShows { get; set; }
        public Show ShowWithHighestTMDBPopularity { get; set; }
        public Show ShowWithMostIMDBVotes { get; set; }
        public List<string> Genres { get; set; } = new List<string>();
        public Person Director { get; set; }
        public List<Show> DirectorShows { get; set; } = new List<Show>();
    }
}