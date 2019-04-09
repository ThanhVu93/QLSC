<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SUCO_DS.ascx.cs" Inherits="QLSC.SUCO_DS" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script type="text/javascript" src="<%=vPathIQueryJavascript%>iscroll/build/iscroll.js"></script>
<script type="text/javascript" src="<%=vPathCommonJS %>"></script>
<link href="<%=vPathCommonCss%>/scroller_table.css" rel="stylesheet" type="text/css">
<script type="text/javascript" src="<%=vPathCommonToastJS%>"></script>
<link rel="stylesheet" href="<%=vPathCommonToastCSS%>" />
<asp:UpdatePanel ID="upd" runat="server">
    <Triggers>
        <asp:PostBackTrigger ControlID="btn_XuatExcel" />
    </Triggers>
    <ContentTemplate>
        <asp:Panel ID="pnlFormDanhSach" runat="server" CssClass="form">
            <div class="form-inline">
                <div class="input-group mr-b3">
                    <span class="input-group-addon" style="border-radius: 1px; background: #FFF; color: #1b5e20; padding: 7px 12px;"><span class="glyphicon glyphicon-search" style="font-size: 14px;"></span></span>
                    <asp:TextBox ID="txt_TK_NoiDung" runat="server" placeholder="Nội dung, nguyên nhân,..." MaxLength="200" AutoPostBack="true" OnTextChanged="txt_TK_NoiDung_TextChanged" Width="250px" CssClass="form-control"></asp:TextBox>
                </div>
                &nbsp;          
                 <div class="form-group">
                     <telerik:RadComboBox Skin="Simple" ID="drpDonVi" Filter="Contains" CssClass="custom-radcombox drp" Style="font-size: 14px;"
                         InputCssClass="form-control" AllowCustomText="true" runat="server" Width="200px"
                         Height="250px" EmptyMessage="Chọn đơn vị" ShowWhileLoading="true" LoadingMessage="Đang tải..." OnSelectedIndexChanged="drpDonVi_SelectedIndexChanged"
                         Localization-NoMatches="Không tìm thấy" AutoPostBack="True">
                     </telerik:RadComboBox>
                 </div>
                &nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Label runat="server" CssClass="control-labeltndn" Text="Từ ngày" Style="font-weight: bold;"></asp:Label>
                <telerik:RadDatePicker DateInput-DisplayDateFormat="dd-MM-yyyy" AutoPostBack="true" ID="txtTuNgay" runat="server" Width="150px" CssClass="radius0 riSingle RadInput RadInput_Default" OnSelectedDateChanged="txtTuNgay_SelectedDateChanged" DateInput-EmptyMessage="Từ ngày" />
                &nbsp;&nbsp;<asp:Label runat="server" CssClass="control-labeltndn" Text="Đến ngày" Style="font-weight: bold;"></asp:Label>
                <telerik:RadDatePicker DateInput-DisplayDateFormat="dd-MM-yyyy" AutoPostBack="true" ID="txtDenNgay" runat="server" Width="150px" CssClass="radius0 riSingle RadInput RadInput_Default" OnSelectedDateChanged="txtDenNgay_SelectedDateChanged" DateInput-EmptyMessage="Đến ngày" />
                <asp:Button ID="btn_TK_Tim" runat="server" CssClass="btn_orange btn-sm none-radius  btn min-width-100" Text="Tìm" OnClick="btn_TK_Tim_Click" Visible="false" />
                <asp:LinkButton ID="btn_ThemMoi" Visible="true" runat="server" OnClick="btn_ThemMoi_Click" CssClass="btn btn-primary waves-effect none-radius none-shadow btn-sm min-width-100 mr-t3 mr-b6 fright"><i class="glyphicon glyphicon-plus"></i> Thêm mới</asp:LinkButton>
                <asp:LinkButton ID="btn_XuatExcel" Visible="true" runat="server" OnClick="btn_XuatExcel_Click" CssClass="btn btn-primary waves-effect none-radius none-shadow btn-sm min-width-100 mr-t3 mr-b6 fright mr-r10"><i class="glyphicon glyphicon-export"></i> Xuất excel</asp:LinkButton>
            </di>    
        </asp:Panel>
        <div class="boxbox">
            <asp:Panel ID="pnlDanhSach" runat="server" CssClass="danhsach danhsach_scroller_container" >
                <div class="wrapper_scroller" id="divTable" runat="server">
                    <asp:DataGrid UseAccessibleHeader="true" Width="3000px" OnItemDataBound="dgDanhSach_ItemDataBound" runat="server" ID="dgDanhSach" CssClass="has_table  scroller_content" AutoGenerateColumns="False" OnInit="dgCustom_Init" OnPreRender="dgDanhSach_PreRender" OnPageIndexChanged="dgDanhSach_PageIndexChanged" AllowPaging="true" AllowCustomPaging="false" OnItemCreated="dgDanhSach_ItemCreated" >
                        <HeaderStyle CssClass="tieude" />
                        <Columns>
                            <asp:BoundColumn DataField="SC_ID" Visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="STT" HeaderStyle-CssClass="generator-stt">
                                <HeaderStyle Width="2%" CssClass="generator-stt"/>
                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                <ItemTemplate>
                                    <%#STT() %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn HeaderText="Ngày tháng xảy ra sự cố" DataField="SC_NGAYXAYRA" HeaderStyle-CssClass="generator-stt">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <HeaderStyle Width="5%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Giờ xảy ra sự cố" DataField="SC_GIOXAYRA" HeaderStyle-CssClass="generator-stt">
                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                <HeaderStyle Width="3%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Giờ tái lập" DataField="SC_NGAYTAILAP" HeaderStyle-CssClass="generator-stt">
                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                <HeaderStyle Width="3%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Nội dung sự cố" DataField="SC_NOIDUNG" HeaderStyle-CssClass="generator-stt">
                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                <HeaderStyle Width="11%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Nguyên nhân" DataField="SC_NGUYENNHAN" HeaderStyle-CssClass="generator-stt">
                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                <HeaderStyle Width="11%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="ĐV để xảy ra sự cố" DataField="SC_DONVI" HeaderStyle-CssClass="generator-stt">
                                <ItemStyle HorizontalAlign="Left" Width="7%" />
                                <HeaderStyle Width="5%" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn ItemStyle-Width="15%" HeaderStyle-Width="12%" ItemStyle-CssClass="pd-0">
                                <HeaderTemplate>
                                    <table class="table-header-child">
                                        <tbody>
                                            <tr>
                                                <td style="width: 100%; border-bottom: 1px solid #ddd" colspan="3">VTTB sự cố</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 40%;" class="generator-stt">Tên chủng loại</td>
                                                <td style="width: 20%;" class="generator-stt">Số lượng</td>
                                                <td style="width: 40%;" class="generator-stt">Nhà sản xuất</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table class="table-data-child" style="width: 100%">
                                        <tbody>
                                            <tr>
                                                <td style="width: 40%; text-align: left"><%#DataBinder.Eval(Container,"DataItem.SC_VTTB_TENCHUNGLOAI")%></td>
                                                <td style="width: 20%; text-align: right"><%#DataBinder.Eval(Container,"DataItem.SC_VTTB_SOLUONG")%></td>
                                                <td style="width: 40%; text-align: left"><%#DataBinder.Eval(Container,"DataItem.SC_VTTB_NHASANXUAT")%></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:BoundColumn HeaderText="Điện áp (TT, HT)" DataField="SC_DIENAP" HeaderStyle-CssClass="generator-stt">
                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                <HeaderStyle Width="3%" />
                            </asp:BoundColumn>

                            <asp:TemplateColumn ItemStyle-Width="20%" HeaderStyle-Width="20%" ItemStyle-CssClass="pd-0">
                                <HeaderTemplate>
                                    <table class="table-header-child">
                                        <tr>
                                            <td style="width: 30%;" colspan="6">Phân loại</td>
                                        </tr>
                                        <tr>
                                            <%--  <td rowspan="2" style="width: 14%;" class="generator-stt">Tổng lưu lượng khai thác (m³/ngđ)</td>    --%>
                                            <%--<td rowspan="2" style="width: 16%;">Số hiệu giếng</td>--%>
                                            <td colspan="2" style="width: 30%;">TQ                                           
                                            </td>
                                            <td colspan="2" style="width: 30%;">VC</td>
                                            <td rowspan="2" style="width: 20%;">TBA</td>
                                            <td rowspan="2" style="width: 20%;">HA</td>
                                        </tr>

                                        <tr>
                                            <td style="width: 15%;">Đ.Trục</td>
                                            <td style="width: 15%;">Ngã rẽ</td>
                                            <td style="width: 15%;">Đ.Trục</td>
                                            <td style="width: 15%;">Ngã rẽ</td>
                                        </tr>

                                        <div style="width: 15%; border-right: 1px solid #ddd" class="generator-stt"></div>
                                        <div style="width: 15%; border-right: 1px solid #ddd" class="generator-stt"></div>
                                        <div style="width: 15%; border-right: 1px solid #ddd" class="generator-stt"></div>
                                        <div style="width: 15%; border-right: 1px solid #ddd" class="generator-stt"></div>
                                        <div style="width: 20%; border-right: 1px solid #ddd" class="generator-stt"></div>
                                        <div style="width: 20%; border-right: 1px solid #ddd" class="generator-stt"></div>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>

                                    <table class="table-data-child" style="width: 100%">
                                        <tbody>
                                            <tr>
                                                <td style="width: 15%; text-align: center"><%#DataBinder.Eval(Container,"DataItem.SC_PHANLOAI_TQ_DUONGTRUC")%></td>
                                                <td style="width: 15%; text-align: center"><%#DataBinder.Eval(Container,"DataItem.SC_PHANLOAI_TQ_NGARE")%></td>
                                                <td style="width: 15%; text-align: center"><%#DataBinder.Eval(Container,"DataItem.SC_PHANLOAI_VC_DUONGTRUC")%></td>
                                                <td style="width: 15%; text-align: center"><%#DataBinder.Eval(Container,"DataItem.SC_PHANLOAI_VC_NGARE")%></td>
                                                <td style="width: 20%; text-align: center"><%#DataBinder.Eval(Container,"DataItem.SC_PHANLOAI_TBA")%></td>
                                                <td style="width: 20%; text-align: center"><%#DataBinder.Eval(Container,"DataItem.SC_PHANLOAI_HA")%></td>
                                            </tr>
                                            <tbody>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:BoundColumn HeaderText="Tổng số khách hàng bị mất điện" DataField="SC_TONGSOKHACHHANG" HeaderStyle-CssClass="generator-stt">
                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                                <HeaderStyle Width="5%" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn ItemStyle-Width="7%" HeaderStyle-Width="7%" ItemStyle-CssClass="pd-0">
                                <HeaderTemplate>
                                    <table class="table-header-child">
                                        <tbody>
                                            <tr>
                                                <td style="width: 30%; border-bottom: 1px solid #ddd" colspan="2">Tài sản</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%;" class="generator-stt">Điện lực</td>
                                                <td style="width: 50%;" class="generator-stt">Khách hàng</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table class="table-data-child" style="width: 100%">
                                        <tbody>
                                            <tr>
                                                <td style="width: 50%; text-align: center"><%#DataBinder.Eval(Container,"DataItem.SC_TAISAN_TBA")%></td>
                                                <td style="width: 50%; text-align: center"><%#DataBinder.Eval(Container,"DataItem.SC_TAISAN_HA")%></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:BoundColumn HeaderText="Ghi chú" DataField="SC_GHICHU" HeaderStyle-CssClass="generator-stt" >
                                <ItemStyle HorizontalAlign="Left"  />
                                <%--<HeaderStyle Width="8%" />--%>
                            </asp:BoundColumn>

                            <asp:TemplateColumn HeaderText="Sửa" HeaderStyle-CssClass="headerGrid generator-stt">
                                <ItemStyle Width="2%" HorizontalAlign="Center" CssClass="c" />
                                <ItemTemplate>
                                    <a onserverclick="dgSua" oncontextmenu="return false" href='<%#DataBinder.Eval(Container,"DataItem.SC_ID")%>' runat="server"><span class="glyphicon glyphicon-pencil" style="color: #F57010;"></span></a>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Xóa" HeaderStyle-CssClass="headerGrid generator-stt ">
                                <ItemStyle Width="2%" HorizontalAlign="Center" CssClass="c" />
                                <ItemTemplate>
                                    <a onserverclick="dgXoa" class="icon-xoa" onclick="return ConfirmDelete();" oncontextmenu="return false" href='<%#DataBinder.Eval(Container,"DataItem.SC_ID")%>' runat="server"></a>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                        </Columns>
                        <PagerStyle Mode="NumericPages" CssClass="paping" PageButtonCount="9999"></PagerStyle>
                    </asp:DataGrid>
                </div>
            </asp:Panel>
        </div>
    </ContentTemplate>    
