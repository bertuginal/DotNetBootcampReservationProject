using Microsoft.AspNetCore.Mvc;

namespace NetBootcampProject.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
