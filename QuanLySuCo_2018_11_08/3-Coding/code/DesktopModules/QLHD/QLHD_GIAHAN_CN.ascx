<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QLHD_GIAHAN_CN.ascx.cs" Inherits="QLHD.QLHD_GIAHAN_CN" %>
<%@ Register TagPrefix="dnnsc" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script type="text/javascript" src="<%=vPathCommonJS%>"></script>
<%=vJavascriptMask%>
<asp:UpdatePanel ID="upn" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlCN" CssClass="form" runat="server" DefaultButton="btnCapNhat">
            <div class="row">
                <div class="col-sm-12 col-md-11 col-lg-10">
                    <div class="col-sm-offset-4 col-sm-6 col-md-7 col-lg-6">
                        <asp:ValidationSummary ID="ValidationSummary1" CssClass="baoloi" runat="server" EnableClientScript="true" />
                        <asp:Panel CssClass="baoloi" runat="server" ID="pnThongBao" Visible="false">
                            <asp:Label ID="lblThongBao" runat="server" Text=""></asp:Label>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-horizontal">
                    <div class="col-sm-12 col-md-11 col-lg-10">
                        <div class="form-group mr-t10">
                            <label class="col-sm-4 control-label pd-r0">Hợp đồng</label>
                            <div class="col-md-3">
                                <telerik:RadDatePicker ID="txtHieuLucHopDong" CssClass="requirements" runat="server" Calendar-ShowRowHeaders="false" AutoPostBack="true"
                                    ShowPopupOnFocus="false" RenderMode="Lightweight" Width="100%" DatePopupButton-ToolTip="Chọn ngày">
                                    <DateInput runat="server" ID="DateInput1" placeholder="" Style="font-size: 13px;" EmptyMessage="">
                                    </DateInput>
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtHieuLucHopDong"
                                    Display="None" ErrorMessage="Vui lòng chọn ngày hiệu lực hợp đồng"
                                    SetFocusOnError="true" />
                            </div>
                            <label class="col-sm-2 col-md-2 control-label pd-r0">Đến ngày</label>
                            <div class="col-md-3">
                                <telerik:RadDatePicker ID="txtNgayHetHanHongDong" CssClass="requirements" runat="server" Calendar-ShowRowHeaders="false" AutoPostBack="true"
                                    ShowPopupOnFocus="false" RenderMode="Lightweight" Width="100%" DatePopupButton-ToolTip="Chọn ngày">
                                    <DateInput runat="server" ID="DateInput3" placeholder="" Style="font-size: 13px;" EmptyMessage="">
                                    </DateInput>
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNgayHetHanHongDong"
                                    Display="None" ErrorMessage="Vui lòng ngày hết hạn hợp đồng"
                                    SetFocusOnError="true" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-11 col-lg-10">
                        <div class="form-group mr-t10">
                            <label class="col-sm-4 control-label pd-r0">Thời gian nhắc đến hạn</label>
                            <div class="col-sm-3 col-md-3">
                                <div class="input-group">
                                    <asp:TextBox ID="txtTGDenHanHD" runat="server" placeholder="" CssClass="form-control requirements auto {aSep: '.', aDec: ',', aSign: '', mNum: 6, mDec: 6, aPad: false}"></asp:TextBox>
                                    <span class="input-group-addon">ngày</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtTGDenHanHD"
                                        Display="None" ErrorMessage="Thời gian nhắc đến hạn hợp đồng không được rỗng"
                                        SetFocusOnError="true" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-11 col-lg-10">
                        <div class="form-group mr-t10">
                            <label class="col-sm-4 control-label pd-r0">Thi công</label>
                            <div class="col-md-3">
                                <telerik:RadDatePicker ID="txtNgayKhoiCong" CssClass="requirements" runat="server" Calendar-ShowRowHeaders="false" AutoPostBack="true"
                                    ShowPopupOnFocus="false" RenderMode="Lightweight" Width="100%" DatePopupButton-ToolTip="Chọn ngày">
                                    <DateInput runat="server" ID="DateInput2" placeholder="" Style="font-size: 13px;" EmptyMessage="">
                                    </DateInput>
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNgayKhoiCong"
                                    Display="None" ErrorMessage="Vui lòng chọn ngày khởi công"
                                    SetFocusOnError="true" />
                            </div>
                            <label class="col-sm-2 col-md-2 control-label pd-r0">Đến ngày</label>
                            <div class="col-md-3">
                                <telerik:RadDatePicker ID="txtNgayHetHanThiCong" CssClass="requirements" runat="server" Calendar-ShowRowHeaders="false" AutoPostBack="true"
                                    ShowPopupOnFocus="false" RenderMode="Lightweight" Width="100%" DatePopupButton-ToolTip="Chọn ngày">
                                    <DateInput runat="server" ID="DateInput4" placeholder="" Style="font-size: 13px;" EmptyMessage="">
                                    </DateInput>
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNgayHetHanThiCong"
                                    Display="None" ErrorMessage="Vui lòng chọn ngày hết hạn thi công"
                                    SetFocusOnError="true" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-11 col-lg-10">
                        <div class="form-group mr-t10">
                            <label class="col-sm-4 control-label pd-r0">Thời gian nhắc đến hạn</label>
                            <div class="col-sm-3 col-md-3">
                                <div class="input-group">
                                    <asp:TextBox ID="txtTGDenHanThiCong" runat="server" placeholder="" CssClass="form-control requirements auto {aSep: '.', aDec: ',', aSign: '', mNum: 6, mDec: 6, aPad: false}"></asp:TextBox>
                                    <span class="input-group-addon">ngày</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTGDenHanThiCong"
                                        Display="None" ErrorMessage="Thời gian nhắc đến hạn thi công không được rỗng"
                                        SetFocusOnError="true" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-11 col-lg-10">
                        <div class="form-group mr-t10">
                            <label class="col-sm-4 control-label pd-r0">BL thực hiện hợp đồng</label>
                            <div class="col-md-3">
                                <telerik:RadDatePicker ID="txtBLThucHienHopDongTuNgay" CssClass="requirements" runat="server" Calendar-ShowRowHeaders="false" AutoPostBack="true"
                                    ShowPopupOnFocus="false" RenderMode="Lightweight" Width="100%" DatePopupButton-ToolTip="Chọn ngày">
                                    <DateInput runat="server" ID="DateInput6" placeholder="" Style="font-size: 13px;" EmptyMessage="">
                                    </DateInput>
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtBLThucHienHopDongTuNgay"
                                    Display="None" ErrorMessage="Vui lòng chọn bảo lãnh thực hiện hợp đồng từ ngày"
                                    SetFocusOnError="true" />
                            </div>
                            <label class="col-sm-2 col-md-2 control-label pd-r0">Đến ngày</label>
                            <div class="col-md-3">
                                <telerik:RadDatePicker ID="txtBLThucHienHopDongDenNgay" CssClass="requirements" runat="server" Calendar-ShowRowHeaders="false" AutoPostBack="true"
                                    ShowPopupOnFocus="false" RenderMode="Lightweight" Width="100%" DatePopupButton-ToolTip="Chọn ngày">
                                    <DateInput runat="server" ID="DateInput7" placeholder="" Style="font-size: 13px;" EmptyMessage="">
                                    </DateInput>
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtBLThucHienHopDongDenNgay"
                                    Display="None" ErrorMessage="Vui lòng chọn bảo lãnh thực hiện hợp đồng đến ngày"
                                    SetFocusOnError="true" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-11 col-lg-10">
                        <div class="form-group mr-t10">
                            <label class="col-sm-4 control-label pd-r0">Thời gian nhắc đến hạn</label>
                            <div class="col-sm-3 col-md-3">
                                <div class="input-group">
                                    <asp:TextBox ID="txtTGDenHanBLThucHienHD" runat="server" placeholder="" CssClass="form-control requirements auto {aSep: '.', aDec: ',', aSign: '', mNum: 6, mDec: 6, aPad: false}"></asp:TextBox>
                                    <span class="input-group-addon">ngày</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtTGDenHanBLThucHienHD"
                                        Display="None" ErrorMessage="Thời gian nhắc đến hạn bảo lãnh thực hiện hợp đồng không được rỗng"
                                        SetFocusOnError="true" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-11 col-lg-10">
                        <div class="form-group mr-t10">
                            <label class="col-sm-4 control-label pd-r0">BL thanh toán vật tư</label>
                            <div class="col-md-3">
                                <telerik:RadDatePicker ID="txtBLThanhToanVatTuTuNgay" CssClass="requirements" runat="server" Calendar-ShowRowHeaders="false" AutoPostBack="true"
                                    ShowPopupOnFocus="false" RenderMode="Lightweight" Width="100%" DatePopupButton-ToolTip="Chọn ngày">
                                    <DateInput runat="server" ID="DateInput8" placeholder="" Style="font-size: 13px;" EmptyMessage="">
                                    </DateInput>
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtBLThanhToanVatTuTuNgay"
                                    Display="None" ErrorMessage="Vui lòng chọn bảo lãnh thanh toán vật tư từ ngày"
                                    SetFocusOnError="true" />
                            </div>
                            <label class="col-sm-2 col-md-2 control-label pd-r0">Đến ngày</label>
                            <div class="col-md-3">
                                <telerik:RadDatePicker ID="txtBLThanhToanVatTuDenNgay" CssClass="requirements" runat="server" Calendar-ShowRowHeaders="false" AutoPostBack="true"
                                    ShowPopupOnFocus="false" RenderMode="Lightweight" Width="100%" DatePopupButton-ToolTip="Chọn ngày">
                                    <DateInput runat="server" ID="DateInput9" placeholder="" Style="font-size: 13px;" EmptyMessage="">
                                    </DateInput>
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtBLThanhToanVatTuDenNgay"
                                    Display="None" ErrorMessage="Vui lòng chọn bảo lãnh thanh toán vật tư đến ngày"
                                    SetFocusOnError="true" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-11 col-lg-10">
                        <div class="form-group mr-t10">
                            <label class="col-sm-4 control-label pd-r0">Thời gian nhắc đến hạn</label>
                            <div class="col-sm-3 col-md-3">
                                <div class="input-group">
                                    <asp:TextBox ID="txtTGDenHanBLThanhToanVatTu" runat="server" placeholder="" CssClass="form-control requirements auto {aSep: '.', aDec: ',', aSign: '', mNum: 6, mDec: 6, aPad: false}"></asp:TextBox>
                                    <span class="input-group-addon">ngày</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtTGDenHanBLThanhToanVatTu"
                                        Display="None" ErrorMessage="Thời gian nhắc đến hạn bảo lãnh thanh toán vật tư không được rỗng"
                                        SetFocusOnError="true" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-11 col-lg-10">
                        <div class="form-group mr-t10">
                            <label class="col-sm-4 control-label pd-r0">BL tạm ứng</label>
                            <div class="col-md-3">
                                <telerik:RadDatePicker ID="txtBLTamUngTuNgay" CssClass="requirements" runat="server" Calendar-ShowRowHeaders="false" AutoPostBack="true"
                                    ShowPopupOnFocus="false" RenderMode="Lightweight" Width="100%" DatePopupButton-ToolTip="Chọn ngày">
                                    <DateInput runat="server" ID="DateInput10" placeholder="" Style="font-size: 13px;" EmptyMessage="">
                                    </DateInput>
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtBLTamUngTuNgay"
                                    Display="None" ErrorMessage="Vui lòng chọn bảo lãnh tạm ứng từ ngày"
                                    SetFocusOnError="true" />
                            </div>
                            <label class="col-sm-2 col-md-2 control-label pd-r0">Đến ngày</label>
                            <div class="col-md-3">
                                <telerik:RadDatePicker ID="txtBLTamUngDenNgay" CssClass="requirements" runat="server" Calendar-ShowRowHeaders="false" AutoPostBack="true"
                                    ShowPopupOnFocus="false" RenderMode="Lightweight" Width="100%" DatePopupButton-ToolTip="Chọn ngày">
                                    <DateInput runat="server" ID="DateInput11" placeholder="" Style="font-size: 13px;" EmptyMessage="">
                                    </DateInput>
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtBLTamUngDenNgay"
                                    Display="None" ErrorMessage="Vui lòng chọn bảo lãnh tạm ứng đến ngày"
                                    SetFocusOnError="true" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-11 col-lg-10">
                        <div class="form-group mr-t10">
                            <label class="col-sm-4 control-label pd-r0">Thời gian nhắc đến hạn</label>
                            <div class="col-sm-3 col-md-3">
                                <div class="input-group">
                                    <asp:TextBox ID="txtTGDenHanBLTamUng" runat="server" placeholder="" CssClass="form-control requirements auto {aSep: '.', aDec: ',', aSign: '', mNum: 6, mDec: 6, aPad: false}"></asp:TextBox>
                                    <span class="input-group-addon">ngày</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtTGDenHanBLThanhToanVatTu"
                                        Display="None" ErrorMessage="Thời gian nhắc đến hạn bảo lãnh tạm ứng không được rỗng"
                                        SetFocusOnError="true" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-11 col-lg-10">
                        <div class="form-group mr-t10">
                            <label class="col-sm-4 control-label pd-r0">Ghi chú</label>
                            <div class="col-sm-8 col-md-9 col-lg-8">
                                <asp:TextBox ID="txtGhiChu" runat="server" CssClass="form-control" MaxLength="490" TextMode="MultiLine" Rows="4" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 col-lg-11 mr-10" style="text-align: center">
                    <asp:LinkButton ID="btnCapNhat" runat="server" OnClick="btnCapNhat_Click" CausesValidation="true" CssClass="btn btn-primary waves-effect none-radius none-shadow btn-sm min-width-100"><i class="glyphicon glyphicon-floppy-disk"></i> Cập nhật</asp:LinkButton>
                    <asp:LinkButton ID="btnBoQua" runat="server" OnClick="btnBoQua_Click" CssClass="btn btn-sm btn-default waves-effect none-radius none-shadow min-width-100" CausesValidation="false"><i class='fa fa-mail-reply-all'></i> Bỏ qua</asp:LinkButton>
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript">
    function pageLoad(sender, args) {
        if (args._isPartialLoad) { // postback
            $('input.auto').autoNumeric('update');
        }
        else { // not postback
            $('input.auto').autoNumeric();
        }
    }
</script>
