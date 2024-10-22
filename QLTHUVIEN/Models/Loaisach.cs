using System;
using System.Collections.Generic;

namespace QLTHUVIEN.Models;

public partial class Loaisach
{
    public string Tenloaisach { get; set; } = null!;

    public string? Mota { get; set; }

    public int Maloaisach { get; set; }

    public string? MaLoai { get; set; }

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
