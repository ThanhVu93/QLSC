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
    public partial class LOAISUCO_CN : DotNetNuke.Entities.Modules.UserModuleBase
    {
        #region  Khai báo định nghĩa đối tượng 
        QLSCDataContext vDC = new QLSCDataContext();
        int vLOAISC_ID;
        DataTable dtTable = new DataTable();
        QLSC_LOAISUCO objLOAISUCO;
        public string vPathCommon = ClassParameter.vPathCommon;
        public string vPathCommonJS = ClassParameter.vPathCommonJavascript;
        UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
        public string vJavascriptMask;
        #endregion

        #region Phương thức sự kiện 
        protected void Page_Load(object sender, EventArgs e)
        {
            vJavascriptMask = ClassParameter.vJavascriptMaskNumber;
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
                    if (Request.QueryString["LOAISC_ID"] != null)
                    {

                        vLOAISC_ID = Convert.ToInt32(Request.QueryString["LOAISC_ID"]);
                        if (vLOAISC_ID > 0)
                        {
                            btnCapNhatTiepTuc.Visible = false;
                        }
                    }
                    SetInfoForm(vLOAISC_ID);
                }
            }
            catch { }
        }
       


        public void SetInfoForm(int loaiSC_ID)
        {
            //Trường hợp cập nhật
            if (loaiSC_ID > 0)
            {
                objLOAISUCO = getLoaiSuCoByID(loaiSC_ID);
                txtTenLoaiSC.Text = objLOAISUCO.LOAISC_TEN;
                txtGhiChu.Text = objLOAISUCO.LOAISC_GHICHU;
            }
        }


        protected void btn_CN_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = sender as LinkButton;
                string action = btn.CommandName;
                vLOAISC_ID = Convert.ToInt32(Request.QueryString["LOAISC_ID"]);
                //Trường hợp thêm mới
                if (vLOAISC_ID == 0)
                {
                    if (txtTenLoaiSC.Text.Trim() == "")
                    {
                        ClassCommon.ShowToastr(this.Page, "Vui lòng nhập tên loại sự cố", "Thông báo lỗi", "error");
                        txtTenLoaiSC.Focus();
                    }
                    else
                    {
                        if (kiemtraTrungLoaiSuCo(txtTenLoaiSC.Text.Trim(), vLOAISC_ID))
                        {
                            ClassCommon.ShowToastr(this.Page, "Tên loại sự cố đã tồn tại, vui lòng nhập tên khác", "Thông báo lỗi", "error");
                            txtTenLoaiSC.Focus();
                        }
                        else
                        {
                            objLOAISUCO = new QLSC_LOAISUCO();
                            objLOAISUCO.LOAISC_TEN = ClassCommon.ClearHTML(txtTenLoaiSC.Text.Trim());
                            objLOAISUCO.LOAISC_GHICHU = ClassCommon.ClearHTML(txtGhiChu.Text.Trim());
                            vDC.QLSC_LOAISUCOs.InsertOnSubmit(objLOAISUCO);
                            vDC.SubmitChanges();
                            Session[TabId + "_Message"] = "Thêm mới loại sự cố thành công";
                            Session[TabId + "_Type"] = "success";
                            if (action == "TiepTuc")
                            {
                                Response.Redirect(Globals.NavigateURL("create_update", "mid=" + this.ModuleId, "title=Thêm mới loại sự cố", "ND_ID=0"));
                            }
                            else
                            {
                                Response.Redirect(Globals.NavigateURL(), false);
                            }
                        }
                    }
                }
                //Trường hợp cập nhật
                else
                {
                    if (txtTenLoaiSC.Text.Trim() == "")
                    {
                        ClassCommon.ShowToastr(this.Page, "Vui lòng nhập tên đăng nhập", "Thông báo lỗi", "error");
                        txtTenLoaiSC.Focus();
                    }
                    else
                    {

                        if (kiemtraTrungLoaiSuCo(txtTenLoaiSC.Text.Trim(), vLOAISC_ID))
                        {
                            ClassCommon.ShowToastr(this.Page, "Vui lòng nhập tên loại sự cố", "Thông báo lỗi", "error");
                            txtTenLoaiSC.Focus();
                        }
                        else
                        {                        
                            objLOAISUCO = getLoaiSuCoByID(vLOAISC_ID);
                            objLOAISUCO.LOAISC_TEN = ClassCommon.ClearHTML(txtTenLoaiSC.Text.Trim());
                            objLOAISUCO.LOAISC_GHICHU = ClassCommon.ClearHTML(txtGhiChu.Text.Trim());
                            vDC.SubmitChanges();
                            Session[TabId + "_Message"] = "Cập nhật thông tin loại sự cố thành công";
                            Session[TabId + "_Type"] = "success";
                            if (action == "TiepTuc")
                            {
                                Response.Redirect(Globals.NavigateURL("create_update", "mid=" + this.ModuleId, "title=Cập nhật thông tin loại sự cố thành công", "ND_=0"));
                            }
                            else
                            {
                                Response.Redirect(Globals.NavigateURL(), false);
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
        public QLSC_LOAISUCO getLoaiSuCoByID(int id)
        {
            return (from obj in vDC.QLSC_LOAISUCOs
                    where id == obj.LOAISC_ID
                    select obj).SingleOrDefault();
        }
        //Kiểm tra trùng tên loại sự cố


        public bool kiemtraTrungLoaiSuCo(string TenLoaiSC, int id)
        {
            var it = (from obj in vDC.QLSC_LOAISUCOs
                      where obj.LOAISC_TEN.Equals(TenLoaiSC) && id != obj.LOAISC_ID
                      select obj).ToList();
            if (it.Count > 0)
                return true;
            else
                return false;

        }
        #endregion      
    }
}
