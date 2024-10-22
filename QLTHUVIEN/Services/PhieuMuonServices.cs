using QLTHUVIEN.Interfaces;
using QLTHUVIEN.Models;

namespace QLTHUVIEN.Services
{
    public class PhieuMuonServices : IPhieuMuonServices
    {
        private readonly QLTVContext _context;

        public PhieuMuonServices( QLTVContext context )
        {

            _context = context;
        }
        public void CreatePhieuMuon( Phieumuon phieuMuon )
        {
            var sach= _context.Saches.SingleOrDefault(s =>s.Masach == phieuMuon.Masach);

            if (sach != null)
            {
                if (sach.SoLuong > 0)
                {

                    sach.SoLuong = sach.SoLuong - 1;
                    phieuMuon.Trangthai = "Đang mượn";

                    _context.Phieumuons.Add(phieuMuon);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Sách đã được mượn hết. Quay lại sau");
                }

            }
        }

        public void UpdatePhieuMuon( Phieumuon phieuMuon )
        {
            _context.Phieumuons.Update(phieuMuon);
            _context.SaveChanges();
        }
        public Phieumuon GetPhieuMuonById( int phieuMuonId )
        {
            var phieu = _context.Phieumuons.Find(phieuMuonId);
            return ( phieu );
        }
        public IEnumerable<Phieumuon> GetAllPhieuMuons()
        {
            var phieus= _context.Phieumuons
                .OrderByDescending(p => p.MaPhieumuon)
                .ToList();
            return ( phieus );
        }
        public void DeletePhieuMuon( int phieuMuonId )
        {

            var phieu = _context.Phieumuons.Find(phieuMuonId) as Phieumuon;
            if (phieu != null)
            {
                _context.Phieumuons.Remove(phieu);
                _context.SaveChanges();
            }
        }
        public void ReturnSach( int maPhieuMuon )
        {
            DateTime now = DateTime.Now;
            var phieu = _context.Phieumuons.Find(maPhieuMuon) as Phieumuon;
            if (now >= phieu.Ngaytra)
            {
                phieu.Trangthai = "Trễ hẹn";
            }
            else
            {
                phieu.Trangthai = "Đã trả";

            }
            var sach = _context.Saches.Find(phieu.Masach);
            sach.SoLuong += 1;
            _context.Phieumuons.Update(phieu);
            _context.Saches.Update(sach);
            _context.SaveChanges();

        }
        public IEnumerable<Phieumuon> Search( string name )
        {
            return _context.Phieumuons
                .Where(p => p.HoTenNguoiMuon.Contains(name) || p.SoDienThoaiNguoiMuon.Contains(name) || p.CmndNguoiMuon.Contains(name)).ToList();

        }
    }
}
