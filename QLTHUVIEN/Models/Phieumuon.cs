using System.ComponentModel.DataAnnotations;

namespace QLTHUVIEN.Models;

public partial class Phieumuon
{
    public int MaPhieumuon { get; set; }

    public string? Masach { get; set; } = null!;

    public DateTime? Ngaymuon { get; set; }

    public DateTime? Ngaytra { get; set; }

    public string? Trangthai { get; set; }

    [Required(ErrorMessage = "Họ tên người mượn là bắt buộc.")]
    [RegularExpression("[A-Za-z\\s]+$", ErrorMessage = "Họ tên không hợp lệ")]
    public string HoTenNguoiMuon { get; set; }
    [Required(ErrorMessage = "CMND người mượn là bắt buộc.")]
    [RegularExpression(@"^\d{9}(\d{3})?$", ErrorMessage = "Số CMND không hợp lệ. CMND phải có 9 hoặc 12 chữ số.")]
    public string CmndNguoiMuon { get; set; }

    public string? DiaChiNguoiMuon { get; set; }

    [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
    [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số không hợp lệ")]
    public string SoDienThoaiNguoiMuon { get; set; }

    public int UserId { get; set; }

    public virtual Sach? MasachNavigation { get; set; } = null!;

    public virtual Taikhoan? User { get; set; }
}
