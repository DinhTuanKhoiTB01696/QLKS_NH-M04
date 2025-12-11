using NUnit.Framework;
using DTO_QLKS;
using BLL_QLKS;
using Tests.Fakes;

namespace NguyenThanhDanh;

public class TrangThaiDatPhongTests
{
    private TrangThaiDatPhongBLL bll;
    private FakeTrangThaiDatPhongDAL fake;

    [SetUp]
    public void Setup()
    {
        fake = new FakeTrangThaiDatPhongDAL();
        bll = new TrangThaiDatPhongBLL(fake); // Thêm constructor dependency
    }

    [Test]
    public void Test_Them_ThanhCong()
    {
        var dto = new TrangThaiDatPhongDTO
        {
            TrangThaiID = "TTDP001",
            HoaDonThueID = "HD001",
            LoaiTrangThaiID = "L1",
            NgayCapNhat = DateTime.Now
        };

        var kq = bll.Them(dto);

        Assert.IsTrue(kq);
        Assert.AreEqual(1, bll.LayDanhSach().Count);
    }

    [Test]
    public void Test_CapNhat_ThanhCong()
    {
        bll.Them(new TrangThaiDatPhongDTO
        {
            TrangThaiID = "TTDP001",
            HoaDonThueID = "HD001",
            LoaiTrangThaiID = "L1"
        });

        var updated = new TrangThaiDatPhongDTO
        {
            TrangThaiID = "TTDP001",
            HoaDonThueID = "HD002",
            LoaiTrangThaiID = "L3"
        };

        var kq = bll.CapNhat(updated);

        Assert.IsTrue(kq);
        Assert.AreEqual("HD002", bll.LayDanhSach()[0].HoaDonThueID);
    }

    [Test]
    public void Test_Xoa_ThanhCong()
    {
        bll.Them(new TrangThaiDatPhongDTO { TrangThaiID = "TTDP001" });

        var kq = bll.Xoa("TTDP001");

        Assert.IsTrue(kq);
        Assert.AreEqual(0, bll.LayDanhSach().Count);
    }

    [Test]
    public void Test_TimKiem_HoaDon()
    {
        bll.Them(new TrangThaiDatPhongDTO { TrangThaiID = "TTDP001", HoaDonThueID = "HD01" });
        bll.Them(new TrangThaiDatPhongDTO { TrangThaiID = "TTDP002", HoaDonThueID = "HD02" });

        var result = bll.TimKiemTheoHoaDon("HD01");

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("TTDP001", result[0].TrangThaiID);
    }

    [Test]
    public void Test_GenerateID_TangDung()
    {
        bll.Them(new TrangThaiDatPhongDTO { TrangThaiID = "TTDP001" });

        string next = bll.GenerateNewTrangThaiID();

        Assert.AreEqual("TTDP002", next);
    }

    [Test]
    public void Test_LayTrangThaiCuoi()
    {
        bll.Them(new TrangThaiDatPhongDTO
        {
            TrangThaiID = "TTDP001",
            HoaDonThueID = "HD01",
            LoaiTrangThaiID = "L1",
            NgayCapNhat = DateTime.Now.AddMinutes(-10)
        });

        bll.Them(new TrangThaiDatPhongDTO
        {
            TrangThaiID = "TTDP002",
            HoaDonThueID = "HD01",
            LoaiTrangThaiID = "L3",
            NgayCapNhat = DateTime.Now
        });

        var last = bll.GetTrangThaiCuoi("HD01");

        Assert.AreEqual("L3", last);
    }
}
