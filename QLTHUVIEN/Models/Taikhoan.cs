using System;
using System.Collections.Generic;

namespace QLTHUVIEN.Models;

public partial class Taikhoan
{
    public string Username { get; set; } = null!;

    public string? PassWord { get; set; }

    public int? Role { get; set; }
}
