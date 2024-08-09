using Microsoft.AspNetCore.Mvc;
using QLTHUVIEN.Interfaces;
using QLTHUVIEN.Models;

namespace QLTHUVIEN.Controllers
{
    public class TacgiaController : Controller
    {
        private readonly ITacGiaServices _t;
        public TacgiaController( ITacGiaServices t )
        {
            _t = t;
        }
        public IActionResult Index()
        {
            var tacgias = _t.GetAll();
            return View(tacgias);
        }
        public IActionResult Details( string id )
        {
            var tacgia  = _t.GetById( id );
            return View(tacgia);
        }
        [HttpGet]
        private string MaTutang( string code )
        {
            var kitu = "TG";
            int so;
            var sohientai = int.Parse( code.Substring(2));
            so = sohientai + 1;
            return kitu + so.ToString("D2");

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create( Tacgia tacgia )
        {
            var maxhientai = _t.GetAll()
                                        .OrderByDescending(t => t.Matg)
                                        .FirstOrDefault()?.Matg;
            tacgia.Matg = MaTutang(maxhientai);

            _t.Add(tacgia);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit( string id )
        {
            var tacgia=_t.GetById(id);
            return View(tacgia);
        }
        [HttpPost]
        public IActionResult Edit( Tacgia tacgia )
        {
            _t.Update(tacgia);
            return View();
        }

        [HttpPost]
        public IActionResult Delete( string id )
        {
            try
            {
                // Kiểm tra xem loại sách có đang được sử dụng không
                bool hasRelatedBooks = _t.IsLoaiSachInUse(id);

                if (hasRelatedBooks)
                {
                    return Json(new { success = false, message = "Tác giả này không thể xóa vì đang được sử dụng trong bảng sách." });
                }

                // Nếu không có liên quan, tiến hành xóa
                _t.Delete(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

    }
}
