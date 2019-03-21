using DotNetNuke.Common;
using DotNetNuke.Entities.Users;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace QLSC
{
    public partial class THONGKE_SUCO_SONAMTRUOC : DotNetNuke.Entities.Modules.UserModuleBase
    {
        #region Khai báo định nghĩa dữ liệu
        public string vPathCommon;
        public string vPathCommonJS = ClassParameter.vPathCommonJavascript;
        public string vPathCommonToastJS = ClassParameter.vPathCommonToastJS;
        public string vPathCommonToastCSS = ClassParameter.vPathCommonToastCSS;
        public string vJavascriptMask = ClassParameter.vJavascriptMaskNumber;
        public string vPathIQueryJavascript = ClassParameter.vPathIQueryJavascript;
        public string vPathCommonCss = ClassParameter.vPathCommonCss;
        int vPageSize = ClassParameter.vPageSize;
        DataTable dtTable = new DataTable();
        public string vPathBieuMau = DotNetNuke.Common.Globals.ApplicationPath.ToString() + "/DesktopModules/QLSC/bieumau1/";
        public static string vPathCommonBieuMau_FN = DotNetNuke.Common.Globals.ApplicationPath.ToString() + "/DesktopModules/QLSC/bieumau/";
        QLSC_NGUOIDUNG objNGUOIDUNG = new QLSC_NGUOIDUNG();
        int vCurentPage = 0;
        int vStt = 1;
        int count;
        int countDS;
        QLSCDataContext vDC = new QLSCDataContext();
        UserInfo _currentUser = UserController.GetCurrentUserInfo();
        #endregion
        #region

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {



        }
        public void loadData()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }
    }
}