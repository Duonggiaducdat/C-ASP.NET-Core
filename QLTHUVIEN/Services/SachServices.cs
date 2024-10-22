using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        public IEnumerable<Sach> GetAllSaches()
        {
            HttpClient client =  new HttpClient();
            HttpResponseMessage reponse = client.GetAsync("https://localhost:7159/api/Sach").Result;
            if (reponse.IsSuccessStatusCode)
            {
                string reponseData = reponse.Content.ReadAsStringAsync().Result;
                IEnumerable<Sach> saches = JsonConvert.DeserializeObject<IEnumerable<Sach>>(reponseData);
                return saches;
            }
            else
            {
                return new List<Sach>();
            }
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
                .Where(s => s.Tensach.Contains(name) || s.MaloaisachNavigation.MaLoai.Contains(name) || s.MatgNavigation.Tentg.Contains(name))
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
        public string GetMaxMaSach()
        {
            return _context.Saches
                .OrderByDescending(s => s.Masach)
                .Select(s => s.Masach)
                .FirstOrDefault();
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