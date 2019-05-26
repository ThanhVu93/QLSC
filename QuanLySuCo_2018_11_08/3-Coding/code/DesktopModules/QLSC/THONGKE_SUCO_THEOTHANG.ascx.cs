using DotNetNuke.Entities.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLSC
{
    public partial class THONGKE_SUCO_THEOTHANG : DotNetNuke.Entities.Modules.UserModuleBase
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
        List<QLSC_DONVI> lstDONVI = new List<QLSC_DONVI>();
        #endregion
        #region

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lstSuCoNamHienTai = vDC.QLSC_SUCOs.Where(x => x.SC_NGAYXAYRA.Value.Year == DateTime.Now.Year).ToList();
                    lstSuCoNamTruoc = vDC.QLSC_SUCOs.Where(x => x.SC_NGAYXAYRA.Value.Year == DateTime.Now.Year - 1).ToList();
                    lstDONVI = vDC.QLSC_DONVIs.ToList();                                        
                    loadData();
                }
               
            }
            catch (Exception ex)
            {

            }
        }

        public int countSuCoTheoThang(List<QLSC_SUCO> lstSUCO, int thang, int loaiID,int donviID)
        {
            int count = lstSUCO.Where(x => x.LOAISC_ID == loaiID && x.SC_NGAYXAYRA.Value.Month == thang && x.DONVI_ID == donviID).Count();
            return count;
        }

        public string xulyChenhLech(int vCountNamHienTai, int vCountNamTruoc)
        {
            int thaydoi = vCountNamHienTai - vCountNamTruoc;
            string result = "None";        
            if (thaydoi > 0)
            {
                result = "Tăng " + thaydoi + " vụ";
            }
            else
            {
                if (thaydoi == 0)
                {
                    result = "Bằng";
                }
                else
                {
                    result = "Giảm " + Math.Abs(thaydoi) + " vụ"; ;
                }
            }
            if(thaydoi == 0 && vCountNamHienTai == 0 && vCountNamTruoc == 0)
            {
                result = "Không SC";
            }
            return result;
        }

        public void loadData()
        {
            try
            {
                int vThang = int.Parse(drpThang.SelectedValue);
                string vHTML = "";
                string namHienTai = DateTime.Now.Year.ToString();
                string namTruoc = (DateTime.Now.Year - 1).ToString();
                //lbNamHienTai.Text = namHienTai;
                //lbNamTruoc.Text = namTruoc;
                vHTML += "<th width='4%' rowspan='2'>STT</th>";
                vHTML += "<th width='16%' rowspan='2'>ĐƠN VỊ</th>";
                vHTML += "<th class='center' colspan='2' width='12%'>SỰ CỐ DO ĐỘNG VẬT<br />(vụ)</th>";
                vHTML += "<th width='8%' rowspan='2'>SO SÁNH CÙNG KỲ NĂM 2018</th>";
                vHTML += "<th colspan='2' width='11%'>SỰ CỐ DO SÉT ĐÁNH<br />(vụ)</th>";
                vHTML += "<th width='8%' rowspan='2'>SO SÁNH CÙNG KỲ NĂM 2018</th>";
                vHTML += "<th colspan='2' width='11%'>SỰ CỐ DO PHÓNG ĐIỆN<br />(vụ)</th>";
                vHTML += "<th width='8%' rowspan='2'>SO SÁNH CÙNG KỲ NĂM 2018</th>";
                vHTML += "<th colspan='2' width='11%'>SỰ CỐ DO VI PHẠM HLATLĐCA<br />(vụ)</th>";
                vHTML += "<th width='8%' rowspan='2'>SO SÁNH CÙNG KỲ NĂM 2018</th>";
                vHTML += "</tr>";
                vHTML += "<tr>";
                string vrowThang = "<th>T."+ vThang+ "/" + namHienTai.ToString() + "</th>" + "<th>" + "T."+ vThang + "/" + namTruoc + "</th>";
                vHTML += vrowThang + vrowThang + vrowThang + vrowThang;
                int j = 1;
                if (lstDONVI != null)
                {
                   foreach (var obj in lstDONVI)
                    {
                        vHTML += "<tr>";                       
                        vHTML += "<td class='center'>" + j + "</td>";
                        j++;
                        vHTML += "<td>" +obj.DONVI_TEN +"</td>";
                        int countDV_NamHienTai = countSuCoTheoThang(lstSuCoNamHienTai, vThang, 1, obj.DONVI_ID);
                        int countDV_NamTruoc = countSuCoTheoThang(lstSuCoNamTruoc, vThang, 1, obj.DONVI_ID);
                        vHTML += "<td>" + countDV_NamHienTai + "</td>";
                        vHTML += "<td>" + countDV_NamTruoc + "</td>";
                        string cssThayDoi = "";
                        if (countDV_NamHienTai - countDV_NamTruoc > 0)
                        {
                            cssThayDoi = "sc_tang";
                        }
                        else
                        {
                            if (countDV_NamHienTai - countDV_NamTruoc < 0)
                            {
                                cssThayDoi = "sc_giam";
                            }
                        }
                        
                        vHTML += "<td class='"+ cssThayDoi + "'>"+ xulyChenhLech(countDV_NamHienTai, countDV_NamTruoc) + "</td>";

                        int countSD_NamHienTai = countSuCoTheoThang(lstSuCoNamHienTai, vThang, 2, obj.DONVI_ID);
                        int countSD_NamTruoc = countSuCoTheoThang(lstSuCoNamTruoc, vThang, 2, obj.DONVI_ID);
                        vHTML += "<td>" + countSD_NamHienTai + "</td>";
                        vHTML += "<td>" + countSD_NamTruoc + "</td>";
                        cssThayDoi = "";
                        if (countSD_NamHienTai - countSD_NamTruoc > 0)
                        {
                            cssThayDoi = "sc_tang";
                        }
                        else
                        {
                            if (countSD_NamHienTai - countSD_NamTruoc < 0)
                            {
                                cssThayDoi = "sc_giam";
                            }
                        } 
                        vHTML += "<td class='"+ cssThayDoi +"'>" + xulyChenhLech(countSD_NamHienTai, countSD_NamTruoc) + "</td>";
                       
                        int countPD_NamHienTai = countSuCoTheoThang(lstSuCoNamHienTai, vThang, 3, obj.DONVI_ID);
                        int countPD_NamTruoc = countSuCoTheoThang(lstSuCoNamTruoc, vThang, 3, obj.DONVI_ID);
                        vHTML += "<td>" + countPD_NamHienTai + "</td>";
                        vHTML += "<td>" + countPD_NamTruoc + "</td>";
                        cssThayDoi = "";
                        if (countPD_NamHienTai - countPD_NamTruoc > 0)
                        {
                            cssThayDoi = "sc_tang";
                        }
                        else
                        {
                            if (countPD_NamHienTai - countPD_NamTruoc < 0)
                            {
                                cssThayDoi = "sc_giam";
                            }
                        }
                        vHTML += "<td class='"+ cssThayDoi + "'>" + xulyChenhLech(countPD_NamHienTai, countPD_NamTruoc) + "</td>";

                        int countVPHL_NamHienTai = countSuCoTheoThang(lstSuCoNamHienTai, vThang, 4, obj.DONVI_ID);
                        int countVPHLD_NamTruoc = countSuCoTheoThang(lstSuCoNamTruoc, vThang, 4, obj.DONVI_ID);
                        vHTML += "<td>" + countVPHL_NamHienTai + "</td>";
                        vHTML += "<td>" + countVPHLD_NamTruoc + "</td>";
                        cssThayDoi = "";
                        if (countVPHL_NamHienTai - countVPHLD_NamTruoc > 0)
                        {
                            cssThayDoi = "sc_tang";
                        }
                        else
                        {
                            if (countVPHL_NamHienTai - countVPHLD_NamTruoc < 0)
                            {
                                cssThayDoi = "sc_giam";
                            }
                        }
                        vHTML += "<td class='"+ cssThayDoi + "'>" + xulyChenhLech(countVPHL_NamHienTai, countVPHLD_NamTruoc) + "</td>";
                        vHTML += "</tr>";
                    }
                    j = 0;
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
