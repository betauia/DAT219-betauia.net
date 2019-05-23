using Microsoft.AspNetCore.Mvc;

namespace betauia.Controllers
{
    public class BetaLanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}