using System;
using System.Collections.Generic;

namespace QLTHUVIEN.Models;

public partial class Chitiethoadon
{
    public string Mahoadon { get; set; } = null!;

    public string? Masach { get; set; }

    public int? Soluong { get; set; }

    public int? Giatien { get; set; }

    public int? Thanhtien { get; set; }

    public virtual Hoadon MahoadonNavigation { get; set; } = null!;

    public virtual Sach? MasachNavigation { get; set; }
}
