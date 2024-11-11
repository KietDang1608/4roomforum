using System.Runtime.ExceptionServices;
using System.Security.Claims;
using _4roomforum.Models;
using _4roomforum.DTOs;
using _4roomforum.Services.Implements;
using _4roomforum.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace _4roomforum.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

    // Constructor nhận IUserService từ Dependency Injection
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: LoginController
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(User user)
        {

            if (user.Email == "admin" && user.Password == "admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            var userDTO = await _userService.Login(user.Email, user.Password);
            if (userDTO != null) // Đăng nhập thành công
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userDTO.Email),
                    new Claim(ClaimTypes.Email, userDTO.Email),
                    new Claim(ClaimTypes.Role, "User"),// Hoặc lấy từ userDTO.Role nếu có
                    new Claim("UserId", userDTO.UserId.ToString()) 
                };
                
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return RedirectToAction("Index", "Home");
            }
            else // Đăng nhập thất bại
            {
                ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không đúng.");
                return View("SignIn"); // Trả về lại trang đăng nhập với lỗi
            }
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(string userName, string email, string password, string confirmPassword)
        {
            if(password != confirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Mật khẩu không trùng khớp");
                return View();
            }
            UserDTO newUser = new UserDTO
            {
                UserName = userName,
                Email = email,
                Password = password,
                Avatar = "",
                RoleId = 2,
                JoinDate = DateOnly.FromDateTime(DateTime.Now),
                LastLogin = DateOnly.FromDateTime(DateTime.Now),
                Status = 1
            };
            var result = await _userService.RegisterUserAsync(newUser);
            if (result == null)
            {
                
                ModelState.AddModelError(string.Empty, "Email đã được sử dụng");
                return View();
            }
            TempData["SignUpSuccess"] = "Tài khoản đã được tạo thành công!";
            return RedirectToAction("SignIn");
        }
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            // Lấy UserId từ claims
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null)
            {
                return Unauthorized(); // Hoặc xử lý lỗi theo ý bạn
            }

            var userId = int.Parse(userIdClaim.Value); // Chuyển đổi thành int hoặc kiểu dữ liệu phù hợp
            var userProfile = await _userService.GetUserProfile(userId);

            if (userProfile == null)
            {
                return NotFound(); // Hoặc xử lý theo cách bạn muốn
            }

            return View(userProfile); // Trả về view với thông tin người dùng
        }

    }
}
