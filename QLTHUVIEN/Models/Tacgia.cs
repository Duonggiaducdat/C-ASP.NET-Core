namespace QLTHUVIEN.Models;

public partial class Tacgia
{
    public string Matg { get; set; } = null!;

    public string Tentg { get; set; }

    public DateOnly? Namsinh { get; set; }

    public DateOnly? Nammat { get; set; }

    public string Quequan { get; set; }

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
