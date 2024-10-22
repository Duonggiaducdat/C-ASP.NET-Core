using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLTHUVIEN.Interfaces;
using QLTHUVIEN.Models;
using System.Diagnostics;
using X.PagedList.Extensions;

namespace QLTHUVIEN.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ISachServices _s;
        private readonly ILoaiSachServices _l;

        private readonly ITacGiaServices _t;
        public HomeController( ISachServices s, ILoaiSachServices l, ITacGiaServices t )
        {
            _s = s;
            _l = l;
            _t = t;
        }

        public IActionResult Index( string search, int page = 1 )
        {
            int pageSize =10;
            IEnumerable<Sach> saches;
            if (!string.IsNullOrEmpty(search))
            {
                saches = _s.Search(search);
            }
            else
            {
                saches = _s.GetAll();
            }
            var pagedSaches = saches.ToPagedList(page, pageSize);
            return View(pagedSaches);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