</asp:UpdatePanel>
<script type="text/javascript">
    function set_generator_stt() {
        $(".generator-stt-row").remove();
        var generator_num_cols = jQuery(".danhsach table.has_table > tbody > tr.tieude").find(".generator-stt");
        var v_html;
        v_html += "<tr class='generator-stt-row aligncenter' >";
        v_html += "<td colspan='" + generator_num_cols.length + "'>";

        for (var i = 0; i < generator_num_cols.length; i++) {
            v_html += "<div class='generator-stt-item' style='width:" + jQuery(generator_num_cols[i]).outerWidth() + "px;' >";
            v_html += "-" + (i + 1);
            v_html += "</div>";
        }
        v_html += "</td>";
        v_html += "</tr>";
        $('.danhsach table.has_table> tbody>tr:nth-child(2)').after(v_html)
    }
    var myScroll;
    function loaded() {
        if ($('.wrapper_scroller').has("table").length != 0) {
            myScroll = new IScroll('.wrapper_scroller', {
                scrollX: true,
                freeScroll: true,
                preventDefaultException: { tagName: /^(INPUT|TEXTAREA|BUTTON|SELECT|P|TABLE|td|tr)$/ },
                scrollbars: true,
                mouseWheel: true,
                interactiveScrollbars: true,
                shrinkScrollbars: 'scale',
                fadeScrollbars: true

            });
        }
    }
    document.addEventListener('touchmove', function (e) { e.preventDefault(); });
    function pageLoad(sender, args) {
        resizeDG();

        $(window).resize(function () {
            resizeDG();
            //set_generator_stt();
        });
        function resizeDG() {
            var ControlPanel_height = $(".scroller_content").height();
            //alert();
            if (ControlPanel_height > 0) {
                $(".danhsach_scroller_container").css("height", (ControlPanel_height + 35));
            }
        }
        loaded();
        //set_generator_stt();
    }
