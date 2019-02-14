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

namespace QLHD
{
    public partial class QLHD_TRANGCHU_DS : DotNetNuke.Entities.Modules.UserModuleBase
    {
        #region Khai báo, định nghĩa đối tượng
        private readonly ILog log = LoggerSource.Instance.GetLogger(typeof(QLHD_TRANGCHU_DS).FullName);

        public string vPathCommon = ClassParameter.vPathCommon;
        public string vPathCommonJS = ClassParameter.vPathCommonJavascript;
        public string vPathCommonBieuMau = ClassParameter.vPathCommonBieuMau;
        int vPageSize = ClassParameter.vPageSize;
        int vCurentPage = 0;
        DataTable dtTable;
        QLHDDataContext vDC = new QLHDDataContext();
        ClassCommon clsCommon = new ClassCommon();
        UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session.Remove("TapTin");
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
                List<QLHD_HDInfo> objQLHD_HDs = Get_QLHD_HDInfos(pCurentPage, out o_Count);
                dgDanhSach.DataSource = objQLHD_HDs;
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

        public List<QLHD_HDInfo> Get_QLHD_HDInfos(int pCurentPage, out int o_Count)
        {
            UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
            var UserID = _currentUser.UserID;
            string tukhoa = "";
            int v_Count = 0;
            string o_Messages = "";
            if (Session[UserID + "txt_TK_NoiDung"] != null)
            {
                tukhoa = Session[UserID + "txt_TK_NoiDung"].ToString().ToUpper();
            }

            int vTrangThai = -1;
            if (Session[UserID + "drpTrangThai"] != null)
            {
                drpTrangThai.SelectedValue = Session[UserID + "drpTrangThai"].ToString();
                vTrangThai = int.Parse(Session[UserID + "drpTrangThai"].ToString());
            }
            vTrangThai = 3;
            try
            {
                List<QLHD_HDInfo> objQLHD_HDInfos = Get_QLHD_HDInfos(out o_Messages);
                v_Count = objQLHD_HDInfos.Count;
                if (objQLHD_HDInfos.Count > 0)
                {
                    if (tukhoa != "")
                    {
                        objQLHD_HDInfos = objQLHD_HDInfos.Where(x => x.HD_SO.ToUpper().Contains(tukhoa) || x.HD_TENCONGTRINH.ToUpper().Contains(tukhoa)).ToList();
                        v_Count = objQLHD_HDInfos.Count();
                    }
                }
                if (objQLHD_HDInfos.Count > 0)
                {
                    if (vTrangThai != -1)
                    {
                        objQLHD_HDInfos = objQLHD_HDInfos.Where(x => x.HD_TRANGTHAI == vTrangThai).ToList();
                        v_Count = objQLHD_HDInfos.Count();
                    }
                }
                objQLHD_HDInfos = objQLHD_HDInfos.Skip((pCurentPage) * vPageSize).Take(vPageSize).ToList();
                o_Count = v_Count;
                return objQLHD_HDInfos;
            }
            catch (Exception ex)
            {
                o_Count = 0;
                return new List<QLHD_HDInfo>();
                log.Error("", ex);
            }
        }
        public List<QLHD_HDInfo> Get_QLHD_HDInfos(out string o_Messages)
        {
            try
            {
                o_Messages = "";
                var objQLHD_HDInfos = (from t in vDC.QLHD_HDs
                                       select new QLHD_HDInfo()
                                       {
                                           HD_ID = t.HD_ID,
                                           HD_TEN = t.HD_TEN,
                                           HD_SO = t.HD_SO,
                                           HD_TENCONGTRINH = t.HD_TENCONGTRINH,
                                           HD_NGAYKY = t.HD_NGAYKY,
                                           HD_THOIGIANTHUCHIEN = t.HD_THOIGIANTHUCHIEN,
                                           HD_HIEULUC_HD = t.HD_HIEULUC_HD,
                                           HD_NGAYHETHAN_HD = t.HD_NGAYHETHAN_HD,
                                           HD_TGNHAC = t.HD_TGNHAC,
                                           HD_GIATRI = t.HD_GIATRI,
                                           HD_GHICHU = t.HD_GHICHU,
                                           HD_TENDONVITHICONG = t.HD_TENDONVITHICONG,
                                           HD_THOIGIANTHICONG = t.HD_THOIGIANTHICONG,
                                           HD_NGAYKHOICONG = t.HD_NGAYKHOICONG,
                                           HD_NGAYHETHANTHICONG = t.HD_NGAYHETHANTHICONG,
                                           HD_THICONG_TGNHAC = t.HD_THICONG_TGNHAC,
                                           HD_BLTHUCHIENHOPDONG_TUNGAY = t.HD_BLTHUCHIENHOPDONG_TUNGAY,
                                           HD_BLTHUCHIENHOPDONG_DENNGAY = t.HD_BLTHUCHIENHOPDONG_DENNGAY,
                                           HD_BLTHUCHIENHOPDONG_TGNHAC = t.HD_BLTHUCHIENHOPDONG_TGNHAC,
                                           HD_BLTHANHTOANVATTU_TUNGAY = t.HD_BLTHANHTOANVATTU_TUNGAY,
                                           HD_BLTHANHTOANVATTU_DENNGAY = t.HD_BLTHANHTOANVATTU_DENNGAY,
                                           HD_BLTHANHTOANVATTU_TGNHAC = t.HD_BLTHANHTOANVATTU_TGNHAC,
                                           HD_BLTAMUNG_TUNGAY = t.HD_BLTAMUNG_TUNGAY,
                                           HD_BLTAMUNG_DENNGAY = t.HD_BLTAMUNG_DENNGAY,
                                           HD_BLTAMUNG_TGNHAC = t.HD_BLTAMUNG_TGNHAC,
                                           HD_TRANGTHAI = t.HD_TRANGTHAI,
                                           HD_COGIAHAN = t.HD_COGIAHAN,
                                           HD_XOA = t.HD_XOA,
                                           HD_SOLANGIAHAN = t.HD_SOLANGIAHAN,
                                           _COUNT_SONGAY_BLTHANHTOAN = 0,
                                           _COUNT_SONGAY_BLVATTU = 0,
                                           _COUNT_SONGAY_BL_TAMUNG = 0,
                                           _COUNT_SONGAY_HD = 0,
                                           _COUNT_SONGAY_THICONG = 0,
                                           COLOR = "",
                                           NHACNHO = 0,
                                           NHACNHOTHICONG = 0,
                                           NHACNHOTAMUNG = 0,
                                           NHACNHOTHANHTOAN = 0,
                                           NHACNHOVATTU = 0
                                       }
                                                 ).OrderByDescending(x => x.HD_TRANGTHAI).ToList();
                if (objQLHD_HDInfos.Count > 0)
                {
                    //Tính toán thời gian nhắc 
                    foreach (var objQLHD_HDInfo in objQLHD_HDInfos)
                    {
                        if (objQLHD_HDInfo.HD_TRANGTHAI != 1)
                        {
                            if (objQLHD_HDInfo.HD_COGIAHAN == false)
                            {
                                //Nếu thời gian hiện tại - thời gian hết hạn hợp đồng <0: Hợp đồng đã quá hạn
                                if (Int32.Parse(Math.Ceiling((DateTime.Now - DateTime.Parse(objQLHD_HDInfo.HD_NGAYHETHAN_HD.ToString())).TotalDays).ToString()) > 0)
                                {
                                    objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID).HD_TRANGTHAI = 3;
                                }
                                else
                                {
                                    DateTime THOIGIANNHAC = objQLHD_HDInfo.HD_NGAYHETHAN_HD.AddDays(-objQLHD_HDInfo.HD_TGNHAC);
                                    DateTime THOIGIANNHACTHICONG = objQLHD_HDInfo.HD_NGAYHETHANTHICONG.AddDays(-objQLHD_HDInfo.HD_THICONG_TGNHAC);
                                    DateTime THOIGIANNHACTHANHTOAN = objQLHD_HDInfo.HD_BLTHUCHIENHOPDONG_DENNGAY.AddDays(-objQLHD_HDInfo.HD_BLTHUCHIENHOPDONG_TGNHAC);
                                    DateTime THOIGIANNHACVATTU = objQLHD_HDInfo.HD_BLTHANHTOANVATTU_DENNGAY.AddDays(-objQLHD_HDInfo.HD_BLTHANHTOANVATTU_TGNHAC);
                                    DateTime THOIGIANNHACTAMUNG = objQLHD_HDInfo.HD_BLTAMUNG_DENNGAY.AddDays(-objQLHD_HDInfo.HD_BLTAMUNG_TGNHAC);
                                    if (THOIGIANNHAC < DateTime.Now)
                                    {
                                        objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID).NHACNHO = 1;
                                    }
                                    if (THOIGIANNHACTHICONG < DateTime.Now)
                                    {
                                        objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID).NHACNHOTHICONG = 1;
                                    }
                                    if (THOIGIANNHACTHANHTOAN < DateTime.Now)
                                    {
                                        objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID).NHACNHOTHANHTOAN = 1;
                                    }
                                    if (THOIGIANNHACTAMUNG < DateTime.Now)
                                    {
                                        objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID).HD_BLTAMUNG_TGNHAC = 1;
                                    }
                                    if (THOIGIANNHACVATTU < DateTime.Now)
                                    {
                                        objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID).HD_BLTHANHTOANVATTU_TGNHAC = 1;
                                    }
                                    objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID).HD_TRANGTHAI = 2;
                                }
                                objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID)._COUNT_SONGAY_HD = Int32.Parse(Math.Ceiling((DateTime.Parse(objQLHD_HDInfo.HD_NGAYHETHAN_HD.ToString()) - DateTime.Now).TotalDays).ToString());
                                objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID)._COUNT_SONGAY_BLTHANHTOAN = Int32.Parse(Math.Ceiling((DateTime.Parse(objQLHD_HDInfo.HD_BLTHUCHIENHOPDONG_DENNGAY.ToString()) - DateTime.Now).TotalDays).ToString());
                                objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID)._COUNT_SONGAY_BL_TAMUNG = Int32.Parse(Math.Ceiling((DateTime.Parse(objQLHD_HDInfo.HD_BLTAMUNG_DENNGAY.ToString()) - DateTime.Now).TotalDays).ToString());
                                objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID)._COUNT_SONGAY_BLVATTU = Int32.Parse(Math.Ceiling((DateTime.Parse(objQLHD_HDInfo.HD_BLTHANHTOANVATTU_DENNGAY.ToString()) - DateTime.Now).TotalDays).ToString());
                                objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID)._COUNT_SONGAY_THICONG = Int32.Parse(Math.Ceiling((DateTime.Parse(objQLHD_HDInfo.HD_NGAYHETHANTHICONG.ToString()) - DateTime.Now).TotalDays).ToString());
                            }
                            else
                            {
                                var objQLHD_GIAHANHD = vDC.QLHD_GIAHANHDs.Where(x => x.HD_ID == objQLHD_HDInfo.HD_ID).OrderByDescending(x => x.GIAHAN_ID).ToList().FirstOrDefault();
                                if (Int32.Parse(Math.Ceiling((DateTime.Now - DateTime.Parse(objQLHD_GIAHANHD.HD_NGAYHETHAN_HD.ToString())).TotalDays).ToString()) > 0)
                                {
                                    objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID).HD_TRANGTHAI = 3;
                                }
                                else
                                {
                                    DateTime THOIGIANNHAC = objQLHD_GIAHANHD.HD_NGAYHETHAN_HD.AddDays(-objQLHD_GIAHANHD.HD_TGNHAC);
                                    DateTime THOIGIANNHACTHICONG = objQLHD_GIAHANHD.HD_NGAYHETHANTHICONG.AddDays(-objQLHD_GIAHANHD.HD_THICONG_TGNHAC);
                                    DateTime THOIGIANNHACTHANHTOAN = objQLHD_GIAHANHD.HD_BLTHUCHIENHOPDONG_DENNGAY.AddDays(-objQLHD_GIAHANHD.HD_BLTHUCHIENHOPDONG_TGNHAC);
                                    DateTime THOIGIANNHACVATTU = objQLHD_GIAHANHD.HD_BLTHANHTOANVATTU_DENNGAY.AddDays(-objQLHD_GIAHANHD.HD_BLTHANHTOANVATTU_TGNHAC);
                                    DateTime THOIGIANNHACTAMUNG = objQLHD_GIAHANHD.HD_BLTAMUNG_DENNGAY.AddDays(-objQLHD_GIAHANHD.HD_BLTAMUNG_TGNHAC);
                                    if (THOIGIANNHAC < DateTime.Now)
                                    {
                                        objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID).NHACNHO = 1;
                                    }
                                    if (THOIGIANNHACTHICONG < DateTime.Now)
                                    {
                                        objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID).NHACNHOTHICONG = 1;
                                    }
                                    if (THOIGIANNHACTHANHTOAN < DateTime.Now)
                                    {
                                        objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID).NHACNHOTHANHTOAN = 1;
                                    }
                                    if (THOIGIANNHACTAMUNG < DateTime.Now)
                                    {
                                        objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID).HD_BLTAMUNG_TGNHAC = 1;
                                    }
                                    if (THOIGIANNHACVATTU < DateTime.Now)
                                    {
                                        objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID).HD_BLTHANHTOANVATTU_TGNHAC = 1;
                                    }
                                }
                                objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID).HD_TRANGTHAI = 2;
                                objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID)._COUNT_SONGAY_HD = Int32.Parse(Math.Ceiling((DateTime.Parse(objQLHD_GIAHANHD.HD_NGAYHETHAN_HD.ToString()) - DateTime.Now).TotalDays).ToString());
                                objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID)._COUNT_SONGAY_BLTHANHTOAN = Int32.Parse(Math.Ceiling((DateTime.Parse(objQLHD_GIAHANHD.HD_BLTHUCHIENHOPDONG_DENNGAY.ToString()) - DateTime.Now).TotalDays).ToString());
                                objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID)._COUNT_SONGAY_BL_TAMUNG = Int32.Parse(Math.Ceiling((DateTime.Parse(objQLHD_GIAHANHD.HD_BLTAMUNG_DENNGAY.ToString()) - DateTime.Now).TotalDays).ToString());
                                objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID)._COUNT_SONGAY_BLVATTU = Int32.Parse(Math.Ceiling((DateTime.Parse(objQLHD_GIAHANHD.HD_BLTHANHTOANVATTU_DENNGAY.ToString()) - DateTime.Now).TotalDays).ToString());
                                objQLHD_HDInfos.Find(x => x.HD_ID == objQLHD_HDInfo.HD_ID)._COUNT_SONGAY_THICONG = Int32.Parse(Math.Ceiling((DateTime.Parse(objQLHD_GIAHANHD.HD_NGAYHETHANTHICONG.ToString()) - DateTime.Now).TotalDays).ToString());
                            }
                            //Cập nhật database
                            var obj = vDC.QLHD_HDs.Where(x => x.HD_ID == objQLHD_HDInfo.HD_ID).FirstOrDefault();
                            obj.HD_TRANGTHAI = objQLHD_HDInfo.HD_TRANGTHAI;
                            vDC.SubmitChanges();

                        }
                    }
                }
                return objQLHD_HDInfos;
            }
            catch (Exception ex)
            {
                o_Messages = "Không lấy được dữ liệu";
                log.Error("", ex);
                return new List<QLHD_HDInfo>();
            }
        }

        #region Infos
        public class QLHD_HDInfo : QLHD.QLHD_HD
        {
            public int _COUNT_SONGAY_HD { get; set; }
            public int _COUNT_SONGAY_THICONG { get; set; }
            public int _COUNT_SONGAY_BLTHANHTOAN { get; set; }
            public int _COUNT_SONGAY_BLVATTU { get; set; }
            public int _COUNT_SONGAY_BL_TAMUNG { get; set; }
            public string COLOR { get; set; }
            public int NHACNHO { get; set; }
            public int NHACNHOTHICONG { get; set; }
            public int NHACNHOTHANHTOAN { get; set; }
            public int NHACNHOVATTU { get; set; }
            public int NHACNHOTAMUNG { get; set; }
        }
        #endregion
        protected void dsXemCT(object sender, EventArgs e)
        {
            HtmlAnchor html = (HtmlAnchor)sender;
            int ID = Convert.ToInt32(html.HRef.ToString());
            Response.Redirect(Globals.NavigateURL("chitiet", "mid=" + this.ModuleId, "title=Xem chi tiết hợp đồng", "HD_ID=" + ID));
        }
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
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
            }
            if (e.Item.Cells[dgDanhSach.Columns.Count - 1].Text == "1")
            {
                e.Item.Cells[0].BackColor = System.Drawing.Color.Orange;
            }
            if (e.Item.Cells[dgDanhSach.Columns.Count - 1].Text == "2")
            {
                e.Item.Cells[0].BackColor = System.Drawing.Color.Green;
            }
            if (e.Item.Cells[dgDanhSach.Columns.Count - 1].Text == "3")
            {
                e.Item.Cells[0].BackColor = System.Drawing.Color.Red;
            }
        }

        protected void dgDanhSach_ItemCreated(object sender, DataGridItemEventArgs e)
        {
            Custom_Paging(sender, e, dgDanhSach.CurrentPageIndex, dgDanhSach.VirtualItemCount, dgDanhSach.PageCount);
        }

        #endregion
        #region Phương thức, sự kiện cho Form Search
        protected void txt_TK_NoiDung_TextChanged(object sender, EventArgs e)
        {
            btn_TK_Tim_Click(sender, new EventArgs());
        }
        protected void btn_ThemMoi_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL("edit", "mid=" + this.ModuleId, "title=Thêm mới hợp đồng", "HD_ID=0"));
        }
        protected void btn_TK_Tim_Click(object sender, EventArgs e)
        {
            try
            {
                UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
                var UserID = _currentUser.UserID;
                string tukhoa = ClassCommon.ClearHTML(txt_TK_NoiDung.Text.Trim());
                Session[UserID + "txt_TK_NoiDung"] = tukhoa;
                Session[PortalSettings.ActiveTab.TabID + _currentUser.UserID + "_CurrenPage"] = 0;

                int vTrangThai = -1;
                if (drpTrangThai.SelectedValue != "-1")
                {
                    vTrangThai = Int32.Parse(drpTrangThai.SelectedValue);
                }
                Session[UserID + "drpTrangThai"] = vTrangThai;

                LoadDanhSach(0);
            }
            catch (Exception ex)
            {
                log.Error("", ex);
            }
        }
        public void dggianhan(object sender, EventArgs e)
        {
            HtmlAnchor html = (HtmlAnchor)sender;
            int v_HD_ID = Convert.ToInt32(html.HRef.ToString());
            var objQLHD_HD = vDC.QLHD_HDs.Where(x => x.HD_ID == v_HD_ID).FirstOrDefault();
            if (objQLHD_HD != null)
            {
                if (objQLHD_HD.HD_TRANGTHAI != 1)
                {
                    Response.Redirect(Globals.NavigateURL("giahan", "mid=" + this.ModuleId, "title=Gia hạn hợp đồng", "HD_ID=" + v_HD_ID));
                }
            }
        }
        public void ketthucHD(object sender, EventArgs e)
        {
            HtmlAnchor html = (HtmlAnchor)sender;
            int v_HD_ID = Convert.ToInt32(html.HRef.ToString());
            var objQLHD_HD = vDC.QLHD_HDs.Where(x => x.HD_ID == v_HD_ID).FirstOrDefault();
            if (objQLHD_HD != null)
            {
                if (objQLHD_HD.HD_TRANGTHAI != 1)
                {
                    objQLHD_HD.HD_TRANGTHAI = 1;
                    vDC.SubmitChanges();
                    ClassCommon.ShowToastr(Page, "Kết thúc hợp đồng thành công.", "Thông báo", "success");
                    LoadDanhSach(0);
                }
                else
                {
                    objQLHD_HD.HD_TRANGTHAI = 2;
                    vDC.SubmitChanges();
                    ClassCommon.ShowToastr(Page, "Hủy kết thúc hợp đồng thành công.", "Thông báo", "success");
                    LoadDanhSach(0);
                }
            }

        }
        protected void drpTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_TK_Tim_Click(sender, new EventArgs());
        }
        public void dgSua(object sender, EventArgs e)
        {
            HtmlAnchor html = (HtmlAnchor)sender;
            int v_HD_ID = Convert.ToInt32(html.HRef.ToString());
            Response.Redirect(Globals.NavigateURL("edit", "mid=" + this.ModuleId, "title=Cập nhật hợp đồng", "HD_ID=" + v_HD_ID));
        }

        public void dgXoa(object sender, EventArgs e)
        {
            HtmlAnchor html = (HtmlAnchor)sender;
            int HD_ID = Convert.ToInt32(html.HRef.ToString());
            try
            {
                var objQLHD_HD = vDC.QLHD_HDs.Where(x => x.HD_ID == HD_ID).FirstOrDefault();
                if (objQLHD_HD != null)
                {
                    var objQLHD_GIAHANHDs = vDC.QLHD_GIAHANHDs.Where(x => x.HD_ID == HD_ID).ToList();
                    if (objQLHD_GIAHANHDs.Count > 0)
                    {

                        foreach (var objQLHD_GIAHANHD in objQLHD_GIAHANHDs)
                        {
                            if (objQLHD_GIAHANHDs.Count == 0) break;
                            if (objQLHD_GIAHANHD != null)
                            {
                                vDC.QLHD_GIAHANHDs.DeleteOnSubmit(objQLHD_GIAHANHD);
                            }
                        }
                    }

                    vDC.QLHD_HDs.DeleteOnSubmit(objQLHD_HD);
                    vDC.SubmitChanges();
                    LoadDanhSach(0);
                    ClassCommon.ShowToastr(Page, "Xóa hợp đồng thành công!", "Thông báo", "success");
                }

            }
            catch (Exception ex)
            {
                ClassCommon.ShowToastr(Page, "Có lỗi xảy ra vui lòng liên hệ quản trị", "Thông báo lỗi", "error");
                log.Error("", ex);
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
