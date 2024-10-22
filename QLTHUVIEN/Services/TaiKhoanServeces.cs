using QLTHUVIEN.Interfaces;
using QLTHUVIEN.Models;

namespace QLTHUVIEN.Services
{
    public class TaiKhoanServeces : ITaiKhoanServiecs
    {
        private readonly QLTVContext _context;
        public TaiKhoanServeces( QLTVContext context )
        {
            _context = context;
        }

        public void Add( Taikhoan taikhoan )
        {
            _context.Add(taikhoan);
            _context.SaveChanges();
        }
    }
}
