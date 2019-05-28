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
                if (!_currentUser.IsInRole("Administrator"))
                {
                    var objNGUOIDUNG = vDC.QLSC_NGUOIDUNGs.Where(x => x.UserID == _currentUser.UserID).FirstOrDefault();
                    lstSuCoNamHienTai = lstSuCoNamHienTai.Where(x => x.DONVI_ID == objNGUOIDUNG.DONVI_ID).ToList();
                    lstSuCoNamTruoc = lstSuCoNamTruoc.Where(x => x.DONVI_ID == objNGUOIDUNG.DONVI_ID).ToList();
                }
                loadData();                
            }
            catch (Exception ex)
            {

            }


        }
        
        public int countSuCoTheoThang(List<QLSC_SUCO> lstSUCO, int thang, int loaiID)
        {
            int count = 0;
            if (_currentUser.IsInRole("Administrator"))
            {                
                count = lstSUCO.Where(x => x.LOAISC_ID == loaiID && x.SC_NGAYXAYRA.Value.Month == thang).Count();
            }
            else
            {
                var objNGUOIDUNG = vDC.QLSC_NGUOIDUNGs.Where(x => x.UserID == _currentUser.UserID).FirstOrDefault();
                count = lstSUCO.Where(x => x.LOAISC_ID == loaiID && x.SC_NGAYXAYRA.Value.Month == thang).Where(x=>x.DONVI_ID == objNGUOIDUNG.DONVI_ID).Count();
            }
            return count;
        }

        public int countSuCoTongTheoLoai(List<QLSC_SUCO> lstSUCO,  int loaiID)
        {
            int count = 0;
            if (_currentUser.IsInRole("Administrator"))
            {
                count = lstSUCO.Where(x => x.LOAISC_ID == loaiID).Count();
            }
            else
            {
                var objNGUOIDUNG = vDC.QLSC_NGUOIDUNGs.Where(x => x.UserID == _currentUser.UserID).FirstOrDefault();
                count = lstSUCO.Where(x => x.LOAISC_ID == loaiID).Where(x => x.DONVI_ID == objNGUOIDUNG.DONVI_ID).Count();
            }


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

                  
                }
                vHTML += " <tr style='font-weight:bold'>";
                vHTML += "<td width='4%' style='font-weight:bold'> Tổng";
                vHTML += "</td>";
                //Tổng sự cố động vật
                int countTongNamHienTai = countSuCoTongTheoLoai(lstSuCoNamHienTai, 1);
                 int countTongNamTruoc = countSuCoTongTheoLoai(lstSuCoNamTruoc, 1);
                 int thaydoiTong = countTongNamHienTai - countTongNamTruoc;

                vHTML += "<td>" + countTongNamHienTai;                
                vHTML += "</td>";
                vHTML += "<td>" + countTongNamTruoc;
                vHTML += "</td>";
                string rsThayDoi_DongVatTong = "None";
                if (thaydoiTong > 0)
                {
                    rsThayDoi_DongVatTong = "Tăng " + thaydoiTong + " vụ";
                }
                else
                {
                    if (thaydoiTong == 0)
                    {
                        rsThayDoi_DongVatTong = "Bằng";
                    }
                    else
                    {
                        rsThayDoi_DongVatTong = "Giảm " + Math.Abs(thaydoiTong) + " vụ";
                    }
                }
                if (thaydoiTong == 0 && countTongNamHienTai == 0 && countTongNamTruoc == 0)
                {
                    rsThayDoi_DongVatTong = "Không SC";
                }
                string cssThayDoiTong = "";
                if (thaydoiTong > 0)
                {
                    cssThayDoiTong = "sc_tang";
                }
                else
                {
                    if (thaydoiTong < 0)
                    {
                        cssThayDoiTong = "sc_giam";
                    }
                }
                vHTML += "<td class='" + cssThayDoiTong + "'>" + rsThayDoi_DongVatTong + "</td>";

                //Tổng sự cố sét đánh 
                countTongNamHienTai = countSuCoTongTheoLoai(lstSuCoNamHienTai, 2);
                 countTongNamTruoc = countSuCoTongTheoLoai(lstSuCoNamTruoc, 2);
                 thaydoiTong = countTongNamHienTai - countTongNamTruoc;

                vHTML += "<td>" + countTongNamHienTai;
                vHTML += "</td>";
                vHTML += "<td>" + countTongNamTruoc;
                vHTML += "</td>";
                string rsThayDoi_SetDanhTong = "None";
                if (thaydoiTong > 0)
                {
                    rsThayDoi_SetDanhTong = "Tăng " + thaydoiTong + " vụ";
                }
                else
                {
                    if (thaydoiTong == 0)
                    {
                        rsThayDoi_SetDanhTong = "Bằng";
                    }
                    else
                    {
                        rsThayDoi_SetDanhTong = "Giảm " + Math.Abs(thaydoiTong) + " vụ";
                    }
                }
                if (thaydoiTong == 0 && countTongNamHienTai == 0 && countTongNamTruoc == 0)
                {
                    rsThayDoi_DongVatTong = "Không SC";
                }
                cssThayDoiTong = "";
                if (thaydoiTong > 0)
                {
                    cssThayDoiTong = "sc_tang";
                }
                else
                {
                    if (thaydoiTong < 0)
                    {
                        cssThayDoiTong = "sc_giam";
                    }
                }
                vHTML += "<td class='" + cssThayDoiTong + "'>" + rsThayDoi_SetDanhTong + "</td>";

                //Tổng sự cố phóng điện
                countTongNamHienTai = countSuCoTongTheoLoai(lstSuCoNamHienTai, 3);
                countTongNamTruoc = countSuCoTongTheoLoai(lstSuCoNamTruoc, 3);
                thaydoiTong = countTongNamHienTai - countTongNamTruoc;

                vHTML += "<td>" + countTongNamHienTai;
                vHTML += "</td>";
                vHTML += "<td>" + countTongNamTruoc;
                vHTML += "</td>";
                string rsThayDoi_PhongDienTong = "None";
                if (thaydoiTong > 0)
                {
                    rsThayDoi_PhongDienTong = "Tăng " + thaydoiTong + " vụ";
                }
                else
                {
                    if (thaydoiTong == 0)
                    {
                        rsThayDoi_PhongDienTong = "Bằng";
                    }
                    else
                    {
                        rsThayDoi_PhongDienTong = "Giảm " + Math.Abs(thaydoiTong) + " vụ";
                    }
                }
                if (thaydoiTong == 0 && countTongNamHienTai == 0 && countTongNamTruoc == 0)
                {
                    rsThayDoi_PhongDienTong = "Không SC";
                }
                cssThayDoiTong = "";
                if (thaydoiTong > 0)
                {
                    cssThayDoiTong = "sc_tang";
                }
                else
                {
                    if (thaydoiTong < 0)
                    {
                        cssThayDoiTong = "sc_giam";
                    }
                }
                vHTML += "<td class='" + cssThayDoiTong + "'>" + rsThayDoi_PhongDienTong + "</td>";

                //Tổng sự cố phóng điện
                countTongNamHienTai = countSuCoTongTheoLoai(lstSuCoNamHienTai, 4);
                countTongNamTruoc = countSuCoTongTheoLoai(lstSuCoNamTruoc, 4);
                thaydoiTong = countTongNamHienTai - countTongNamTruoc;

                vHTML += "<td>" + countTongNamHienTai;
                vHTML += "</td>";
                vHTML += "<td>" + countTongNamTruoc;
                vHTML += "</td>";
                string rsThayDoi_HanhLangTong = "None";
                if (thaydoiTong > 0)
                {
                    rsThayDoi_HanhLangTong = "Tăng " + thaydoiTong + " vụ";
                }
                else
                {
                    if (thaydoiTong == 0)
                    {
                        rsThayDoi_HanhLangTong = "Bằng";
                    }
                    else
                    {
                        rsThayDoi_HanhLangTong = "Giảm " + Math.Abs(thaydoiTong) + " vụ";
                    }
                }
                if (thaydoiTong == 0 && countTongNamHienTai == 0 && countTongNamTruoc == 0)
                {
                    rsThayDoi_HanhLangTong = "Không SC";
                }
                cssThayDoiTong = "";
                if (thaydoiTong > 0)
                {
                    cssThayDoiTong = "sc_tang";
                }
                else
                {
                    if (thaydoiTong < 0)
                    {
                        cssThayDoiTong = "sc_giam";
                    }
                }
                vHTML += "<td class='" + cssThayDoiTong + "'>" + rsThayDoi_HanhLangTong + "</td>";

                vHTML += "</tr>";
                lbContent.Text = vHTML;
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