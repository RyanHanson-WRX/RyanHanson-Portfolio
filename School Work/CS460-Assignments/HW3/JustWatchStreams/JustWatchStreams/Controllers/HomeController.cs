using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JustWatchStreams.Models;
using JustWatchStreams.DAL.Abstract;
using JustWatchStreams.ViewModels;

namespace JustWatchStreams.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IShowRepository _showRepository;
    private readonly IRepository<Genre> _genreRepository;
    private readonly ICreditRepository _creditRepository;

    public HomeController(ILogger<HomeController> logger, IShowRepository showRepository, IRepository<Genre> genreRepository, ICreditRepository creditRepository)
    {
        _logger = logger;
        _showRepository = showRepository;
        _genreRepository = genreRepository;
        _creditRepository = creditRepository;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Info()
    {
        return View(new InfoVM
        {
            NumberOfShows = _showRepository.NumberOfShowsByType().show,
            NumberOfMovies = _showRepository.NumberOfShowsByType().movie,
            NumberOfTVShows = _showRepository.NumberOfShowsByType().tv,
            ShowWithHighestTMDBPopularity = _showRepository.ShowWithHighestTMDBPopularity(),
            ShowWithMostIMDBVotes = _showRepository.ShowWithMostIMDBVotes(),
            Genres = _genreRepository.GetAll().OrderBy(x => x.GenreString).Select(g => g.GenreString).ToList(),
            Director = _creditRepository.DirectorWithMostShowsAndShowList().Director,
            DirectorShows = _creditRepository.DirectorWithMostShowsAndShowList().DirectorShows
        });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
