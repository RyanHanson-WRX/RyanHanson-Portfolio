using Microsoft.AspNetCore.Mvc;

namespace JustWatchStreams.Controllers
{
    [Route("admin")]
    public class ManageController : Controller
    {
        [HttpGet("person/create")]
        public IActionResult Index()
        {
            return View();
        }
    }
}