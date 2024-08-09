namespace QLTHUVIEN.Models;

public partial class Sach
{
    public string Masach { get; set; } = null!;

    public string? Tensach { get; set; }

    public string Matg { get; set; } = null!;

    public string? Tenlinhvuc { get; set; }

    public int? Maloaisach { get; set; }

    public int? Giamua { get; set; }

    public int? Giaban { get; set; }

    public int? Lantaiban { get; set; }

    public string? Tennhaxuatban { get; set; }

    public string? Namxuatban { get; set; }

    public int? Giamgia { get; set; }

    public byte[]? HinhAnh { get; set; }

    public virtual Kho? Kho { get; set; }

    public virtual Loaisach? MaloaisachNavigation { get; set; }

    public virtual Tacgia MatgNavigation { get; set; } = null!;
}
