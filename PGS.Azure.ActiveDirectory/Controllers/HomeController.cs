using Microsoft.AspNetCore.Mvc;

namespace PGS.Azure.ActiveDirectory.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
