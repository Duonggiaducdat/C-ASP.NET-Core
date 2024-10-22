using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QLTHUVIEN.Interfaces;
using QLTHUVIEN.Models;
using System.Security.Claims;
using X.PagedList.Extensions;

namespace QLTHUVIEN.Controllers
{
    [Authorize]

    public class PhieumuonController : Controller
    {
        private IPhieuMuonServices _p;
        private ISachServices _s;
        private readonly QLTVContext _context;
        public PhieumuonController( IPhieuMuonServices p, ISachServices s, QLTVContext context )
        {
            _p = p;
            _s = s;
            _context = context;
        }
        [Route("Phieumuon/index")]
        public IActionResult Index( string search, int page = 1 )
        {
            int pageSize =10;
            IEnumerable<Phieumuon>phieumuons;
            if (!string.IsNullOrEmpty(search))
            {
                phieumuons = _p.Search(search);
            }
            else
            {
                phieumuons = _p.GetAllPhieuMuons();
            }
            var pagedPhieus = phieumuons.ToPagedList(page, pageSize);
            return View(pagedPhieus);
        }

        [HttpGet]
        [Route("phieumuon/create/{id?}")]
        public IActionResult Create( string id )
        {

            var sach = _s.GetById(id);
            if (sach != null)
            {
                var phieuMuon = new Phieumuon
                {
                    Masach = id,
                    Ngaymuon = DateTime.Now,

                };
                var Sachlist = _s.GetAll();
                ViewBag.sach = new SelectList(Sachlist, "Masach", "Tensach");
                return View(phieuMuon);
            }
            else
            {

                var getUserid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var phieuMuon = new Phieumuon
                {
                    Ngaymuon = DateTime.Now
                };
                var Sachlist = _s.GetAll();
                ViewBag.sach = new SelectList(Sachlist, "Masach", "Tensach");
                return View(phieuMuon);
            }



        }

        [HttpPost]
        public IActionResult Create( Phieumuon phieuMuon )
        {
            var getUserid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (getUserid != null)
            {
                phieuMuon.UserId = int.Parse(getUserid);
                //phieuMuon.MasachNavigation = 
                // Kiểm tra ngày trả
                if (phieuMuon.Ngaytra < phieuMuon.Ngaymuon)
                {
                    ModelState.AddModelError("Ngaytra", "Ngày trả không thể trước ngày mượn.");
                }

                // Nếu có lỗi, trả về view với thông tin và lỗi
                if (!ModelState.IsValid)
                {
                    var Sachlist = _s.GetAll();
                    ViewBag.sach = new SelectList(Sachlist, "Masach", "Tensach");
                    return View(phieuMuon);
                }

                // Gọi Service để thực hiện logic nghiệp vụ
                try
                {
                    _p.CreatePhieuMuon(phieuMuon);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    var Sachlist = _s.GetAll();
                    ViewBag.sach = new SelectList(Sachlist, "Masach", "Tensach");
                    return View(phieuMuon);
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Người dùng không tồn tại!";
                return RedirectToAction("Login", "TaiKhoan");
            }
        }


        [HttpGet]
        public IActionResult Edit( int id )

        {

            var Sachlist = _s.GetAll();
            ViewBag.sach = new SelectList(Sachlist, "Masach", "Tensach");

            var phieu = _p.GetPhieuMuonById(id);
            return View(phieu);

        }

        [HttpPost]
        public IActionResult Edit( Phieumuon phieuMuon )
        {
            if (ModelState.IsValid)
            {
                if (phieuMuon.Ngaytra < phieuMuon.Ngaymuon)
                {
                    ModelState.AddModelError("Ngaytra", "Ngày trả không thể trước ngày mượn.");
                    return View(phieuMuon);
                }

                var getUserid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                phieuMuon.UserId = int.Parse(getUserid);
                _p.UpdatePhieuMuon(phieuMuon);
                return RedirectToAction("Index");

            }

            return View(phieuMuon);

        }

        public IActionResult Delete( int id )
        {

            _p.DeletePhieuMuon(id);
            return RedirectToAction("Index");
        }

        public IActionResult ReturnBook( int id )
        {
            _p.ReturnSach(id);
            return RedirectToAction("Index");
        }

    }
}
