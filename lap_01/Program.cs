using System;

namespace QuanLyNganHang
{
    public class Customer
    {
        public string TenKhachHang { get; set; }
        public string DiaChi { get; set; }
        
        private decimal _soTienConLai;
        public decimal SoTienConLai
        {
            get => _soTienConLai;
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Lỗi: Số tiền tài khoản không thể nhỏ hơn 0.");
                }
                else
                {
                    _soTienConLai = value;
                }
            }
        }
        public Customer(string ten, string diaChi, decimal soTienBanDau)
        {
            TenKhachHang = ten;
            DiaChi = diaChi;
            SoTienConLai = soTienBanDau;
        }
        public void GuiTien(decimal soTien)
        {
            if (soTien <= 0)
            {
                Console.WriteLine("Số tiền gửi phải lớn hơn 0.");
                return;
            }
            SoTienConLai += soTien;
            Console.WriteLine($"-> Gửi tiền thành công! Đã nạp thêm: {soTien:N0} VND.");
        }

        public void RutTien(decimal soTien)
        {
            if (soTien <= 0)
            {
                Console.WriteLine("Số tiền rút phải lớn hơn 0.");
                return;
            }
            if (soTien > SoTienConLai)
            {
                Console.WriteLine("Thất bại: Số tiền muốn rút vượt quá số dư hiện tại.");
                return;
            }
            SoTienConLai -= soTien;
            Console.WriteLine($"-> Rút tiền thành công! Đã rút: {soTien:N0} VND.");
        }

        public void ChuyenTien(Customer nguoiNhan, decimal soTien)
        {
            if (soTien <= 0)
            {
                Console.WriteLine("Số tiền chuyển phải lớn hơn 0.");
                return;
            }
            if (soTien > SoTienConLai)
            {
                Console.WriteLine("Thất bại: Số dư tài khoản không đủ để thực hiện chuyển tiền.");
                return;
            }
            this.SoTienConLai -= soTien;
            nguoiNhan.SoTienConLai += soTien;
            Console.WriteLine($"-> Chuyển tiền thành công! Đã chuyển {soTien:N0} VND cho {nguoiNhan.TenKhachHang}.");
        }
    }

    class BankMenu
    {
        private Customer _kh1;
        private Customer _kh2;

        // Truyền dữ liệu khách hàng vào Menu khi khởi tạo
        public BankMenu(Customer khachHang1, Customer khachHang2)
        {
            _kh1 = khachHang1;
            _kh2 = khachHang2;
        }

        public void HienThiMenu()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; 
            while (true)
            {
                Console.WriteLine("\n========= HỆ THỐNG NGÂN HÀNG =========");
                Console.WriteLine("1. Thông tin khách hàng");
                Console.WriteLine("2. Chuyển tiền (Từ KH1 sang KH2)");
                Console.WriteLine("3. Rút tiền (Từ KH1)");
                Console.WriteLine("4. Gửi tiền (Vào KH1)");
                Console.WriteLine("5. Thoát chương trình");
                Console.Write("Vui lòng nhập lựa chọn của bạn (1-5): ");

                if (int.TryParse(Console.ReadLine(), out int luaChon))
                {
                    if (luaChon == 5) 
                    {
                        Console.WriteLine("Cảm ơn bạn đã sử dụng dịch vụ!");
                        break;
                    }
                    XuLyMenu(luaChon);
                }
                else
                {
                    Console.WriteLine("Vui lòng nhập một số nguyên hợp lệ.");
                }
            }
        }

        private void XuLyMenu(int luaChon)
        {
            switch (luaChon)
            {
                case 1:
                    Console.WriteLine("\n--- THÔNG TIN KHÁCH HÀNG ---");
                    Console.WriteLine($"KH1: Tên: {_kh1.TenKhachHang} | Địa chỉ: {_kh1.DiaChi} | Số dư: {_kh1.SoTienConLai:N0} VND");
                    Console.WriteLine($"KH2: Tên: {_kh2.TenKhachHang} | Địa chỉ: {_kh2.DiaChi} | Số dư: {_kh2.SoTienConLai:N0} VND");
                    break;

                case 2:
                    Console.Write("\nNhập số tiền KH1 muốn chuyển cho KH2: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal tienChuyen))
                    {
                        _kh1.ChuyenTien(_kh2, tienChuyen);
                    }
                    else Console.WriteLine("Số tiền không hợp lệ.");
                    break;

                case 3:
                    Console.Write("\nNhập số tiền KH1 muốn rút: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal tienRut))
                    {
                        _kh1.RutTien(tienRut);
                    }
                    else Console.WriteLine("Số tiền không hợp lệ.");
                    break;

                case 4:
                    Console.Write("\nNhập số tiền KH1 muốn gửi: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal tienGui))
                    {
                        _kh1.GuiTien(tienGui);
                    }
                    else Console.WriteLine("Số tiền không hợp lệ.");
                    break;

                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại từ 1 đến 5.");
                    break;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Customer khachHang1 = new Customer("Nguyen Van A", "Ha Noi", 1000000);
            Customer khachHang2 = new Customer("Tran Thi B", "TP.HCM", 500000);

            BankMenu menu = new BankMenu(khachHang1, khachHang2);
            menu.HienThiMenu();
        }
    }
}