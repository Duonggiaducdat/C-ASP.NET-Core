using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLTHUVIEN.Interfaces;
using QLTHUVIEN.Models;
using System.Security.Claims;

public class TaiKhoan : Controller
{
    private readonly QLTVContext _context;
    private readonly ITaiKhoanServiecs _tk;

    public TaiKhoan( QLTVContext context, ITaiKhoanServiecs tk )
    {
        _tk = tk;
        _context = context;
    }

    [HttpGet]// Thử thêm filter trực tiếp lên action
    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated)
            return Redirect("/");

        return View();
    }

    [HttpPost]
    public IActionResult Login( string username, string password )
    {

        // Tìm user trong cơ sở dữ liệu
        var user = _context.Taikhoans
            .FirstOrDefault(u => u.Username == username && u.Password == password);

        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("Index", "Home"); // Điều hướng tới trang chính sau khi đăng nhập
        }

        // Đăng nhập thất bại
        ViewBag.Error = "Invalid credentials!";
        return View();
    }
    [Authorize]
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Register( Taikhoan taikhoan )
    {
        var existingUser = GetByUsername(taikhoan.Username);
        if (existingUser != null)
        {
            // Nếu đã tồn tại, thêm thông báo lỗi vào ViewBag và trả về View
            ViewBag.Error = "Tên đăng nhập bị trùng";
            return View();
        }
        taikhoan.Role = "User";
        _tk.Add(taikhoan);
        return RedirectToAction("Login");

    }
    [HttpPost]
    public IActionResult Logout()
    {
        // Xóa session
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        // Chuyển hướng về trang đăng nhập
        return RedirectToAction("Login", "TaiKhoan");
    }
    public Taikhoan GetByUsername( string username )
    {
        return _context.Taikhoans.SingleOrDefault(u => u.Username == username);
    }
}

//using Microsoft.AspNetCore.Mvc;
//using QLTHUVIEN.Models;
//using QLTHUVIEN.Models.RequestModel;

//namespace QLTHUVIEN.Controllers
//{
//    public class TaiKhoanController : Controller
//    {
//        private readonly QLTVContext _context;

//        public TaiKhoanController( QLTVContext context )
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public IActionResult Login()
//        {
//            // Lấy thông báo chuyển hướng từ session, nếu có
//            var redirectMessage = HttpContext.Session.GetString("RedirectMessage");
//            if (!string.IsNullOrEmpty(redirectMessage))
//            {
//                TempData["RedirectMessage"] = redirectMessage;
//                HttpContext.Session.Remove("RedirectMessage"); // Xóa thông báo sau khi lấy
//            }

//            return View();
//        }

//        [HttpPost]
//        public IActionResult Login( User model )
//        {
//            // Kiểm tra tính hợp lệ của dữ liệu nhập vào
//            if (!ModelState.IsValid)
//            {
//                return View(model);
//            }

//            // Tìm user trong cơ sở dữ liệu
//            var user = _context.Taikhoans
//                .FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

//            if (user != null)
//            {
//                // Đăng nhập thành công
//                // Lưu thông tin đăng nhập vào session
//                HttpContext.Session.SetString("Username", user.Username);

//                // Xử lý thông báo chuyển hướng nếu có
//                var redirectMessage = HttpContext.Session.GetString("RedirectMessage");
//                if (!string.IsNullOrEmpty(redirectMessage))
//                {
//                    TempData["RedirectMessage"] = redirectMessage;
//                    HttpContext.Session.Remove("RedirectMessage"); // Xóa thông báo sau khi lấy
//                }

//                return RedirectToAction("Index", "Home"); // Điều hướng tới trang chính sau khi đăng nhập
//            }

//            // Đăng nhập thất bại
//            ModelState.AddModelError(string.Empty, "Invalid credentials!");
//            return View(model);
//        }
//    }
//}
