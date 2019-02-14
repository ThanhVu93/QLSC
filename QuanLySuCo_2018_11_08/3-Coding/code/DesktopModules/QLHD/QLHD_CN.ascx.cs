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
    public partial class QLHD_CN : DotNetNuke.Entities.Modules.UserModuleBase
    {
        #region Khai báo, định nghĩa đối tượng

        private readonly ILog log = LoggerSource.Instance.GetLogger(typeof(QLHD_CN).FullName);

        public string vPathCommon = ClassParameter.vPathCommon;
        public string vPathCommonJS = ClassParameter.vPathCommonJavascript;
        ClassCommon clsCommon = new ClassCommon();
        QLHDDataContext vDC = new QLHDDataContext();
        UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
        DataTable dtTable;
        int HD_ID = 0;
        TAPTINController objTAPTINController = new TAPTINController();
        QLHD_TAPTIN objTapTin = new QLHD_TAPTIN();
        public string vPathDataBieuMau = ClassParameter.vPathDataBieuMau;
        public string vJavascriptMask;

        #endregion
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            vJavascriptMask = ClassParameter.vJavascriptMaskNumber;
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

                if (!IsPostBack)
                {
                    SetInfoForm(HD_ID);
                    LoadDSTT(HD_ID);
                    txtGhiChu.Attributes.Add("maxlength", txtGhiChu.MaxLength.ToString());
                    txtTenCongTrinh.Attributes.Add("maxlength", txtTenCongTrinh.MaxLength.ToString());
                    GetSessionTapTin();
                }

            }
            catch (Exception ex)
            {
                ClassCommon.THONGBAO_TOASTR(Page, ex, _currentUser, "Có lỗi trong quá trình xử lý, vui lòng liên hệ với quản trị!", "Thông báo lỗi", "error");
                log.Error("", ex);
            }
        }

        #endregion
        protected void GetSessionTapTin()
        {
            if (Session["TapTin"] != null)
            {
                DataTable dt = Session["TapTin"] as DataTable;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objTapTin = new QLHD_TAPTIN();
                    objTapTin.FILE_NAME = dt.Rows[0]["FILE_NAME"].ToString();
                    objTapTin.FILE_MOTA = dt.Rows[0]["FILE_MOTA"].ToString().Replace("<i class='fa fa-file-word-o icon_upload'></i>", "");
                    objTapTin.FILE_EXT = dt.Rows[0]["FILE_EXT"].ToString();
                    objTapTin.FILE_SIZE = Int32.Parse(dt.Rows[0]["FILE_SIZE"].ToString());
                }
                dtTable = new DataTable();
                dtTable.Columns.Add("FILE_NAME");
                dtTable.Columns.Add("FILE_MOTA");
                dtTable.Columns.Add("FILE_EXT");
                dtTable.Columns.Add("FILE_SIZE");

                DataRow row = dtTable.NewRow();
                row["FILE_NAME"] = objTapTin.FILE_NAME;
                row["FILE_MOTA"] = objTapTin.FILE_MOTA;
                row["FILE_EXT"] = objTapTin.FILE_EXT;
                row["FILE_SIZE"] = objTapTin.FILE_SIZE;

                dtTable.Rows.Add(row);
                //Session["dgDanhSach"] = dtTable;
                GridView1.DataSource = dtTable;
                GridView1.DataBind();
            }

        }

        #region Tập tin
        public void LoadDSTT(int id = 0)
        {
            try
            {
                dtTable = new DataTable();
                dtTable.Columns.Add("FILE_NAME");
                dtTable.Columns.Add("FILE_MOTA");
                dtTable.Columns.Add("FILE_EXT");
                dtTable.Columns.Add("FILE_SIZE");
                if (id > 0)
                {
                    var temp = objTAPTINController.Get_TapTin_By_ObjectID_BieuMau(id);
                    if (temp != null)
                    {
                        DataRow row = dtTable.NewRow();

                        int FileSize_info = (temp.FILE_SIZE) / 1024;
                        string FileSize = "";
                        if (FileSize_info > 1024 * 1024)
                            FileSize = FileSize_info / 1024 + " MB";
                        else
                            FileSize = FileSize_info + " KB";

                        row["FILE_NAME"] = temp.FILE_NAME;
                        row["FILE_MOTA"] = "<i class='fa fa-file-word-o icon_upload'></i>" + temp.FILE_MOTA;
                        row["FILE_EXT"] = temp.FILE_EXT;
                        row["FILE_SIZE"] = temp.FILE_SIZE;
                        dtTable.Rows.Add(row);
                    }
                }

                Session["dgDanhSach"] = dtTable;
                GridView1.DataSource = dtTable;
                GridView1.DataBind();
                if (Session["dgDanhSach"] != null)
                {
                    if (dtTable.Rows.Count == 0) Session["dgDanhSach"] = null;

                }
            }
            catch (Exception ex)
            {
                log.Error("", ex);
            }

        }
        protected void btn_TaiXuong(object sender, EventArgs e)
        {
            try
            {
                HtmlAnchor html = (HtmlAnchor)sender;
                string FILE_NAME = html.HRef.ToString();
                if (Session["TapTin"] != null)
                {
                    DataTable dt = Session["TapTin"] as DataTable;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objTapTin = new QLHD_TAPTIN();
                        objTapTin.FILE_NAME = dt.Rows[0]["FILE_NAME"].ToString();
                        objTapTin.FILE_MOTA = dt.Rows[0]["FILE_MOTA"].ToString().Replace("<i class='fa fa-file-word-o icon_upload'></i>", "");
                    }
                }
                else
                {
                    if (Session["dgDanhSach"] != null)
                    {
                        DataTable dt = Session["dgDanhSach"] as DataTable;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objTapTin = new QLHD_TAPTIN();
                            objTapTin.FILE_NAME = dt.Rows[0]["FILE_NAME"].ToString();
                            objTapTin.FILE_MOTA = dt.Rows[0]["FILE_MOTA"].ToString().Replace("<i class='fa fa-file-word-o icon_upload'></i>", "");
                        }
                    }
                    //BM_ID = Convert.ToInt32(Request.QueryString["BM_ID"]);
                    //objTapTin = new QLCC_TAPTIN();
                    //objTapTin = objTAPTINController.Get_TapTin_By_ObjectID_BieuMau(BM_ID);
                }
                string sourceFile = Server.MapPath(ClassParameter.vPathDataBieuMau) + objTapTin.FILE_NAME;
                byte[] byteArray = File.ReadAllBytes(sourceFile);
                using (MemoryStream mem = new MemoryStream())
                {
                    mem.Write(byteArray, 0, (int)byteArray.Length);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ClearHeaders();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + objTapTin.FILE_MOTA + ".docx");
                    Response.Charset = "";
                    Response.ContentType = "application/octet-stream";
                    Response.BinaryWrite(mem.ToArray());
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.SuppressContent = true;
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception ex)
            {
                log.Error("", ex);
            }

        }

        protected void btn_TL_Click(object sender, EventArgs e)
        {
            pnThongBao.Visible = false;
            lblThongBao.Text = "";
            if (f_TapTin.HasFile)
            {
                string filepath = Server.MapPath(vPathDataBieuMau);
                HttpFileCollection uploadedFiles = Request.Files;
                HttpPostedFile userPostedFile = uploadedFiles[0];
                try
                {
                    string filePath = f_TapTin.PostedFile.FileName;          // getting the file path of uploaded file
                    string filename1 = Path.GetFileName(filePath);
                    string ext = Path.GetExtension(filename1);
                    string type = String.Empty;
                    if (userPostedFile.ContentLength < 1048576 * 10)
                    {
                        string filename = userPostedFile.FileName;
                        string extension = System.IO.Path.GetExtension(filename);
                        //string result = filename.Substring(0, filename.Length - extension.Length)+"_"+ ClassCommon.GetUploadDateTime().ToString()+"."+extension;
                        string result_Name = filename.Substring(0, filename.Length - extension.Length);
                        string result = ClassCommon.GetGuid() + extension;
                        ClassCommon.UploadFile(userPostedFile, filepath, result, "");
                        //ClassCommon.UploadFile(userPostedFile, filepath, filename, "");

                        dtTable = new DataTable();
                        dtTable.Columns.Add("FILE_NAME");
                        dtTable.Columns.Add("FILE_MOTA");
                        dtTable.Columns.Add("FILE_EXT");
                        dtTable.Columns.Add("FILE_SIZE");
                        int FileSize_info = (userPostedFile.ContentLength) / 1024;
                        string FileSize = "";
                        if (FileSize_info > 1024 * 1024)
                            FileSize = FileSize_info / 1024 + " MB";
                        else
                            FileSize = FileSize_info + " KB";
                        DataRow row = dtTable.NewRow();
                        row["FILE_NAME"] = result;
                        row["FILE_MOTA"] = "<i class='fa fa-file-word-o icon_upload'></i>" + filename.ToString() + " (" + FileSize + ")";
                        row["FILE_EXT"] = extension;
                        row["FILE_SIZE"] = userPostedFile.ContentLength.ToString();

                        dtTable.Rows.Add(row);
                        Session["dgDanhSach"] = dtTable;
                        Session["TapTin"] = dtTable;
                        GridView1.DataSource = dtTable;
                        GridView1.DataBind();
                    }
                    else
                    {
                        pnThongBao.Visible = true;
                        lblThongBao.Text = "Kích thước tập tin nhỏ hơn 10M";
                    }
                }
                catch (Exception ex)
                {
                    log.Error("", ex);
                }
            }
        }

        #endregion

        #region SetInfoFrom
        public void SetInfoForm(int HD_ID)
        {
            txtTenCongTrinh.Focus();
            if (HD_ID == 0) // Thêm mới
            {
                btnKetThucHopDong.Visible = false;
            }
            else
            {
                var objQLHD_HD = vDC.QLHD_HDs.Where(x => x.HD_ID == HD_ID).FirstOrDefault();
                txtTenCongTrinh.Text = objQLHD_HD.HD_TENCONGTRINH.ToString();
                txtTenHopDong.Text = objQLHD_HD.HD_TEN.ToString();
                txtSoHopDong.Text = objQLHD_HD.HD_SO;
                txtNgayKy.SelectedDate = objQLHD_HD.HD_NGAYKY;
                txtHieuLucHopDong.SelectedDate = objQLHD_HD.HD_HIEULUC_HD;
                txtThoiGianThucHien.Text = String.Format("{0:#,##0.##}", objQLHD_HD.HD_THOIGIANTHUCHIEN).Replace(",", ".");
                txtNgayHetHanHongDong.SelectedDate = objQLHD_HD.HD_NGAYHETHAN_HD;
                txtGiaTri.Text = String.Format("{0:#,##0.##}", objQLHD_HD.HD_GIATRI).Replace(",", ".");
                txtTGDenHanHD.Text = String.Format("{0:#,##0.##}", objQLHD_HD.HD_TGNHAC).Replace(",", ".");
                txtGhiChu.Text = objQLHD_HD.HD_GHICHU;

                txtDonViThiCong.Text = objQLHD_HD.HD_TENDONVITHICONG;
                txtNgayKhoiCong.SelectedDate = objQLHD_HD.HD_NGAYKHOICONG;
                txtThoiGianThiCong.Text = String.Format("{0:#,##0.##}", objQLHD_HD.HD_THOIGIANTHICONG).Replace(",", ".");
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
                btnCapNhatTiepTuc.Visible = false;
                if (objQLHD_HD.HD_TRANGTHAI == 1)
                {
                    btnKetThucHopDong.Visible = false;
                    btnHuyKetThucHopDong.Visible = true;
                }
            }
        }

        #endregion
        #region Controller
        public void InsertQLHD_HD(QLHD_HD objQLHD_HD)
        {
            vDC.QLHD_HDs.InsertOnSubmit(objQLHD_HD);
            vDC.SubmitChanges();
        }
        public void UpdateQLHD_HD(QLHD_HD objQLHD_HD)
        {
            var obj = GetQLHD_HD(objQLHD_HD.HD_ID);
            obj.HD_BLTAMUNG_TUNGAY = objQLHD_HD.HD_BLTAMUNG_TUNGAY;
            obj.HD_BLTAMUNG_DENNGAY = objQLHD_HD.HD_BLTAMUNG_DENNGAY;
            obj.HD_BLTAMUNG_TGNHAC = objQLHD_HD.HD_BLTAMUNG_TGNHAC;
            obj.HD_BLTHANHTOANVATTU_DENNGAY = objQLHD_HD.HD_BLTHANHTOANVATTU_DENNGAY;
            obj.HD_BLTHANHTOANVATTU_TGNHAC = objQLHD_HD.HD_BLTHANHTOANVATTU_TGNHAC;
            obj.HD_BLTHANHTOANVATTU_TUNGAY = objQLHD_HD.HD_BLTHANHTOANVATTU_TUNGAY;
            obj.HD_BLTHUCHIENHOPDONG_DENNGAY = objQLHD_HD.HD_BLTHUCHIENHOPDONG_DENNGAY;
            obj.HD_BLTHUCHIENHOPDONG_TGNHAC = objQLHD_HD.HD_BLTHUCHIENHOPDONG_TGNHAC;
            obj.HD_BLTHUCHIENHOPDONG_TUNGAY = objQLHD_HD.HD_BLTHUCHIENHOPDONG_TUNGAY;
            obj.HD_COGIAHAN = false;
            obj.HD_GHICHU = objQLHD_HD.HD_GHICHU;
            obj.HD_GIATRI = objQLHD_HD.HD_GIATRI;
            obj.HD_HIEULUC_HD = objQLHD_HD.HD_HIEULUC_HD;
            obj.HD_NGAYHETHANTHICONG = objQLHD_HD.HD_NGAYHETHANTHICONG;
            obj.HD_NGAYHETHAN_HD = objQLHD_HD.HD_NGAYHETHAN_HD;
            obj.HD_NGAYKHOICONG = objQLHD_HD.HD_NGAYKHOICONG;
            obj.HD_NGAYKY = objQLHD_HD.HD_NGAYKY;
            obj.HD_SO = objQLHD_HD.HD_SO;
            obj.HD_SOLANGIAHAN = 0;
            obj.HD_TEN = objQLHD_HD.HD_TEN;
            obj.HD_TENCONGTRINH = objQLHD_HD.HD_TENCONGTRINH;
            obj.HD_TENDONVITHICONG = objQLHD_HD.HD_TENDONVITHICONG;
            obj.HD_TGNHAC = objQLHD_HD.HD_TGNHAC;
            obj.HD_THICONG_TGNHAC = objQLHD_HD.HD_THICONG_TGNHAC;
            obj.HD_THOIGIANTHICONG = objQLHD_HD.HD_THOIGIANTHICONG;
            obj.HD_THOIGIANTHUCHIEN = objQLHD_HD.HD_THOIGIANTHUCHIEN;
            obj.HD_TRANGTHAI = objQLHD_HD.HD_TRANGTHAI;
            obj.HD_XOA = false;
            obj.HD_ID = objQLHD_HD.HD_ID;
            obj.HD_SOLANGIAHAN = objQLHD_HD.HD_SOLANGIAHAN;
            vDC.SubmitChanges();
        }
        public QLHD_HD GetQLHD_HD(int ID)
        {
            var objQLHD_HD = (from obj in vDC.QLHD_HDs
                              where obj.HD_ID == ID
                              orderby obj.HD_ID descending
                              select obj).FirstOrDefault();

            return objQLHD_HD;
        }

        #endregion
        #region Page Events
        protected void btnCapNhat_Click(object sender, EventArgs e)
        {

            try
            {
                string o_Messages = "";
                LinkButton btn = sender as LinkButton;
                string action = btn.CommandName;
                if (Check_ThoiGian(DateTime.Parse(txtNgayKhoiCong.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanThiCong.SelectedDate.ToString()), out o_Messages) == false
                    || Check_ThoiGian(DateTime.Parse(txtBLThucHienHopDongTuNgay.SelectedDate.ToString()), DateTime.Parse(txtBLThucHienHopDongDenNgay.SelectedDate.ToString()), out o_Messages) == false
                    || Check_ThoiGian(DateTime.Parse(txtBLThanhToanVatTuTuNgay.SelectedDate.ToString()), DateTime.Parse(txtBLThanhToanVatTuDenNgay.SelectedDate.ToString()), out o_Messages) == false
                    || Check_ThoiGian(DateTime.Parse(txtBLTamUngTuNgay.SelectedDate.ToString()), DateTime.Parse(txtBLTamUngDenNgay.SelectedDate.ToString()), out o_Messages) == false
                    || Check_ThoiGian(DateTime.Parse(txtHieuLucHopDong.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanHongDong.SelectedDate.ToString()), out o_Messages) == false

                    //|| Check_ThoiGian_DuAn_GiaiDoan(DateTime.Parse(txtHieuLucHopDong.SelectedDate.ToString()), DateTime.Parse(txtNgayKhoiCong.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanThiCong.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanHongDong.SelectedDate.ToString()), out o_Messages) == false
                    //|| Check_ThoiGian_DuAn_GiaiDoan(DateTime.Parse(txtHieuLucHopDong.SelectedDate.ToString()), DateTime.Parse(txtBLThucHienHopDongTuNgay.SelectedDate.ToString()), DateTime.Parse(txtBLThucHienHopDongDenNgay.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanHongDong.SelectedDate.ToString()), out o_Messages) == false
                    //|| Check_ThoiGian_DuAn_GiaiDoan(DateTime.Parse(txtHieuLucHopDong.SelectedDate.ToString()), DateTime.Parse(txtBLThanhToanVatTuTuNgay.SelectedDate.ToString()), DateTime.Parse(txtBLThanhToanVatTuDenNgay.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanHongDong.SelectedDate.ToString()), out o_Messages) == false
                    //|| Check_ThoiGian_DuAn_GiaiDoan(DateTime.Parse(txtHieuLucHopDong.SelectedDate.ToString()), DateTime.Parse(txtBLTamUngTuNgay.SelectedDate.ToString()), DateTime.Parse(txtBLTamUngDenNgay.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanHongDong.SelectedDate.ToString()), out o_Messages) == false
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


                    if (Check_ThoiGian_DuAn_GiaiDoan(DateTime.Parse(txtHieuLucHopDong.SelectedDate.ToString()), DateTime.Parse(txtBLTamUngTuNgay.SelectedDate.ToString()), DateTime.Parse(txtBLTamUngDenNgay.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanHongDong.SelectedDate.ToString()), out o_Messages) == false)
                    {
                        pnThongBao.Visible = true;
                        lblThongBao.Text = "Thời gian bảo lãnh tạm ứng phải nằm trong thời gian của hợp đồng. Vui lòng kiểm tra lại.";
                        txtBLTamUngTuNgay.Focus();
                    }
                    if (Check_ThoiGian_DuAn_GiaiDoan(DateTime.Parse(txtHieuLucHopDong.SelectedDate.ToString()), DateTime.Parse(txtBLThanhToanVatTuTuNgay.SelectedDate.ToString()), DateTime.Parse(txtBLThanhToanVatTuDenNgay.SelectedDate.ToString()), DateTime.Parse(txtNgayHetHanHongDong.SelectedDate.ToString()), out o_Messages) == false)
                    {
                        pnThongBao.Visible = true;
                        lblThongBao.Text = "Thời gian bảo lãnh thanh toán vật tư phải nằm trong thời gian của hợp đồng. Vui lòng kiểm tra lại.";
                        txtBLThanhToanVatTuTuNgay.Focus();
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
                    if (HD_ID == 0) // Thêm mới
                    {
                        var objQLHD_HD = new QLHD_HD();
                        objQLHD_HD.HD_BLTAMUNG_TUNGAY = txtBLTamUngTuNgay.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_BLTAMUNG_DENNGAY = txtBLTamUngDenNgay.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_BLTAMUNG_TGNHAC = Int32.Parse(txtTGDenHanBLTamUng.Text.ToString().Replace(".", ""));

                        objQLHD_HD.HD_BLTHANHTOANVATTU_DENNGAY = txtBLThanhToanVatTuDenNgay.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_BLTHANHTOANVATTU_TGNHAC = Int32.Parse(txtTGDenHanBLThanhToanVatTu.Text.ToString().Replace(".", ""));
                        objQLHD_HD.HD_BLTHANHTOANVATTU_TUNGAY = txtBLThanhToanVatTuTuNgay.SelectedDate ?? DateTime.Now; ;

                        objQLHD_HD.HD_BLTHUCHIENHOPDONG_DENNGAY = txtBLThucHienHopDongDenNgay.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_BLTHUCHIENHOPDONG_TGNHAC = Int32.Parse(txtTGDenHanBLThucHienHD.Text.ToString().Replace(".", ""));
                        objQLHD_HD.HD_BLTHUCHIENHOPDONG_TUNGAY = txtBLThucHienHopDongTuNgay.SelectedDate ?? DateTime.Now;

                        objQLHD_HD.HD_COGIAHAN = false;
                        objQLHD_HD.HD_GHICHU = txtGhiChu.Text;
                        objQLHD_HD.HD_GIATRI = decimal.Parse(txtGiaTri.Text.ToString()); ;
                        objQLHD_HD.HD_HIEULUC_HD = txtHieuLucHopDong.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_NGAYHETHANTHICONG = txtNgayHetHanThiCong.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_NGAYHETHAN_HD = txtNgayHetHanHongDong.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_NGAYKHOICONG = txtNgayKhoiCong.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_NGAYKY = txtNgayKy.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_SO = txtSoHopDong.Text;
                        objQLHD_HD.HD_SOLANGIAHAN = 0;
                        objQLHD_HD.HD_TEN = txtTenHopDong.Text;
                        objQLHD_HD.HD_TENCONGTRINH = txtTenCongTrinh.Text;
                        objQLHD_HD.HD_TENDONVITHICONG = txtDonViThiCong.Text;
                        objQLHD_HD.HD_TGNHAC = Int32.Parse(txtTGDenHanHD.Text.ToString().Replace(".", "")); ;
                        objQLHD_HD.HD_THICONG_TGNHAC = Int32.Parse(txtTGDenHanThiCong.Text.ToString().Replace(".", ""));
                        objQLHD_HD.HD_THOIGIANTHICONG = Int32.Parse(txtThoiGianThiCong.Text.ToString().Replace(".", ""));
                        objQLHD_HD.HD_THOIGIANTHUCHIEN = Int32.Parse(txtThoiGianThucHien.Text.ToString().Replace(".", ""));
                        objQLHD_HD.HD_TRANGTHAI = 2;
                        objQLHD_HD.HD_XOA = false;
                        objQLHD_HD.HD_SOLANGIAHAN = 0;
                        InsertQLHD_HD(objQLHD_HD);


                        //Tập tin
                        DataTable dt = new DataTable();
                        if (Session["TapTin"] != null)
                        {
                            dt = Session["TapTin"] as DataTable;
                        }
                        else
                        {
                            if (Session["dgDanhSach"] != null)
                            {
                                dt = Session["dgDanhSach"] as DataTable;
                            }
                        }

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objTapTin = new QLHD_TAPTIN();
                            objTapTin.FILE_NAME = dt.Rows[0]["FILE_NAME"].ToString();
                            objTapTin.FILE_MOTA = dt.Rows[0]["FILE_MOTA"].ToString().Replace("<i class='fa fa-file-word-o icon_upload'></i>", "");
                            objTapTin.FILE_EXT = dt.Rows[i]["FILE_EXT"].ToString();
                            objTapTin.FILE_SIZE = Int32.Parse(dt.Rows[i]["FILE_SIZE"].ToString());
                            objTapTin.FILE_USERID_CAPNHAT = _currentUser.UserID;
                            objTapTin.FILE_NGAYCAPNHAT = DateTime.Now;
                            objTapTin.OBJECT_LOAI = (int)CommonEnum.TapTinObjectLoai.BieuMau;
                            objTapTin.OBJECT_ID = objQLHD_HD.HD_ID;
                            objTAPTINController.ThemTapTin(objTapTin);
                        }

                        Session[TabId + "_Message"] = "Thêm mới hợp đồng thành công";
                        Session[TabId + "_Type"] = "success";

                        if (action == "TiepTuc")
                        {
                            Session.Remove("TapTin");
                            Response.Redirect(Globals.NavigateURL("edit", "mid=" + Request.Params["mid"].ToString(), "title=Thêm mới hợp đồng"));
                        }
                        else
                        {
                            Response.Redirect(Globals.NavigateURL());
                        }
                    }
                    else
                    {
                        var objQLHD_HD = new QLHD_HD();
                        objQLHD_HD.HD_ID = HD_ID;
                        objQLHD_HD.HD_BLTAMUNG_TUNGAY = txtBLTamUngTuNgay.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_BLTAMUNG_DENNGAY = txtBLTamUngDenNgay.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_BLTAMUNG_TGNHAC = Int32.Parse(txtTGDenHanBLTamUng.Text.ToString().Replace(".", ""));
                        objQLHD_HD.HD_BLTHANHTOANVATTU_DENNGAY = txtBLThanhToanVatTuDenNgay.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_BLTHANHTOANVATTU_TGNHAC = Int32.Parse(txtTGDenHanBLThanhToanVatTu.Text.ToString().Replace(".", ""));
                        objQLHD_HD.HD_BLTHANHTOANVATTU_TUNGAY = txtBLThanhToanVatTuTuNgay.SelectedDate ?? DateTime.Now; ;
                        objQLHD_HD.HD_BLTHUCHIENHOPDONG_DENNGAY = txtBLThucHienHopDongDenNgay.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_BLTHUCHIENHOPDONG_TGNHAC = Int32.Parse(txtTGDenHanBLThucHienHD.Text.ToString().Replace(".", ""));
                        objQLHD_HD.HD_BLTHUCHIENHOPDONG_TUNGAY = txtBLThucHienHopDongTuNgay.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_COGIAHAN = false;
                        objQLHD_HD.HD_GHICHU = txtGhiChu.Text;
                        objQLHD_HD.HD_GIATRI = decimal.Parse(txtGiaTri.Text.ToString()); ;
                        objQLHD_HD.HD_HIEULUC_HD = txtHieuLucHopDong.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_NGAYHETHANTHICONG = txtNgayHetHanThiCong.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_NGAYHETHAN_HD = txtNgayHetHanHongDong.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_NGAYKHOICONG = txtNgayKhoiCong.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_NGAYKY = txtNgayKy.SelectedDate ?? DateTime.Now;
                        objQLHD_HD.HD_SO = txtSoHopDong.Text;
                        objQLHD_HD.HD_TEN = txtTenHopDong.Text;
                        objQLHD_HD.HD_TENCONGTRINH = txtTenCongTrinh.Text;
                        objQLHD_HD.HD_TENDONVITHICONG = txtDonViThiCong.Text;
                        objQLHD_HD.HD_TGNHAC = Int32.Parse(txtTGDenHanHD.Text.ToString().Replace(".", ""));
                        objQLHD_HD.HD_THICONG_TGNHAC = Int32.Parse(txtTGDenHanThiCong.Text.ToString().Replace(".", ""));
                        objQLHD_HD.HD_THOIGIANTHICONG = Int32.Parse(txtThoiGianThiCong.Text.ToString().Replace(".", ""));
                        objQLHD_HD.HD_THOIGIANTHUCHIEN = Int32.Parse(txtThoiGianThucHien.Text.ToString().Replace(".", ""));
                        objQLHD_HD.HD_TRANGTHAI = 2;
                        objQLHD_HD.HD_XOA = false;

                        int vSoLanGiaHan = vDC.QLHD_GIAHANHDs.Where(x => x.HD_ID == HD_ID).ToList().Count;
                        objQLHD_HD.HD_SOLANGIAHAN = vSoLanGiaHan;
                        UpdateQLHD_HD(objQLHD_HD);

                        //Tập tin
                        var objTT = objTAPTINController.Get_TapTin_By_ObjectID(HD_ID);
                        foreach (var it1 in objTT)
                        {
                            objTAPTINController.XOA_TAPTIN(it1.FILE_ID);
                        }

                        if (Session["TapTin"] != null)
                        {
                            DataTable dt = Session["TapTin"] as DataTable;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                objTapTin = new QLHD_TAPTIN();
                                objTapTin.FILE_NAME = dt.Rows[0]["FILE_NAME"].ToString();
                                objTapTin.FILE_MOTA = dt.Rows[0]["FILE_MOTA"].ToString().Replace("<i class='fa fa-file-word-o icon_upload'></i>", "");
                                objTapTin.FILE_EXT = dt.Rows[0]["FILE_EXT"].ToString();
                                objTapTin.FILE_SIZE = Int32.Parse(dt.Rows[0]["FILE_SIZE"].ToString());
                                objTapTin.FILE_USERID_CAPNHAT = _currentUser.UserID;
                                objTapTin.FILE_NGAYCAPNHAT = DateTime.Now;
                                objTapTin.OBJECT_LOAI = (int)CommonEnum.TapTinObjectLoai.BieuMau;
                                objTapTin.OBJECT_ID = HD_ID;
                                objTAPTINController.ThemTapTin(objTapTin);
                            }
                        }
                        else
                        {
                            if (Session["dgDanhSach"] != null)
                            {
                                DataTable dt = Session["dgDanhSach"] as DataTable;
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    objTapTin = new QLHD_TAPTIN();
                                    objTapTin.FILE_NAME = dt.Rows[0]["FILE_NAME"].ToString();
                                    objTapTin.FILE_MOTA = dt.Rows[0]["FILE_MOTA"].ToString().Replace("<i class='fa fa-file-word-o icon_upload'></i>", "");
                                    objTapTin.FILE_EXT = dt.Rows[0]["FILE_EXT"].ToString();
                                    objTapTin.FILE_SIZE = Int32.Parse(dt.Rows[0]["FILE_SIZE"].ToString());
                                    objTapTin.FILE_USERID_CAPNHAT = _currentUser.UserID;
                                    objTapTin.FILE_NGAYCAPNHAT = DateTime.Now;
                                    objTapTin.OBJECT_LOAI = (int)CommonEnum.TapTinObjectLoai.BieuMau;
                                    objTapTin.OBJECT_ID = HD_ID;
                                    objTAPTINController.ThemTapTin(objTapTin);
                                }
                            }
                        }

                        Session[TabId + "_Message"] = "Cập nhật hợp đồng thành công";
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
            Response.Redirect(Globals.NavigateURL());
        }
        public void ketthucHD(object sender, EventArgs e)
        {
            var objQLHD_HD = vDC.QLHD_HDs.Where(x => x.HD_ID == HD_ID).FirstOrDefault();
            if (objQLHD_HD != null)
            {
                if (objQLHD_HD.HD_TRANGTHAI != 1)
                {
                    objQLHD_HD.HD_TRANGTHAI = 1;
                    vDC.SubmitChanges();
                    btnHuyKetThucHopDong.Visible = true;
                    btnKetThucHopDong.Visible = false;
                    ClassCommon.ShowToastr(Page, "Kết thúc hợp đồng thành công.", "Thông báo", "success");
                }
                else
                {
                    objQLHD_HD.HD_TRANGTHAI = 2;
                    vDC.SubmitChanges();
                    btnHuyKetThucHopDong.Visible = false;
                    btnKetThucHopDong.Visible = true;
                    ClassCommon.ShowToastr(Page, "Hủy kết thúc hợp đồng thành công.", "Thông báo", "success");
                }
            }

        }
        protected void txtThoiGianThucHien_TextChanged(object sender, EventArgs e)
        {
            if (txtHieuLucHopDong.SelectedDate.HasValue)
            {

                DateTime temp = txtHieuLucHopDong.SelectedDate.Value.AddDays(Int32.Parse(txtThoiGianThucHien.Text.ToString().Replace(".", "")));
                txtNgayHetHanHongDong.SelectedDate = temp;
            }
        }
        protected void txtHieuLucHopDong_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if(txtThoiGianThucHien.Text != "")
            {
                DateTime temp = txtHieuLucHopDong.SelectedDate.Value.AddDays(Int32.Parse(txtThoiGianThucHien.Text.ToString().Replace(".", "")));
                txtNgayHetHanHongDong.SelectedDate = temp;
            }
        }

        protected void txtThoiGianThiCong_TextChanged(object sender, EventArgs e)
        {
            if (txtNgayKhoiCong.SelectedDate.HasValue)
            {
                DateTime temp = txtNgayKhoiCong.SelectedDate.Value.AddDays(Int32.Parse(txtThoiGianThiCong.Text.ToString().Replace(".", "")));
                txtNgayHetHanThiCong.SelectedDate = temp;
            }
        }

        protected void txtNgayKhoiCong_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if (txtThoiGianThiCong.Text != "")
            {
                DateTime temp = txtNgayKhoiCong.SelectedDate.Value.AddDays(Int32.Parse(txtThoiGianThiCong.Text.ToString().Replace(".", "")));
                txtNgayHetHanThiCong.SelectedDate = temp;
            }
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
