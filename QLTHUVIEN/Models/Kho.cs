using System;
using System.Collections.Generic;

namespace QLTHUVIEN.Models;

public partial class Kho
{
    public string Masach { get; set; } = null!;

    public int? Soluong { get; set; }

    public virtual Sach MasachNavigation { get; set; } = null!;
}
