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
        List<QLSC_SUCO> lstSuCoNamHienTai = new List<QLSC_SUCO>();
        List<QLSC_SUCO> lstSuCoNamTruoc = new List<QLSC_SUCO>();

        #endregion
        #region

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lstSuCoNamHienTai = vDC.QLSC_SUCOs.Where(x => x.SC_NGAYXAYRA.Value.Year == DateTime.Now.Year).ToList();
                lstSuCoNamTruoc = vDC.QLSC_SUCOs.Where(x => x.SC_NGAYXAYRA.Value.Year == DateTime.Now.Year - 1).ToList();
                loadData();                
            }
            catch (Exception ex)
            {

            }


        }
        
        public int countSuCoTheoThang(List<QLSC_SUCO> lstSUCO, int thang, int loaiID)
        {
            int count = lstSUCO.Where(x => x.LOAISC_ID == loaiID && x.SC_NGAYXAYRA.Value.Month == thang).Count();
            return count;
        }

        public void loadData()
        {
            try
            {                
                string vHTML = "";
                string namHienTai = DateTime.Now.Year.ToString();
                string namTruoc = (DateTime.Now.Year - 1).ToString();
                lbNamHienTai.Text = namHienTai;
                lbNamTruoc.Text = namTruoc;
                for (int i = 1; i <= 12; i++)
                {
                    vHTML += "<tr>";
                    vHTML += "<td rowspan='2' class='center'>" + i + "</td>";
                    vHTML += "<td>T." + i + "/" + namHienTai + "</td>";
                    vHTML += "<td>T." + i + "/" + namTruoc + "</td>";
                    vHTML += "<td></td>";
                    
                    vHTML += "<td>T." + i + "/" + namHienTai + "</td>";
                    vHTML += "<td>T." + i + "/" + namTruoc + "</td>";
                    vHTML += "<td></td>";

                    
                    vHTML += "<td>T." + i + "/" + namHienTai + "</td>";
                    vHTML += "<td>T." + i + "/" + namTruoc + "</td>";
                    vHTML += "<td></td>";
                    
                    vHTML += "<td>T." + i + "/" + namHienTai + "</td>";
                    vHTML += "<td>T." + i + "/" + namTruoc + "</td>";
                    vHTML += "<td></td>";
                    vHTML += "</tr>";

                    vHTML += "<tr>";
                    int countNamHienTai = countSuCoTheoThang(lstSuCoNamHienTai, i, 1);
                    int countNamTruoc = countSuCoTheoThang(lstSuCoNamTruoc, i, 1);
                    int thaydoi = countNamHienTai - countNamTruoc;
                    vHTML += "<td class='text_right'>" + countNamHienTai + "</td>";
                    vHTML += "<td class='text_right'>" + countNamTruoc + "</td>";
                    string rsThayDoi_DongVat = "None";
                    if(thaydoi > 0)
                    {
                        rsThayDoi_DongVat = "Tăng " + thaydoi + " vụ";
                    }
                    else
                    {
                        if (thaydoi == 0)
                        {
                            rsThayDoi_DongVat = "Bằng";
                        }
                        else
                        {
                            rsThayDoi_DongVat = "Giảm " + Math.Abs(thaydoi) + " vụ";
                        }
                    }
                    if(thaydoi == 0 && countNamHienTai == 0 && countNamTruoc == 0)
                    {
                        rsThayDoi_DongVat = "Không SC";
                    }
                    string cssThayDoi = "";
                    if(thaydoi > 0)
                    {
                        cssThayDoi = "sc_tang";
                    }
                    else
                    {
                        if(thaydoi < 0)
                        {
                            cssThayDoi = "sc_giam";
                        }
                    }
                    vHTML += "<td class='"+ cssThayDoi +"'>" + rsThayDoi_DongVat + "</td>";

                    countNamHienTai = countSuCoTheoThang(lstSuCoNamHienTai, i, 2);
                    countNamTruoc = countSuCoTheoThang(lstSuCoNamTruoc, i, 2);
                    thaydoi = countNamHienTai - countNamTruoc;
                    vHTML += "<td class='text_right'>" + countNamHienTai + "</td>";
                    vHTML += "<td class='text_right'>" + countNamTruoc + "</td>";
                    string rsThayDoi_SetDanh = "None";
                    if (thaydoi > 0)
                    {
                        rsThayDoi_SetDanh = "Tăng " + thaydoi + " vụ";
                    }
                    else
                    {
                        if (thaydoi == 0)
                        {
                            rsThayDoi_SetDanh = "Bằng";
                        }
                        else
                        {
                            rsThayDoi_SetDanh = "Giảm " + Math.Abs(thaydoi) + " vụ";
                        }
                    }
                    if (thaydoi == 0 && countNamHienTai == 0 && countNamTruoc == 0)
                    {
                        rsThayDoi_SetDanh = "Không SC";
                    }
                    cssThayDoi = "";
                    if (thaydoi > 0)
                    {
                        cssThayDoi = "sc_tang";
                    }
                    else
                    {
                        if (thaydoi < 0)
                        {
                            cssThayDoi = "sc_giam";
                        }
                    }
                    vHTML += "<td class='" + cssThayDoi + "'>" + rsThayDoi_SetDanh + "</td>";                    

                    countNamHienTai = countSuCoTheoThang(lstSuCoNamHienTai, i, 3);
                    countNamTruoc = countSuCoTheoThang(lstSuCoNamTruoc, i, 3);
                    thaydoi = countNamHienTai - countNamTruoc;
                    vHTML += "<td class='text_right'>" + countNamHienTai + "</td>";
                    vHTML += "<td class='text_right'>" + countNamTruoc + "</td>";
                    string rsThayDoi_PhongDien = "None";
                    if (thaydoi > 0)
                    {
                        rsThayDoi_PhongDien = "Tăng " + thaydoi + " vụ";
                    }
                    else
                    {
                        if (thaydoi == 0)
                        {
                            rsThayDoi_PhongDien = "Bằng";
                        }
                        else
                        {
                            rsThayDoi_PhongDien = "Giảm " + Math.Abs(thaydoi) + " vụ";
                        }
                    }
                    if (thaydoi == 0 && countNamHienTai == 0 && countNamTruoc == 0)
                    {
                        rsThayDoi_PhongDien = "Không SC";
                    }
                    cssThayDoi = "";
                    if (thaydoi > 0)
                    {
                        cssThayDoi = "sc_tang";
                    }
                    else
                    {
                        if (thaydoi < 0)
                        {
                            cssThayDoi = "sc_giam";
                        }
                    }
                    vHTML += "<td class='" + cssThayDoi + "'>" + rsThayDoi_PhongDien + "</td>";

                    countNamHienTai = countSuCoTheoThang(lstSuCoNamHienTai, i, 4);
                    countNamTruoc = countSuCoTheoThang(lstSuCoNamTruoc, i, 4);
                    thaydoi = countNamHienTai - countNamTruoc;
                    vHTML += "<td class='text_right'>" + countNamHienTai + "</td>";
                    vHTML += "<td class='text_right'>" + countNamTruoc + "</td>";
                    string rsThayDoi_HanhLang = "None";
                    if (thaydoi > 0)
                    {
                        rsThayDoi_HanhLang = "Tăng " + thaydoi + " vụ";
                    }
                    else
                    {
                        if (thaydoi == 0)
                        {
                            rsThayDoi_HanhLang = "Bằng";
                        }
                        else
                        {
                            rsThayDoi_HanhLang = "Giảm " + Math.Abs(thaydoi) + " vụ";
                        }
                    }
                    if (thaydoi == 0 && countNamHienTai == 0 && countNamTruoc == 0)
                    {
                        rsThayDoi_HanhLang = "Không SC";
                    }
                    cssThayDoi = "";
                    if (thaydoi > 0)
                    {
                        cssThayDoi = "sc_tang";
                    }
                    else
                    {
                        if (thaydoi < 0)
                        {
                            cssThayDoi = "sc_giam";
                        }
                    }
                    vHTML += "<td class='" + cssThayDoi + "'>" + rsThayDoi_HanhLang + "</td>";                    
                    vHTML += "</tr>";
                    lbContent.Text = vHTML;
                }
                               
            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_XuatExcel_Click(object sender, EventArgs e)
        {
            int i = 0;
        }
    }
}