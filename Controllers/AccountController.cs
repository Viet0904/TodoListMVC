using Microsoft.AspNetCore.Mvc;

namespace TodoListMVC.Controllers
{
    public class AccountController : Controller
    {

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["Title"] = "Login";
            return View();
        }
    }
}
