using DAL_QLKS;
using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL_QLKS
{
    public class TrangThaiDatPhongBLL
    {
        private readonly TrangThaiDatPhongDAL dal;

        // ===============================
        // Constructor mặc định (chạy thật)
        // ===============================
        public TrangThaiDatPhongBLL()
        {
            dal = new TrangThaiDatPhongDAL();
        }

        // ===============================
        // Constructor DI (dùng cho FakeDAL)
        // ===============================
        public TrangThaiDatPhongBLL(TrangThaiDatPhongDAL fakeDal)
        {
            dal = fakeDal ?? new TrangThaiDatPhongDAL();
        }

        // ===============================
        // Lấy danh sách
        // ===============================
        public List<TrangThaiDatPhongDTO> LayDanhSach()
        {
            return dal.LayDanhSach();
        }

        // ===============================
        // Thêm
        // ===============================
        public bool Them(TrangThaiDatPhongDTO dto)
        {
            if (dto == null) return false;

            return dal.Them(dto);
        }

        // ===============================
        // Cập nhật
        // ===============================
        public bool CapNhat(TrangThaiDatPhongDTO dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.TrangThaiID))
                return false;

            return dal.CapNhat(dto);
        }

        // ===============================
        // Xóa
        // ===============================
        public bool Xoa(string id)
        {
            if (string.IsNullOrEmpty(id)) return false;

            return dal.Xoa(id);
        }

        // ===============================
        // Tìm theo hóa đơn thuê
        // ===============================
        public List<TrangThaiDatPhongDTO> TimKiemTheoHoaDon(string hoaDonID)
        {
            return dal.TimKiemTheoHoaDon(hoaDonID);
        }

        // ===============================
        // Generate mã
        // ===============================
        public string GenerateNewTrangThaiID()
        {
            return dal.GenerateNewTrangThaiID();
        }

        // ===============================
        // Lấy phòng theo hóa đơn thuê
        // ===============================
        public string GetPhongIDByHoaDonThueID(string hoaDonThueID)
        {
            if (string.IsNullOrEmpty(hoaDonThueID))
                return null;

            return dal.GetPhongIDByHoaDonThueID(hoaDonThueID);
        }

        // ===============================
        // Lấy trạng thái cuối cùng
        // ===============================
        public string GetTrangThaiCuoi(string hoaDonThueID)
        {
            if (string.IsNullOrEmpty(hoaDonThueID))
                return null;

            try
            {
                var ds = LayDanhSach()
                    .Where(x => x.HoaDonThueID == hoaDonThueID)
                    .OrderByDescending(x => x.NgayCapNhat)
                    .FirstOrDefault();

                return ds?.LoaiTrangThaiID;
            }
            catch
            {
                return null;
            }
        }
    }
}
