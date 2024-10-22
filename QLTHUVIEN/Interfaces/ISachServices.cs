using QLTHUVIEN.Models;
namespace QLTHUVIEN.Interfaces
{
    public interface ISachServices
    {

        IEnumerable<Sach> Search( string name );
        IEnumerable<Sach> GetAll();
        Sach GetById( string id );
        void Add( Sach sach );
        void Update( Sach sach );
        void Delete( string id );
        string GetMaxMaSach();
        IEnumerable<Sach> GetAllSaches();
    }
}