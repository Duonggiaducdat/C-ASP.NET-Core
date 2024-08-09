using QLTHUVIEN.Interfaces;
using QLTHUVIEN.Models;

namespace QLTHUVIEN.Services
{
    public class LoaiSachServices : ILoaiSachServices
    {
        private readonly QLTVContext _context;
        public LoaiSachServices( QLTVContext context )
        {
            _context = context;
        }
        public IEnumerable<Loaisach> GetAll()
        {
            return _context.Loaisaches.ToList();
        }
        public Loaisach GetById( int id )
        {
            return _context.Loaisaches.Find(id);

        }
        public void Add( Loaisach loaisach )
        {
            _context.Loaisaches.Add(loaisach);
            _context.SaveChanges();
        }
        public void Upgrade( Loaisach loaisach )
        {
            _context.Loaisaches.Update(loaisach);
            _context.SaveChanges();
        }
        public bool IsLoaiSachInUse( int id )
        {
            return _context.Saches.Any(s => s.Maloaisach == id);
        }
        public void Delete( int id )
        {
            var loai = _context.Loaisaches.Find(id);

            _context.Loaisaches.Remove(loai);
            _context.SaveChanges();

        }


    }
}
