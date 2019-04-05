using Microsoft.AspNetCore.Mvc;

namespace betauia.Controllers
{
    public class AboutController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        
    }
    
}