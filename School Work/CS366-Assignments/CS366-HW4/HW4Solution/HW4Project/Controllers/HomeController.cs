using System.Diagnostics;

namespace HW4Project.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public ViewResult Index() {
        return View();
    }

    [HttpGet]
    public IActionResult RGBColor(RGBInput rgbcolor) {
        if (ModelState.IsValid) {
                return View("RGBColor", rgbcolor);
            } else if (rgbcolor == null) {
                rgbcolor = new RGBInput();
                return View("RGBColor", rgbcolor);
            } else {
                return View();
            }
    }

}
