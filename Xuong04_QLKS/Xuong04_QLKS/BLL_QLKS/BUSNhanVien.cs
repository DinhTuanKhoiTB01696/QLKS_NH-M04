using System;
using System.Collections.Generic;
using DAL_QLKS;
using DTO_QLKS;

namespace BLL_QLKS
{
    public class BUSNhanVien
    {
        private DALNhanVien dalNhanVien;

        // ⭐ Constructor mặc định (chạy thật)
        public BUSNhanVien()
        {
            dalNhanVien = new DALNhanVien();
        }

        // ⭐ Constructor DI dùng cho Unit Test (FakeDAL)
        public BUSNhanVien(DALNhanVien fakeDal)
        {
            dalNhanVien = fakeDal;
        }

        // ==========================
        //      CHỨC NĂNG LOGIN
        // ==========================
        public NhanVien DangNhap(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            return dalNhanVien.getNhanVien(username.Trim(), password.Trim());
        }

        // ==========================
        //      SELECT ALL
        // ==========================
        public List<NhanVien> GetNhanVienList()
        {
            return dalNhanVien.selectAll();
        }

        // alias cho dễ dùng
        public List<NhanVien> GetAll()
        {
            return dalNhanVien.selectAll();
        }

        // ==========================
        //      RESET PASSWORD
        // ==========================
        public bool ResetMatKhau(string email, string mk)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(mk))
                    return false;

                dalNhanVien.ResetMatKhau(mk, email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // ==========================
        //      INSERT
        // ==========================
        public string InsertNhanVien(NhanVien nv)
        {
            try
            {
                nv.MaNV = dalNhanVien.generateMaNhanVien();
                if (string.IsNullOrEmpty(nv.MaNV))
                    return "Mã nhân viên không hợp lệ.";

                if (dalNhanVien.checkEmailExists(nv.Email))
                    return "Email đã tồn tại.";

                dalNhanVien.insertNhanVien(nv);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        // ==========================
        //      UPDATE
        // ==========================
        public string UpdateNhanVien(NhanVien nv)
        {
            try
            {
                if (string.IsNullOrEmpty(nv.MaNV))
                    return "Mã nhân viên không hợp lệ.";

                dalNhanVien.updateNhanVien(nv);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        // ==========================
        //      DELETE
        // ==========================
        public string DeleteNhanVien(string maNV)
        {
            try
            {
                if (string.IsNullOrEmpty(maNV))
                    return "Mã nhân viên không hợp lệ.";

                dalNhanVien.deleteNhanVien(maNV);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        // ==========================
        //      SELECT BY EMAIL
        // ==========================
        public NhanVien GetNhanVienByEmail(string email)
        {
            return dalNhanVien.getNhanVienByEmail(email);
        }
    }
}
