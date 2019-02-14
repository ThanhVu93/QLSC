using DotNetNuke.Common;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Permissions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DotNetNuke.Instrumentation;

namespace QLHD
{
    public partial class QLHD_GIAHAN_CN : DotNetNuke.Entities.Modules.UserModuleBase
    {
        #region Khai báo, định nghĩa đối tượng

        private readonly ILog log = LoggerSource.Instance.GetLogger(typeof(QLHD_GIAHAN_CN).FullName);

        public string vPathCommon = ClassParameter.vPathCommon;
        public string vPathCommonJS = ClassParameter.vPathCommonJavascript;
        ClassCommon clsCommon = new ClassCommon();
        QLHDDataContext vDC = new QLHDDataContext();
        UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
        DataTable dtTable;
        int HD_ID = 0;
        int GIAHAN_ID = 0;
        public string vJavascriptMask;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vJavascriptMask = ClassParameter.vJavascriptMaskNumber;
            HD_ID = Convert.ToInt32(Request.QueryString["HD_ID"]);
            GIAHAN_ID = Convert.ToInt32(Request.QueryString["GIAHAN_ID"]);
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

                if (!IsPostBack)
                {
                    SetInfoForm(GIAHAN_ID);
                }

            }
            catch (Exception ex)
            {
                ClassCommon.THONGBAO_TOASTR(Page, ex, _currentUser, "Có lỗi trong quá trình xử lý, vui lòng liên hệ với quản trị!", "Thông báo lỗi", "error");
                log.Error("", ex);
            }
        }
        #region Controller
        public void UpdateQLHD_GIAHANHD(int  HD_ID, int vSoLanGiaHan)
        {
            var obj = vDC.QLHD_HDs.Where(x => x.HD_ID == HD_ID).FirstOrDefault();
            obj.HD_COGIAHAN = true;
            obj.HD_SOLANGIAHAN = vSoLanGiaHan;
            vDC.SubmitChanges();
        }
        public void InsertQLHD_GIAHANHD(QLHD_GIAHANHD objQLHD_GIAHANHD)
        {
            vDC.QLHD_GIAHANHDs.InsertOnSubmit(objQLHD_GIAHANHD);
            vDC.SubmitChanges();
        }
        public void UpdateQLHD_GIAHANHD(QLHD_GIAHANHD objQLHD_GIAHANHD)
        {
            var obj = GetQLHD_GIAHANHD(objQLHD_GIAHANHD.GIAHAN_ID);

            obj.HD_BLTAMUNG_TUNGAY = objQLHD_GIAHANHD.HD_BLTAMUNG_TUNGAY;
            obj.HD_BLTAMUNG_DENNGAY = objQLHD_GIAHANHD.HD_BLTAMUNG_DENNGAY;
            obj.HD_BLTAMUNG_TGNHAC = objQLHD_GIAHANHD.HD_BLTAMUNG_TGNHAC;

            obj.HD_BLTHANHTOANVATTU_DENNGAY = objQLHD_GIAHANHD.HD_BLTHANHTOANVATTU_DENNGAY;
            obj.HD_BLTHANHTOANVATTU_TGNHAC = objQLHD_GIAHANHD.HD_BLTHANHTOANVATTU_TGNHAC;
            obj.HD_BLTHANHTOANVATTU_TUNGAY = objQLHD_GIAHANHD.HD_BLTHANHTOANVATTU_TUNGAY;

            obj.HD_BLTHUCHIENHOPDONG_DENNGAY = objQLHD_GIAHANHD.HD_BLTHUCHIENHOPDONG_DENNGAY;
            obj.HD_BLTHUCHIENHOPDONG_TGNHAC = objQLHD_GIAHANHD.HD_BLTHUCHIENHOPDONG_TGNHAC;
            obj.HD_BLTHUCHIENHOPDONG_TUNGAY = objQLHD_GIAHANHD.HD_BLTHUCHIENHOPDONG_TUNGAY;

            obj.HD_HIEULUC_HD = objQLHD_GIAHANHD.HD_HIEULUC_HD;
            obj.HD_NGAYHETHAN_HD = objQLHD_GIAHANHD.HD_NGAYHETHAN_HD;
            obj.HD_NGAYKHOICONG = objQLHD_GIAHANHD.HD_NGAYKHOICONG;

            obj.HD_NGAYHETHANTHICONG = objQLHD_GIAHANHD.HD_NGAYHETHANTHICONG;
            obj.HD_TGNHAC = objQLHD_GIAHANHD.HD_TGNHAC;
            obj.HD_THICONG_TGNHAC = objQLHD_GIAHANHD.HD_THICONG_TGNHAC;
            obj.HD_ID = objQLHD_GIAHANHD.HD_ID;
            obj.GIAHAN_ID = objQLHD_GIAHANHD.GIAHAN_ID;
            obj.GIAHAN_GHICHU = objQLHD_GIAHANHD.GIAHAN_GHICHU;
            vDC.SubmitChanges();
        }
        public QLHD_GIAHANHD GetQLHD_GIAHANHD(int ID)
        {
            var objQLHD_GIAHANHD = (from obj in vDC.QLHD_GIAHANHDs
                                    where obj.GIAHAN_ID == ID
                              orderby obj.HD_ID descending
                              select obj).FirstOrDefault();

            return objQLHD_GIAHANHD;
        }

        #endregion
        #region SetFormInfo
        public void SetInfoForm(int GIAHAN_ID)
        {
            if (GIAHAN_ID == 0) // Thêm mới
            {
                var objQLHD_HD = vDC.QLHD_HDs.FirstOrDefault();
                if (objQLHD_HD != null)
                {
                    txtHieuLucHopDong.SelectedDate = objQLHD_HD.HD_HIEULUC_HD;
                    txtNgayHetHanHongDong.SelectedDate = objQLHD_HD.HD_NGAYHETHAN_HD;
                    txtTGDenHanHD.Text = String.Format("{0:#,##0.##}", objQLHD_HD.HD_TGNHAC).Replace(",", ".");

                    txtNgayKhoiCong.SelectedDate = objQLHD_HD.HD_NGAYKHOICONG;
                    txtNgayHetHanThiCong.SelectedDate = objQLHD_HD.HD_NGAYHETHANTHICONG;
                    txtTGDenHanThiCong.Text = String.Format("{0:#,##0.##}", objQLHD_HD.HD_THICONG_TGNHAC).Replace(",", ".");

                    txtBLThucHienHopDongTuNgay.SelectedDate = objQLHD_HD.HD_BLTHUCHIENHOPDONG_TUNGAY;
                    txtBLThucHienHopDongDenNgay.SelectedDate = objQLHD_HD.HD_BLTHUCHIENHOPDONG_DENNGAY;
                    txtTGDenHanBLThucHienHD.Text = String.Format("{0:#,##0.##}", objQLHD_HD.HD_BLTHUCHIENHOPDONG_TGNHAC).Replace(",", ".");

                    txtBLThanhToanVatTuTuNgay.SelectedDate = objQLHD_HD.HD_BLTHANHTOANVATTU_TUNGAY;
                    txtBLThanhToanVatTuDenNgay.SelectedDate = objQLHD_HD.HD_BLTHANHTOANVATTU_DENNGAY;
                    txtTGDenHanBLThanhToanVatTu.Text = String.Format("{0:#,##0.##}", objQLHD_HD.HD_BLTHANHTOANVATTU_TGNHAC).Replace(",", ".");

                    txtBLTamUngTuNgay.SelectedDate = objQLHD_HD.HD_BLTAMUNG_TUNGAY;
                    txtBLTamUngDenNgay.SelectedDate = objQLHD_HD.HD_BLTAMUNG_DENNGAY;
                    txtTGDenHanBLTamUng.Text = String.Format("{0:#,##0.##}", objQLHD_HD.HD_BLTAMUNG_TGNHAC).Replace(",", ".");
                }
            }
            else
            {
                var objQLHD_GIAHANHD = vDC.QLHD_GIAHANHDs.Where(x => x.HD_ID == HD_ID && x.GIAHAN_ID == GIAHAN_ID).FirstOrDefault();
                txtHieuLucHopDong.SelectedDate = objQLHD_GIAHANHD.HD_HIEULUC_HD;
                txtNgayHetHanHongDong.SelectedDate = objQLHD_GIAHANHD.HD_NGAYHETHAN_HD;
                txtTGDenHanHD.Text = String.Format("{0:#,##0.##}", objQLHD_GIAHANHD.HD_TGNHAC).Replace(",", ".");

                txtNgayKhoiCong.SelectedDate = objQLHD_GIAHANHD.HD_NGAYKHOICONG;
                txtNgayHetHanThiCong.SelectedDate = objQLHD_GIAHANHD.HD_NGAYHETHANTHICONG;
                txtTGDenHanThiCong.Text = String.Format("{0:#,##0.##}", objQLHD_GIAHANHD.HD_THICONG_TGNHAC).Replace(",", ".");

                txtBLThucHienHopDongTuNgay.SelectedDate = objQLHD_GIAHANHD.HD_BLTHUCHIENHOPDONG_TUNGAY;
                txtBLThucHienHopDongDenNgay.SelectedDate = objQLHD_GIAHANHD.HD_BLTHUCHIENHOPDONG_DENNGAY;
                txtTGDenHanBLThucHienHD.Text = String.Format("{0:#,##0.##}", objQLHD_GIAHANHD.HD_BLTHUCHIENHOPDONG_TGNHAC).Replace(",", ".");

                txtBLThanhToanVatTuTuNgay.SelectedDate = objQLHD_GIAHANHD.HD_BLTHANHTOANVATTU_TUNGAY;
                txtBLThanhToanVatTuDenNgay.SelectedDate = objQLHD_GIAHANHD.HD_BLTHANHTOANVATTU_DENNGAY;
                txtTGDenHanBLThanhToanVatTu.Text = String.Format("{0:#,##0.##}", objQLHD_GIAHANHD.HD_BLTHANHTOANVATTU_TGNHAC).Replace(",", ".");

                txtBLTamUngTuNgay.SelectedDate = objQLHD_GIAHANHD.HD_BLTAMUNG_TUNGAY;
                txtBLTamUngDenNgay.SelectedDate = objQLHD_GIAHANHD.HD_BLTAMUNG_DENNGAY;
                txtTGDenHanBLTamUng.Text = String.Format("{0:#,##0.##}", objQLHD_GIAHANHD.HD_BLTAMUNG_TGNHAC).Replace(",", ".");
                txtGhiChu.Text = objQLHD_GIAHANHD.GIAHAN_GHICHU;
            }
        }
        #endregion
        #region Page Event
        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                string o_Messages = "";
                if (Check_ThoiGian(DateTime.Parse(txtNgayKhoiCong.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanThiCong.SelectedDate.ToString()), out o_Messages) == false
                  || Check_ThoiGian(DateTime.Parse(txtBLThucHienHopDongTuNgay.SelectedDate.ToString()), DateTime.Parse(txtBLThucHienHopDongDenNgay.SelectedDate.ToString()), out o_Messages) == false
                  || Check_ThoiGian(DateTime.Parse(txtBLThanhToanVatTuTuNgay.SelectedDate.ToString()), DateTime.Parse(txtBLThanhToanVatTuDenNgay.SelectedDate.ToString()), out o_Messages) == false
                  || Check_ThoiGian(DateTime.Parse(txtBLTamUngTuNgay.SelectedDate.ToString()), DateTime.Parse(txtBLTamUngDenNgay.SelectedDate.ToString()), out o_Messages) == false
                  || Check_ThoiGian(DateTime.Parse(txtHieuLucHopDong.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanHongDong.SelectedDate.ToString()), out o_Messages) == false

                  || Check_ThoiGian_DuAn_GiaiDoan(DateTime.Parse(txtHieuLucHopDong.SelectedDate.ToString()), DateTime.Parse(txtNgayKhoiCong.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanThiCong.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanHongDong.SelectedDate.ToString()), out o_Messages) == false
                  || Check_ThoiGian_DuAn_GiaiDoan(DateTime.Parse(txtHieuLucHopDong.SelectedDate.ToString()), DateTime.Parse(txtBLThucHienHopDongTuNgay.SelectedDate.ToString()), DateTime.Parse(txtBLThucHienHopDongDenNgay.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanHongDong.SelectedDate.ToString()), out o_Messages) == false
                  || Check_ThoiGian_DuAn_GiaiDoan(DateTime.Parse(txtHieuLucHopDong.SelectedDate.ToString()), DateTime.Parse(txtBLThanhToanVatTuTuNgay.SelectedDate.ToString()), DateTime.Parse(txtBLThanhToanVatTuDenNgay.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanHongDong.SelectedDate.ToString()), out o_Messages) == false
                  || Check_ThoiGian_DuAn_GiaiDoan(DateTime.Parse(txtHieuLucHopDong.SelectedDate.ToString()), DateTime.Parse(txtBLTamUngTuNgay.SelectedDate.ToString()), DateTime.Parse(txtBLTamUngDenNgay.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanHongDong.SelectedDate.ToString()), out o_Messages) == false
                  )
                {
                    if (Check_ThoiGian(DateTime.Parse(txtHieuLucHopDong.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanHongDong.SelectedDate.ToString()), out o_Messages) == false)
                    {
                        pnThongBao.Visible = true;
                        lblThongBao.Text = "Ngày hết hạn hợp đồng phải lớn hơn hoặc bằng ngày hiệu lực hợp đồng. Vui lòng kiểm tra lại.";
                        txtNgayHetHanThiCong.Focus();
                    }
                    if (Check_ThoiGian(DateTime.Parse(txtNgayKhoiCong.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanThiCong.SelectedDate.ToString()), out o_Messages) == false)
                    {
                        pnThongBao.Visible = true;
                        lblThongBao.Text = "Ngày hết hạn thi công phải lớn hơn hoặc bằng ngày khởi công. Vui lòng kiểm tra lại.";
                        txtNgayHetHanThiCong.Focus();
                    }
                    if (Check_ThoiGian(DateTime.Parse(txtBLThucHienHopDongTuNgay.SelectedDate.ToString()), DateTime.Parse(txtBLThucHienHopDongDenNgay.SelectedDate.ToString()), out o_Messages) == false)
                    {
                        pnThongBao.Visible = true;
                        lblThongBao.Text = "Bảo lãnh thực hiện hợp đồng đến ngày phải lớn hơn hoặc bằng từ ngày. Vui lòng kiểm tra lại.";
                        txtBLThucHienHopDongDenNgay.Focus();
                    }
                    if (Check_ThoiGian(DateTime.Parse(txtBLThanhToanVatTuTuNgay.SelectedDate.ToString()), DateTime.Parse(txtBLThanhToanVatTuDenNgay.SelectedDate.ToString()), out o_Messages) == false)
                    {
                        pnThongBao.Visible = true;
                        lblThongBao.Text = "Bảo lãnh thanh toán vật tư đến ngày phải lớn hơn hoặc bằng từ ngày. Vui lòng kiểm tra lại.";
                        txtBLThanhToanVatTuDenNgay.Focus();
                    }
                    if (Check_ThoiGian(DateTime.Parse(txtBLTamUngTuNgay.SelectedDate.ToString()), DateTime.Parse(txtBLTamUngDenNgay.SelectedDate.ToString()), out o_Messages) == false)
                    {
                        pnThongBao.Visible = true;
                        lblThongBao.Text = "Bảo lãnh tạm ứng đến ngày phải lớn hơn hoặc bằng từ ngày. Vui lòng kiểm tra lại.";
                        txtBLThanhToanVatTuDenNgay.Focus();
                    }
                    if (Check_ThoiGian_DuAn_GiaiDoan(DateTime.Parse(txtHieuLucHopDong.SelectedDate.ToString()), DateTime.Parse(txtBLThanhToanVatTuTuNgay.SelectedDate.ToString()), DateTime.Parse(txtBLThanhToanVatTuDenNgay.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanHongDong.SelectedDate.ToString()), out o_Messages) == false)
                    {
                        pnThongBao.Visible = true;
                        lblThongBao.Text = "Thời gian bảo lãnh thanh toán vật tư phải nằm trong thời gian của hợp đồng. Vui lòng kiểm tra lại.";
                        txtNgayKhoiCong.Focus();
                    }
                    if (Check_ThoiGian_DuAn_GiaiDoan(DateTime.Parse(txtHieuLucHopDong.SelectedDate.ToString()), DateTime.Parse(txtBLThucHienHopDongTuNgay.SelectedDate.ToString()), DateTime.Parse(txtBLThucHienHopDongDenNgay.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanHongDong.SelectedDate.ToString()), out o_Messages) == false)
                    {
                        pnThongBao.Visible = true;
                        lblThongBao.Text = "Thời gian bảo lãnh thực hiện hợp đồng phải nằm trong thời gian của hợp đồng. Vui lòng kiểm tra lại.";
                        txtBLThucHienHopDongTuNgay.Focus();
                    }
                    if (Check_ThoiGian_DuAn_GiaiDoan(DateTime.Parse(txtHieuLucHopDong.SelectedDate.ToString()), DateTime.Parse(txtNgayKhoiCong.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanThiCong.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanHongDong.SelectedDate.ToString()), out o_Messages) == false)
                    {
                        pnThongBao.Visible = true;
                        lblThongBao.Text = "Thời gian thi công phải nằm trong thời gian của hợp đồng. Vui lòng kiểm tra lại.";
                        txtNgayKhoiCong.Focus();
                    }
                }
                else
                {
                    if (GIAHAN_ID == 0) // Thêm mới
                    {
                        var objQLHD_GIAHANHD = new QLHD_GIAHANHD();
                        objQLHD_GIAHANHD.HD_BLTAMUNG_TUNGAY = txtBLTamUngTuNgay.SelectedDate ?? DateTime.Now;
                        objQLHD_GIAHANHD.HD_BLTAMUNG_DENNGAY = txtBLTamUngDenNgay.SelectedDate ?? DateTime.Now;
                        objQLHD_GIAHANHD.HD_BLTAMUNG_TGNHAC = Int32.Parse(txtTGDenHanBLTamUng.Text.ToString().Replace(".", ""));

                        objQLHD_GIAHANHD.HD_BLTHANHTOANVATTU_DENNGAY = txtBLThanhToanVatTuDenNgay.SelectedDate ?? DateTime.Now;
                        objQLHD_GIAHANHD.HD_BLTHANHTOANVATTU_TGNHAC = Int32.Parse(txtTGDenHanBLThanhToanVatTu.Text.ToString().Replace(".", ""));
                        objQLHD_GIAHANHD.HD_BLTHANHTOANVATTU_TUNGAY = txtBLThanhToanVatTuTuNgay.SelectedDate ?? DateTime.Now;

                        objQLHD_GIAHANHD.HD_BLTHUCHIENHOPDONG_DENNGAY = txtBLThucHienHopDongDenNgay.SelectedDate ?? DateTime.Now;
                        objQLHD_GIAHANHD.HD_BLTHUCHIENHOPDONG_TGNHAC = Int32.Parse(txtTGDenHanBLThucHienHD.Text.ToString().Replace(".", ""));
                        objQLHD_GIAHANHD.HD_BLTHUCHIENHOPDONG_TUNGAY = txtBLThucHienHopDongTuNgay.SelectedDate ?? DateTime.Now;

                        objQLHD_GIAHANHD.HD_HIEULUC_HD = txtHieuLucHopDong.SelectedDate ?? DateTime.Now;
                        objQLHD_GIAHANHD.HD_NGAYHETHAN_HD = txtNgayHetHanHongDong.SelectedDate ?? DateTime.Now;
                        objQLHD_GIAHANHD.HD_TGNHAC = Int32.Parse(txtTGDenHanHD.Text.ToString().Replace(".", ""));

                        objQLHD_GIAHANHD.HD_NGAYHETHANTHICONG = txtNgayHetHanThiCong.SelectedDate ?? DateTime.Now;
                        objQLHD_GIAHANHD.HD_NGAYKHOICONG = txtNgayKhoiCong.SelectedDate ?? DateTime.Now;
                        objQLHD_GIAHANHD.HD_THICONG_TGNHAC = Int32.Parse(txtTGDenHanThiCong.Text.ToString().Replace(".", ""));

                        objQLHD_GIAHANHD.HD_ID = HD_ID;
                        objQLHD_GIAHANHD.GIAHAN_GHICHU = txtGhiChu.Text;
                        InsertQLHD_GIAHANHD(objQLHD_GIAHANHD);
                        int vSoLanGiaHan = vDC.QLHD_GIAHANHDs.Where(x => x.HD_ID == HD_ID).ToList().Count;
                        UpdateQLHD_GIAHANHD(HD_ID, vSoLanGiaHan);
                        Session[TabId + "_Message"] = "Thêm mới gia hạn hợp đồng thành công";
                        Session[TabId + "_Type"] = "success";
                        Response.Redirect(Globals.NavigateURL());
                    }
                    else
                    {
                        var objQLHD_HD = new QLHD_GIAHANHD();
                        objQLHD_HD.HD_ID = HD_ID;
                        objQLHD_HD.HD_BLTAMUNG_TUNGAY = txtBLTamUngTuNgay.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_BLTAMUNG_DENNGAY = txtBLTamUngDenNgay.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_BLTAMUNG_TGNHAC = Int32.Parse(txtTGDenHanBLTamUng.Text.ToString().Replace(".", ""));

                        objQLHD_HD.HD_BLTHANHTOANVATTU_DENNGAY = txtBLThanhToanVatTuDenNgay.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_BLTHANHTOANVATTU_TGNHAC = Int32.Parse(txtTGDenHanBLThanhToanVatTu.Text.ToString().Replace(".", ""));
                        objQLHD_HD.HD_BLTHANHTOANVATTU_TUNGAY = txtBLThanhToanVatTuTuNgay.SelectedDate ?? DateTime.Now;

                        objQLHD_HD.HD_BLTHUCHIENHOPDONG_DENNGAY = txtBLThucHienHopDongDenNgay.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_BLTHUCHIENHOPDONG_TGNHAC = Int32.Parse(txtTGDenHanBLThucHienHD.Text.ToString().Replace(".", ""));
                        objQLHD_HD.HD_BLTHUCHIENHOPDONG_TUNGAY = txtBLThucHienHopDongTuNgay.SelectedDate ?? DateTime.Now;

                        objQLHD_HD.HD_HIEULUC_HD = txtHieuLucHopDong.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_NGAYHETHAN_HD = txtNgayHetHanHongDong.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_TGNHAC = Int32.Parse(txtTGDenHanHD.Text.ToString().Replace(".", ""));

                        objQLHD_HD.HD_NGAYHETHANTHICONG = txtNgayHetHanThiCong.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_NGAYKHOICONG = txtNgayKhoiCong.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_THICONG_TGNHAC = Int32.Parse(txtTGDenHanThiCong.Text.ToString().Replace(".", ""));

                        objQLHD_HD.GIAHAN_ID = GIAHAN_ID;
                        objQLHD_HD.GIAHAN_GHICHU = txtGhiChu.Text;
                        UpdateQLHD_GIAHANHD(objQLHD_HD);
                        int vSoLanGiaHan = vDC.QLHD_GIAHANHDs.Where(x => x.HD_ID == HD_ID).ToList().Count;
                        UpdateQLHD_GIAHANHD(HD_ID, vSoLanGiaHan);

                        Session[TabId + "_Message"] = "Cập nhật gia hạn hợp đồng thành công";
                        Session[TabId + "_Type"] = "success";
                        Response.Redirect(Globals.NavigateURL(), false);
                    }

                }


            }
            catch (Exception ex)
            {
                ClassCommon.THONGBAO_TOASTR(Page, ex, _currentUser, ClassParameter.unknownErrorMessage, "Thông báo lỗi", "error");
                log.Error("", ex);
            }
        }
        protected void btnBoQua_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL("giahan", "mid=" + this.ModuleId, "title=Gia hạn hợp đồng", "HD_ID=" + HD_ID));
        }
        #endregion
        #region Hàm kiểm tra ngày
        public bool Check_ThoiGian(DateTime NgayBatDau, DateTime NgayKetThuc, out string o_Messages)
        {
            try
            {
                o_Messages = "";
                if (NgayBatDau > NgayKetThuc)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                o_Messages = "Đã có lỗi trong quá trình xử lý";
                ClassCommon.THONGBAO_TOASTR(Page, ex, _currentUser, ClassParameter.unknownErrorMessage, "Thông báo lỗi", "error");
                log.Error("", ex);
                return false;
            }
        }
        public bool Check_ThoiGian_DuAn_GiaiDoan(DateTime NgayBatDau, DateTime NgayBatDauGiaiDoan, DateTime NgayKetThucGiaiDoan, DateTime NgayKetThuc, out string o_Messages)
        {
            try
            {
                o_Messages = "";
                if (NgayBatDau <= NgayBatDauGiaiDoan && NgayBatDau <= NgayKetThucGiaiDoan && NgayKetThuc >= NgayBatDauGiaiDoan && NgayKetThuc >= NgayKetThucGiaiDoan)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                o_Messages = "Đã có lỗi trong quá trình xử lý";
                ClassCommon.THONGBAO_TOASTR(Page, ex, _currentUser, ClassParameter.unknownErrorMessage, "Thông báo lỗi", "error");
                log.Error("", ex);
                return false;
            }
        }

        #endregion

    }
}
