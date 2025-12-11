using System;
using System.Collections.Generic;
using DTO_QLKS;

namespace DAL_QLKS
{
    public class TrangThaiDatPhongDAL
    {
        public virtual List<TrangThaiDatPhongDTO> LayDanhSach()
        {
            string sql = @"
                SELECT 
                    ttdp.TrangThaiID,
                    ttdp.HoaDonThueID,
                    ttdp.LoaiTrangThaiID,
                    lttdp.TenTrangThai,
                    ttdp.NgayCapNhat
                FROM TrangThaiDatPhong ttdp
                JOIN LoaiTrangThaiDatPhong lttdp 
                    ON ttdp.LoaiTrangThaiID = lttdp.LoaiTrangThaiID";

            return DBUtil.Select<TrangThaiDatPhongDTO>(sql, null);
        }

        public virtual bool Them(TrangThaiDatPhongDTO dto)
        {
            string query = @"
                INSERT INTO TrangThaiDatPhong 
                (TrangThaiID, HoaDonThueID, LoaiTrangThaiID, NgayCapNhat)
                VALUES (@TrangThaiID, @HoaDonThueID, @LoaiTrangThaiID, @NgayCapNhat)";

            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "@TrangThaiID", dto.TrangThaiID },
                    { "@HoaDonThueID", dto.HoaDonThueID },
                    { "@LoaiTrangThaiID", dto.LoaiTrangThaiID },
                    { "@NgayCapNhat", dto.NgayCapNhat }
                };

                DBUtil.Update(query, parameters);
                return true;
            }
            catch { return false; }
        }

        public virtual bool CapNhat(TrangThaiDatPhongDTO dto)
        {
            string query = @"
                UPDATE TrangThaiDatPhong 
                SET 
                    HoaDonThueID = @HoaDonThueID,
                    LoaiTrangThaiID = @LoaiTrangThaiID,
                    NgayCapNhat = @NgayCapNhat
                WHERE TrangThaiID = @TrangThaiID";

            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "@HoaDonThueID", dto.HoaDonThueID },
                    { "@LoaiTrangThaiID", dto.LoaiTrangThaiID },
                    { "@NgayCapNhat", dto.NgayCapNhat },
                    { "@TrangThaiID", dto.TrangThaiID }
                };

                DBUtil.Update(query, parameters);
                return true;
            }
            catch { return false; }
        }

        public virtual bool Xoa(string id)
        {
            string query = "DELETE FROM TrangThaiDatPhong WHERE TrangThaiID = @TrangThaiID";
            try
            {
                var parameters = new Dictionary<string, object> { { "@TrangThaiID", id } };
                DBUtil.Update(query, parameters);
                return true;
            }
            catch { return false; }
        }

        public virtual List<TrangThaiDatPhongDTO> TimKiemTheoHoaDon(string hoaDonID)
        {
            string query = "SELECT * FROM TrangThaiDatPhong WHERE HoaDonThueID LIKE @HoaDonID";
            var parameters = new Dictionary<string, object> { { "@HoaDonID", "%" + hoaDonID + "%" } };
            return DBUtil.QueryList<TrangThaiDatPhongDTO>(query, parameters);
        }

        public virtual string GenerateNewTrangThaiID()
        {
            const string prefix = "TTDP";
            string sql = "SELECT MAX(TrangThaiID) FROM TrangThaiDatPhong";
            object result = DBUtil.ScalarQuery(sql, null);

            if (result != null && result.ToString().StartsWith(prefix))
            {
                string numberPart = result.ToString().Substring(prefix.Length);
                if (int.TryParse(numberPart, out int number))
                    return $"{prefix}{(number + 1):D3}";
            }
            return $"{prefix}001";
        }

        public virtual string GetPhongIDByHoaDonThueID(string hoaDonThueID)
        {
            string sql = "SELECT PhongID FROM HoaDonThue WHERE HoaDonThueID = @HoaDonThueID";
            var args = new Dictionary<string, object> { { "@HoaDonThueID", hoaDonThueID } };
            var dt = DBUtil.Query(sql, args);
            if (dt.Rows.Count > 0) return dt.Rows[0]["PhongID"].ToString();
            return null;
        }
    }
}
