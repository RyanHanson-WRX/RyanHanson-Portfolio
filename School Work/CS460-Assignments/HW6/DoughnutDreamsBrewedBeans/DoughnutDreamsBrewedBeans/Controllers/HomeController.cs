using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DoughnutDreamsBrewedBeans.Models;
using DoughnutDreamsBrewedBeans.DAL.Abstract;
using DoughnutDreamsBrewedBeans.ViewModels;

namespace DoughnutDreamsBrewedBeans.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepository<Station> _stationRepo;

    public HomeController(ILogger<HomeController> logger, IRepository<Station> stationRepo)
    {
        _logger = logger;
        _stationRepo = stationRepo;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult CurrentOrders()
    {
        return View();
    }

    public IActionResult Fulfillment()
    {
        try 
        {
            var stations = _stationRepo.GetAll().ToList();
            List<StationViewModel> stationVMs = new List<StationViewModel>();
            foreach (var station in stations)
            {
                stationVMs.Add(new StationViewModel {Id = station.Id, Name = station.Name});
            }
            return View(stationVMs);
        }
        catch
        {
            return View();
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
