using DAL_QLKS;
using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Fakes
{
    public class FakeTrangThaiDatPhongDAL : TrangThaiDatPhongDAL
    {
        private readonly List<TrangThaiDatPhongDTO> _db = new List<TrangThaiDatPhongDTO>();

        public override List<TrangThaiDatPhongDTO> LayDanhSach()
            => _db.ToList();

        public override bool Them(TrangThaiDatPhongDTO dto)
        {
            _db.Add(dto);
            return true;
        }

        public override bool CapNhat(TrangThaiDatPhongDTO dto)
        {
            var old = _db.FirstOrDefault(x => x.TrangThaiID == dto.TrangThaiID);
            if (old == null) return false;

            old.HoaDonThueID = dto.HoaDonThueID;
            old.LoaiTrangThaiID = dto.LoaiTrangThaiID;
            old.NgayCapNhat = dto.NgayCapNhat;

            return true;
        }

        public override bool Xoa(string id)
        {
            return _db.RemoveAll(x => x.TrangThaiID == id) > 0;
        }

        public override List<TrangThaiDatPhongDTO> TimKiemTheoHoaDon(string hoaDonID)
        {
            return _db.Where(x => x.HoaDonThueID.Contains(hoaDonID)).ToList();
        }

        public override string GenerateNewTrangThaiID()
        {
            string prefix = "TTDP";
            int count = _db.Count + 1;
            return $"{prefix}{count:000}";
        }

        public override string GetPhongIDByHoaDonThueID(string hoaDonThueID)
        {
            // Fake → chỉ return "P101" cho test
            return "P101";
        }
    }
}
