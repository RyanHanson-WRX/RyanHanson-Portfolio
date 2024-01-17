using HW4.Models;
using System.Globalization;
using System.Net;
using System.Reflection.Metadata;
using System.Text.Json;
using HW4.Utilities;

namespace HW4.Services
{
    class Cast
    {
        public string name { get; set; }
        public string character { get; set; }
    }

    class TmdbMovieCreditsResponse
    {
        public List<Cast> cast { get; set; }
    }
    
    class Genre
    {
        public string name { get; set; }
    }

    class TmdbMovieDetailsResponse
    {
        public string backdrop_path { get; set; }
        public List<Genre> genres { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public string release_date { get; set; }
        public long revenue { get; set; }
        public int runtime { get; set; }
        public string title { get; set; }
    }

    class Result
    {
        public int id { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }
    }

    class TmdbSearchMoviesResponse
    {
        public List<Result> results { get; set; }
    }

    public class TMDBService : ITMDBService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TMDBService> _logger;

        public TMDBService(HttpClient httpClient, ILogger<TMDBService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<Movie>> SearchMoviesAsync(string query)
        {
            string endpoint = $"search/movie?query={query}&include_adult=false&language=en-US&page=1";
            _logger.LogInformation($"Calling TMDB API at {endpoint}");
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
            string responseBody;
            if (response.IsSuccessStatusCode)
            {
                responseBody = await response.Content.ReadAsStringAsync();
            }
            else
            {
                Exception e = HandleResponse(response);
                throw e;
            }
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            try
            {
                TmdbSearchMoviesResponse tmdbResponse = JsonSerializer.Deserialize<TmdbSearchMoviesResponse>(responseBody, options);
                return tmdbResponse.results.Select(result => new Movie
                {
                    Title = result.title,
                    Id = result.id,
                    Description = result.overview,
                    ReleaseDate = FormatMethods.FormatMovieReleaseDate(result.release_date),
                    PosterImagePath = result.poster_path,
                    Popularity = result.popularity
                }).OrderByDescending(movie => movie.Popularity);
            }
            catch (JsonException e)
            {
                _logger.LogError(e, "Error deserializing TMDB search response");
                throw new Exception("Error deserializing TMDB search response");
            }
        }

        public async Task<MovieDetails> GetMovieDetailsAsync(int id) 
        {
            string endpoint = $"movie/{id}?language=en-US";
            _logger.LogInformation($"Calling TMDB API at {endpoint}");
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
            string responseBody;
            if (response.IsSuccessStatusCode)
            {
                responseBody = await response.Content.ReadAsStringAsync();
            }
            else
            {
                Exception e = HandleResponse(response);
                throw e;
            }
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            try
            {
                TmdbMovieDetailsResponse tmdbResponse = JsonSerializer.Deserialize<TmdbMovieDetailsResponse>(responseBody, options);
                return new MovieDetails {
                    Title = tmdbResponse.title,
                    BackgroundImagePath = tmdbResponse.backdrop_path,
                    Description = tmdbResponse.overview,
                    Genres = tmdbResponse.genres.Select(genre => genre.name).ToList(),
                    Popularity = tmdbResponse.popularity,
                    Year = string.IsNullOrEmpty(tmdbResponse.release_date) 
                        ? "N/A" 
                        : DateTime.Parse(tmdbResponse.release_date).Year.ToString(),
                    ReleaseDate = FormatMethods.FormatMovieReleaseDate(tmdbResponse.release_date),
                    Revenue = FormatMethods.FormatMovieRevenue(tmdbResponse.revenue),
                    Runtime = FormatMethods.FormatMovieRuntime(tmdbResponse.runtime)
                };
            }
            catch (JsonException e)
            {
                _logger.LogError(e, "Error deserializing TMDB movie details response");
                throw new Exception("Error deserializing TMDB movie details response");
            }
        }

        public async Task<MovieCredits> GetMovieCreditsAsync(int id) 
        {
            string endpoint = $"movie/{id}/credits?language=en-US";
            _logger.LogInformation($"Calling TMDB API at {endpoint}");
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
            string responseBody;
            if (response.IsSuccessStatusCode)
            {
                responseBody = await response.Content.ReadAsStringAsync();
            }
            else
            {
                Exception e = HandleResponse(response);
                throw e;
            }
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            try 
            {
                TmdbMovieCreditsResponse tmdbResponse = JsonSerializer.Deserialize<TmdbMovieCreditsResponse>(responseBody, options);
                List<string> formattedCast = new List<string>();
                foreach(Cast cast in tmdbResponse.cast) {
                    formattedCast.Add($"{cast.name} as \"{cast.character}\"");
                }
                return new MovieCredits {
                    Cast = formattedCast
                };
            }
            catch (JsonException e) 
            {
                _logger.LogError(e, "Error deserializing TMDB movie credits response");
                throw new Exception("Error deserializing TMDB movie credits response");
            }
        }

        public Exception HandleResponse(HttpResponseMessage response) 
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.ServiceUnavailable: 
                    return new Exception($"Error: {(int)response.StatusCode} - Service unavailable");
                case HttpStatusCode.Conflict: 
                    return new Exception($"Error: {(int)response.StatusCode} - Conflict");
                case HttpStatusCode.BadRequest: 
                    return new Exception($"Error: {(int)response.StatusCode} - Bad request");
                case HttpStatusCode.NotFound: 
                    return new Exception($"Error: {(int)response.StatusCode} - Not found");
                case HttpStatusCode.Unauthorized: 
                    return new Exception($"Error: {(int)response.StatusCode} - Unauthorized");
                case HttpStatusCode.Forbidden:
                    return new Exception($"Error: {(int)response.StatusCode} - Forbidden");
                case HttpStatusCode.InternalServerError: 
                    return new Exception($"Error: {(int)response.StatusCode} - Internal server error");
                default: 
                    return new Exception($"Error: {(int)response.StatusCode} - {response.StatusCode}");
                };
        }
    }
}