</script>
<style>
    .danhsach table > tbody > tr.tieude > td,
    .danhsach table > tbody > tr.tieude > th,
    .danhsach table > thead > tr.tieude > th,
    .danhsach table > thead > tr.tieude > td {
        height: 36px;
        border-top: 1px solid #ddd;
        border-left: 1px solid #ddd;
        border-right: 1px solid #ddd;
        border-bottom: 1px solid #ddd;
        background: #eee;
        text-align: center;
        color: #000;
    }

    .danhsach > table.has_table > tbody > tr.tieude > th > table.table-header-child,
    .danhsach > div > table.has_table > tbody > tr.tieude > th > table.table-header-child {
        width: 100%;
        height: 100%;
    }

        .danhsach > table.has_table > tbody > tr.tieude > th > table.table-header-child *,
        .danhsach > div > table.has_table > tbody > tr.tieude > th > table.table-header-child * {
            border-top: 1px solid #ddd;
            background-color: transparent !important;
            font-weight: bold;
            border-bottom: 0px solid #ddd;
            text-align: center;
        }

        .danhsach > table.has_table > tbody > tr.tieude > th > table.table-header-child > tbody > tr > td,
        .danhsach > div > table.has_table > tbody > tr.tieude > th > table.table-header-child > tbody > tr > td {
            border-right: 1px solid #ddd;
        }

            .danhsach > table.has_table > tbody > tr.tieude > th > table.table-header-child > tbody > tr > td:last-child,
            .danhsach > div > table.has_table > tbody > tr.tieude > th > table.table-header-child > tbody > tr > td:last-child {
                border-right: 0px;
            }

    .generator-stt-row > td {
        padding: 0px !important;
        /*padding-top: 0px;
            padding-bottom: 0px;*/
    }

    .generator-stt-item {
        display: table-cell;
        text-align: center;
        height: 35px;
        line-height: 35px;
        overflow: hidden;
        vertical-align: middle;
        border-right: 1px solid #ddd;
    }

    .danhsach > table.has_table > tbody > tr > td > table.table-data-child,
    .danhsach > div > table.has_table > tbody > tr > td > table.table-data-child {
        height: 100%;
    }

        .danhsach > table.has_table > tbody > tr > td > table.table-data-child > tbody > tr > td,
        .danhsach > div > table.has_table > tbody > tr > td > table.table-data-child > tbody > tr > td {
            border-right: 1px solid #ddd !important;
        }

            .danhsach > table.has_table > tbody > tr > td > table.table-data-child > tbody > tr > td:last-child,
            .danhsach > div > table.has_table > tbody > tr > td > table.table-data-child > tbody > tr > td:last-child {
                border-right: 0px !important;
            }

    .danhsach .tooltip.in {
        opacity: 1;
    }

    .tooltip-inner {
        background-color: dimgray !important;
        color: white !important;
    }
</style>
