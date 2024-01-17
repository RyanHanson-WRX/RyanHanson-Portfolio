using System.Diagnostics;

namespace FinalProject.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public ViewResult Index() {
        return View();
    }

    // Server-side Dynamic Page using GET
    [HttpGet]
    public IActionResult AddCompany(Company company) {
      // This may look weird, however it is to bypass field validation when the view is first opened
      if (company.Name == " "){
        company = new Company();
        ModelState.Remove("Name");
        ModelState.Remove("Address");
        ModelState.Remove("OpenTime");
        ModelState.Remove("CloseTime");
        ModelState.Remove("DaysOpen");
        return View("AddCompany", company);
      } else if (ModelState.IsValid) {
            int matchCounter = 0;
            foreach (Company c in Repository.Companies){
              if (company.Name == c.Name){
                matchCounter += 1;
              } else {
                continue;
              }
            }
            if (matchCounter == 0) {
              Repository.AddCompany(company);
              return View("AddCompany", company);
            } else {
              return View("AddCompany", company);
            }
      } else {
            return View();
      }
    }
}
