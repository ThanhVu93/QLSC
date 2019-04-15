using DotNetNuke.Common;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Permissions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace QLSC
{
    public partial class LOAISUCO_DS : DotNetNuke.Entities.Modules.UserModuleBase
    {
        #region Properties 
        int vPageSize = ClassParameter.vPageSize;
        int vCurentPage = 0;

        QLSCDataContext vDC = new QLSCDataContext();
        ClassCommon clsCommon = new ClassCommon();
        UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
        DataTable dtTable = new DataTable();
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Session[TabId.ToString() + "_Message"] as string) && !String.IsNullOrEmpty(Session[TabId.ToString() + "_Type"] as string))
                {
                    if (Session[TabId.ToString() + "_Message"].ToString() != "" && Session[TabId.ToString() + "_Type"].ToString() != "")
                    {
                        ClassCommon.THONGBAO_TOASTR(Page, null, _currentUser, Session[TabId.ToString() + "_Message"].ToString(), "Thông báo", Session[TabId.ToString() + "_Type"].ToString());
                    }
                    Session[TabId.ToString() + "_Message"] = "";
                    Session[TabId.ToString() + "_Type"] = "";
                }
                DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(DotNetNuke.Framework.JavaScriptLibraries.CommonJs.jQuery);
                DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(DotNetNuke.Framework.JavaScriptLibraries.CommonJs.DnnPlugins);
                DotNetNuke.UI.Utilities.ClientAPI.RegisterClientReference(this.Page, DotNetNuke.UI.Utilities.ClientAPI.ClientNamespaceReferences.dnn);
                Get_Cache();
                if (!IsPostBack)
                {
                    LoadDS(0);
                    //if (ModulePermissionController.CanAdminModule(this.ModuleConfiguration) == true)
                    //{
                    //    btn_ThemMoi.Visible = true;
                    //    int count_columns_table = dgDanhSach.Columns.Count;
                    //}
                }
            }
            catch (Exception ex)
            {
                ClassCommon.THONGBAO_TOASTR(Page, ex, _currentUser, "Có lỗi trong quá trình xữ lý, vui lòng liên hệ với quản trị!", "Thông báo lỗi", "error");

            }
        }      

        public int countLoaiSC()
        {
            var count = (from lsc in vDC.QLSC_LOAISUCOs
                         where SqlMethods.Like(lsc.LOAISC_TEN, "%" + txt_TK_NoiDung.Text.Trim() + "%") || SqlMethods.Like(lsc.LOAISC_GHICHU, "%" + txt_TK_NoiDung.Text.Trim() + "%")
                         select lsc).Count();            
            return count;
        }

        #endregion

        #region Methods


        /// <summary>
        /// Load danh sách người dùng lên datagrid
        /// </summary>
        protected void LoadDS(int pCurentPage)
        {
            try
            {
                dtTable = new DataTable();
                dtTable.Columns.Add("STT");
                dtTable.Columns.Add("LOAISC_ID");
                dtTable.Columns.Add("LOAISC_TEN");
                dtTable.Columns.Add("LOAISC_GHICHU");
                var lstLoaiSC = (from lsc in vDC.QLSC_LOAISUCOs
                                 where SqlMethods.Like(lsc.LOAISC_TEN, "%" + txt_TK_NoiDung.Text.Trim() + "%") || SqlMethods.Like(lsc.LOAISC_GHICHU, "%" + txt_TK_NoiDung.Text.Trim() + "%")
                                 select lsc).ToList();
                lstLoaiSC.Skip((pCurentPage) * vPageSize).Take(vPageSize).ToList();
                if (lstLoaiSC != null)
                {
                    if (lstLoaiSC.Count() > 0)
                    {
                        foreach (var obj in lstLoaiSC)
                        {
                            DataRow row = dtTable.NewRow();
                            row["LOAISC_ID"] = obj.LOAISC_ID;
                            row["LOAISC_TEN"] = obj.LOAISC_TEN;
                            row["LOAISC_GHICHU"] = obj.LOAISC_GHICHU;

                            dtTable.Rows.Add(row);
                        }
                    }
                }

                dgDanhSach.DataSource = dtTable;
                dgDanhSach.PageSize = vPageSize;
                dgDanhSach.VirtualItemCount = countLoaiSC();//dsNhomHH.Count();
                dgDanhSach.CurrentPageIndex = pCurentPage;
                dgDanhSach.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region DataGrid Events
        private int vStt = 1;
        public string STT()
        {
            return (((dgDanhSach.CurrentPageIndex) * vPageSize) + vStt++).ToString();
        }

        protected void dgDanhSach_ItemDataBound(object sender, DataGridItemEventArgs e)
        { }

        protected void dgDanhSach_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
            dgDanhSach.CurrentPageIndex = e.NewPageIndex;
            Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"] = Int16.Parse(e.NewPageIndex.ToString());
            vCurentPage = Int16.Parse(e.NewPageIndex.ToString());
            LoadDS(vCurentPage);
        }

        protected void dgDanhSach_ItemCreated(object sender, DataGridItemEventArgs e)
        {
            Custom_Paging(sender, e, dgDanhSach.CurrentPageIndex, dgDanhSach.VirtualItemCount, dgDanhSach.PageCount);
        }
        #endregion

        #region Page Events
        protected void btn_ThemMoi_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL("create_update", "mid=" + this.ModuleId, "title=Thêm mới loại sự cố", "LOAISC_ID=0"));
        }
        protected void dgDanhSach_Sua(object sender, EventArgs e)
        {
            HtmlAnchor html = (HtmlAnchor)sender;
            int loaisc_id = Convert.ToInt32(html.HRef.ToString());
            Response.Redirect(Globals.NavigateURL("create_update", "mid=" + this.ModuleId, "title=Cập nhật loại sự cố", "LOAISC_ID=" + loaisc_id));
        }

        protected void dgDanhSach_Xoa(object sender, EventArgs e)
        {
            HtmlAnchor html = (HtmlAnchor)sender;
            int loaisc_id = Convert.ToInt32(html.HRef.ToString());

            try
            {
                var objCheck_KhoaNgoai = vDC.QLSC_SUCOs.Where(x => x.LOAISC_ID == loaisc_id).Count();
                if (objCheck_KhoaNgoai == 0)
                {
                    var objLoaiSC = (from obj in vDC.QLSC_LOAISUCOs
                                        where obj.LOAISC_ID == loaisc_id
                                        select obj).SingleOrDefault();
                    vDC.QLSC_LOAISUCOs.DeleteOnSubmit(objLoaiSC);
                    vDC.SubmitChanges();
                    LoadDS(0);
                    ClassCommon.ShowToastr(Page, "Xóa loại sự cố thành công!", "Thông báo", "success");
                }
                else
                {
                    ClassCommon.ShowToastr(Page, "Loại sự cố đã được sử dụng. Không thể xóa!", "Thông báo", "error");
                }
            }
            catch (Exception ex)
            {
                ClassCommon.ShowToastr(Page, "Có lỗi xảy ra vui lòng liên hệ quản trị", "Thông báo lỗi", "error");
                //log.Error("", ex);
            }

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
        protected void dgCustom_Init(object sender, EventArgs e)
        {
            lbFirstPage = new LinkButton();
            lbFirstPage.ID = "lbFirstPage";
            lbFirstPage.Text = "<<";
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
            lbLastPage.Text = ">>";
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
            lblPageSize.CssClass = "fright paging_label";

            ddlPageSize = new DropDownList();
            ddlPageSize.ID = "ddlPageSize";

            ddlPageSize.CssClass = "fright form-control input-sm ddl_pagesize";
            ddlPageSize.AutoPostBack = true;
            ListItem vPageSize2 = new ListItem("5", "5");
            ListItem vPageSize10 = new ListItem("10", "10");
            ListItem vPageSize20 = new ListItem("20", "20");
            ListItem vPageSize30 = new ListItem("30", "30");
            ListItem vPageSize50 = new ListItem("50", "50");
            ListItem vPageSize100 = new ListItem("100", "100");
            ListItem vPageSize200 = new ListItem("200", "200");
            ListItem vPageSize9999 = new ListItem("Tất cả", "9999");
            ddlPageSize.Items.Add(vPageSize2);
            ddlPageSize.Items.Add(vPageSize10);
            ddlPageSize.Items.Add(vPageSize20);
            ddlPageSize.Items.Add(vPageSize30);
            ddlPageSize.Items.Add(vPageSize50);
            ddlPageSize.Items.Add(vPageSize100);
            ddlPageSize.Items.Add(vPageSize200);
            ddlPageSize.Items.Add(vPageSize9999);

            ddlPageSize.SelectedIndexChanged += DdlPageSize_SelectedIndexChanged;
        }
        void DdlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();

            Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_PageSize"] = Int16.Parse(ddlPageSize.SelectedValue);
            vPageSize = Int16.Parse(ddlPageSize.SelectedValue);
            Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"] = 0;
            vCurentPage = 0;
            LoadDS(vCurentPage);
        }
        protected void Get_Cache()
        {
            UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();

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
                        lblThongBao.Text = ex.ToString();
                        //log.Error("", ex);
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
                        lblCurentViewerRecord.Text += " trong tổng số " + dgDanhSach.VirtualItemCount + "";
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
            UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
            LoadDS(dgDanhSach.PageCount - 1);
            Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"] = (dgDanhSach.PageCount - 1);
        }
        void lbNextPage_Click(object sender, EventArgs e)
        {
            UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
            if (dgDanhSach.CurrentPageIndex < (dgDanhSach.PageCount - 1))
            {
                LoadDS(dgDanhSach.CurrentPageIndex + 1);
                Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"] = (dgDanhSach.CurrentPageIndex);
            }
        }
        void lbPreviousPage_Click(object sender, EventArgs e)
        {
            UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
            if (dgDanhSach.CurrentPageIndex > 0)
            {
                LoadDS(dgDanhSach.CurrentPageIndex - 1);
                Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"] = (dgDanhSach.CurrentPageIndex);
            }
        }
        void lbFirstPage_Click(object sender, EventArgs e)
        {
            UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
            LoadDS(0);
            Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"] = 0;
        }
        #endregion

        protected void drpDonVi_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadDS(0);
        }

        protected void txt_TK_NoiDung_TextChanged(object sender, EventArgs e)
        {
            LoadDS(0);
        }
    }
}
