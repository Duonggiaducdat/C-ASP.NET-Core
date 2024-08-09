using QLTHUVIEN.Models;

namespace QLTHUVIEN.Interfaces
{
    public interface ITacGiaServices
    {
        IEnumerable<Tacgia> GetAll();
        Tacgia GetById( String id );
        void Add( Tacgia tacgia );
        void Update( Tacgia tacgia );
        void Delete( String id );
        bool IsLoaiSachInUse( string id );
    }
}
