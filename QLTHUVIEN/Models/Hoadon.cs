using System;
using System.Collections.Generic;

namespace QLTHUVIEN.Models;

public partial class Hoadon
{
    public string Mahoadon { get; set; } = null!;

    public string? Tenkhachhang { get; set; }

    public DateTime? Ngaylap { get; set; }

    public decimal? Tongtien { get; set; }
}
