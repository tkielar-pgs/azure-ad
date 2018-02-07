using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PGS.Azure.ActiveDirectory.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        [Authorize]
        public IActionResult Profile() => View(User.Claims);

        public IActionResult SignIn() => Challenge();

        public IActionResult SignOut() => SignOut(new AuthenticationProperties());
    }
}
