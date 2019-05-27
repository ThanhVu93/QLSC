<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NGUOIDUNG_DMK.ascx.cs" Inherits="QLSC.NGUOIDUNG_DMK" %>
<%@ Register TagPrefix="dnnsc" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:UpdateProgress ID="prgLoadingStatus" runat="server" DynamicLayout="true">
    <ProgressTemplate>
        <div id="overlay">
            <div id="modalprogress">
                <asp:Image ID="imgWaitIcon" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/DesktopModules/SANGIAODICH/Images/ajax-loader.gif" />
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="upn" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlFormDanhSach" runat="server" CssClass="form form-capnhat">
            <div class="row">
                <div class="col-sm-8 col-sm-offset-2">
                    <div class="col-sm-offset-3 col-sm-8">
                        <asp:ValidationSummary ID="vsForm" DisplayMode="List" CssClass="baoloi" runat="server" />
                        <asp:Panel CssClass="baoloi" runat="server" ID="pnThongBao" Visible="false">
                            <asp:Label ID="lblThongBao" runat="server" Text=""></asp:Label>
                        </asp:Panel>
                        <asp:Panel CssClass="thanhcong" runat="server" ID="pnSuccess" Visible="false">
                            <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-8 col-sm-offset-2">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-3 control-label">Tài khoản thành viên   </label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtTenDangNhap" runat="server" CssClass="form-control" Width="100%" MaxLength="200" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
             <div class="row">
                <div class="col-sm-8 col-sm-offset-2">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-3 control-label">Tên thành viên   </label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtHoTen" runat="server" CssClass="form-control " Width="100%" MaxLength="200" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-8 col-sm-offset-2">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-3 control-label">Mật khẩu mới   </label>
                            <div class="col-sm-8">
                                 <asp:TextBox ID="txtMatKhau" runat="server" placeholder="**********" MaxLength="200" TextMode="Password" CssClass="form-control requirements" Width="100%" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="txtMatKhau_rv" runat="server" ErrorMessage="Vui lòng nhập mật khẩu mới" ControlToValidate="txtMatKhau" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display = "None" ControlToValidate = "txtMatKhau" ID="txtMatKhau_length" ValidationExpression = "^[\s\S]{7,}$" SetFocusOnError="True" runat="server" ErrorMessage="Độ dài mật khẩu bắt buộc lớn hơn 7 ký tự"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="row">
                <div class="col-sm-8 col-sm-offset-2">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-3 control-label">Xác nhận mật khẩu mới   </label>
                            <div class="col-sm-8">
                                 <asp:TextBox ID="txtXacNhanMatKhau" runat="server" placeholder="**********" MaxLength="200" TextMode="Password" CssClass="form-control requirements" Width="100%" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="txtXacNhanMatKhau_rv" runat="server" ErrorMessage="Vui lòng nhập mật khẩu xác nhận" ControlToValidate="txtXacNhanMatKhau" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="rfvCompare" runat="server"  ErrorMessage="Xác nhận mật khẩu không chính xác" ControlToValidate="txtXacNhanMatKhau" SetFocusOnError="True" ControlToCompare="txtMatKhau" Display="None"></asp:CompareValidator>
                             </div>
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="row mr-b5">
                <div class="col-sm-8 col-sm-offset-2">
                    <div class="col-sm-offset-3 col-sm-8 mr-t10 mr-b10">
                        <asp:LinkButton ID="btn_CN" runat="server" CommandName="CapNhat" CausesValidation="true" OnClick="btn_CN_Click" CssClass="btn btn-primary waves-effect none-radius btn-sm min-width-100"> <i class="glyphicon glyphicon-floppy-disk"></i>&ensp; Cập nhật 
                      </asp:LinkButton>&nbsp;
                         <asp:LinkButton ID="btn_CN_BoQua" runat="server" CommandName="HuyBo" CssClass="btn btn-sm btn-default waves-effect none-radius none-shadow min-width-100" OnClick="btn_CN_BoQua_Click" CausesValidation="false" Style="background: #FFF; color: #272727; background-image: none !important;"><i class='fa fa-reply-all'></i> Bỏ qua</asp:LinkButton>
                    </div>
                </div>
            </div>
            
            <div class="row mr-t20"> 
                <div class="col-sm-12 ">
                       <span><strong>Lưu ý: </strong><span class="batbuoc">*</span> là bắt buộc nhập</span>
                </div>
            </div>

            </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<style>
    a:hover{
        color:#fff !important;
    }
    .form .form-control.requirements{
            background-position-y: -2px;
    }
</style>