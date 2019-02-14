using System;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Log.EventLog;
using System.ComponentModel;
using System.Diagnostics;

namespace QLHD
{
    /// <summary>
    /// Summary description for Logger
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Mỗi LogType ứng với một chức năng
        /// </summary>
        public enum LogType
        {
            CongThucThucTe,
            CongThucKhuyenCao,
            DiaPhuong,
            DoiTac,
            BIEUMAU,
            DonViTinh,
            HangHoa,
            HopDongBan,
            HopDongChoThueKho,
            HopDongMua,
            HopDongThueKho,
            KhoCang,
            LenhNhapHangDichVu,
            LenhNhapHangMua,
            LenhXuatHang,
            LenhXuatKiemLuanChuyenNoiBo,
            NhapKho,
            NhomHangHoa,
            SangMan,
            XuatKho,
            DangKyPhuongTien,
            LoaiHinhDoanhNghiep,
            LoaiCayTrong,
            SoLieuGieoTrong,
            HangTrenPhuongTien,
            ChuyenSoHuu,
            LenhXuatHangDichVu
        }

        public enum LogAction
        {
            [Description("Thêm")]
            Them,
            [Description("Sửa")]
            Sua,
            [Description("Xóa")]
            Xoa
        }

        public Logger()
        {

        }


        /// <summary>
        /// Ghi nhật ký
        /// </summary>
        /// <param name="logType">Được khai báo trong enum LogType</param>
        /// <param name="logAction">Thao tác: thêm, sửa, xóa, duyệt,...</param>
        /// <param name="value"></param>
        public static void saveLog(LogType logType, LogAction logAction, string value)
        {
            String PortalName = PortalController.Instance.GetCurrentPortalSettings().PortalName;
            int PortalID = PortalController.Instance.GetCurrentPortalSettings().PortalId;
            String UserName = UserController.Instance.GetCurrentUserInfo().Username;
            int UserID = UserController.Instance.GetCurrentUserInfo().UserID;

            EventLogController elc = new EventLogController();
            LogInfo loginfo = new LogInfo();
            loginfo.LogCreateDate = DateTime.Now;   //Ngày tạo Log
            loginfo.LogPortalName = PortalName;     //Tên Portal Thao tác
            loginfo.LogPortalID = PortalID;         //ID Portal Thao tác
            loginfo.LogTypeKey = logType.ToString();        //Khóa nhật ký
            loginfo.LogUserName = UserName;         //Người dùng thao tác
            loginfo.LogUserID = UserID;             //ID người dùng thao tác
            loginfo.AddProperty("Hành động", ClassCommon.GetEnumDescription(logAction));  //Thuộc tính nhật ký
            loginfo.AddProperty("Nội dung", value);   //Thuộc tính nhật ký
            elc.AddLog(loginfo);
        }
    }
}