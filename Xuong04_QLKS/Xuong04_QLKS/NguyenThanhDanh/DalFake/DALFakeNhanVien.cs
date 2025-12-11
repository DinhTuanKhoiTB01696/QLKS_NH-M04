using System;
using System.Collections.Generic;
using System.Linq;
using DAL_QLKS;
using DTO_QLKS;

namespace QLKS.Tests.FakeDAL
{
    public class FakeDALNhanVien : DALNhanVien
    {
        private readonly List<NhanVien> _db = new List<NhanVien>();

        // ==========================
        //       LOGIN
        // ==========================
        public override NhanVien getNhanVien(string email, string password)
        {
            return _db.FirstOrDefault(x =>
                x.Email.Equals(email, StringComparison.OrdinalIgnoreCase)
                && x.MatKhau == password);
        }

        // ==========================
        //      SELECT ALL
        // ==========================
        public override List<NhanVien> selectAll()
        {
            return _db.ToList();
        }

        // ==========================
        //      SELECT BY ID
        // ==========================
        public override NhanVien selectById(string id)
        {
            return _db.FirstOrDefault(x => x.MaNV == id);
        }

        // ==========================
        //      SELECT BY EMAIL
        // ==========================
        public override NhanVien getNhanVienByEmail(string email)
        {
            return _db.FirstOrDefault(x =>
                x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        // ==========================
        //      CHECK EMAIL EXISTS
        // ==========================
        public override bool checkEmailExists(string email)
        {
            return _db.Any(x =>
                x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        // ==========================
        //      INSERT
        // ==========================
        public override void insertNhanVien(NhanVien nv)
        {
            _db.Add(nv);
        }

        // ==========================
        //      UPDATE
        // ==========================
        public override void updateNhanVien(NhanVien nv)
        {
            var old = _db.FirstOrDefault(x => x.MaNV == nv.MaNV);
            if (old == null) return;

            old.HoTen = nv.HoTen;
            old.Email = nv.Email;
            old.MatKhau = nv.MatKhau;
            old.VaiTro = nv.VaiTro;
            old.TinhTrang = nv.TinhTrang;
            old.DiaChi = nv.DiaChi;
            old.GioiTinh = nv.GioiTinh;
        }

        // ==========================
        //      DELETE
        // ==========================
        public override void deleteNhanVien(string maNV)
        {
            _db.RemoveAll(x => x.MaNV == maNV);
        }

        // ==========================
        //      RESET PASSWORD
        // ==========================
        public override void ResetMatKhau(string mk, string email)
        {
            var nv = _db.FirstOrDefault(x =>
                x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (nv != null)
                nv.MatKhau = mk;
        }

        // ==========================
        //      AUTO ID
        // ==========================
        public override string generateMaNhanVien()
        {
            int next = _db.Count + 1;
            return $"NV{next:000}";
        }
    }
}
