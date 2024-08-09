using Microsoft.EntityFrameworkCore;
using QLTHUVIEN.Interfaces;
using QLTHUVIEN.Models;


namespace QLTHUVIEN.Services
{
    public class SachService : ISachServices
    {
        private readonly QLTVContext _context;

        public SachService( QLTVContext context )
        {
            _context = context;
        }

        public IEnumerable<Sach> GetAll()
        {
            return _context.Saches
                  .Include(x => x.MatgNavigation)
                .Include(x => x.MaloaisachNavigation)
                .ToList();
        }
        public IEnumerable<Sach> Search( string name )
        {
            return _context.Saches
                .Where(s => s.Tensach.Contains(name) || s.MaloaisachNavigation.Maloai.Contains(name) || s.MatgNavigation.Matg.Contains(name))
                .Include(s => s.MaloaisachNavigation)
                .Include(s => s.MatgNavigation).ToList();
        }
        public Sach GetById( string id )
        {
            return _context.Saches
                .Include(x => x.MatgNavigation)
                .Include(x => x.MaloaisachNavigation)
                .FirstOrDefault(x => x.Masach == id);
        }

        public void Add( Sach sach )
        {
            _context.Saches.Add(sach);
            _context.SaveChanges();
        }

        public void Update( Sach sach )
        {
            _context.Saches.Update(sach);
            _context.SaveChanges();
        }

        public void Delete( string id )
        {
            var sach = _context.Saches.Find(id);
            if (sach != null)
            {
                _context.Saches.Remove(sach);
                _context.SaveChanges();
            }
        }
    }
}