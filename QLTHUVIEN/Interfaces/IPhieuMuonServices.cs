using QLTHUVIEN.Models;

namespace QLTHUVIEN.Interfaces
{
    public interface IPhieuMuonServices
    {
        void CreatePhieuMuon( Phieumuon phieuMuon );
        void UpdatePhieuMuon( Phieumuon phieuMuon );
        void DeletePhieuMuon( int phieuMuonId );
        IEnumerable<Phieumuon> GetAllPhieuMuons();
        Phieumuon GetPhieuMuonById( int phieuMuonId );
        void ReturnSach( int maPhieuMuon );
        IEnumerable<Phieumuon> Search( string name );

    }
}
