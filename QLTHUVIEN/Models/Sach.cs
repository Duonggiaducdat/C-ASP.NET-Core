using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace QLTHUVIEN.Models;

public partial class Sach
{
    //[BindNever] //cách thứ nhất để không nhật trên ui mà ko bị modelstate không bị false
    public string? Masach { get; set; }
    [Required(ErrorMessage = "Nhập tên sách")]
    public string? Tensach { get; set; }
    [Required(ErrorMessage = "Chọn mã tác giả")]
    public string? Matg { get; set; } = null!;

    public string? Tenlinhvuc { get; set; }
    [Required(ErrorMessage = "Chọn loại sách")]
    public int? Maloaisach { get; set; }
    [Required(ErrorMessage = "Nhập giá mua")]
    public int? Giamua { get; set; }
    [Required(ErrorMessage = "Nhập giá bán")]
    public int? Giaban { get; set; }
    public int? Lantaiban { get; set; }
    [Required(ErrorMessage = "nhập")]
    public string? Tennhaxuatban { get; set; }

    public string? Namxuatban { get; set; }

    public int? Giamgia { get; set; }

    public string? HinhAnh { get; set; }
    [Required(ErrorMessage = "nhập")]
    public int SoLuong { get; set; }

    public int? UserId { get; set; }

    public virtual Loaisach? MaloaisachNavigation { get; set; }

    public virtual Tacgia? MatgNavigation { get; set; } = null!;

    public virtual ICollection<Phieumuon> Phieumuons { get; set; } = new List<Phieumuon>();

    public virtual Taikhoan? User { get; set; }

    public class NgayxuatbanBeforeTodayAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid( object value, ValidationContext validationContext )
        {
            if (value is DateTime dateTime && dateTime >= DateTime.Today)
            {
                return new ValidationResult("Ngày xuất bản không hợp lệ");
            }
            return ValidationResult.Success;
        }
    }
}
