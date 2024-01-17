using System.Diagnostics;

// Project has a unique controller, with a defined model and view

namespace FinalProject.Controllers;

public class ReviewController : Controller
{
    [HttpGet]
    public ViewResult AddReview() {
        return View("AddReview");
    }

    // Server-side Dynamic Page using POST
    [HttpPost]
    public IActionResult AddReview(Review review) {
        if (ModelState.IsValid){
            Repository.AddReview(review);
            Repository.CalculateStars();
            return View("AddReview", review);
        } else {
            return View();
        }
    }

    [HttpGet]
    public IActionResult AllReviews(Repository r) {
        return View("AllReviews", r);
    }

}
