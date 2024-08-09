namespace QLTHUVIEN.Models.RequestModel
{
    public class CreateSach
    {

        public string? Tensach { get; set; }

        public string Matg { get; set; } = null!;

        public string? Tenlinhvuc { get; set; }

        public int? Maloaisach { get; set; }

        public int? Giamua { get; set; }

        public int? Giaban { get; set; }

        public int? Lantaiban { get; set; }

        public string? Tennhaxuatban { get; set; }

        public String? Namxuatban { get; set; }

        public int? Giamgia { get; set; }
    }
}
