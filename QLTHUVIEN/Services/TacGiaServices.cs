using Newtonsoft.Json;
using QLTHUVIEN.Interfaces;
using QLTHUVIEN.Models;
namespace QLTHUVIEN.Services
{
    public class TacGiaServices : ITacGiaServices
    {

        private readonly QLTVContext _context;
        public TacGiaServices( QLTVContext context )
        {
            _context = context;
        }
        public IEnumerable<Tacgia> GetAll()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage reponse = httpClient.GetAsync("https://localhost:7159/api/TacGia").Result;
            if (reponse.IsSuccessStatusCode)
            {
                String reponseData = reponse.Content.ReadAsStringAsync().Result;
                IEnumerable<Tacgia> tacgias = JsonConvert.DeserializeObject<IEnumerable<Tacgia>>(reponseData);
                return tacgias;
            }
            return _context.Tacgias.ToList();
        }
        public Tacgia GetById( string id )
        {
            return _context.Tacgias.Find(id);
        }
        public void Add( Tacgia tacgia )
        {
            _context.Tacgias.Add(tacgia);
            _context.SaveChanges();
        }
        public void Update( Tacgia tacgia )
        {
            _context.Tacgias.Update(tacgia);
            _context.SaveChanges();
        }
        public bool IsLoaiSachInUse( string id )
        {
            return _context.Saches.Any(s => s.Matg == id);
        }
        public void Delete( string id )
        {
            var ten = _context.Tacgias.Find(id);
            _context.Tacgias.Remove(ten);
            _context.SaveChanges();

        }
    }
}
