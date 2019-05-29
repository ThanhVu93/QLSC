<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SUCO_CN.ascx.cs" Inherits="QLSC.SUCO_CN" %>
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
                                    <label class="col-sm-4 control-label pd-r0">Loại sự cố </label>
                                    <div class="col-sm-8 col-md-9 col-lg-8">
                                        <telerik:RadComboBox Skin="Simple" ID="drpLoaiSuCo" Filter="Contains"  CausesValidation="false" CssClass="custom-radcombox "
                                    InputCssClass="form-control requirements" AllowCustomText="true" runat="server" Width="50%" Height="250px" 
                                    EmptyMessage="-- Chọn --" ShowWhileLoading="true" AutoPostBack="true"
                                    LoadingMessage="Đang tải..." Localization-NoMatches="Không tìm thấy">                                            
                                </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="rq_LSC" runat="server" ControlToValidate="drpLoaiSuCo"
                                            Display="None" ErrorMessage="Vui lòng chọn Loại sự cố"
                                            SetFocusOnError="true" />
                                    </div>
                                </div>
                            </div>  
                            <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group   mr-t10">
                                    <label class="col-sm-4 control-label pd-r0">Ngày, tháng xảy ra sự cố</label>
                                    <div class="col-md-3">
                                        <telerik:RadDatePicker ID="txtNgayXayRaSuCo" CssClass="requirements" runat="server" Calendar-ShowRowHeaders="false" AutoPostBack="true"
                                            ShowPopupOnFocus="false" RenderMode="Lightweight" Width="70%" DatePopupButton-ToolTip="Chọn ngày">
                                            <DateInput runat="server" ID="DateInput12" placeholder="Ngày xảy ra sự cố" Style="font-size: 13px;" EmptyMessage="">
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNgayXayRaSuCo"
                                            Display="None" ErrorMessage="Vui lòng chọn Ngày, tháng xảy ra sự cố"
                                            SetFocusOnError="true" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group mr-t10">
                                    <label class="col-sm-4 control-label pd-r0">Giờ xảy ra sự cố</label>
                                    <div class="col-xs-1 pd-r5">
                                        <asp:DropDownList ID="drpGioXayRa" runat="server" CssClass="form-control  pd-l5 pd-r5">
                                            <asp:ListItem Value="0">0 Giờ</asp:ListItem>
                                            <asp:ListItem Value="1">1 Giờ </asp:ListItem>
                                            <asp:ListItem Value="2">2 Giờ</asp:ListItem>
                                            <asp:ListItem Value="3">3 Giờ</asp:ListItem>
                                            <asp:ListItem Value="4">4 Giờ</asp:ListItem>
                                            <asp:ListItem Value="5">5 Giờ</asp:ListItem>
                                            <asp:ListItem Value="6">6 Giờ</asp:ListItem>
                                            <asp:ListItem Value="7">7 Giờ</asp:ListItem>
                                            <asp:ListItem Value="8">8 Giờ</asp:ListItem>
                                            <asp:ListItem Value="9">9 Giờ</asp:ListItem>
                                            <asp:ListItem Value="10">10 Giờ</asp:ListItem>
                                            <asp:ListItem Value="11">11 Giờ</asp:ListItem>
                                            <asp:ListItem Value="12">12 Giờ</asp:ListItem>
                                            <asp:ListItem Value="13">13 Giờ</asp:ListItem>
                                            <asp:ListItem Value="14">14 Giờ</asp:ListItem>
                                            <asp:ListItem Value="15">15 Giờ</asp:ListItem>
                                            <asp:ListItem Value="16">16 Giờ</asp:ListItem>
                                            <asp:ListItem Value="17">17 Giờ</asp:ListItem>
                                            <asp:ListItem Value="18">18 Giờ</asp:ListItem>
                                            <asp:ListItem Value="19">19 Giờ</asp:ListItem>
                                            <asp:ListItem Value="20">20 Giờ</asp:ListItem>
                                            <asp:ListItem Value="21">21 Giờ</asp:ListItem>
                                            <asp:ListItem Value="22">22 Giờ</asp:ListItem>
                                            <asp:ListItem Value="23">23 Giờ</asp:ListItem>
                                            <asp:ListItem Value="24">24 Giờ</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-xs-1   pd-r5">
                                        <asp:DropDownList ID="drpPhutXayra" runat="server" CssClass="form-control  pd-l5 pd-r5">
                                            <asp:ListItem Value="0">0 Phút</asp:ListItem>
                                            <asp:ListItem Value="10">10 Phút</asp:ListItem>
                                            <asp:ListItem Value="15">15 Phút</asp:ListItem>
                                            <asp:ListItem Value="20">20 Phút</asp:ListItem>
                                            <asp:ListItem Value="25">25 Phút</asp:ListItem>
                                            <asp:ListItem Value="30">30 Phút</asp:ListItem>
                                            <asp:ListItem Value="35">35 Phút</asp:ListItem>
                                            <asp:ListItem Value="40">40 Phút</asp:ListItem>
                                            <asp:ListItem Value="45">45 Phút</asp:ListItem>
                                            <asp:ListItem Value="50">50 Phút</asp:ListItem>
                                            <asp:ListItem Value="55">55 Phút</asp:ListItem>
                                        </asp:DropDownList>



                                    </div>
                                    <label class="col-sm-2 control-label pd-r0">Giờ tái lập</label>
                                    <div class="col-xs-1 pd-r5">
                                        <asp:DropDownList ID="drpGioTaiLap" runat="server" CssClass="form-control  pd-l5 pd-r5">
                                            <asp:ListItem Value="0">0 Giờ</asp:ListItem>
                                            <asp:ListItem Value="1">1 Giờ</asp:ListItem>
                                            <asp:ListItem Value="2">2 Giờ</asp:ListItem>
                                            <asp:ListItem Value="3">3 Giờ</asp:ListItem>
                                            <asp:ListItem Value="4">4 Giờ</asp:ListItem>
                                            <asp:ListItem Value="5">5 Giờ</asp:ListItem>
                                            <asp:ListItem Value="6">6 Giờ</asp:ListItem>
                                            <asp:ListItem Value="7">7 Giờ</asp:ListItem>
                                            <asp:ListItem Value="8">8 Giờ</asp:ListItem>
                                            <asp:ListItem Value="9">9 Giờ</asp:ListItem>
                                            <asp:ListItem Value="10">10 Giờ</asp:ListItem>
                                            <asp:ListItem Value="11">11 Giờ</asp:ListItem>
                                            <asp:ListItem Value="12">12 Giờ</asp:ListItem>
                                            <asp:ListItem Value="13">13 Giờ</asp:ListItem>
                                            <asp:ListItem Value="14">14 Giờ</asp:ListItem>
                                            <asp:ListItem Value="15">15 Giờ</asp:ListItem>
                                            <asp:ListItem Value="16">16 Giờ</asp:ListItem>
                                            <asp:ListItem Value="17">17 Giờ</asp:ListItem>
                                            <asp:ListItem Value="18">18 Giờ</asp:ListItem>
                                            <asp:ListItem Value="19">19 Giờ</asp:ListItem>
                                            <asp:ListItem Value="20">20 Giờ</asp:ListItem>
                                            <asp:ListItem Value="21">21 Giờ</asp:ListItem>
                                            <asp:ListItem Value="22">22 Giờ</asp:ListItem>
                                            <asp:ListItem Value="23">23 Giờ</asp:ListItem>
                                            <asp:ListItem Value="24">24 Giờ</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-xs-1 pd-r5">
                                        <asp:DropDownList ID="drpPhutTaiLap" runat="server" CssClass="form-control  pd-l5 pd-r5">
                                            <asp:ListItem Value="0">0 Phút</asp:ListItem>
                                            <asp:ListItem Value="10">10 Phút</asp:ListItem>
                                            <asp:ListItem Value="15">15 Phút</asp:ListItem>
                                            <asp:ListItem Value="20">20 Phút</asp:ListItem>
                                            <asp:ListItem Value="25">25 Phút</asp:ListItem>
                                            <asp:ListItem Value="30">30 Phút</asp:ListItem>
                                            <asp:ListItem Value="35">35 Phút</asp:ListItem>
                                            <asp:ListItem Value="40">40 Phút</asp:ListItem>
                                            <asp:ListItem Value="45">45 Phút</asp:ListItem>
                                            <asp:ListItem Value="50">50 Phút</asp:ListItem>
                                            <asp:ListItem Value="55">55 Phút</asp:ListItem>
                                        </asp:DropDownList>


                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group mr-t10">
                                    <label class="col-sm-4 control-label pd-r0">Nội dung sự cố</label>
                                    <div class="col-sm-8 col-md-9 col-lg-8">
                                        <asp:TextBox ID="txtNoiDungSuCo" runat="server" CssClass="form-control requirements" TextMode="MultiLine" Rows="5" />
                                         <asp:RequiredFieldValidator ID="rq_NDSC" runat="server" ControlToValidate="txtNoiDungSuCo"
                                            Display="None" ErrorMessage="Vui lòng nhập Nội dung sự cố"
                                            SetFocusOnError="true" />
                                    </div>

                                </div>
                            </div>
                            <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group mr-t10">
                                    <label class="col-sm-4 control-label pd-r0">Nguyên nhân sự cố</label>
                                    <div class="col-sm-8 col-md-9 col-lg-8">
                                        <asp:TextBox ID="txtNguyenNhan" runat="server" CssClass="form-control requirements" TextMode="MultiLine" Rows="5" />
                                         <asp:RequiredFieldValidator ID="rq_NguyenNhan" runat="server" ControlToValidate="txtNguyenNhan"
                                            Display="None" ErrorMessage="Vui lòng nhập Nguyên nhân sự cố"
                                            SetFocusOnError="true" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group mr-t10">
                                    <label class="col-sm-4 control-label pd-r0">VTTB hư hỏng </label>
                                    <div class="col-sm-8 col-md-9 col-lg-8">
                                        <asp:TextBox ID="txtTenChungLoai" runat="server" CssClass="form-control" placeholder='01 chì 3K, "01 MBA 15KBVA "' />
                                    </div>
                                </div>
                            </div>                           
                            <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group mr-t10">
                                    <label class="col-sm-4 control-label pd-r0">VTTB số lượng</label>
                                    <div class="col-sm-3 col-md-2 col-lg-3">
                                        <asp:TextBox ID="txtSoLuong" runat="server" CssClass="form-control requirements auto {aSep: '.', aDec: ',', aSign: '', mNum: 12, mDec: 12, aPad: false}" MaxLength="500" />
                                    </div>
                                    <label class="col-sm-2 control-label pd-r0">VTTB nhà sản xuất</label>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtNhaSX" runat="server" CssClass="form-control requirements" MaxLength="500" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group mr-t10">
                                    <label class="col-sm-4 control-label pd-r0">Điện áp </label>
                                    <div class="radio col-xs-8 rdoList-3-col form_radiobuttonlist">
                                        <div class="col-sm-8 col-md-9 col-lg-8">
                                            <asp:RadioButtonList runat="server" RepeatColumns="2" AutoPostBack="true" RepeatLayout="Table" ID="DienAp">
                                                <asp:ListItem Value="1" Selected="true" >HT </asp:ListItem>
                                                <asp:ListItem Value="2"  style="padding-left:100px">TT</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                               <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group mr-t10">
                                    <label class="col-sm-4 control-label pd-r0"></label>
                                    <div class="radio col-xs-8 rdoList-3-col form_radiobuttonlist">
                                        <div class="col-sm-8 col-md-9 col-lg-8">
                                            <asp:RadioButtonList runat="server" RepeatColumns="2" AutoPostBack="true" RepeatLayout="Table" ID="rd_CQ_KQ">
                                                <asp:ListItem Value="CQ" Selected="true" >CQ </asp:ListItem>
                                                <asp:ListItem Value="KQ"  style="padding-left:100px">KQ</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                             <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group mr-t10">
                                    <label class="col-sm-4 control-label pd-r0">Phân loại sự cố</label>
                                    <div class="col-sm-8 col-md-9 col-lg-8">
                                        <telerik:RadComboBox Skin="Simple" ID="drpPhanLoai" Filter="Contains"  CausesValidation="false" CssClass="custom-radcombox "
                                    InputCssClass="form-control requirements" AllowCustomText="true" runat="server" Width="50%" Height="250px" DropDownWidth="500px"
                                    EmptyMessage="-- Chọn --" ShowWhileLoading="true" AutoPostBack="true"
                                    LoadingMessage="Đang tải..." Localization-NoMatches="Không tìm thấy">
                                            <Items>
                                                <telerik:RadComboBoxItem  Value="1" Text="TQ - Bật MC"/>
                                                <telerik:RadComboBoxItem  Value="2" Text="TQ - RCS"/>
                                                <telerik:RadComboBoxItem  Value="3" Text="VC - Bật MC"/>
                                                <telerik:RadComboBoxItem  Value="4" Text="VC - RCS"/>
                                                <telerik:RadComboBoxItem  Value="5" Text="TBA"/>
                                                <telerik:RadComboBoxItem  Value="6" Text="HA"/>
                                            </Items>
                                </telerik:RadComboBox>
                                         <asp:RequiredFieldValidator ID="rq_PHANLOAI" runat="server" ControlToValidate="drpPhanLoai"
                                            Display="None" ErrorMessage="Vui lòng chọn Phân loại sự cố"
                                            SetFocusOnError="true" />

                                    </div>
                                </div>
                            </div>
                              <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group mr-t10">
                                    <label class="col-sm-4 control-label pd-r0">Tổng số khách hàng mất điện </label>
                                    <div class="col-sm-8 col-md-9 col-lg-8">
                                        <asp:TextBox ID="txtTongSoKH" runat="server" CssClass="form-control auto {aSep: '.', aDec: ',', aSign: '', mNum: 12, mDec: 12, aPad: false}" Width="50%"   />
                                    </div>
                                </div>
                            </div>
                              <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group mr-t10">
                                    <label class="col-sm-4 control-label pd-r0">Tài sản </label>
                                    <div class="radio col-xs-8 rdoList-3-col form_radiobuttonlist">
                                        <div class="col-sm-8 col-md-9 col-lg-8">
                                            <asp:RadioButtonList runat="server" RepeatColumns="2" AutoPostBack="true" RepeatLayout="Table" ID="TaiSan">
                                                <asp:ListItem Value="1" Selected="true" >Điện lực </asp:ListItem>
                                                <asp:ListItem Value="2"  style="padding-left:74px">Khách hàng </asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                             <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group mr-t10">
                                    <label class="col-sm-4 control-label pd-r0">Ghi chú </label>
                                    <div class="col-sm-8 col-md-9 col-lg-8">
                                        <asp:TextBox ID="txtGhiChu" runat="server" CssClass="form-control" MaxLength="490" TextMode="MultiLine" Rows="4" />
                                    </div>
                                </div>
                            </div>
                           
                              <div class="col-sm-12 col-md-11 col-lg-10">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label pd-r0">Tập tin </label>
                                    <div class="col-sm-6 col-md-7 col-lg-6">
                                        <asp:FileUpload ID="f_HinhAnh" runat="server" CssClass="" AllowMultiple="true" Style="float: left;" />
                                        <asp:LinkButton ID="btn_TL" OnClick="btn_TL_Click" runat="server" UseSubmitBehavior="false" CausesValidation="false" CssClass="btn btn-sm btn-default none-radius shadow-btn-sm min-width-100" Style="float: right;"><i class="fa fa-upload" aria-hidden="true"></i>Tải lên</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="form-group mr-t20">
                                    <label class="col-sm-4 control-label pd-r0"></label>
                                    <div class="col-sm-6 col-md-7 col-lg-6">
                                        <asp:Panel ID="pnlDanhSach" runat="server" CssClass="danhsach" Style="padding: 0px">
                                    <asp:GridView  ID="dgDanhSach" CssClass="Grid" OnRowDataBound="OnRowDataBound" runat="server" OnRowDeleting="OnRowDeleting"
                                        AutoGenerateColumns="false">
                                        <HeaderStyle CssClass="tieude" />
                                        <Columns>                                          
                                            <asp:TemplateField HeaderText="Tên file">
                                                <ItemStyle Width="20%" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                      <%#Eval("FILE_MOTA")%>                                                   
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Tải về">
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <a oncontextmenu="return false" target="_blank" href='<%#vPathCommonData+Eval("HA_FILE_PATH")%>' runat="server"><span class="glyphicon glyphicon-download-alt"></span></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ItemStyle-Width="10%" HeaderText="Xóa" ShowDeleteButton="True" ItemStyle-HorizontalAlign="Center" DeleteText="<span class='glyphicon glyphicon-remove'></span>" />
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
            <div class="row">
                <div class="form-horizontal">
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 col-lg-11 mr-10" style="text-align: center">
                    <asp:LinkButton ID="btnCapNhat" runat="server" CausesValidation="true" OnClick="btnCapNhat_Click" CssClass="btn btn-primary waves-effect none-radius none-shadow btn-sm min-width-100"><i class="glyphicon glyphicon-floppy-disk"></i> Cập nhật</asp:LinkButton>
                    <%--<asp:LinkButton ID="btnCapNhatTiepTuc" CommandName="TiepTuc" runat="server" CausesValidation="true" OnClick="btnCapNhat_Click" CssClass="btn btn-primary waves-effect none-radius none-shadow btn-sm min-width-100"><i class="glyphicon glyphicon-floppy-disk"></i> Cập nhật & Tiếp tục</asp:LinkButton>
                    <asp:LinkButton ID="btnHuyKetThucHopDong" CommandName="HuyKetThuc" Visible="false" OnClientClick="return ConfirmHuyKetThucHopDong()" oncontextmenu="return false" runat="server" CausesValidation="true" CssClass="btn btn-primary waves-effect none-radius none-shadow btn-sm min-width-100"><i class="glyphicon glyphicon-check"></i> Hủy kết thúc hợp đồng</asp:LinkButton>
                    <asp:LinkButton ID="btnKetThucHopDong" OnClientClick="return ConfirmKetThucHopDong()" oncontextmenu="return false" runat="server" CausesValidation="true" CssClass="btn btn-primary waves-effect none-radius none-shadow btn-sm min-width-100"><i class="glyphicon glyphicon-check"></i> Kết thúc hợp đồng</asp:LinkButton>--%>
                    <asp:LinkButton ID="btnBoQua" runat="server" OnClick="btnBoQua_Click" CssClass="btn btn-sm btn-default waves-effect none-radius none-shadow min-width-100" CausesValidation="false"><i class='fa fa-mail-reply-all'></i> Bỏ qua</asp:LinkButton>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-12 mr-b10">
                    <strong>Lưu ý: </strong>
                    <br />
                    - <span>(<span class="batbuoc" style="top: 3px; left: 1px;">*</span>) bắt buộc nhập</span><br />
                    - <span>Chỉ hỗ trợ tập tin có kích thước tối đa là <span style="color: red">10MB</span></span>.<br />
                    - <span>Chỉ hỗ trợ tập tin có kích định dạng  <span style="color: red"> (jpg, jpeg, png, xls, xlsx, doc, docx, pdf, zip, rar)</span></span>.
                </div>
            </div>

        </asp:Panel>
    </ContentTemplate>
      <Triggers>
        <asp:PostBackTrigger ControlID="btn_TL" />        
    </Triggers>
</asp:UpdatePanel>

<style>
    .css-radio input[type=radio]:checked + label, .form_radiobuttonlist input[type=radio]:checked + label {
        background-position: 0 -22px;
    }

    .css-checkbox input[type=checkbox] + label, .css-radio input[type=radio] + label, .form_checkboxlist input[type=checkbox] + label, .form_radiobuttonlist input[type=radio] + label {
        padding-left: 27px;
        height: 22px;
        display: inline-block;
        line-height: 22px;
        background-repeat: no-repeat;
        background-position: 0 0;
        vertical-align: middle;
        cursor: pointer;
    }

    .css-checkbox input[type=checkbox], .css-radio input[type=radio], .form_checkboxlist input[type=checkbox], .form_radiobuttonlist input[type=radio] {
        position: absolute;
        z-index: -1000;
        left: -1000px;
        overflow: hidden;
        clip: rect(0 0 0 0);
        height: 1px;
        width: 1px;
        margin: -1px;
        padding: 0;
        border: 0;
    }

    table.rdoList-3-col tr td label {
        margin-right: 15px !important;
        font-weight: normal !important;
    }
</style>

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

