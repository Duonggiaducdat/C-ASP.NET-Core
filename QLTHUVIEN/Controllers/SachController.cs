using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QLTHUVIEN.Interfaces;
using QLTHUVIEN.Models;
using QLTHUVIEN.Models.RequestModel;
using X.PagedList;


namespace QUANLYTHUVIEN.Controllers
{

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
            var loaiSachs = _l.GetAll().Select(x => new { Value = x.Maloai, Text = $"{x.Maloai} - {x.Tenloaisach}" });
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
                saches = _s.GetAll();
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
        public IActionResult Create( CreateSach sachcreate )
        {
            DanhSach();
            if (sachcreate.Giaban < 0)
            {
                ModelState.AddModelError("Giaban", "Giá bán không thể âm.");
            }

            if (sachcreate.Giamua < 0)
            {
                ModelState.AddModelError("Giamua", "Giá mua không thể âm.");
            }

            if (sachcreate.Giamgia < 0)
            {
                ModelState.AddModelError("Giamgia", "Giảm giá không thể âm.");
            }
            //var maxhientai = _s.GetAll()
            //                    .OrderByDescending(s => s.Masach)
            //                    .FirstOrDefault()?.Masach;
            //sach.Masach = MaTutang(maxhientai);
            if (ModelState.IsValid)
            {
                try
                {
                    var maxhientai = _s.GetAll()
                                        .OrderByDescending(s => s.Masach)
                                        .FirstOrDefault()?.Masach;
                    var sach = new Sach()
                    {
                        Giaban = sachcreate.Giaban,
                        Giamgia = sachcreate.Giamgia,
                        Giamua = sachcreate.Giamua,
                        Maloaisach = sachcreate.Maloaisach,
                        Matg = sachcreate.Matg,
                        Lantaiban = sachcreate.Lantaiban,
                        Namxuatban = sachcreate.Namxuatban,
                        Tennhaxuatban = sachcreate.Tennhaxuatban,
                        Tenlinhvuc = sachcreate.Tenlinhvuc,
                        Tensach = sachcreate.Tensach,
                        Masach =MaTutang(maxhientai),
                    };
                    _s.Add(sach);
                    return RedirectToAction("Index"); // Chuyển hướng đến trang danh sách sau khi thêm thành công
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
                }
            }


            // Nếu dữ liệu không hợp lệ, hiển thị lại trang với lỗi xác thực
            return View("Create");
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


    }
}