using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClassParameter
/// </summary>
/// 
namespace QLHD
{
    public class ClassParameter
    {
        #region Message
        public static string vKhongTinThayDuLieu = "Không tìm thấy dữ liệu";
        public static string vKhongDuQuyen = "Không được phép truy cập";
        public static string vKhongHopLe = "Dữ liệu không hợp lệ";
        public static string vFileUploadQuaLon = "Tập tin tải lên quá lớn";
        public static string unknownErrorMessage = "Có lỗi xảy ra. Vui lòng thử lại sau.";
        #endregion

        #region Paths
        public static string vPathCommonJavascript = DotNetNuke.Common.Globals.ApplicationPath.ToString() + "/DesktopModules/QLHD/js/common.js";
        public static string vPathCommonImages = DotNetNuke.Common.Globals.ApplicationPath.ToString() + "/DesktopModules/QLHD/image/";
        public static string vPathCommonData = DotNetNuke.Common.Globals.ApplicationPath.ToString() + "/DesktopModules/QLHD/data/";
        public static string vPathDataBieuMau = DotNetNuke.Common.Globals.ApplicationPath.ToString() + "/DesktopModules/QLHD/data/";
        public static string vPathCommonBieuMau = DotNetNuke.Common.Globals.ApplicationPath.ToString() + "/DesktopModules/QLHD/bieumau/";
        public static string vPathCommon = DotNetNuke.Common.Globals.ApplicationPath.ToString();
        public static string vJavascriptMaskNumber = "<script type='text/javascript' src='" + DotNetNuke.Common.Globals.ApplicationPath.ToString() + "/DesktopModules/QLHD/js/mask/jquery.metadata.js'></script>"
         + "<script type='text/javascript' src='" + DotNetNuke.Common.Globals.ApplicationPath.ToString() + "/DesktopModules/QLHD/js/mask/autoNumeric-1.6.2.js'></script>";
        #endregion

        public static int vSizeFile = 20971520; // 20MB        
        public static int vPageSizeTimKiem = 10;
        public static int vKyTuNoiDung = 60000;
        public static int vMaxLengthFCK = 80000;
        public static int vPageSize = 30;
        public static string v_NHOM_IDs = "3,7,11,23,17,16,12,27";
        
        #region Format string
        public static string dateFormat = "dd/MM/yyyy";
        public static string numberWithThousandSeparatorFormat = "{0:#,##0}";
        public static string numberWithDecimalPointFormat = "{0:#,##0.#}";
        #endregion

        #region ModuleByDefinitions
        public static string v_ModuleByDefinition_QUYETTOANQUANLYDUAN = "Quyết toán quản lý dự án";
        public static string v_ModuleByDefinition_QUYETTOANGIAMSATDUAN = "Quyết toán giám sát dự án";
        public static string v_ModuleByDefinition_QUYETTOANTUVANTHIETKE = "Quyết toán tư vấn thiết kế";
        public static string v_ModuleByDefinition_CHAMCONG = "QLCC_CHAMCONG";
        #endregion
    }
}