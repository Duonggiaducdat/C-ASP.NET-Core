namespace QLTHUVIEN.Models;

public partial class Taikhoan
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Phieumuon> Phieumuons { get; set; } = new List<Phieumuon>();

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
