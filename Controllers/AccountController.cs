using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using TodoListMVC.Models;
using TodoListMVC.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;

namespace TodoListMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        // Inject Service thông qua Constructor
        public 
            AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // chống tấn công CSRF
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            // 1. Validate dữ liệu đầu vào (Data Annotations)
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 2. Gọi Services để xỷ lý logic kiểm tra
            var user = await _accountService.ValidateUserAsync(model);
            if(user == null)
            {
                ModelState.AddModelError(string.Empty, "Tài khoản hoặc mật khẩu không chính xác.");
                return View(model);
            }

            // 3. Tạo Claims (Claims nơi chứa thông tin định danh người dùng user trong phiên làm việc)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name ?? user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim("UserName",user.UserName),
                new Claim(ClaimTypes.Role,"User") // // Có thể lấy từ bảng Permission nếu cần
            };

            var claimsIndentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe // Ghi nhớ đăng nhập nếu user chọn
            };

            // 4. Ghi Cookie vào trình duyệt
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIndentity),
                authProperties);

            // 5. Điều hướng an toàn (Kiểm tra xem URL có thuộc nội bộ ứng dụng không)
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
