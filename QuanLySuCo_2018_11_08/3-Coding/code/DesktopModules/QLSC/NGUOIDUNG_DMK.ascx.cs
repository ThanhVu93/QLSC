using DotNetNuke.Common;
using DotNetNuke.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLSC
{
    public partial class NGUOIDUNG_DMK : DotNetNuke.Entities.Modules.UserModuleBase
    {
        QLSCDataContext vDC = new QLSCDataContext();
        UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
        int vUserId;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["UserID"] != null)
                {
                    vUserId = Convert.ToInt32(Request.QueryString["UserID"]);
                }
                else
                {
                    vUserId = _currentUser.UserID;
                }
                SetInfoForm(vUserId);
            }

        }
        public void SetInfoForm(int userID)
        {
            UserInfo objUser = UserController.GetUserById(this.PortalId, userID);
            var tv = (from obj in vDC.QLSC_NGUOIDUNGs
                      where obj.UserID == userID
                      select obj).SingleOrDefault();
            txtTenDangNhap.Text = tv.UserName;

            txtHoTen.Text = tv.ND_TEN;
        }
        //Get User by UserID
        public User getUserByUserID(int userID)
        {
            return (from obj in vDC.Users
                    where userID == obj.UserID
                    select obj).SingleOrDefault();
        }


        protected void btn_CN_Click(object sender, EventArgs e)
        {
            pnThongBao.Visible = false;
            try
            {
                if (Request.QueryString["UserID"] != null)
                {
                    vUserId = Convert.ToInt32(Request.QueryString["UserID"]);
                }
                else
                {
                    vUserId = _currentUser.UserID;
                }
                if (txtMatKhau.Text != "")
                {
                    UserInfo objUser = UserController.GetUserById(this.PortalId, vUserId);
                    objUser.UserID = vUserId;
                    if (txtMatKhau.Text != "")
                    {
                        string oldPassword = UserController.ResetPassword(objUser, objUser.Membership.PasswordAnswer);

                        if (UserController.ChangePassword(objUser, oldPassword, txtMatKhau.Text.Trim()) == true)
                        {
                            ClassCommon.ShowToastr(Page, "Đổi mật khẩu thành công", "Thông báo", "Success");
                            if (Request.QueryString["UserID"] != null)
                            {
                                Session[TabId + "_Message"] = "Đổi mật khẩu thành công";
                                Session[TabId + "_Type"] = "success";
                                Response.Redirect(Globals.NavigateURL(), false);
                            }
                            else
                            {
                                Session["Home_Message"] = "Đổi mật khẩu thành công";
                                Session["Home_Type"] = "success";
                                Response.Redirect("/Default.aspx?tabid=55");
                            }
                        }
                        else
                        {
                            ClassCommon.ShowToastr(Page, "Đổi mật khẩu thất bại, mật khẩu mới không được trùng với mật khẩu hiện tại và mật khẩu trước đó", "Thông báo", "error");
                        }
                    }
                    else
                    {
                        pnThongBao.Visible = true;
                        lblThongBao.Text = "Mật khẩu đăng nhập không chính xác";
                    }
                }
            }
            catch (Exception ex)
            {
                ClassCommon.ThongBaoNgoaiLe(lblThongBao, ex, UserInfo);
            }

        }

        protected void btn_CN_BoQua_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(), false);
        }
    }
}
