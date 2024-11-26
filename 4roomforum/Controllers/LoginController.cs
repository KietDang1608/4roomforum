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
using BCrypt.Net;

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
                    new Claim(ClaimTypes.Name, userDTO.UserName),
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
                TempData["SuccessMessage"] = "Email or Password is incorrect"; 
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

            string hashedPassword= BCrypt.Net.BCrypt.HashPassword(password);
            UserDTO newUser = new UserDTO
            {
                UserName = userName,
                Email = email,
                Password = hashedPassword,
                Avatar = "voi.png",
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
            TempData["SuccessMessage"] = "Tài khoản đã được tạo thành công!";
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
                return Unauthorized(); 
            }

            var userId = int.Parse(userIdClaim.Value); // Chuyển đổi thành int hoặc kiểu dữ liệu phù hợp
            var userProfile = await _userService.GetUserProfile(userId);

            if (userProfile == null)
            {
                return NotFound(); 
            }

            return View(userProfile); 
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(User updatedUser,IFormFile AvatarFile)
        {
            // Lấy thông tin người dùng hiện tại từ Claims
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToAction("SignIn", "Login"); // Điều hướng tới trang đăng nhập nếu không tìm thấy UserId
            }
            if (AvatarFile != null && AvatarFile.Length > 0)
            {
                var uploadsFolder = Path.Combine("wwwroot", "imgs");
                var uniqueFileName = AvatarFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // save file lên sever
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await AvatarFile.CopyToAsync(fileStream);
                }

                updatedUser.Avatar = uniqueFileName;
            }
            
            var userUpdateDto = new UserDTO
            {
                UserId = userId,
                Email = updatedUser.Email,
                UserName = updatedUser.UserName,
                Password=updatedUser.Password,
                JoinDate = updatedUser.JoinDate,
                LastLogin=updatedUser.LastLogin,
                Status = updatedUser.Status,
                Avatar = updatedUser.Avatar
            };

            var isUpdated = await _userService.UpdateUser(userId, userUpdateDto);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, updatedUser.Email),
                new Claim(ClaimTypes.Email, updatedUser.Email),
                new Claim(ClaimTypes.Role, "User"), // Giữ nguyên hoặc lấy từ updatedUser nếu có thay đổi
                new Claim("UserId", userId.ToString())
            };
            
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = true,
            };

            // Đăng nhập lại để làm mới claims
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            TempData["SuccessMessage"] = "Update successfully."; // Gửi thông báo thành công
            return RedirectToAction("Profile", "Login"); 
        }

        [HttpGet]
        public async Task<IActionResult> ChangePass()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null)
            {
                return Unauthorized(); 
            }

            var userId = int.Parse(userIdClaim.Value);
            var userProfile = await _userService.GetUserProfile(userId);

            if (userProfile == null)
            {
                return NotFound(); 
            }

            return View(userProfile);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePass(User updatedUser)
        {
            var currentPassword = Request.Form["CurrentPassword"];
            var newPassword = Request.Form["NewPassword"];
            var confirmPassword = Request.Form["ConfirmPassword"];
            var userIdClaim =User.FindFirst("UserId")?.Value;

            
            if(userIdClaim ==null || !int.TryParse(userIdClaim,out int userId))
            {
                return RedirectToAction("SignIn","Login");
            }
            var user =await _userService.GetUserById(userId);
            string hashedPassword= BCrypt.Net.BCrypt.HashPassword(currentPassword);
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(currentPassword, user.Password);
            if(user ==null || !isPasswordValid )
            {
                TempData["SuccessMessage"] = "Incorrect Password";
                return View();
            }

            if(newPassword !=confirmPassword)
            {
                TempData["SuccessMessage"] = "NewPassword and ConfirmPassword are different";
                return View();
            }
            string newHashedPassword= BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.Password=newHashedPassword;
            var isUpdated= await _userService.UpdateUser(userId,user);
            if (isUpdated)
            {
                TempData["SuccessMessage"] = "Password updated successfully."; // Success message
                return RedirectToAction("Profile", "Login");
            }
            return View();
        }


    }
}
