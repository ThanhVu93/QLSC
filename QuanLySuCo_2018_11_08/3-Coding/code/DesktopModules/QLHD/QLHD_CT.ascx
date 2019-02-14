<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QLHD_CT.ascx.cs" Inherits="QLHD.QLHD_CT" %>
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
            <div class="panel panel-white">
                <div class="panel-heading pd-8" role="tab" id="heading1">
                    <a role="button" data-toggle="collapse" href="#collapse1" id="tt1" aria-expanded="true" aria-controls="collapse1" tabindex="-1">
                        <h4 class="panel-title">Thông tin chung
                            
                        </h4>
                    </a>
                </div>
                <div id="collapse1" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="heading1">
                    <div class="panel-body">
                        <div class="form-horizontal">
                            <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group mr-t10">
                                    <label class="col-sm-4 control-label pd-r0">Tên công trình</label>
                                    <div class="col-sm-8 col-md-9 col-lg-8">
                                        <asp:TextBox ID="txtTenCongTrinh" runat="server" CssClass="form-control requirements" MaxLength="490" TextMode="MultiLine" Rows="3" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTenCongTrinh"
                                            Display="None" ErrorMessage="Tên công trình không được rỗng"
                                            SetFocusOnError="true" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group mr-t10">
                                    <label class="col-sm-4 control-label pd-r0">Tên hợp đồng</label>
                                    <div class="col-sm-8 col-md-9 col-lg-8">
                                        <asp:TextBox ID="txtTenHopDong" runat="server" CssClass="form-control requirements" MaxLength="500" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTenHopDong"
                                            Display="None" ErrorMessage="Tên hợp đồng không được rỗng"
                                            SetFocusOnError="true" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group mr-t10">
                                    <label class="col-sm-4 control-label pd-r0">Số</label>
                                    <div class="col-sm-3 col-md-2 col-lg-3">
                                        <asp:TextBox ID="txtSoHopDong" runat="server" CssClass="form-control requirements" MaxLength="500" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSoHopDong"
                                            Display="None" ErrorMessage="Số hợp đồng không được rỗng"
                                            SetFocusOnError="true" />
                                    </div>
                                    <label class="col-sm-2 control-label pd-r0">Ngày ký</label>
                                    <div class="col-md-3">
                                        <telerik:RadDatePicker ID="txtNgayKy" CssClass="requirements" runat="server" Calendar-ShowRowHeaders="false" AutoPostBack="true"
                                            ShowPopupOnFocus="false" RenderMode="Lightweight" Width="100%" DatePopupButton-ToolTip="Chọn ngày">
                                            <DateInput runat="server" ID="DateInput1" placeholder="Ngày ký" Style="font-size: 13px;" EmptyMessage="">
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtNgayKy"
                                            Display="None" ErrorMessage="Vui lòng chọn ngày ký hợp đồng"
                                            SetFocusOnError="true" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group mr-t10">
                                    <label class="col-sm-4 control-label pd-r0">Hiệu lực hợp đồng</label>
                                    <div class="col-md-3">
                                        <telerik:RadDatePicker OnSelectedDateChanged="txtHieuLucHopDong_SelectedDateChanged" ID="txtHieuLucHopDong" CssClass="requirements" runat="server" Calendar-ShowRowHeaders="false" AutoPostBack="true"
                                            ShowPopupOnFocus="false" RenderMode="Lightweight" Width="100%" DatePopupButton-ToolTip="Chọn ngày">
                                            <DateInput runat="server" ID="DateInput2" placeholder="" Style="font-size: 13px;" EmptyMessage="">
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtHieuLucHopDong"
                                            Display="None" ErrorMessage="Vui lòng chọn ngày hiệu lực hợp đồng"
                                            SetFocusOnError="true" />
                                    </div>
                                    <label class="col-sm-2 col-md-2 control-label pd-r0">T/g thực hiện</label>
                                    <div class="col-sm-3 col-md-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtThoiGianThucHien" AutoPostBack="true" OnTextChanged="txtThoiGianThucHien_TextChanged" runat="server" placeholder="" CssClass="form-control requirements auto {aSep: '.', aDec: ',', aSign: '', mNum: 6, mDec: 6, aPad: false}"></asp:TextBox>
                                            <span class="input-group-addon">ngày</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtThoiGianThucHien"
                                                Display="None" ErrorMessage="Thời gian thực hiện không được rỗng"
                                                SetFocusOnError="true" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group mr-t10">
                                    <label class="col-sm-4 control-label pd-r0">Ngày hết hạn hợp đồng</label>
                                    <div class="col-md-3">
                                        <telerik:RadDatePicker DateInput-ReadOnly="true" ID="txtNgayHetHanHongDong" CssClass="" runat="server" Calendar-ShowRowHeaders="false" AutoPostBack="true"
                                            ShowPopupOnFocus="false" RenderMode="Lightweight" Width="100%" DatePopupButton-ToolTip="Chọn ngày">
                                            <DateInput runat="server" ID="DateInput3" placeholder="" Style="font-size: 13px;" EmptyMessage="">
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                    </div>
                                    <label class="col-sm-2 col-md-2 control-label pd-r0">Giá trị</label>
                                    <div class="col-sm-3 col-md-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtGiaTri" runat="server" placeholder="" CssClass="form-control requirements auto {aSep: '.', aDec: ',', aSign: '', mNum: 12, mDec: 12, aPad: false}"></asp:TextBox>
                                            <span class="input-group-addon">VNĐ</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtThoiGianThucHien"
                                                Display="None" ErrorMessage="Giá trị hợp đồng không được rỗng"
                                                SetFocusOnError="true" />
                                        </div>
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
                                    <label class="col-sm-4 control-label pd-r0">Ghi chú</label>
                                    <div class="col-sm-8 col-md-9 col-lg-8">
                                        <asp:TextBox ID="txtGhiChu" runat="server" CssClass="form-control" MaxLength="490" TextMode="MultiLine" Rows="4" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label pd-r0">Tập tin </label>
                                    <div class="col-sm-6 col-md-7 col-lg-6">
                                        <asp:FileUpload ID="f_TapTin" runat="server" CssClass="" AllowMultiple="true" Style="float: left;" />
                                        <asp:LinkButton ID="btn_TL" OnClick="btn_TL_Click" runat="server" UseSubmitBehavior="false" CausesValidation="false" CssClass="btn btn-sm btn-default none-radius shadow-btn-sm min-width-100" Style="float: right;"><i class="fa fa-upload" aria-hidden="true"></i>Tải lên</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="form-group mr-t20">
                                    <label class="col-sm-4 control-label pd-r0"></label>
                                    <div class="col-sm-6 col-md-7 col-lg-6">
                                        <asp:Panel ID="pnlTapTin" runat="server" CssClass="danhsach custom_ds_file" Style="padding: 0px">
                                            <asp:GridView ID="GridView1" ShowHeader="False" Width="100%" CssClass="Grid" runat="server"
                                                AutoGenerateColumns="false">
                                                <HeaderStyle CssClass="tieude" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Tập tin">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <a oncontextmenu="return false" onserverclick="btn_TaiXuong" causesvalidation="false" runat="server">
                                                                <div>
                                                                    <%#Eval("FILE_MOTA")%>
                                                                </div>
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-white">
                <div class="panel-heading pd-8" role="tab" id="heading2">
                    <a role="button" data-toggle="collapse" href="#collapse2" id="tt2" aria-expanded="true" aria-controls="collapse2" tabindex="-1">
                        <h4 class="panel-title">Thông tin thi công
                        </h4>
                    </a>
                </div>
                <div id="collapse2" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="heading2">
                    <div class="panel-body">
                        <div class="form-horizontal">
                            <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group mr-t10">
                                    <label class="col-sm-4 control-label pd-r0">Đơn vị thi công</label>
                                    <div class="col-sm-8 col-md-9 col-lg-8">
                                        <asp:TextBox ID="txtDonViThiCong" runat="server" CssClass="form-control requirements" MaxLength="500" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDonViThiCong"
                                            Display="None" ErrorMessage="Đơn vị thi công không được rỗng"
                                            SetFocusOnError="true" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group mr-t10">
                                    <label class="col-sm-4 control-label pd-r0">Ngày khởi công</label>
                                    <div class="col-md-3">
                                        <telerik:RadDatePicker OnSelectedDateChanged="txtNgayKhoiCong_SelectedDateChanged" ID="txtNgayKhoiCong" CssClass="requirements" runat="server" Calendar-ShowRowHeaders="false" AutoPostBack="true"
                                            ShowPopupOnFocus="false" RenderMode="Lightweight" Width="100%" DatePopupButton-ToolTip="Chọn ngày">
                                            <DateInput runat="server" ID="DateInput4" placeholder="" Style="font-size: 13px;" EmptyMessage="">
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtNgayKhoiCong"
                                            Display="None" ErrorMessage="Vui lòng chọn ngày khởi công"
                                            SetFocusOnError="true" />
                                    </div>
                                    <label class="col-sm-2 col-md-2 control-label pd-r0">Thời gian thi công</label>
                                    <div class="col-sm-3 col-md-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtThoiGianThiCong" OnTextChanged="txtThoiGianThiCong_TextChanged" AutoPostBack="true" runat="server" placeholder="" CssClass="form-control requirements auto {aSep: '.', aDec: ',', aSign: '', mNum: 6, mDec: 6, aPad: false}"></asp:TextBox>
                                            <span class="input-group-addon">ngày</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtThoiGianThucHien"
                                                Display="None" ErrorMessage="Thời gian thi công không được rỗng"
                                                SetFocusOnError="true" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group mr-t10">
                                    <label class="col-sm-4 control-label pd-r0">Ngày hết hạn thi công</label>
                                    <div class="col-md-3">
                                        <telerik:RadDatePicker DateInput-ReadOnly="true" ID="txtNgayHetHanThiCong" CssClass="" runat="server" Calendar-ShowRowHeaders="false" AutoPostBack="true"
                                            ShowPopupOnFocus="false" RenderMode="Lightweight" Width="100%" DatePopupButton-ToolTip="Chọn ngày">
                                            <DateInput runat="server" ID="DateInput5" placeholder="" Style="font-size: 13px;" EmptyMessage="">
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                    </div>
                                    <label class="col-sm-2 col-md-2 control-label pd-r0">T/g nhắc đến hạn</label>
                                    <div class="col-sm-3 col-md-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtTGDenHanThiCong" runat="server" placeholder="" CssClass="form-control requirements auto {aSep: '.', aDec: ',', aSign: '', mNum: 6, mDec: 6, aPad: false}"></asp:TextBox>
                                            <span class="input-group-addon">ngày</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtTGDenHanThiCong"
                                                Display="None" ErrorMessage="Thời gian nhắc đến hạn thi công không được rỗng"
                                                SetFocusOnError="true" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-white">
                <div class="panel-heading pd-8" role="tab" id="heading3">
                    <a role="button" data-toggle="collapse" href="#collapse3" id="tt3" aria-expanded="true" aria-controls="collapse3" tabindex="-1">
                        <h4 class="panel-title">Thông tin bảo lãnh ban đầu
                            
                        </h4>
                    </a>
                </div>
                <div id="collapse3" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="heading3">
                    <div class="panel-body">
                        <div class="form-horizontal">
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

                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-horizontal">
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 col-lg-11 mr-10" style="text-align: center">
                    <asp:LinkButton ID="btnCapNhat" runat="server" CausesValidation="true" OnClick="btnCapNhat_Click" CssClass="btn btn-primary waves-effect none-radius none-shadow btn-sm min-width-100"><i class="glyphicon glyphicon-floppy-disk"></i> Cập nhật</asp:LinkButton>
                    <asp:LinkButton ID="btnCapNhatTiepTuc" CommandName="TiepTuc" runat="server" CausesValidation="true" OnClick="btnCapNhat_Click" CssClass="btn btn-primary waves-effect none-radius none-shadow btn-sm min-width-100"><i class="glyphicon glyphicon-floppy-disk"></i> Cập nhật & Tiếp tục</asp:LinkButton>
                    <asp:LinkButton ID="btnHuyKetThucHopDong" CommandName="HuyKetThuc" Visible="false" OnClick="ketthucHD" OnClientClick="return ConfirmHuyKetThucHopDong()"  oncontextmenu="return false"  runat="server" CausesValidation="true" CssClass="btn btn-primary waves-effect none-radius none-shadow btn-sm min-width-100"><i class="glyphicon glyphicon-check"></i> Hủy kết thúc hợp đồng</asp:LinkButton>
                    <asp:LinkButton ID="btnKetThucHopDong" OnClick="ketthucHD" OnClientClick="return ConfirmKetThucHopDong()"  oncontextmenu="return false"  runat="server" CausesValidation="true" CssClass="btn btn-primary waves-effect none-radius none-shadow btn-sm min-width-100"><i class="glyphicon glyphicon-check"></i> Kết thúc hợp đồng</asp:LinkButton>
                    <asp:LinkButton ID="btnBoQua" runat="server" OnClick="btnBoQua_Click" CssClass="btn btn-sm btn-default waves-effect none-radius none-shadow min-width-100" CausesValidation="false"><i class='fa fa-mail-reply-all'></i> Bỏ qua</asp:LinkButton>
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btn_TL" />
        <asp:PostBackTrigger ControlID="GridView1" />
    </Triggers>
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
    function ConfirmKetThucHopDong() {
        if (confirm("Bạn có muốn kết thúc hợp đồng không?") == true) {
            return true;
        }
        else {
            return false;
        }
    }
    function ConfirmHuyKetThucHopDong() {
        if (confirm("Bạn có muốn hủy kết thúc hợp đồng không?") == true) {
            return true;
        }
        else {
            return false;
        }
    }
</script>
