using QLTHUVIEN.Models;
namespace QLTHUVIEN.Interfaces
{
    public interface ILoaiSachServices
    {
        IEnumerable<Loaisach> GetAll();
        Loaisach GetById( int id );
        void Add( Loaisach loaisach );
        void Upgrade( Loaisach loaisach );
        void Delete( int id );
        bool IsLoaiSachInUse( int id );



    }
}
