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
    public partial class SUCO_DS : DotNetNuke.Entities.Modules.UserModuleBase
    {
        #region Khai báo định nghĩa dữ liệu
        public string vPathCommon;
        public string vPathCommonJS = ClassParameter.vPathCommonJavascript;

        public string vPathCommonToastJS = ClassParameter.vPathCommonToastJS;
        public string vPathCommonToastCSS = ClassParameter.vPathCommonToastCSS;
        public string vJavascriptMask = ClassParameter.vJavascriptMaskNumber;
        public string vPathIQueryJavascript = ClassParameter.vPathIQueryJavascript;
        TAPTINController objTAPTINController = new TAPTINController();
        public string vPathCommonCss = ClassParameter.vPathCommonCss;
        public string vPathCommonData = ClassParameter.vPathCommonData;
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
            try
            {
                Get_Cache();
                if (!String.IsNullOrEmpty(Session[TabId.ToString() + _currentUser.UserID + "_Message"] as string) && !String.IsNullOrEmpty(Session[TabId.ToString() + _currentUser.UserID + "_Type"] as string))
                {
                    if (Session[TabId.ToString() + _currentUser.UserID + "_Message"].ToString() != "" && Session[TabId.ToString() + _currentUser.UserID + "_Type"].ToString() != "")
                    {
                        ClassCommon.THONGBAO_TOASTR(Page, null, _currentUser, Session[TabId.ToString() + _currentUser.UserID + "_Message"].ToString(), "Thông báo", Session[TabId.ToString() + _currentUser.UserID + "_Type"].ToString());
                    }
                    Session[TabId.ToString() + _currentUser.UserID + "_Message"] = "";
                    Session[TabId.ToString() + _currentUser.UserID + "_Type"] = "";
                }
                DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(DotNetNuke.Framework.JavaScriptLibraries.CommonJs.jQuery);
                DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(DotNetNuke.Framework.JavaScriptLibraries.CommonJs.DnnPlugins);
                DotNetNuke.UI.Utilities.ClientAPI.RegisterClientReference(this.Page, DotNetNuke.UI.Utilities.ClientAPI.ClientNamespaceReferences.dnn);
                if (!IsPostBack)
                {
                    txtTuNgay.SelectedDate = DateTime.Now.AddYears(-1);
                    txtDenNgay.SelectedDate = DateTime.Now;
                    LoadDSNhomTV();
                    drpDonVi.SelectedValue = "0";
                    LoadDanhSach(0,0);
                }
            }
            catch (Exception ex)
            {
                ClassCommon.ShowToastr(Page, "Có lỗi xãy ra, vui lòng liên hệ quản trị", "Thông báo", "error");
            }
        }

        protected void btn_ThemMoi_Click(object sender, EventArgs e)
        {
            Session.Remove("dgDanhSach");
            Response.Redirect(Globals.NavigateURL("create_update", "mid=" + this.ModuleId, "title=Thêm mới sự cố", "ID=0"));
        }

        public string STT()
        {
            return (((dgDanhSach.CurrentPageIndex) * vPageSize) + vStt++).ToString();
        }

        public void LoadDSNhomTV()
        {
            try
            {
                List<QLSC_DONVI> lstDonVi = new List<QLSC_DONVI>();
                lstDonVi = vDC.QLSC_DONVIs.ToList();
                dtTable = new DataTable();
                dtTable.Columns.Add("DV_TEN");
                dtTable.Columns.Add("DV_ID");
                DataRow row2 = dtTable.NewRow();
                row2["DV_ID"] = "0";
                row2["DV_TEN"] = "Tất cả đơn vị";
                dtTable.Rows.Add(row2);
                foreach (var it in lstDonVi)
                {
                    DataRow row = dtTable.NewRow();
                    row["DV_TEN"] = HttpUtility.HtmlDecode(it.DONVI_TEN);
                    row["DV_ID"] = it.DONVI_ID;
                    dtTable.Rows.Add(row);
                }
                drpDonVi.Items.Clear();
                drpDonVi.DataSource = dtTable;
                drpDonVi.DataTextField = "DV_TEN";
                drpDonVi.DataValueField = "DV_ID";
                drpDonVi.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        public void LoadDanhSach(int pCurentPage,int  vXuatExcel)
        {
            if (vXuatExcel == 1) vPageSize = 99999;
            try
            {
                UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
                var pUserID = _currentUser.UserID;
                string pKEYWORD = "";
                if (Session[PortalSettings.ActiveTab.TabID + pUserID + "tukhoa_search"] != null)
                {
                    pKEYWORD = Session[PortalSettings.ActiveTab.TabID + pUserID + "tukhoa_search"].ToString();
                }
                txt_TK_NoiDung.Text = pKEYWORD;

                int DONVI_ID = 0;
                if (Session[PortalSettings.ActiveTab.TabID + pUserID + "tukhoa_donvi"] != null)
                {
                    drpDonVi.SelectedValue = Session[PortalSettings.ActiveTab.TabID + pUserID + "tukhoa_donvi"].ToString();
                    DONVI_ID = int.Parse(Session[PortalSettings.ActiveTab.TabID + pUserID + "tukhoa_donvi"].ToString());
                }

                DateTime tungay = DateTime.Parse(txtTuNgay.SelectedDate.ToString()).Date;
                DateTime denngay = DateTime.Parse(txtDenNgay.SelectedDate.ToString()).Date;

                if (Session[PortalSettings.ActiveTab.TabID + pUserID + "tukhoa_tungay"] != null)
                {
                    txtTuNgay.SelectedDate = DateTime.Parse(Session[PortalSettings.ActiveTab.TabID + pUserID + "tukhoa_tungay"].ToString());
                    tungay = DateTime.Parse(Session[PortalSettings.ActiveTab.TabID + pUserID + "tukhoa_tungay"].ToString()).Date;
                }
                if (Session[PortalSettings.ActiveTab.TabID + pUserID + "tukhoa_denngay"] != null)
                {
                    txtDenNgay.SelectedDate = DateTime.Parse(Session[PortalSettings.ActiveTab.TabID + pUserID + "tukhoa_denngay"].ToString());
                    denngay = DateTime.Parse(Session[PortalSettings.ActiveTab.TabID + pUserID + "tukhoa_denngay"].ToString()).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                }

                //var lstSuCo = vDC.QLSC_SUCOs.ToList();
                dtTable = new DataTable();
                dtTable.Columns.Add("SC_ID");
                dtTable.Columns.Add("SC_NGAYXAYRA");
                dtTable.Columns.Add("SC_GIOXAYRA");
                dtTable.Columns.Add("SC_NGAYTAILAP");
                dtTable.Columns.Add("SC_NOIDUNG");
                dtTable.Columns.Add("SC_NGUYENNHAN");
                dtTable.Columns.Add("SC_DONVI");
                dtTable.Columns.Add("SC_DONVI_TENRUTGON");
                dtTable.Columns.Add("SC_VTTB_TENCHUNGLOAI");
                dtTable.Columns.Add("SC_VTTB_SOLUONG");
                dtTable.Columns.Add("SC_VTTB_NHASANXUAT");
                dtTable.Columns.Add("SC_VTTB_NAMSANXUAT");

                dtTable.Columns.Add("SC_VTTB_TENCHUNGLOAI2");
                dtTable.Columns.Add("SC_VTTB_SOLUONG2");
                dtTable.Columns.Add("SC_VTTB_NHASANXUAT2");
                dtTable.Columns.Add("SC_VTTB_NAMSANXUAT2");

                dtTable.Columns.Add("SC_VTTB_TENCHUNGLOAI5");
                dtTable.Columns.Add("SC_VTTB_SOLUONG5");
                dtTable.Columns.Add("SC_VTTB_NHASANXUAT5");
                dtTable.Columns.Add("SC_VTTB_NAMSANXUAT5");

                dtTable.Columns.Add("SC_VTTB_TENCHUNGLOAI3");
                dtTable.Columns.Add("SC_VTTB_SOLUONG3");
                dtTable.Columns.Add("SC_VTTB_NHASANXUAT3");
                dtTable.Columns.Add("SC_VTTB_NAMSANXUAT3");

                dtTable.Columns.Add("SC_VTTB_TENCHUNGLOAI4");
                dtTable.Columns.Add("SC_VTTB_SOLUONG4");
                dtTable.Columns.Add("SC_VTTB_NHASANXUAT4");
                dtTable.Columns.Add("SC_VTTB_NAMSANXUAT4");

                dtTable.Columns.Add("SC_DIENAP");
                //dtTable.Columns.Add("");
                dtTable.Columns.Add("SC_CQ");
                dtTable.Columns.Add("SC_KQ");
                dtTable.Columns.Add("SC_PHANLOAI_TQ_DUONGTRUC");
                dtTable.Columns.Add("SC_PHANLOAI_TQ_NGARE");
                dtTable.Columns.Add("SC_PHANLOAI_VC_DUONGTRUC");
                dtTable.Columns.Add("SC_PHANLOAI_VC_NGARE");
                dtTable.Columns.Add("SC_PHANLOAI_TBA");
                dtTable.Columns.Add("SC_PHANLOAI_HA");
                dtTable.Columns.Add("SC_TONGSOKHACHHANG");
                dtTable.Columns.Add("SC_TAISAN_TBA");
                dtTable.Columns.Add("SC_TAISAN_HA");
                dtTable.Columns.Add("FILE");
                dtTable.Columns.Add("SC_GHICHU"); 
                dtTable.Columns.Add("SC_THIETBIDONGCAT_MSRCS");

                dtTable.Columns.Add("UserID");
                var lstSuCo1 = (from sc in vDC.QLSC_SUCOs
                                join dv in vDC.QLSC_DONVIs on sc.DONVI_ID equals dv.DONVI_ID
                                join loai in vDC.QLSC_LOAISUCOs on sc.LOAISC_ID equals loai.LOAISC_ID
                                where (SqlMethods.Like(sc.SC_NOIDUNG, "%" + pKEYWORD + "%") || SqlMethods.Like(sc.SC_NGUYENNHAN, "%" + pKEYWORD + "%"))
                                && sc.SC_NGAYXAYRA >= tungay
                                && sc.SC_NGAYXAYRA <= denngay
                                && (dv.DONVI_ID == DONVI_ID || DONVI_ID == 0)
                                orderby sc.SC_NGAYXAYRA descending
                                select new
                                {
                                    sc.DONVI_ID,
                                    sc.SC_ID,
                                    sc.SC_NGAYXAYRA,
                                    sc.SC_NGAYTAILAP,
                                    sc.SC_NOIDUNG,
                                    sc.SC_NGUYENNHAN,
                                    dv.DONVI_TEN,    
                                    dv.DONVI_TENRUTGON,                                
                                    sc.SC_VTTB_TENCHUNGLOAI,
                                    sc.SC_VTTB_SOLUONG,
                                    sc.SC_VTTB_NHASANXUAT,
                                    sc.SC_VTTB_TENCHUNGLOAI2,
                                    sc.SC_VTTB_SOLUONG2,
                                    sc.SC_VTTB_NHASANXUAT2,
                                    sc.SC_VTTB_TENCHUNGLOAI3,
                                    sc.SC_VTTB_SOLUONG3,
                                    sc.SC_VTTB_NHASANXUAT3,
                                    sc.SC_VTTB_TENCHUNGLOAI4,
                                    sc.SC_VTTB_SOLUONG4,
                                    sc.SC_VTTB_NHASANXUAT4,
                                    sc.SC_VTTB_TENCHUNGLOAI5,
                                    sc.SC_VTTB_SOLUONG5,
                                    sc.SC_VTTB_NHASANXUAT5,
                                    sc.SC_DIENAP,
                                    sc.SC_CQ,
                                    sc.SC_KQ,
                                    sc.SC_LOAI,
                                    sc.SC_TONGSOKH,
                                    sc.SC_TAISAN,
                                    sc.SC_GHICHU,
                                    sc.UserID,
                                    sc.SC_THIETBIDONGCAT_MSRCS
                                });

                count = lstSuCo1.Count();
                if (!_currentUser.IsInRole("Administrators"))
                {
                    objNGUOIDUNG = vDC.QLSC_NGUOIDUNGs.Where(x => x.UserID == _currentUser.UserID).SingleOrDefault();
                    if (objNGUOIDUNG != null)
                    {
                        lstSuCo1 = lstSuCo1.Where(x => x.DONVI_ID == objNGUOIDUNG.DONVI_ID);
                        count = lstSuCo1.Count();
                    }
                }
                //lstSuCo1 = lstSuCo1.Skip((pCurentPage) * vPageSize).Take(vPageSize).ToList();
                foreach (var it in lstSuCo1)
                {
                    DataRow row = dtTable.NewRow();
                    row["SC_ID"] = it.SC_ID;
                    row["SC_NGAYXAYRA"] = ClassCommon.HienThiNgayThangNam(Convert.ToDateTime(it.SC_NGAYXAYRA));
                    row["SC_GIOXAYRA"] = String.Format("{0:HH:mm}", it.SC_NGAYTAILAP);
                    row["SC_NGAYTAILAP"] = String.Format("{0:HH:mm}", it.SC_NGAYTAILAP);
                    row["SC_NOIDUNG"] = it.SC_NOIDUNG;
                    row["SC_NGUYENNHAN"] = it.SC_NGUYENNHAN;
                    row["SC_DONVI"] = it.DONVI_TEN;
                    row["SC_DONVI_TENRUTGON"] = it.DONVI_TENRUTGON;

                    row["SC_VTTB_TENCHUNGLOAI"] = it.SC_VTTB_TENCHUNGLOAI;
                    row["SC_VTTB_SOLUONG"] = it.SC_VTTB_SOLUONG;
                    row["SC_VTTB_NHASANXUAT"] = it.SC_VTTB_NHASANXUAT;

                    row["SC_VTTB_TENCHUNGLOAI2"] = it.SC_VTTB_TENCHUNGLOAI2;
                    row["SC_VTTB_SOLUONG2"] = it.SC_VTTB_SOLUONG2;
                    row["SC_VTTB_NHASANXUAT2"] = it.SC_VTTB_NHASANXUAT2;

                    row["SC_VTTB_TENCHUNGLOAI3"] = it.SC_VTTB_TENCHUNGLOAI3;
                    row["SC_VTTB_SOLUONG3"] = it.SC_VTTB_SOLUONG3;
                    row["SC_VTTB_NHASANXUAT3"] = it.SC_VTTB_NHASANXUAT3;

                    row["SC_VTTB_TENCHUNGLOAI4"] = it.SC_VTTB_TENCHUNGLOAI4;
                    row["SC_VTTB_SOLUONG4"] = it.SC_VTTB_SOLUONG4;
                    row["SC_VTTB_NHASANXUAT4"] = it.SC_VTTB_NHASANXUAT4;

                    row["SC_VTTB_TENCHUNGLOAI5"] = it.SC_VTTB_TENCHUNGLOAI5;
                    row["SC_VTTB_SOLUONG5"] = it.SC_VTTB_SOLUONG5;
                    row["SC_VTTB_NHASANXUAT5"] = it.SC_VTTB_NHASANXUAT5;

                    row["SC_DIENAP"] = it.SC_DIENAP == 1 ? "HT" : "";
                    row["SC_CQ"] = it.SC_CQ == 1 ? "x" : "";
                    row["SC_KQ"] = it.SC_KQ == 1 ? "x" : "";
                    row["SC_DIENAP"] = it.SC_DIENAP == 2 ? "HT" : "TT";
                    row["SC_PHANLOAI_TQ_DUONGTRUC"] = it.SC_LOAI == 1 ? "x" : "";
                    row["SC_PHANLOAI_TQ_NGARE"] = it.SC_LOAI == 2 ? "x" : "";
                    row["SC_PHANLOAI_VC_DUONGTRUC"] = it.SC_LOAI == 3 ? "x" : "";
                    row["SC_PHANLOAI_VC_NGARE"] = it.SC_LOAI == 4 ? "x" : "";
                    row["SC_PHANLOAI_TBA"] = it.SC_LOAI == 5 ? "x" : "";
                    row["SC_PHANLOAI_HA"] = it.SC_LOAI == 6 ? "x" : "";
                    row["SC_TONGSOKHACHHANG"] = it.SC_TONGSOKH;
                    row["SC_TAISAN_TBA"] = it.SC_TAISAN == 1 ? "x" : "";
                    row["SC_TAISAN_HA"] = it.SC_TAISAN == 2 ? "x" : "";
                    row["SC_THIETBIDONGCAT_MSRCS"] = it.SC_THIETBIDONGCAT_MSRCS;
                    var temp = objTAPTINController.Get_TapTin_By_ObjectID_LoaiID(it.SC_ID, (int)CommonEnum.TapTinObjectLoai.File);
                    string strFile = "";
                    if (temp.Count > 0)
                    {
                        
                        foreach (var file in temp)
                        {
                            strFile += "<a title='"+ file.FILE_NAME+"' href='" + vPathCommonData + "/" + file.FILE_NAME + "' target='_blank' style='padding-left:10px;'> ";
                            strFile += "<span class='glyphicon glyphicon-download-alt' style='color:blue;font-size:13px'></span>&emsp;";
                            //strFile += file.FILE_MOTA;
                            strFile += "</a>";
                            //strFile += temp.Count > 1? ",":"";
                        }
                        row["FILE"] = strFile;
                    }

                    row["SC_GHICHU"] = it.SC_GHICHU;
                    row["UserID"] = it.UserID;
                    dtTable.Rows.Add(row);
                }


                dgDanhSach.DataSource = dtTable;
                dgDanhSach.PageSize = vPageSize;
                dgDanhSach.VirtualItemCount = count;
                dgDanhSach.CurrentPageIndex = pCurentPage;
                dgDanhSach.DataBind();
                ViewState["Table_data"] = dtTable;
            }
            catch (Exception ex)
            {

            }

        }

        protected void dgDanhSach_PreRender(object sender, EventArgs e)
        {
            Table table = Controls[0] as Table;
            if (table != null && table.Rows.Count > 0)
            {
                table.Rows[0].TableSection = TableRowSection.TableHeader;
                table.Rows[table.Rows.Count - 1].TableSection = TableRowSection.TableFooter;
                FieldInfo field = typeof(WebControl).GetField("tagKey", BindingFlags.Instance | BindingFlags.NonPublic);
                foreach (TableCell cell in table.Rows[0].Cells)
                {
                    field.SetValue(cell, HtmlTextWriterTag.Th);
                }
            }
            base.OnPreRender(e);
        }


        #region Group header
        //property for storing of information about merged columns
        private MergedColumnsInfo info
        {
            get
            {
                if (ViewState["info"] == null)
                    ViewState["info"] = new MergedColumnsInfo();
                return (MergedColumnsInfo)ViewState["info"];
            }
        }
        [Serializable]
        private class MergedColumnsInfo
        {
            // indexes of merged columns
            public List<int> MergedColumns = new List<int>();
            // key-value pairs: key = the first column index, value = number of the merged columns
            public Hashtable StartColumns = new Hashtable();
            // key-value pairs: key = the first column index, value = common title of the merged columns 
            public Hashtable Titles = new Hashtable();

            //parameters: the merged columns indexes, common title of the merged columns 
            public void AddMergedColumns(int[] columnsIndexes, string title)
            {
                MergedColumns.AddRange(columnsIndexes);
                StartColumns.Add(columnsIndexes[0], columnsIndexes.Length);
                Titles.Add(columnsIndexes[0], title);
            }

        }
        private void RenderHeader(HtmlTextWriter output, Control container)
        {
            for (int i = 0; i < container.Controls.Count; i++)
            {
                TableCell cell = (TableCell)container.Controls[i];

                //stretch non merged columns for two rows
                if (!info.MergedColumns.Contains(i))
                {
                    cell.Attributes["rowspan"] = "2";
                    cell.RenderControl(output);
                }
                else //render merged columns common title
                    if (info.StartColumns.Contains(i))
                {
                    output.Write(string.Format("<td style='width:100px;padding:0px;'  align='center' colspan='{0}'>{1}</td>",
                             info.StartColumns[i], info.Titles[i]));
                }
            }

            //close the first row 
            output.Write("</tr>");
            //set attributes for the second row

            dgDanhSach.HeaderStyle.AddAttributesToRender(output);
            //start the second row
            //  output.RenderBeginTag("tr");
            output.RenderBeginTag("tr");

            //render the second row (only the merged columns)
            for (int i = 0; i < info.MergedColumns.Count; i++)
            {
                TableCell cell = (TableCell)container.Controls[info.MergedColumns[i]];
                cell.RenderControl(output);
            }
            output.RenderEndTag();
        }
        #endregion


        #region Phương thức phân trang
        LinkButton lbFirstPage = null;
        LinkButton lbPreviousPage = null;
        LinkButton lbNextPage = null;
        LinkButton lbLastPage = null;
        LinkButton lblToltalRecord = null;
        LinkButton lblCurentViewerRecord = null;
        LinkButton lblPageSize = null;
        DropDownList ddlPageSize = null;

        protected void dgDanhSach_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
        }

        protected void dgDanhSach_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgDanhSach.CurrentPageIndex = e.NewPageIndex;
            Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"] = Int16.Parse(e.NewPageIndex.ToString());
            vCurentPage = Int16.Parse(e.NewPageIndex.ToString());
            LoadDanhSach(vCurentPage,0);
        }

        protected void dgDanhSach_ItemCreated(object sender, DataGridItemEventArgs e)
        {
            Custom_Paging(sender, e, dgDanhSach.CurrentPageIndex, dgDanhSach.VirtualItemCount, dgDanhSach.PageCount);

            if (e.Item.ItemType == ListItemType.Header)
            {
                e.Item.SetRenderMethodDelegate(RenderHeader);
            }
        }

        protected void dgCustom_Init(object sender, EventArgs e)
        {
            lbFirstPage = new LinkButton();
            lbFirstPage.ID = "lbFirstPage";
            lbFirstPage.Text = "<div class='col-xs-12 col-sm-6 pageCustom'> <<";
            lbFirstPage.CssClass = "paging_btn btn_first";
            lbFirstPage.Click += new EventHandler(lbFirstPage_Click);

            lbPreviousPage = new LinkButton();
            lbPreviousPage.ID = "lbPreviousPage";
            lbPreviousPage.Text = "<";
            lbPreviousPage.CssClass = "paging_btn btn_previous";
            lbPreviousPage.Click += new EventHandler(lbPreviousPage_Click);

            lbNextPage = new LinkButton();
            lbNextPage.ID = "lbNextPage";
            lbNextPage.Text = ">";
            lbNextPage.CssClass = "paging_btn btn_next";
            lbNextPage.Click += new EventHandler(lbNextPage_Click);

            lbLastPage = new LinkButton();
            lbLastPage.ID = "lbLastPage";
            lbLastPage.Text = ">></div>";
            lbLastPage.CssClass = "paging_btn btn_last";
            lbLastPage.Click += new EventHandler(lbLastPage_Click);

            lblToltalRecord = new LinkButton();
            lblToltalRecord.ID = "lblToltalRecord";
            lblToltalRecord.CssClass = "paging_label fright";

            lblCurentViewerRecord = new LinkButton();
            lblCurentViewerRecord.ID = "lblZ";
            lblCurentViewerRecord.CssClass = "paging_label fright";

            lblPageSize = new LinkButton();
            lblPageSize.ID = "lblPageSize";
            lblPageSize.CssClass = "paging_label lblPageSize";

            ddlPageSize = new DropDownList();
            ddlPageSize.ID = "ddlPageSize";

            ddlPageSize.CssClass = " form-control input-sm ddl_pagesize fright";
            ddlPageSize.AutoPostBack = true;
            //ListItem vPageSize1 = new ListItem("1", "1");
            ListItem vPageSize2 = new ListItem("5", "5");
            ListItem vPageSize10 = new ListItem("10", "10");
            ListItem vPageSize20 = new ListItem("20", "20");
            ListItem vPageSize30 = new ListItem("30", "30");
            ListItem vPageSize50 = new ListItem("50", "50");
            ListItem vPageSize100 = new ListItem("100", "100");
            ListItem vPageSize200 = new ListItem("200", "200");
            //ddlPageSize.Items.Add(vPageSize1);
            ddlPageSize.Items.Add(vPageSize2);
            ddlPageSize.Items.Add(vPageSize10);
            ddlPageSize.Items.Add(vPageSize20);
            ddlPageSize.Items.Add(vPageSize30);
            ddlPageSize.Items.Add(vPageSize50);
            ddlPageSize.Items.Add(vPageSize100);
            ddlPageSize.Items.Add(vPageSize200);

            ddlPageSize.SelectedIndexChanged += DdlPageSize_SelectedIndexChanged;
        }

        void DdlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserInfo _currentUser = UserController.GetCurrentUserInfo();

            Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_PageSize"] = Int16.Parse(ddlPageSize.SelectedValue);
            vPageSize = Int16.Parse(ddlPageSize.SelectedValue);
            Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"] = 0;
            vCurentPage = 0;
            LoadDanhSach(vCurentPage,0);
        }

        protected void Get_Cache()
        {
            if (Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_PageSize"] != null)
            {
                vPageSize = Int32.Parse(Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_PageSize"].ToString());
            }
            if (Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"] != null)
            {
                vCurentPage = Int32.Parse(Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"].ToString());
            }
        }

        protected void Custom_Paging(object sender, DataGridItemEventArgs e, int vCurrentPageIndex, int vVirtualItemCount, int vPageCount)
        {

            if (vCurrentPageIndex == 0)
            {
                lbPreviousPage.Enabled = false;
                lbFirstPage.Enabled = false;
            }
            else
            {
                lbPreviousPage.Enabled = true;
                lbFirstPage.Enabled = true;
            }
            if (vCurrentPageIndex + 1 == vPageCount)
            {
                lbLastPage.Enabled = false;
                lbNextPage.Enabled = false;
            }
            else
            {
                lbLastPage.Enabled = true;
                lbNextPage.Enabled = true;
            }
            if (e.Item.ItemType == ListItemType.Pager)
            {
                e.Item.Cells[0].Text.Replace("&nbsp;", "");
                TableCell Pager = (TableCell)e.Item.Controls[0];
                for (int i = 0; i < Pager.Controls.Count; i++)
                {
                    try
                    {

                        object pgNumbers = Pager.Controls[i];
                        int endPagingIndex = Pager.Controls.Count - 1;
                        string Typea = pgNumbers.GetType().Name;
                        if (pgNumbers.GetType().Name == "DataGridLinkButton")
                        {
                            LinkButton lb = (LinkButton)pgNumbers;
                            lb.CssClass = "paging_item";
                            if (lb.Text == "...")
                            {
                                lb.Visible = false;
                            }
                            if (vPageCount > 5)
                            {
                                if (vCurrentPageIndex >= 2 && vPageCount > (vCurrentPageIndex + 2))
                                {
                                    if (Int32.Parse(lb.Text) > (vCurrentPageIndex + 3))
                                    {
                                        lb.Visible = false;
                                    }
                                    if (Int32.Parse(lb.Text) < (vCurrentPageIndex - 1))
                                    {
                                        lb.Visible = false;
                                    }
                                }
                                else if (vCurrentPageIndex < 2)
                                {
                                    if (Int32.Parse(lb.Text) > 5)
                                    {
                                        lb.Visible = false;
                                    }
                                }
                                else
                                {
                                    if (Int32.Parse(lb.Text) < (vPageCount - 4))
                                    {
                                        lb.Visible = false;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //lblThongBao.Text = ex.ToString();
                    }

                }
                // add the previous page link
                if (e.Item.Cells[0].FindControl("lbPreviousPage") == null)
                {
                    e.Item.Cells[0].Controls.AddAt(0, new LiteralControl(""));
                    e.Item.Cells[0].Controls.AddAt(0, lbPreviousPage);
                }
                if (e.Item.Cells[0].FindControl("lbFirstPage") == null)
                {
                    e.Item.Cells[0].Controls.AddAt(0, new LiteralControl(""));
                    e.Item.Cells[0].Controls.AddAt(0, lbFirstPage);
                }
                if (e.Item.Cells[0].FindControl("lbNextPage") == null)
                {
                    e.Item.Cells[0].Controls.Add(new LiteralControl(""));
                    e.Item.Cells[0].Controls.Add(lbNextPage);
                }
                if (e.Item.Cells[0].FindControl("lbLastPage") == null)
                {
                    e.Item.Cells[0].Controls.Add(new LiteralControl(""));
                    e.Item.Cells[0].Controls.Add(lbLastPage);
                }
                if (e.Item.Cells[0].FindControl("lblToltalRecord") == null)
                {
                    e.Item.Cells[0].Controls.AddAt(0, new LiteralControl(""));
                    e.Item.Cells[0].Controls.AddAt(0, lbPreviousPage);
                }

                // Add total record in paging

                if (e.Item.Cells[0].FindControl("ddlPageSize") == null)
                {
                    e.Item.Cells[0].Controls.AddAt(0, new LiteralControl(""));
                    e.Item.Cells[0].Controls.AddAt(0, lblPageSize);
                    lblPageSize.Text = "Số dòng hiển thị: ";
                }
                if (e.Item.Cells[0].FindControl("ddlPageSize") == null)
                {
                    e.Item.Cells[0].Controls.AddAt(0, new LiteralControl(""));
                    ddlPageSize.SelectedValue = vPageSize.ToString();
                    e.Item.Cells[0].Controls.AddAt(0, ddlPageSize);
                }
                if (e.Item.Cells[0].FindControl("lblZ") == null)
                {
                    if (dgDanhSach.VirtualItemCount != 0)
                    {
                        lblCurentViewerRecord.Text = " " + ((dgDanhSach.CurrentPageIndex * dgDanhSach.PageSize) + 1).ToString() + " - " + (dgDanhSach.CurrentPageIndex + 1 == dgDanhSach.PageCount ? dgDanhSach.VirtualItemCount.ToString() : ((dgDanhSach.CurrentPageIndex + 1) * dgDanhSach.PageSize).ToString()).ToString();
                        lblCurentViewerRecord.Text += " trong tổng số " + dgDanhSach.VirtualItemCount.ToString() + "";
                        e.Item.Cells[0].Controls.AddAt(0, new LiteralControl(""));
                        e.Item.Cells[0].Controls.AddAt(0, lblCurentViewerRecord);
                    }
                    else
                    {
                        lblCurentViewerRecord.Text = "Chưa có dữ liệu.";
                        e.Item.Cells[0].Controls.AddAt(0, new LiteralControl(""));
                        e.Item.Cells[0].Controls.AddAt(0, lblCurentViewerRecord);
                    }
                }
            }
        }

        void lbLastPage_Click(object sender, EventArgs e)
        {
            UserInfo _currentUser = UserController.GetCurrentUserInfo();
            LoadDanhSach(dgDanhSach.PageCount - 1,0);
            Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"] = (dgDanhSach.PageCount - 1);
        }

        void lbNextPage_Click(object sender, EventArgs e)
        {
            UserInfo _currentUser = UserController.GetCurrentUserInfo();
            if (dgDanhSach.CurrentPageIndex < (dgDanhSach.PageCount - 1))
            {
                LoadDanhSach(dgDanhSach.CurrentPageIndex + 1,0);
                Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"] = (dgDanhSach.CurrentPageIndex);
            }
        }

        void lbPreviousPage_Click(object sender, EventArgs e)
        {
            UserInfo _currentUser = UserController.GetCurrentUserInfo();
            if (dgDanhSach.CurrentPageIndex > 0)
            {
                LoadDanhSach(dgDanhSach.CurrentPageIndex - 1,0);
                Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"] = (dgDanhSach.CurrentPageIndex);
            }
        }

        void lbFirstPage_Click(object sender, EventArgs e)
        {
            UserInfo _currentUser = UserController.GetCurrentUserInfo();
            LoadDanhSach(0,0);
            Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"] = 0;
        }
        #endregion

        protected void dgSua(object sender, EventArgs e)
        {
            Session.Remove("dgDanhSach");
            HtmlAnchor html = (HtmlAnchor)sender;
            int vSC_ID = Convert.ToInt32(html.HRef.ToString());
            Response.Redirect(EditUrl(string.Empty, string.Empty, "create_update", "ID=" + vSC_ID));


        }

        protected void dgXoa(object sender, EventArgs e)
        {

        }

        protected void btn_TK_Tim_Click(object sender, EventArgs e)
        {
            try
            {
                UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
                var UserID = _currentUser.UserID;
                Session[PortalSettings.ActiveTab.TabID + UserID + "tukhoa_search"] = txt_TK_NoiDung.Text.Trim();

                if (drpDonVi.SelectedValue != "")
                    Session[PortalSettings.ActiveTab.TabID + UserID + "tukhoa_donvi"] = int.Parse(drpDonVi.SelectedValue);
                else
                    Session[PortalSettings.ActiveTab.TabID + UserID + "tukhoa_donvi"] = null;

                if (txtTuNgay.SelectedDate != null)
                    Session[PortalSettings.ActiveTab.TabID + UserID + "tukhoa_tungay"] = txtTuNgay.SelectedDate;
                if (txtDenNgay.SelectedDate != null)
                    Session[PortalSettings.ActiveTab.TabID + UserID + "tukhoa_denngay"] = txtDenNgay.SelectedDate;

                LoadDanhSach(vCurentPage,0);
            }
            catch (Exception ex)
            {

            }

        }

        protected void txt_TK_NoiDung_TextChanged(object sender, EventArgs e)
        {
            btn_TK_Tim_Click(sender, new EventArgs());
        }

        protected void drpDonVi_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            btn_TK_Tim_Click(sender, new EventArgs());
        }

        protected void txtTuNgay_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            btn_TK_Tim_Click(sender, new EventArgs());
        }

        protected void txtDenNgay_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            btn_TK_Tim_Click(sender, new EventArgs());
        }

        protected void btn_XuatExcel_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    LoadDanhSach(0,1);
                    var dt = new DataTable();
                    dt = ViewState["Table_data"] as DataTable;
                    if (dt.Rows.Count > 0)
                    {
                        var ExistFile = Server.MapPath(vPathCommonBieuMau_FN + "BAO_CAO_THANG.xlsx");
                        var File = new System.IO.FileInfo(ExistFile);
                        using (ExcelPackage pck = new ExcelPackage(File))
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.First();
                            int vIndexRow = 11;
                            //Gán đơn vị
                            if (!_currentUser.IsInRole("Administrators"))
                            {
                                var objND = vDC.QLSC_NGUOIDUNGs.Where(x => x.UserID == _currentUser.UserID).SingleOrDefault();
                                if (objND != null)
                                {
                                    var tenDV = vDC.QLSC_DONVIs.Where(x => x.DONVI_ID == objND.DONVI_ID).SingleOrDefault().DONVI_TEN;
                                    ws.Cells[2, 1].Value = "ĐIỆN LỰC " + tenDV.ToUpper();
                                    ws.Cells[4, 13].Value = tenDV + ", ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                                }
                              
                            }
                            else
                            {
                                ws.Cells[2, 1].Value = "";
                                ws.Cells[4, 13].Value = "Sóc Trăng" + ", ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                            }
                            if (drpDonVi.SelectedValue != "0")
                            {
                                ws.Cells[2, 1].Value = "ĐIỆN LỰC " + drpDonVi.Text.ToUpper();
                                ws.Cells[4, 13].Value = drpDonVi.Text + ", ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                            }

                            //Gán thời gian cho báo cáo
                            ws.Cells[6, 1].Value = "Báo cáo tình hình sự cố từ ngày " + ClassCommon.HienThiNgayThangNam(txtTuNgay.SelectedDate??DateTime.Now.AddDays(-7)) + " đến ngày " + ClassCommon.HienThiNgayThangNam(txtDenNgay.SelectedDate ?? DateTime.Now);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                ws.Cells[vIndexRow, 1].Value = (i + 1);
                                ws.Cells[vIndexRow, 2].Value = dt.Rows[i]["SC_NGAYXAYRA"];
                                ws.Cells[vIndexRow, 3].Value = dt.Rows[i]["SC_GIOXAYRA"];
                                ws.Cells[vIndexRow, 4].Value = dt.Rows[i]["SC_NGAYTAILAP"];
                                ws.Cells[vIndexRow, 5].Value = dt.Rows[i]["SC_NOIDUNG"];
                                ws.Cells[vIndexRow, 6].Value = dt.Rows[i]["SC_NGUYENNHAN"];
                                ws.Cells[vIndexRow, 7].Value = dt.Rows[i]["SC_DONVI_TENRUTGON"];

                                ws.Cells[vIndexRow, 8].Value = dt.Rows[i]["SC_VTTB_TENCHUNGLOAI"];
                                ws.Cells[vIndexRow, 9].Value = dt.Rows[i]["SC_VTTB_SOLUONG"];
                                ws.Cells[vIndexRow, 10].Value = dt.Rows[i]["SC_VTTB_NHASANXUAT"];

                                ws.Cells[vIndexRow, 11].Value = dt.Rows[i]["SC_VTTB_TENCHUNGLOAI2"];
                                ws.Cells[vIndexRow, 12].Value = dt.Rows[i]["SC_VTTB_SOLUONG2"];
                                ws.Cells[vIndexRow, 13].Value = dt.Rows[i]["SC_VTTB_NHASANXUAT2"];

                                ws.Cells[vIndexRow, 14].Value = dt.Rows[i]["SC_VTTB_TENCHUNGLOAI3"];
                                ws.Cells[vIndexRow, 15].Value = dt.Rows[i]["SC_VTTB_SOLUONG3"];
                                ws.Cells[vIndexRow, 16].Value = dt.Rows[i]["SC_VTTB_NHASANXUAT3"];

                                ws.Cells[vIndexRow, 17].Value = dt.Rows[i]["SC_VTTB_TENCHUNGLOAI4"];
                                ws.Cells[vIndexRow, 18].Value = dt.Rows[i]["SC_VTTB_SOLUONG4"];
                                ws.Cells[vIndexRow, 19].Value = dt.Rows[i]["SC_VTTB_NHASANXUAT4"];

                                ws.Cells[vIndexRow, 20].Value = dt.Rows[i]["SC_VTTB_TENCHUNGLOAI5"];
                                ws.Cells[vIndexRow, 21].Value = dt.Rows[i]["SC_VTTB_SOLUONG5"];
                                ws.Cells[vIndexRow, 22].Value = dt.Rows[i]["SC_VTTB_NHASANXUAT5"];

                                ws.Cells[vIndexRow, 23].Value = dt.Rows[i]["SC_DIENAP"];                                
                                ws.Cells[vIndexRow, 24].Value = dt.Rows[i]["SC_CQ"];
                                ws.Cells[vIndexRow, 25].Value = dt.Rows[i]["SC_KQ"];
                                ws.Cells[vIndexRow, 26].Value = dt.Rows[i]["SC_PHANLOAI_TQ_DUONGTRUC"];
                                ws.Cells[vIndexRow, 27].Value = dt.Rows[i]["SC_PHANLOAI_TQ_NGARE"];
                                ws.Cells[vIndexRow, 28].Value = dt.Rows[i]["SC_PHANLOAI_VC_DUONGTRUC"];
                                ws.Cells[vIndexRow, 29].Value = dt.Rows[i]["SC_PHANLOAI_VC_NGARE"];
                                ws.Cells[vIndexRow, 30].Value = dt.Rows[i]["SC_PHANLOAI_TBA"];
                                ws.Cells[vIndexRow, 31].Value = dt.Rows[i]["SC_PHANLOAI_HA"];
                                ws.Cells[vIndexRow, 32].Value = dt.Rows[i]["SC_THIETBIDONGCAT_MSRCS"];
                                ws.Cells[vIndexRow, 33].Value = dt.Rows[i]["SC_TONGSOKHACHHANG"];
                                ws.Cells[vIndexRow, 34].Value = dt.Rows[i]["SC_TAISAN_TBA"];
                                ws.Cells[vIndexRow, 35].Value = dt.Rows[i]["SC_TAISAN_HA"];
                                ws.Cells[vIndexRow, 36].Value = dt.Rows[i]["SC_GHICHU"];
                                vIndexRow++;
                                ws.InsertRow(vIndexRow, 1, vIndexRow - 1);
                            }
                            ws.DeleteRow(vIndexRow);
                            Byte[] fileBytes = pck.GetAsByteArray();
                            Response.Clear();
                            Response.Buffer = true;
                            Response.AddHeader("content-disposition", "attachment;filename=" + "Bao_Cao_Su_Co_Dien_Luc_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_"
                                 + DateTime.Now.Year + ".xlsx");
                            Response.Charset = "";
                            Response.ContentType = "application/vnd.ms-excel";
                            StringWriter sw = new StringWriter();
                            Response.BinaryWrite(fileBytes);
                            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                        }
                    }
                }
                catch (Exception Ex)
                { }
            }
        }
    }
}