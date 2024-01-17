using HW4.Models;

namespace HW4.Services
{
    public interface ITMDBService
    {
        Task<IEnumerable<Movie>> SearchMoviesAsync(string query);
        Task<MovieDetails> GetMovieDetailsAsync(int id);
        Task<MovieCredits> GetMovieCreditsAsync(int id);
    }
}