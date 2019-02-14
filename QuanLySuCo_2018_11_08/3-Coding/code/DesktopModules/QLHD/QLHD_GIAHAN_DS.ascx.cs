using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Users;
using DotNetNuke.Instrumentation;
using DotNetNuke.Security.Permissions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.IO;
using Telerik.Web.UI;
using System.Collections;
using System.Reflection;

namespace QLHD
{
    public partial class QLHD_GIAHAN_DS : DotNetNuke.Entities.Modules.UserModuleBase
    {
        #region Khai báo, định nghĩa đối tượng
        private readonly ILog log = LoggerSource.Instance.GetLogger(typeof(QLHD_GIAHAN_DS).FullName);

        public string vPathCommon = ClassParameter.vPathCommon;
        public string vPathCommonJS = ClassParameter.vPathCommonJavascript;
        public string vPathCommonBieuMau = ClassParameter.vPathCommonBieuMau;
        int vPageSize = ClassParameter.vPageSize;
        int vCurentPage = 0;
        DataTable dtTable;
        QLHDDataContext vDC = new QLHDDataContext();
        ClassCommon clsCommon = new ClassCommon();
        UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
        int HD_ID = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            HD_ID = Convert.ToInt32(Request.QueryString["HD_ID"]);
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
                Get_Cache();
                if (!IsPostBack)
                {
                    try
                    {

                        LoadDanhSach(vCurentPage);
                        Mercolum();
                    }
                    catch (Exception ex)
                    {
                        ClassCommon.ShowToastr(Page, ex + "", "Thông báo lỗi", "error");
                        log.Error("", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ClassCommon.THONGBAO_TOASTR(Page, ex, _currentUser, "Có lỗi trong quá trình xử lý, vui lòng liên hệ với quản trị!", "Thông báo lỗi", "error");
                log.Error("", ex);
            }
        }
        public void LoadDanhSach(int pCurentPage)
        {
            int o_Count = 0;
            try
            {
                List<QLHD_GIAHANHD> objQLHD_GIAHANHDs = new List<QLHD_GIAHANHD>();
                objQLHD_GIAHANHDs = vDC.QLHD_GIAHANHDs.Where(x => x.HD_ID == HD_ID).ToList();
                objQLHD_GIAHANHDs = objQLHD_GIAHANHDs.Skip((pCurentPage) * vPageSize).Take(vPageSize).ToList();
                dgDanhSach.DataSource = objQLHD_GIAHANHDs;
                dgDanhSach.PageSize = vPageSize;
                dgDanhSach.CurrentPageIndex = pCurentPage;
                dgDanhSach.VirtualItemCount = o_Count;
                dgDanhSach.DataBind();

            }
            catch (Exception ex)
            {
                log.Error("", ex);
            }
        }
        #region MergeColumn
        public void Mercolum()
        {
            info.AddMergedColumns(new int[] { 1, 2 }, "Hợp đồng");
            info.AddMergedColumns(new int[] { 3, 4 }, "Thi công");
            info.AddMergedColumns(new int[] { 5, 6 }, "Bảo lãnh thực hiện");
            info.AddMergedColumns(new int[] { 7, 8 }, "Bảo lãnh vật tư");
            info.AddMergedColumns(new int[] { 9, 10 }, "Bảo lãnh tạm ứng");
        }

        #endregion
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
                    output.Write(string.Format("<td style='width:100px' align='center' colspan='{0}'>{1}</td>",
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

        #region Phương thức, sự kiện cho DataGrid

        private int vStt = 1;
        public string STT()
        {
            return (((dgDanhSach.CurrentPageIndex) * vPageSize) + vStt++).ToString();
        }

        protected void dgDanhSach_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
            dgDanhSach.CurrentPageIndex = e.NewPageIndex;
            Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"] = Int16.Parse(e.NewPageIndex.ToString());
            vCurentPage = Int16.Parse(e.NewPageIndex.ToString());
            LoadDanhSach(vCurentPage);
        }
        protected void dgDanhSach_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            //check if it is a header row
            //since allowsorting is set to true, column names are added as command arguments to
            //the linkbuttons by DOTNET API
            if (e.Item.ItemType == ListItemType.Header)
            {

            }
        }

        protected void dgDanhSach_ItemCreated(object sender, DataGridItemEventArgs e)
        {
            Custom_Paging(sender, e, dgDanhSach.CurrentPageIndex, dgDanhSach.VirtualItemCount, dgDanhSach.PageCount);
            if (e.Item.ItemType == ListItemType.Header)
            {
                e.Item.SetRenderMethodDelegate(RenderHeader);
            }
        }
        #endregion
        #region Phương thức, sự kiện cho Form Search
        protected void txt_TK_NoiDung_TextChanged(object sender, EventArgs e)
        {
            btn_TK_Tim_Click(sender, new EventArgs());
        }
        protected void btn_ThemMoi_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL("giahancn", "mid=" + this.ModuleId, "title=Thêm mới gia hạn hợp đồng", "GIAHAN_ID=0", "HD_ID=" + HD_ID));
        }
        protected void btn_TK_Tim_Click(object sender, EventArgs e)
        {
            try
            {
                UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
                var UserID = _currentUser.UserID;
                Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"] = 0;
                LoadDanhSach(0);
            }
            catch (Exception ex)
            {
                log.Error("", ex);
            }
        }
        public void dgSua(object sender, EventArgs e)
        {
            HtmlAnchor html = (HtmlAnchor)sender;
            int v_GIAHAN_ID = Convert.ToInt32(html.HRef.ToString());
            Response.Redirect(Globals.NavigateURL("giahancn", "mid=" + this.ModuleId, "title=Cập nhật gia hạn hợp đồng", "HD_ID=" + HD_ID, "GIAHAN_ID=" + v_GIAHAN_ID));
        }
        public void dgXoa(object sender, EventArgs e)
        {
            HtmlAnchor html = (HtmlAnchor)sender;
            int GIAHAN_ID = Convert.ToInt32(html.HRef.ToString());
            try
            {
                var objQLHD_GIAHANHD = vDC.QLHD_GIAHANHDs.Where(x => x.GIAHAN_ID == GIAHAN_ID).FirstOrDefault();
                if (objQLHD_GIAHANHD != null)
                {
                    vDC.QLHD_GIAHANHDs.DeleteOnSubmit(objQLHD_GIAHANHD);
                    var objQLHD_HD = vDC.QLHD_HDs.Where(x => x.HD_ID == objQLHD_GIAHANHD.HD_ID).FirstOrDefault();
                    objQLHD_HD.HD_SOLANGIAHAN = objQLHD_HD.HD_SOLANGIAHAN - 1;
                    if(objQLHD_HD.HD_SOLANGIAHAN == 0)
                    {
                        objQLHD_HD.HD_COGIAHAN = false;
                    }
                    vDC.SubmitChanges();
                    LoadDanhSach(0);
                    ClassCommon.ShowToastr(Page, "Xóa gia hạn hợp đồng thành công!", "Thông báo", "success");
                }
            }
            catch (Exception ex)
            {
                ClassCommon.ShowToastr(Page, "Có lỗi xảy ra vui lòng liên hệ quản trị", "Thông báo lỗi", "error");
                log.Error("", ex);
            }
        }
        protected void btnBoQua_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL());
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
            //ListItem vPageSize1 = new ListItem("1", "1");
            ListItem vPageSize2 = new ListItem("5", "5");
            ListItem vPageSize10 = new ListItem("10", "10");
            ListItem vPageSize20 = new ListItem("20", "20");
            ListItem vPageSize30 = new ListItem("30", "30");
            ListItem vPageSize50 = new ListItem("50", "50");
            ListItem vPageSize100 = new ListItem("100", "100");
            ListItem vPageSize200 = new ListItem("200", "200");
            ListItem vPageSize9999 = new ListItem("Tất cả", "9999");
            //ddlPageSize.Items.Add(vPageSize1);
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
            LoadDanhSach(vCurentPage);
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
                        log.Error("", ex);
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
            UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
            LoadDanhSach(dgDanhSach.PageCount - 1);
            Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"] = (dgDanhSach.PageCount - 1);
        }
        void lbNextPage_Click(object sender, EventArgs e)
        {
            UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
            if (dgDanhSach.CurrentPageIndex < (dgDanhSach.PageCount - 1))
            {
                LoadDanhSach(dgDanhSach.CurrentPageIndex + 1);
                Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"] = (dgDanhSach.CurrentPageIndex);
            }
        }
        void lbPreviousPage_Click(object sender, EventArgs e)
        {
            UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
            if (dgDanhSach.CurrentPageIndex > 0)
            {
                LoadDanhSach(dgDanhSach.CurrentPageIndex - 1);
                Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"] = (dgDanhSach.CurrentPageIndex);
            }
        }
        void lbFirstPage_Click(object sender, EventArgs e)
        {
            UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
            LoadDanhSach(0);
            Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"] = 0;
        }
        #endregion

    }
}
