<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NGUOIDUNG_CN.ascx.cs" Inherits="QLSC.NGUOIDUNG_CN" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:UpdatePanel runat="server" ID="upn">
    <ContentTemplate>
        <asp:Panel runat="server" ID="pnCN">
            <div class="row ">
                <div class="col-sm-12 col-md-11 col-lg-10">
                    <div class="col-sm-offset-4 col-sm-6 col-md-7 col-lg-6">
                        <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" CssClass="baoloi" runat="server" EnableClientScript="true" />
                        <asp:Panel CssClass="baoloi" runat="server" ID="pnThongBao" Visible="false">
                            <asp:Label ID="lblThongBao" runat="server" Text=""></asp:Label>
                        </asp:Panel>
                    </div>
                </div>
            </div>

            <div class="row form">
                <div class="form-horizontal">
                    <div class="col-sm-12 col-md-11 col-lg-10">
                        <div class="form-group">
                            <label class="col-sm-4 control-label pd-r0">Tên đăng nhập </label>
                            <div class="col-sm-6 col-md-7 col-lg-6">
                                <asp:TextBox ID="txtTenDangNhap" runat="server" Enabled="true" placeholder="" MaxLength="500" CssClass="form-control requirements" />
                            </div>
                        </div>
                        <div runat="server" id="divMatKhau"> 
                            <div class="form-group">
                                <label class="col-sm-4 control-label pd-r0">Mật khẩu </label>
                                <div class="col-sm-6 col-md-7 col-lg-6">
                                    <asp:TextBox ID="txtMatKhau" runat="server" placeholder="Mật khẩu" MaxLength="200" TextMode="Password" CssClass="form-control requirements"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label pd-r0">Xác nhận mật khẩu </label>
                                <div class="col-sm-6 col-md-7 col-lg-6">
                                    <asp:TextBox ID="txtXacNhanMatKhau" runat="server" placeholder="Xác nhận mật khẩu" MaxLength="200" TextMode="Password" CssClass="form-control requirements"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group mr-t10">
                            <label class="col-sm-4 control-label pd-r0">Tên người dùng </label>
                            <div class="col-sm-6 col-md-7 col-lg-6">
                                <asp:TextBox ID="txtTenNguoiDung" runat="server" placeholder="" MaxLength="500" CssClass="form-control requirements" />
                            </div>
                        </div>
                        <div class="form-group mr-t10">
                            <label class="col-sm-4 control-label pd-r0">Đơn vị </label>
                            <div class="col-sm-6 col-md-7 col-lg-6">
                                <telerik:RadComboBox Skin="Simple" ID="drpDonVi" Filter="Contains" CssClass="custom-radcombox "
                                    InputCssClass="form-control requirements" AllowCustomText="true" runat="server" Width="100%"
                                    Height="250px" EmptyMessage="-- Chọn --" ShowWhileLoading="true" LoadingMessage="Đang tải..."
                                    Localization-NoMatches="Không tìm thấy">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="form-group mr-t10">
                            <label class="col-sm-4 control-label pd-r0">Ghi chú </label>
                            <div class="col-sm-6 col-md-7 col-lg-6">
                                <asp:TextBox ID="txtGhiChu" runat="server" TextMode="MultiLine" Rows="3" placeholder="" MaxLength="9" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row mr-t20">
                <div class="col-sm-12 col-lg-11 mr-10" style="text-align: center">
                    <asp:LinkButton ID="btn_CN" runat="server" CommandName="CapNhat" CausesValidation="true" OnClick="btn_CN_Click" CssClass="btn btn-primary waves-effect none-radius none-shadow btn-sm min-width-100"> 
                    <i class="glyphicon glyphicon-floppy-disk"></i> Cập nhật
                    </asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="btnCapNhatTiepTuc" CommandName="TiepTuc" runat="server" CausesValidation="true" OnClick="btn_CN_Click" CssClass="btn btn-primary waves-effect none-radius none-shadow btn-sm min-width-100"><i class="glyphicon glyphicon-floppy-disk"></i> Cập nhật & Tiếp tục</asp:LinkButton>
                    &nbsp;
                    <asp:LinkButton ID="btn_CN_BoQua" runat="server" CommandName="HuyBo" CssClass="btn btn-sm btn-default waves-effect none-radius none-shadow min-width-100" OnClick="btn_CN_BoQua_Click" CausesValidation="false" Style="background: #FFF; color: #272727; background-image: none !important;"><i class='fa fa-reply-all'></i> Bỏ qua</asp:LinkButton>
                </div>
            </div>
            <br />
        </asp:Panel>

        <div class="row">
            <div class="col-sm-12 mr-b10">
                <strong>Lưu ý </strong><span>(<span class="batbuoc" style="top: 3px; left: 1px;">*</span>) bắt buộc nhập</span><br />
                - Phí đặt lệnh và phí đăng ký thành viên sau khi thay đổi thiết lập sẽ áp dụng trên toàn bộ hệ thống.
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
<script language="Javascript">

    function pageLoad(sender, args) {
        if (args._isPartialLoad) { // postback
            $('input.auto').autoNumeric('update');
        }
        else { // not postback
            $('input.auto').autoNumeric();
        }
    }
</script>

