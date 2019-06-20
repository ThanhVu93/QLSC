using DotNetNuke.Common;
using DotNetNuke.Entities.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace QLSC
{
    public partial class SUCO_CN : DotNetNuke.Entities.Modules.UserModuleBase
    {
        #region Khai báo, định nghĩa đối tượng
        public string vPathCommon = ClassParameter.vPathCommon;
        public string vPathCommonJS = ClassParameter.vPathCommonJavascript;
        public string vPathCommonBieuMau = ClassParameter.vPathCommonBieuMau;
        public string vPathCommonData = ClassParameter.vPathCommonData;
        ClassCommon clsCommon = new ClassCommon();
        QLSCDataContext vDC = new QLSCDataContext();

        UserInfo _currentUser = UserController.Instance.GetCurrentUserInfo();
        QLSC_NGUOIDUNG objNGUOIDUNG = new QLSC_NGUOIDUNG();

        DataTable dtTable;
        QLSC_TAPTIN objTapTin = new QLSC_TAPTIN();
        QLSC_SUCO objSUCO = new QLSC_SUCO();
        int vSC_ID = 0;
        TAPTINController objTAPTINController = new TAPTINController();

        //public string vPathDataBieuMau = ClassParameter.vPathDataBieuMau;
        public string vJavascriptMask;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            vJavascriptMask = ClassParameter.vJavascriptMaskNumber;
            if (Request.QueryString["ID"] != null)
            {
                vSC_ID = int.Parse(Request.QueryString["ID"]);
            }
            if (!IsPostBack)
            {
                if (_currentUser.IsInRole("Administrators"))
                {
                    drpDonVi.Enabled = true; 
                }
                    LoadDSNhomDV();
                loadDrpLoaiSuCo();
                SetInfoForm(vSC_ID);
                if (Session["dgDanhSach"] == null)
                    Session["dgDanhSach"] = LoadDSHinhAnh(vSC_ID);
                BindGrid();
            }
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

        public void SetInfoForm(int vSUCO_ID)
        {
            try
            {
               
                //Thêm mới sự cố
                if (vSC_ID == 0)
                {
                    if (_currentUser.IsInRole("Administrator"))
                    {                       
                        drpDonVi.SelectedIndex = 0;
                    }
                    else
                    {
                        var objNGUOIDUNG = vDC.QLSC_NGUOIDUNGs.Where(x => x.UserID == _currentUser.UserID).SingleOrDefault();
                        if (objNGUOIDUNG != null)
                        {
                            drpDonVi.SelectedValue = objNGUOIDUNG.DONVI_ID.ToString();
                        }
                    }
                  
                }
                // Cập nhật sự cố
                else
                {
                    
                    objSUCO = vDC.QLSC_SUCOs.Where(x => x.SC_ID == vSC_ID).SingleOrDefault();
                    if (objSUCO != null)
                    {
                        drpLoaiSuCo.SelectedValue = objSUCO.LOAISC_ID.ToString();
                        drpDonVi.SelectedValue = objSUCO.DONVI_ID.ToString();
                        txtNgayXayRaSuCo.SelectedDate = objSUCO.SC_NGAYXAYRA;
                        drpGioXayRa.SelectedValue = objSUCO.SC_NGAYXAYRA.Value.Hour.ToString();
                        drpPhutXayra.SelectedValue = objSUCO.SC_NGAYXAYRA.Value.Minute.ToString();

                        drpGioTaiLap.SelectedValue = objSUCO.SC_NGAYTAILAP.Value.Hour.ToString();
                        drpPhutTaiLap.SelectedValue = objSUCO.SC_NGAYTAILAP.Value.Minute.ToString();

                        txtNoiDungSuCo.Text = objSUCO.SC_NOIDUNG;
                        txtNguyenNhan.Text = objSUCO.SC_NGUYENNHAN;
                        txtTenChungLoai1.Text = objSUCO.SC_VTTB_TENCHUNGLOAI;
                        txtSoLuong1.Text = String.Format("{0:#,0.#}", objSUCO.SC_VTTB_SOLUONG);
                        txtNhaSX1.Text = objSUCO.SC_VTTB_NHASANXUAT;
                        txtNamVanHanh1.Text = objSUCO.SC_VTTB_NAMVANHANH;

                        txtTenChungLoai2.Text = objSUCO.SC_VTTB_TENCHUNGLOAI2;
                        txtSoLuong2.Text = String.Format("{0:#,0.#}", objSUCO.SC_VTTB_SOLUONG2);
                        txtNhaSanXuat2.Text = objSUCO.SC_VTTB_NHASANXUAT2;
                        txtNamVanHanh2.Text = objSUCO.SC_VTTB_NAMVANHANH2;

                        txtTenChungLoai3.Text = objSUCO.SC_VTTB_TENCHUNGLOAI3;
                        txtSoLuong3.Text = String.Format("{0:#,0.#}", objSUCO.SC_VTTB_SOLUONG3);
                        txtNhaSanXuat3.Text = objSUCO.SC_VTTB_NHASANXUAT3;
                        txtNamVanHanh3.Text = objSUCO.SC_VTTB_NAMVANHANH3;

                        txtTenChungLoai4.Text = objSUCO.SC_VTTB_TENCHUNGLOAI4;
                        txtSoLuong4.Text = String.Format("{0:#,0.#}", objSUCO.SC_VTTB_SOLUONG4);
                        txtNhaSanXuat4.Text = objSUCO.SC_VTTB_NHASANXUAT4;
                        txtNamVanHanh4.Text = objSUCO.SC_VTTB_NAMVANHANH4;

                        txtTenChungLoai5.Text = objSUCO.SC_VTTB_TENCHUNGLOAI5;
                        txtSoLuong5.Text = String.Format("{0:#,0.#}", objSUCO.SC_VTTB_SOLUONG5);
                        txtNhaSanXuat5.Text = objSUCO.SC_VTTB_NHASANXUAT5;
                        txtNamVanHanh5.Text = objSUCO.SC_VTTB_NAMVANHANH5;

                        txtThietBiDongCat.Text = objSUCO.SC_THIETBIDONGCAT_MSRCS;

                        DienAp.SelectedValue = objSUCO.SC_DIENAP.ToString();
                        rd_CQ_KQ.SelectedValue = objSUCO.SC_CQ == 1 ? "CQ" :"KQ";                        
                        drpPhanLoai.SelectedValue = objSUCO.SC_LOAI.ToString();
                        txtTongSoKH.Text = String.Format("{0:#,0.#}", objSUCO.SC_TONGSOKH);
                        txtGhiChu.Text = objSUCO.SC_GHICHU;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void loadDrpLoaiSuCo()
        {
            try
            {
                List<QLSC_LOAISUCO> lstSuCo = (from obj in vDC.QLSC_LOAISUCOs
                                               select obj).ToList();
                if (lstSuCo.Count > 0)
                {
                    dtTable = new DataTable();
                    dtTable.Columns.Add("LSC_ID");
                    dtTable.Columns.Add("LSC_TEN");
                    foreach (var it in lstSuCo)
                    {
                        DataRow row = dtTable.NewRow();
                        row["LSC_ID"] = it.LOAISC_ID;
                        row["LSC_TEN"] = it.LOAISC_TEN;
                        dtTable.Rows.Add(row);
                    }
                    drpLoaiSuCo.Items.Clear();
                    drpLoaiSuCo.DataSource = dtTable;
                    drpLoaiSuCo.DataValueField = "LSC_ID";
                    drpLoaiSuCo.DataTextField = "LSC_TEN";
                    drpLoaiSuCo.DataBind();
                }
            }
            catch (Exception ex)
            {
                ClassCommon.ShowToastr(Page, "Có lỗi xãy ra, vui lòng liên hệ quản trị", "Thông báo", "error");
            }
        }




        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                 objNGUOIDUNG = vDC.QLSC_NGUOIDUNGs.Where(x => x.UserID == _currentUser.UserID).SingleOrDefault();
                if (vSC_ID == 0)//Thêm mới sự cố
                {
                    objSUCO = new QLSC_SUCO();
                    objSUCO.UserID = _currentUser.UserID;
                    if (_currentUser.IsInRole("Administrator"))
                    {
                       if(drpDonVi.SelectedValue != null)
                        {
                            objSUCO.DONVI_ID = int.Parse(drpDonVi.SelectedValue);
                        }
                    }
                    else
                    {
                        if (objNGUOIDUNG != null)
                        {
                            objSUCO.DONVI_ID = objNGUOIDUNG.DONVI_ID;
                        }
                        else
                        {
                            objSUCO.DONVI_ID = 1;
                        }
                    }
                    objSUCO.LOAISC_ID = int.Parse(drpLoaiSuCo.SelectedValue);
                    //Thời gian xãy ra sự cố
                    string ctrl_ngayxayra = txtNgayXayRaSuCo.SelectedDate.ToString();
                    string[] temptxtngayxayra = ctrl_ngayxayra.Split(' ');
                    string gioxayra = drpGioXayRa.SelectedValue;
                    string phutxayra = drpPhutXayra.SelectedValue;
                    string ngayxayra = temptxtngayxayra[0] + " " + gioxayra + ":" + phutxayra + ":00";
                    string tg_xayra = DateTime.Parse(ngayxayra).ToString("yyyy-MM-dd HH:mm:ss");
                    DateTime dt_xayra = DateTime.Parse(ngayxayra);
                    objSUCO.SC_NGAYXAYRA = dt_xayra;
                    //Thời gian tái lập
                    string ctrl_ngaytailap = txtNgayXayRaSuCo.SelectedDate.ToString();
                    string[] temptxtngaytailap = ctrl_ngaytailap.Split(' ');
                    string giotailap = drpGioTaiLap.SelectedValue;
                    string phuttailap = drpPhutTaiLap.SelectedValue;
                    string ngaytailap = temptxtngayxayra[0] + " " + giotailap + ":" + phuttailap + ":00";
                    string tg_tailap = DateTime.Parse(ngaytailap).ToString("yyyy-MM-dd HH:mm:ss");
                    DateTime dt_tailap = DateTime.Parse(ngayxayra);
                    objSUCO.SC_NGAYTAILAP = dt_tailap;

                    objSUCO.SC_NOIDUNG = ClassCommon.ClearHTML(txtNoiDungSuCo.Text.Trim());
                    objSUCO.SC_NGUYENNHAN = ClassCommon.ClearHTML(txtNguyenNhan.Text.Trim());

                    objSUCO.SC_VTTB_TENCHUNGLOAI = ClassCommon.ClearHTML(txtTenChungLoai1.Text.Trim());
                    if(txtSoLuong1.Text != "")
                    {
                        objSUCO.SC_VTTB_SOLUONG = int.Parse(txtSoLuong1.Text.ToString().Replace(".", ""));
                    }
                  
                    objSUCO.SC_VTTB_NHASANXUAT = ClassCommon.ClearHTML(txtNhaSX1.Text.Trim());
                    objSUCO.SC_VTTB_NAMVANHANH = ClassCommon.ClearHTML(txtNamVanHanh1.Text.Trim());

                    objSUCO.SC_VTTB_TENCHUNGLOAI2 = ClassCommon.ClearHTML(txtTenChungLoai2.Text.Trim());
                    if (txtSoLuong1.Text != "")
                    {
                        objSUCO.SC_VTTB_SOLUONG2 = int.Parse(txtSoLuong2.Text.ToString().Replace(".", ""));
                    }
                   
                    objSUCO.SC_VTTB_NHASANXUAT2 = ClassCommon.ClearHTML(txtNhaSanXuat2.Text.Trim());
                    objSUCO.SC_VTTB_NAMVANHANH2 = ClassCommon.ClearHTML(txtNamVanHanh2.Text.Trim());

                    objSUCO.SC_VTTB_TENCHUNGLOAI3 = ClassCommon.ClearHTML(txtTenChungLoai3.Text.Trim());
                    if (txtSoLuong1.Text != "")
                    {
                        objSUCO.SC_VTTB_SOLUONG3 = int.Parse(txtSoLuong3.Text.ToString().Replace(".", ""));
                    }
                    
                    objSUCO.SC_VTTB_NHASANXUAT3 = ClassCommon.ClearHTML(txtNhaSanXuat3.Text.Trim());
                    objSUCO.SC_VTTB_NAMVANHANH3 = ClassCommon.ClearHTML(txtNamVanHanh3.Text.Trim());

                    objSUCO.SC_VTTB_TENCHUNGLOAI4 = ClassCommon.ClearHTML(txtTenChungLoai4.Text.Trim());

                   
                    if (txtSoLuong1.Text != "")
                    {
                        objSUCO.SC_VTTB_SOLUONG4 = int.Parse(txtSoLuong4.Text.ToString().Replace(".", ""));
                    }
                    objSUCO.SC_VTTB_NHASANXUAT4 = ClassCommon.ClearHTML(txtNhaSanXuat4.Text.Trim());
                    objSUCO.SC_VTTB_NAMVANHANH4 = ClassCommon.ClearHTML(txtNamVanHanh4.Text.Trim());

                    objSUCO.SC_VTTB_TENCHUNGLOAI5 = ClassCommon.ClearHTML(txtTenChungLoai5.Text.Trim());
                    if (txtSoLuong1.Text != "")
                    {
                        objSUCO.SC_VTTB_SOLUONG5 = int.Parse(txtSoLuong5.Text.ToString().Replace(".", ""));
                    }
                    
                    objSUCO.SC_VTTB_NHASANXUAT5 = ClassCommon.ClearHTML(txtNhaSanXuat5.Text.Trim());
                    objSUCO.SC_VTTB_NAMVANHANH5 = ClassCommon.ClearHTML(txtNamVanHanh5.Text.Trim());

                    objSUCO.SC_THIETBIDONGCAT_MSRCS = ClassCommon.ClearHTML(txtThietBiDongCat.Text.Trim());

                    objSUCO.SC_DIENAP = int.Parse(DienAp.SelectedValue);
                    int vKQ = rd_CQ_KQ.SelectedValue == "KQ" ? 1 : 0;
                    objSUCO.SC_KQ = vKQ;
                    int vCQ = rd_CQ_KQ.SelectedValue == "CQ" ? 1 : 0;
                    objSUCO.SC_CQ = vCQ;

                    objSUCO.SC_LOAI = int.Parse(drpPhanLoai.SelectedValue);
                    if (txtTongSoKH.Text.Trim() != "")
                    {
                        objSUCO.SC_TONGSOKH = Int32.Parse(txtTongSoKH.Text.ToString().Replace(".", ""));
                    }
                    objSUCO.SC_TAISAN = int.Parse(TaiSan.SelectedValue);
                    objSUCO.SC_GHICHU = ClassCommon.ClearHTML(txtGhiChu.Text.Trim());

                    vDC.QLSC_SUCOs.InsertOnSubmit(objSUCO);
                    vDC.SubmitChanges();

                    if (dgDanhSach.Rows.Count > 0)
                    {
                        DataTable dt = Session["dgDanhSach"] as DataTable;                        
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objTapTin = new QLSC_TAPTIN();
                            objTapTin.FILE_NAME = dt.Rows[i]["HA_FILE_PATH"].ToString();
                            objTapTin.FILE_MOTA = dt.Rows[i]["HA_TENFILE"].ToString();
                            objTapTin.FILE_EXT = dt.Rows[i]["HA_EXT"].ToString();
                            objTapTin.FILE_SIZE = Int32.Parse(dt.Rows[i]["HA_SIZE"].ToString());
                            objTapTin.FILE_USERID_CAPNHAT = _currentUser.UserID;
                            objTapTin.FILE_NGAYCAPNHAT = DateTime.Now;
                            objTapTin.OBJECT_LOAI = (int)CommonEnum.TapTinObjectLoai.File;
                            objTapTin.OBJECT_ID = objSUCO.SC_ID;
                            objTAPTINController.ThemTapTin(objTapTin);
                        }
                    }
                    else
                    { }
                    Session.Remove("dgDanhSach");

                }
                else //cập nhật sự cố
                {
                    objSUCO = vDC.QLSC_SUCOs.Where(x => x.SC_ID == vSC_ID).SingleOrDefault();
                    if (objSUCO != null)
                    {
                        objSUCO.LOAISC_ID = int.Parse(drpLoaiSuCo.SelectedValue);
                        //Thời gian xãy ra sự cố
                        string ctrl_ngayxayra = txtNgayXayRaSuCo.SelectedDate.ToString();
                        string[] temptxtngayxayra = ctrl_ngayxayra.Split(' ');
                        string gioxayra = drpGioXayRa.SelectedValue;
                        string phutxayra = drpPhutXayra.SelectedValue;
                        string ngayxayra = temptxtngayxayra[0] + " " + gioxayra + ":" + phutxayra + ":00";
                        string tg_xayra = DateTime.Parse(ngayxayra).ToString("yyyy-MM-dd HH:mm:ss");
                        DateTime dt_xayra = DateTime.Parse(ngayxayra);
                        objSUCO.SC_NGAYXAYRA = dt_xayra;
                        //Thời gian tái lập
                        string ctrl_ngaytailap = txtNgayXayRaSuCo.SelectedDate.ToString();
                        string[] temptxtngaytailap = ctrl_ngaytailap.Split(' ');
                        string giotailap = drpGioTaiLap.SelectedValue;
                        string phuttailap = drpPhutTaiLap.SelectedValue;
                        string ngaytailap = temptxtngayxayra[0] + " " + giotailap + ":" + phuttailap + ":00";
                        string tg_tailap = DateTime.Parse(ngaytailap).ToString("yyyy-MM-dd HH:mm:ss");
                        DateTime dt_tailap = DateTime.Parse(ngayxayra);
                        objSUCO.SC_NGAYTAILAP = dt_tailap;
                      
                        if (drpDonVi.SelectedValue != null)
                        {
                            objSUCO.DONVI_ID = int.Parse(drpDonVi.SelectedValue);
                        }
                        
                        objSUCO.SC_NOIDUNG = ClassCommon.ClearHTML(txtNoiDungSuCo.Text.Trim());
                        objSUCO.SC_NGUYENNHAN = ClassCommon.ClearHTML(txtNguyenNhan.Text.Trim());

                        objSUCO.SC_VTTB_TENCHUNGLOAI = ClassCommon.ClearHTML(txtTenChungLoai1.Text.Trim());
                        if (txtSoLuong1.Text != "")
                        {
                            objSUCO.SC_VTTB_SOLUONG = int.Parse(txtSoLuong1.Text.ToString().Replace(".", ""));
                        }

                        objSUCO.SC_VTTB_NHASANXUAT = ClassCommon.ClearHTML(txtNhaSX1.Text.Trim());
                        objSUCO.SC_VTTB_NAMVANHANH = ClassCommon.ClearHTML(txtNamVanHanh1.Text.Trim());

                        objSUCO.SC_VTTB_TENCHUNGLOAI2 = ClassCommon.ClearHTML(txtTenChungLoai2.Text.Trim());
                        if (txtSoLuong2.Text != "")
                        {
                            objSUCO.SC_VTTB_SOLUONG2 = int.Parse(txtSoLuong2.Text.ToString().Replace(".", ""));
                        }

                        objSUCO.SC_VTTB_NHASANXUAT2 = ClassCommon.ClearHTML(txtNhaSanXuat2.Text.Trim());
                        objSUCO.SC_VTTB_NAMVANHANH2 = ClassCommon.ClearHTML(txtNamVanHanh2.Text.Trim());

                        objSUCO.SC_VTTB_TENCHUNGLOAI3 = ClassCommon.ClearHTML(txtTenChungLoai3.Text.Trim());
                        if (txtSoLuong3.Text != "")
                        {
                            objSUCO.SC_VTTB_SOLUONG3 = int.Parse(txtSoLuong3.Text.ToString().Replace(".", ""));
                        }

                        objSUCO.SC_VTTB_NHASANXUAT3 = ClassCommon.ClearHTML(txtNhaSanXuat3.Text.Trim());
                        objSUCO.SC_VTTB_NAMVANHANH3 = ClassCommon.ClearHTML(txtNamVanHanh3.Text.Trim());

                        objSUCO.SC_VTTB_TENCHUNGLOAI4 = ClassCommon.ClearHTML(txtTenChungLoai4.Text.Trim());


                        if (txtSoLuong4.Text != "")
                        {
                            objSUCO.SC_VTTB_SOLUONG4 = int.Parse(txtSoLuong4.Text.ToString().Replace(".", ""));
                        }
                        objSUCO.SC_VTTB_NHASANXUAT4 = ClassCommon.ClearHTML(txtNhaSanXuat4.Text.Trim());
                        objSUCO.SC_VTTB_NAMVANHANH4 = ClassCommon.ClearHTML(txtNamVanHanh4.Text.Trim());

                        objSUCO.SC_VTTB_TENCHUNGLOAI5 = ClassCommon.ClearHTML(txtTenChungLoai5.Text.Trim());
                        if (txtSoLuong5.Text != "")
                        {
                            objSUCO.SC_VTTB_SOLUONG5 = int.Parse(txtSoLuong5.Text.ToString().Replace(".", ""));
                        }

                        objSUCO.SC_VTTB_NHASANXUAT5 = ClassCommon.ClearHTML(txtNhaSanXuat5.Text.Trim());
                        objSUCO.SC_VTTB_NAMVANHANH5 = ClassCommon.ClearHTML(txtNamVanHanh5.Text.Trim());

                        objSUCO.SC_THIETBIDONGCAT_MSRCS = ClassCommon.ClearHTML(txtThietBiDongCat.Text.Trim());

                        objSUCO.SC_DIENAP = int.Parse(DienAp.SelectedValue);
                        int vKQ = rd_CQ_KQ.SelectedValue == "KQ" ? 1 : 0;
                        objSUCO.SC_KQ = vKQ;
                        int vCQ = rd_CQ_KQ.SelectedValue == "CQ" ? 1 : 0;
                        objSUCO.SC_CQ = vCQ;

                        objSUCO.SC_LOAI = int.Parse(drpPhanLoai.SelectedValue);
                        if(txtTongSoKH.Text.Trim() != "")
                        {
                            objSUCO.SC_TONGSOKH = Int32.Parse(txtTongSoKH.Text.ToString().Replace(".", ""));
                        }
                        objSUCO.SC_TAISAN = int.Parse(TaiSan.SelectedValue);
                        objSUCO.SC_GHICHU = ClassCommon.ClearHTML(txtGhiChu.Text.Trim());
                        vDC.SubmitChanges();

                        var objTT = objTAPTINController.Get_TapTin_By_ObjectID_LoaiID(vSC_ID, (int)CommonEnum.TapTinObjectLoai.File);
                        foreach (var it1 in objTT)
                        {
                            objTAPTINController.XOA_TAPTIN(it1.FILE_ID);
                        }
                        if (dgDanhSach.Rows.Count > 0)
                        {
                            DataTable dt = Session["dgDanhSach"] as DataTable;
                            string fname = dt.Rows[0]["HA_FILE_PATH"].ToString();
                            dt = Session["dgDanhSach"] as DataTable;
                            fname = dt.Rows[0]["HA_FILE_PATH"].ToString();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                objTapTin = new QLSC_TAPTIN();
                                objTapTin.FILE_NAME = dt.Rows[i]["HA_FILE_PATH"].ToString();
                                objTapTin.FILE_MOTA = dt.Rows[i]["HA_TENFILE"].ToString();
                                objTapTin.FILE_EXT = dt.Rows[i]["HA_EXT"].ToString();
                                objTapTin.FILE_SIZE = Int32.Parse(dt.Rows[i]["HA_SIZE"].ToString());
                                objTapTin.FILE_USERID_CAPNHAT = _currentUser.UserID;
                                objTapTin.FILE_NGAYCAPNHAT = DateTime.Now;
                                objTapTin.OBJECT_LOAI = (int)CommonEnum.TapTinObjectLoai.File;
                                objTapTin.OBJECT_ID = vSC_ID;
                                objTAPTINController.ThemTapTin(objTapTin);
                            }
                        }
                        Session.Remove("dgDanhSach");
                    }
                }
                Session[TabId + _currentUser.UserID + "_Message"] = "Cập nhật đơn hàng thành công";
                Session[TabId + _currentUser.UserID + "_Type"] = "success";
                Response.Redirect(Globals.NavigateURL(), true);
            }
            catch (Exception ex)
            {
                ClassCommon.ShowToastr(Page, "Có lỗi xãy ra, vui lòng liên hệ quản trị", "Thông báo", "error");
            }
        }

        protected void btnBoQua_Click(object sender, EventArgs e)
        {
            Session.Remove("dgDanhSach");
            Response.Redirect(Globals.NavigateURL(), false);
        }

        #region Phương thức - sự kiện datagrid
        protected void BindGrid()
        {
            dgDanhSach.DataSource = Session["dgDanhSach"];
            dgDanhSach.DataBind();
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = Session["dgDanhSach"] as DataTable;
            if (vSC_ID == 0)
                File.Delete(Server.MapPath(vPathCommonData) + dt.Rows[index].Field<string>(0));
            dt.Rows[index].Delete();
            Session["dgDanhSach"] = dt;
            BindGrid();
        }

        public List<QLSC_TAPTIN> DS_SUCO_TAPTIN(int id)
        {
            var objHHHA = (from p in vDC.QLSC_TAPTINs
                           where p.OBJECT_ID == id && p.OBJECT_LOAI == (int)CommonEnum.TapTinObjectLoai.File
                           select p).ToList();
            return objHHHA;
        }


        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        { }

        #endregion

        #region Nhiều hình ảnh
        public DataTable LoadDSHinhAnh(int id = 0)
        {
            dtTable = new DataTable();
            dtTable.Columns.Add("HA_FILE_PATH");
            dtTable.Columns.Add("HA_ID");
            dtTable.Columns.Add("HA_TENFILE");
            dtTable.Columns.Add("FILE_MOTA");
            dtTable.Columns.Add("HA_EXT");
            dtTable.Columns.Add("HA_SIZE");
            if (id > 0)
            {
                var temp = DS_SUCO_TAPTIN(id);
                if (temp.Count > 0)
                {
                    foreach (var it in temp)
                    {
                        DataRow row = dtTable.NewRow();
                        row["HA_FILE_PATH"] = (it.FILE_NAME);
                        row["HA_ID"] = (it.FILE_ID);
                        row["FILE_MOTA"] = (it.FILE_MOTA);
                        row["HA_TENFILE"] = it.FILE_NAME;
                        row["HA_EXT"] = it.FILE_EXT;
                        row["HA_SIZE"] = it.FILE_SIZE;
                        dtTable.Rows.Add(row);
                    }
                }
            }
            Session["dgDanhSach"] = dtTable;
            return dtTable;
        }

        protected void btn_TL_Click(object sender, EventArgs e)
        {
            //if (dgDanhSach.Rows.Count < 1)
            //{
            if (f_HinhAnh.HasFile)
            {
                string filepath = Server.MapPath(vPathCommonData);
                HttpFileCollection uploadedFiles = Request.Files;
                for (int i = 0; i < uploadedFiles.Count; i++)
                {
                    HttpPostedFile userPostedFile = uploadedFiles[i];
                    try
                    {
                        if (userPostedFile.ContentType == "image/jpg" || userPostedFile.ContentType == "image/png" || userPostedFile.ContentType == "image/jpeg" || userPostedFile.ContentType == "application/msword" || userPostedFile.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || userPostedFile.ContentType == "application/pdf" || userPostedFile.ContentType == "application/vnd.ms-excel" || userPostedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || userPostedFile.ContentType == "application/x-zip-compressed" || userPostedFile.ContentType == "application/octet-stream")
                        {
                            if (userPostedFile.ContentLength < 1048576 * 5)
                            {
                                string filename = userPostedFile.FileName;
                                string extension = System.IO.Path.GetExtension(filename);
                                string result = filename.Substring(0, filename.Length - extension.Length)+"."+extension;
                                //string result = ClassCommon.GetGuid() + extension;
                                ClassCommon.UploadFile(userPostedFile, filepath, result, "");

                                DataTable dt = Session["dgDanhSach"] as DataTable;
                                DataRow row = dt.NewRow();
                                row["HA_FILE_PATH"] = result;
                                row["HA_ID"] = 0;
                                row["FILE_MOTA"] = filename.Substring(0, filename.Length - extension.Length);
                                row["HA_TENFILE"] = filename.Substring(0, filename.Length - extension.Length);
                                row["HA_EXT"] = extension;
                                row["HA_SIZE"] = userPostedFile.ContentLength.ToString();
                                dt.Rows.Add(row);
                                Session["dgDanhSach"] = dt;
                                BindGrid();
                            }
                            else
                            {
                                pnThongBao.Visible = true;
                                lblThongBao.Text = "Kích thước hình ảnh phải nhỏ hơn 5M!";
                            }
                        }
                        else
                        {
                            pnThongBao.Visible = true;
                            lblThongBao.Text = "Tập tin không đúng định dạng!";
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }
        #endregion

    }
}

