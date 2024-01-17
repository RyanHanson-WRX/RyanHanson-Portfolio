
namespace HW3Project.Controllers {
    public class HomeController : Controller {

        [HttpGet]
        public ViewResult Index() {
            return View();
        }

        [HttpPost]
        public ViewResult Index(StudentResponse studentResponse) {
            if (ModelState.IsValid) {
                Repository.AddResponse(studentResponse);
                return View("Success", studentResponse);
            } else {
                return View();
            }
        }

        public ViewResult Schedule() {
            var sortedRepoResponses = Repository.Responses.SortByClass();
            return View(sortedRepoResponses);
        }
    }
}
