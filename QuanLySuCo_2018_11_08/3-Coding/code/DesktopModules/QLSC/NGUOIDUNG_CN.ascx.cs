using DotNetNuke.Common;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Membership;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLSC
{
    public partial class NGUOIDUNG_CN : DotNetNuke.Entities.Modules.UserModuleBase
    {
        #region  Khai báo định nghĩa đối tượng 
        QLSCDataContext vDC = new QLSCDataContext();
        int vND_ID;
        DataTable dtTable = new DataTable();
        QLSC_NGUOIDUNG objNGUOIDUNG;
        
        UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
        #endregion

        #region Phương thức sự kiện 
        protected void Page_Load(object sender, EventArgs e)
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
            try
            {                
                if (!IsPostBack)
                {
                    if (Request.QueryString["ND_ID"] != null)
                    {

                        vND_ID = Convert.ToInt32(Request.QueryString["ND_ID"]);
                        if (vND_ID > 0)
                        {
                            btnCapNhatTiepTuc.Visible = false;
                        }
                    }
                    LoadDSNhomDV();
                    SetInfoForm(vND_ID);
                }
            }
            catch { }
        }

        public void LoadDSNhomDV()
        {
            try
            {
                List<QLSC_DONVI> lstDonVi = new List<QLSC_DONVI>();
                lstDonVi = vDC.QLSC_DONVIs.ToList();
                dtTable = new DataTable();
                dtTable.Columns.Add("DV_TEN");
                dtTable.Columns.Add("DV_ID");               
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


        public void SetInfoForm(int ND_ID)
        {
            //Trường hợp cập nhật
            if (ND_ID != 0)
            {
                divMatKhau.Visible = false; //ẩn trường mật khẩu
                objNGUOIDUNG = getNguoiDungByID(ND_ID);                
                txtTenDangNhap.Text = objNGUOIDUNG.UserName;
                txtTenNguoiDung.Text = objNGUOIDUNG.ND_TEN;
                drpDonVi.SelectedValue = objNGUOIDUNG.DONVI_ID.ToString();
                txtGhiChu.Text = objNGUOIDUNG.ND_GHICHU;                               
            }
        }
        

        protected void btn_CN_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = sender as LinkButton;
                string action = btn.CommandName;
                vND_ID = Convert.ToInt32(Request.QueryString["ND_ID"]);
                //Trường hợp thêm mới
                if (vND_ID == 0)
                {
                    if (txtTenDangNhap.Text.Trim() == "")
                    {
                        ClassCommon.ShowToastr(this.Page, "Vui lòng nhập tên đăng nhập", "Thông báo lỗi", "error");
                        txtTenDangNhap.Focus();
                    }
                    else
                    {
                        if (KiemTraTrungTenDangNhap(txtTenDangNhap.Text.Trim(), vND_ID))
                        {
                            ClassCommon.ShowToastr(this.Page, "Tên đăng nhập đã tồn tại vui lòng nhập tên khác", "Thông báo lỗi", "error");
                            txtTenDangNhap.Focus();
                        }
                        else
                        {
                            if (txtTenNguoiDung.Text.Trim() == "")
                            {
                                ClassCommon.ShowToastr(this.Page, "Vui lòng nhập tên người dùng", "Thông báo lỗi", "error");
                                txtTenDangNhap.Focus();
                            }
                            else
                            {
                                if (KiemTraTrungTenNguoiDung(txtTenNguoiDung.Text.Trim(), vND_ID))
                                {
                                   
                                    ClassCommon.ShowToastr(this.Page, "Tên người dùng đã tồn tại vui lòng chọn tên khác", "Thông báo lỗi", "error");
                                    txtTenNguoiDung.Focus();                                
                                }
                                else
                                {

                                    if (txtMatKhau.Text.Trim() == "")
                                    {
                                        ClassCommon.ShowToastr(this.Page, "Vui lòng nhập mật khẩu", "Thông báo lỗi", "error");
                                        txtMatKhau.Focus();
                                    }
                                    else
                                    {
                                        if (txtMatKhau.Text.Length < 7)
                                        {
                                            ClassCommon.ShowToastr(this.Page, "Vui lòng nhập mật khẩu lớn hơn 6 ký tự", "Thông báo lỗi", "error");
                                            txtMatKhau.Focus();
                                        }
                                        else
                                        {
                                            if (drpDonVi.SelectedValue == "")
                                            {
                                                ClassCommon.ShowToastr(this.Page, "Vui lòng chọn đơn vị", "Thông báo lỗi", "error");
                                                drpDonVi.Focus();
                                            }
                                            else
                                            {
                                                UserInfo objUser = new UserInfo();
                                                objUser.PortalID = this.PortalId;
                                                objUser.IsSuperUser = false;
                                                objUser.FirstName = ClassCommon.ClearHTML(txtTenNguoiDung.Text.Trim());
                                                objUser.LastName = ClassCommon.ClearHTML(txtTenNguoiDung.Text.Trim());
                                                objUser.DisplayName = ClassCommon.ClearHTML(txtTenNguoiDung.Text.Trim());
                                                objUser.Email = "test@gmail.com";
                                                objUser.Username = ClassCommon.ClearHTML(txtTenDangNhap.Text.Trim());
                                                //objUser.Profile.PreferredLocale = PortalSettings.DefaultLanguage;
                                                //objUser.Profile.TimeZone = PortalSettings.TimeZoneOffset;
                                                //objUser.Profile.FirstName = ClassCommon.ClearHTML(txtTenNguoiDung.Text.Trim());
                                                //objUser.Profile.LastName = ClassCommon.ClearHTML(txtTenNguoiDung.Text.Trim());
                                                //Nạp giá trị vào objMembership

                                                UserMembership objMembership = new UserMembership();
                                                objMembership.Approved = true;
                                                objMembership.Username = ClassCommon.ClearHTML(txtTenDangNhap.Text.Trim());
                                                objMembership.CreatedDate = DateTime.Now;
                                                objMembership.Email = "test@gmail.com";
                                                objMembership.IsOnLine = false;
                                                objMembership.Password = txtMatKhau.Text.Trim();
                                                objUser.Membership = objMembership;
                                                //Thêm user và trả đối tượng user vừa thêm
                                                UserCreateStatus result = UserController.CreateUser(ref objUser);

                                                if (result == UserCreateStatus.Success)
                                                {
                                                    QLSC_NGUOIDUNG objNGUOIDUNG = new QLSC_NGUOIDUNG();
                                                    objNGUOIDUNG = new QLSC_NGUOIDUNG();
                                                    objNGUOIDUNG.UserName = ClassCommon.ClearHTML(txtTenDangNhap.Text.Trim());
                                                    objNGUOIDUNG.ND_TEN = ClassCommon.ClearHTML(txtTenNguoiDung.Text.Trim());
                                                    objNGUOIDUNG.ND_GHICHU = ClassCommon.ClearHTML(txtGhiChu.Text.Trim());
                                                    objNGUOIDUNG.DONVI_ID = int.Parse(drpDonVi.SelectedValue);
                                                    objNGUOIDUNG.UserID = objUser.UserID;
                                                    vDC.QLSC_NGUOIDUNGs.InsertOnSubmit(objNGUOIDUNG);
                                                    vDC.SubmitChanges();
                                                }
                                                Session[TabId + "_Message"] = "Thêm mới nhóm thành viên thành công";
                                                Session[TabId + "_Type"] = "success";
                                                if (action == "TiepTuc")
                                                {
                                                    Response.Redirect(Globals.NavigateURL("create_update", "mid=" + this.ModuleId, "title=Thêm mới nhóm thành viên", "NTV_ID=0"));
                                                }
                                                else
                                                {
                                                    Response.Redirect(Globals.NavigateURL(), false);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                           
                    }

                }

                //Trường hợp cập nhật
                else
                {                    
                    if (txtTenDangNhap.Text.Trim() == "")
                    {
                        ClassCommon.ShowToastr(this.Page, "Vui lòng nhập tên đăng nhập", "Thông báo lỗi", "error");
                        txtTenDangNhap.Focus();
                    }
                    else
                    {
                       
                        if (KiemTraTrungTenDangNhap(txtTenDangNhap.Text.Trim(), vND_ID))
                        {
                            ClassCommon.ShowToastr(this.Page, "Tên đăng nhập đã tồn tại vui lòng nhập tên khác", "Thông báo lỗi", "error");
                            txtTenDangNhap.Focus();
                        }
                        else
                        {
                            if (txtTenNguoiDung.Text.Trim() == "")
                            {
                                ClassCommon.ShowToastr(this.Page, "Vui lòng nhập tên người dùng", "Thông báo lỗi", "error");
                                txtTenNguoiDung.Focus();
                            }
                            else
                            {
                                if (KiemTraTrungTenNguoiDung(txtTenNguoiDung.Text.Trim(), vND_ID))
                                {
                                    ClassCommon.ShowToastr(this.Page, "Tên người dùng đã tồn tại vui lòng chọn tên khác", "Thông báo lỗi", "error");
                                    txtTenNguoiDung.Focus();
                                }
                                else
                                {
                                    if (drpDonVi.SelectedValue == "")
                                    {
                                        ClassCommon.ShowToastr(this.Page, "Vui lòng chọn đơn vị", "Thông báo lỗi", "error");
                                        drpDonVi.Focus();
                                    }
                                    else
                                    {
                                        vND_ID = Convert.ToInt32(Request.QueryString["ND_ID"]);
                                        objNGUOIDUNG = getNguoiDungByID(vND_ID);
                                        UserInfo objUser = UserController.GetUserById(this.PortalId, objNGUOIDUNG.UserID ?? 0);
                                        objNGUOIDUNG.UserName = ClassCommon.ClearHTML(txtTenDangNhap.Text.Trim());
                                        objNGUOIDUNG.ND_TEN = ClassCommon.ClearHTML(txtTenNguoiDung.Text.Trim());
                                        objNGUOIDUNG.ND_GHICHU = ClassCommon.ClearHTML(txtGhiChu.Text.Trim());
                                        objNGUOIDUNG.DONVI_ID = int.Parse(drpDonVi.SelectedValue);
                                        UserController.ChangeUsername(objNGUOIDUNG.UserID ?? 0, ClassCommon.ClearHTML(txtTenDangNhap.Text.Trim()));
                                        vDC.SubmitChanges();
                                        Session[TabId + "_Message"] = "Cập nhật nhóm thành viên thành công";
                                        Session[TabId + "_Type"] = "success";
                                        if (action == "TiepTuc")
                                        {
                                            Response.Redirect(Globals.NavigateURL("create_update", "mid=" + this.ModuleId, "title=Thêm mới nhóm thành viên", "NTV_ID=0"));
                                        }
                                        else
                                        {
                                            Response.Redirect(Globals.NavigateURL(), false);
                                        }
                                    }
                                }
                            }
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                ClassCommon.ShowToastr(this.Page, "Có lỗi xãy ra trong quá trình xử lý vui lòng liên hệ quản trị", "Thông báo lỗi", "error");
            }
        }

        protected void btn_CN_BoQua_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(), false);
        }
        #endregion

        #region Phương thức sự kiện linq
        public QLSC_NGUOIDUNG getNguoiDungByID(int id)
        {
            return (from obj in vDC.QLSC_NGUOIDUNGs
                    where id == obj.ND_ID
                    select obj).SingleOrDefault();
        }

        //Kiểm tra trùng mã nhóm thành viên
      

        public bool KiemTraTrungTenDangNhap(string Username, int id)
        {
            var it = (from obj in vDC.QLSC_NGUOIDUNGs
                      where obj.UserName.Equals(Username) && id != obj.ND_ID
                      select obj).ToList();
            if (it.Count > 0)
                return true;
            else
                return false;

        }

        public bool KiemTraTrungTenNguoiDung(string vND_TEN, int id)
        {
            var it = (from obj in vDC.QLSC_NGUOIDUNGs
                      where obj.ND_TEN.Equals(vND_TEN) && id != obj.ND_ID
                      select obj).ToList();
            if (it.Count > 0)
                return true;
            else
                return false;

        }
        #endregion      
    }
}
