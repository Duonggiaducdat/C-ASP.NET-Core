using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QLTHUVIEN.Interfaces;
using QLTHUVIEN.Models;
using System.Security.Claims;
using X.PagedList.Extensions;


namespace QUANLYTHUVIEN.Controllers
{
    [Authorize]
    public class SachController : Controller
    {
        private readonly ISachServices _s;
        private readonly ILoaiSachServices _l;

        private readonly ITacGiaServices _t;

        public SachController( ISachServices s, ILoaiSachServices l, ITacGiaServices t )
        {
            _s = s;
            _l = l;
            _t = t;

        }
        private void DanhSach()
        {
            var loaiSachs = _l.GetAll().Select(x => new { Value = x.Maloaisach, Text = $"{x.MaLoai} - {x.Tenloaisach}" });
            var tacGias = _t.GetAll().Select(x => new { Value = x.Matg, Text = $"{x.Matg} - {x.Tentg}" });
            ViewBag.MaloaisachList = new SelectList(loaiSachs, "Value", "Text");
            ViewBag.MatgList = new SelectList(tacGias, "Value", "Text");
        }
        private string MaTutang( string code )
        {
            var kitu = "S";
            int so;
            var sohientai = int.Parse( code.Substring(1));
            so = sohientai + 1;
            return kitu + so.ToString("D2");

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
                saches = _s.GetAllSaches();
            }
            var pagedSaches = saches.ToPagedList(page, pageSize);
            return View(pagedSaches);
        }
        public IActionResult Details( string id )
        {

            var sach = _s.GetById(id);
            return View(sach);

        }

        [HttpGet]
        public IActionResult Create()
        {
            DanhSach();
            return View();
        }

        [HttpPost]
        public IActionResult Create( Sach sach )
        {
            if (!ModelState.IsValid)
            {
                return View(sach); // Trả về view cùng với thông tin lỗi
            }
            var maxhientai = _s.GetMaxMaSach();
            DanhSach();
            var getUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            sach.Masach = MaTutang(maxhientai);
            // cách 2 trên UI @Html.HiddenFor(model => model.Masach)
            // Cách 3 Xóa lỗi liên quan đến Masach trong ModelState (nếu có)
            ModelState.Remove("Masach");
            sach.UserId = int.Parse(getUser);

            _s.Add(sach);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Edit( string id )
        {
            DanhSach();
            var sach = _s.GetById(id);
            return View(sach);
        }

        [HttpPost]
        public IActionResult Edit( Sach sach )
        {
            DanhSach();
            _s.Update(sach);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete( string id )
        {
            var sach = _s.GetById(id);
            _s.Delete(id);
            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> GetAllSaches()
        //{
        //    var saches = await _s.GetAllSachesAsync();
        //    return View(saches);
        //}
    }
}