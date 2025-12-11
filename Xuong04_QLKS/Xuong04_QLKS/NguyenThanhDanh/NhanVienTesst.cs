using NUnit.Framework;
using DTO_QLKS;
using QLKS.Tests.FakeDAL;
using BLL_QLKS;

namespace NguyenThanhDanh
{
    [TestFixture]
    public class NhanVienTests
    {
        private FakeDALNhanVien _fakeDal;
        private BUSNhanVien _bus;

        [SetUp]
        public void Setup()
        {
            _fakeDal = new FakeDALNhanVien();
            _bus = new BUSNhanVien(_fakeDal);

            // Fake dữ liệu
            _fakeDal.insertNhanVien(new NhanVien
            {
                MaNV = "NV001",
                HoTen = "Nguyen Van A",
                GioiTinh = "Nam",
                Email = "a@gmail.com",
                MatKhau = "123",
                VaiTro = "Admin",
                DiaChi = "HN",
                TinhTrang = true
            });
        }

        // ================================
        //   LOGIN TESTS
        // ================================
        [Test]
        public void Login_ThanhCong()
        {
            var result = _bus.DangNhap("a@gmail.com", "123");
            Assert.IsNotNull(result);
        }

        [Test]
        public void Login_SaiPassword()
        {
            var result = _bus.DangNhap("a@gmail.com", "xxx");
            Assert.IsNull(result);
        }

        [Test]
        public void Login_EmailNull()
        {
            var result = _bus.DangNhap(null, "123");
            Assert.IsNull(result);
        }

        [Test]
        public void Login_PasswordNull()
        {
            var result = _bus.DangNhap("a@gmail.com", null);
            Assert.IsNull(result);
        }

        [Test]
        public void Login_EmailSai()
        {
            var result = _bus.DangNhap("khongco@gmail.com", "123");
            Assert.IsNull(result);
        }

        // ================================
        //   INSERT TEST
        // ================================
        [Test]
        public void Insert_ThanhCong()
        {
            var nv = new NhanVien
            {
                HoTen = "Test",
                GioiTinh = "Nam",
                Email = "test@gmail.com",
                MatKhau = "111",
                VaiTro = "User",
                DiaChi = "SG",
                TinhTrang = true
            };

            string msg = _bus.InsertNhanVien(nv);

            Assert.IsEmpty(msg);
            Assert.AreEqual(2, _fakeDal.selectAll().Count);
        }

        [Test]
        public void Insert_EmailTrung()
        {
            var nv = new NhanVien
            {
                Email = "a@gmail.com",
                HoTen = "XX",
                MatKhau = "111"
            };

            string msg = _bus.InsertNhanVien(nv);

            Assert.AreEqual("Email đã tồn tại.", msg);
        }

        // ================================
        //   UPDATE TEST
        // ================================
        [Test]
        public void Update_ThanhCong()
        {
            var nv = _fakeDal.selectById("NV001");
            nv.HoTen = "Ten Moi";

            string msg = _bus.UpdateNhanVien(nv);

            Assert.IsEmpty(msg);
            Assert.AreEqual("Ten Moi", _fakeDal.selectById("NV001").HoTen);
        }

        // ================================
        //   DELETE TEST
        // ================================
        [Test]
        public void Delete_ThanhCong()
        {
            string msg = _bus.DeleteNhanVien("NV001");

            Assert.IsEmpty(msg);
            Assert.AreEqual(0, _fakeDal.selectAll().Count);
        }

        // ================================
        //   RESET PASSWORD
        // ================================
        [Test]
        public void ResetMatKhau_ThanhCong()
        {
            bool ok = _bus.ResetMatKhau("a@gmail.com", "999");

            Assert.IsTrue(ok);
            Assert.AreEqual("999", _fakeDal.getNhanVienByEmail("a@gmail.com").MatKhau);
        }
    }
}