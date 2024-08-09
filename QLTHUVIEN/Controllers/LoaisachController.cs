using Microsoft.AspNetCore.Mvc;
using QLTHUVIEN.Interfaces;
using QLTHUVIEN.Models;

namespace QLTHUVIEN.Controllers
{
    public class LoaisachController : Controller
    {
        private readonly ILoaiSachServices _l;
        private readonly ISachServices _s;
        public LoaisachController( ILoaiSachServices l, ISachServices s )
        {
            _l = l;
            _s = s;
        }
        public IActionResult Index()
        {
            var loais = _l.GetAll();
            return View(loais);
        }
        public IActionResult Details( int id )
        {
            var loaisach = _l.GetById( id );
            return View(loaisach);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        private string MaTutang( string code )
        {
            var kitu = "LS";
            int so;
            var sohientai = int.Parse( code.Substring(2));
            so = sohientai + 1;
            return kitu + so.ToString("D2");

        }
        [HttpPost]
        public IActionResult Create( Loaisach loaisach )
        {
            var maxhientai = _l.GetAll()
                                        .OrderByDescending(l => l.Maloai)
                                        .FirstOrDefault()?.Maloai;
            loaisach.Maloai = MaTutang(maxhientai);

            _l.Add(loaisach);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit( int id )
        {
            var loaisach = _l.GetById( id );
            return View(loaisach);
        }
        [HttpPost]
        public IActionResult Edit( Loaisach loaisach )
        {
            _l.Upgrade(loaisach);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete( int id )
        {
            try
            {
                // Kiểm tra xem loại sách có đang được sử dụng không
                bool hasRelatedBooks = _l.IsLoaiSachInUse(id);

                if (hasRelatedBooks)
                {
                    return Json(new { success = false, message = "Loại sách này không thể xóa vì đang được sử dụng trong bảng sách." });
                }

                // Nếu không có liên quan, tiến hành xóa
                _l.Delete(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

    }
}
