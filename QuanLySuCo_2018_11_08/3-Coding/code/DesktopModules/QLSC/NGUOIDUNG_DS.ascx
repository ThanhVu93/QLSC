<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NGUOIDUNG_DS.ascx.cs" Inherits="QLSC.NGUOIDUNG_DS" %>
<%@ Register TagPrefix="dnnsc" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:UpdatePanel runat="server" ID="upn">
    <ContentTemplate>
        <asp:Panel ID="pnlFormDanhSach" runat="server" CssClass="form">
    <div class="form-inline">
           <div class="input-group" >
                    <span class="input-group-addon" style="border-radius:1px;background:#FFF;color: #1b5e20;    padding: 7px 12px;"><span class="glyphicon glyphicon-search" style="font-size:14px;"></span></span>
                   <asp:TextBox ID="txt_TK_NoiDung" runat="server" placeholder="Nhập từ khóa ..." MaxLength="200" AutoPostBack="true" OnTextChanged="txt_TK_NoiDung_TextChanged" Width="300" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group pd-l10" style="margin-bottom:-3px">
                    <telerik:RadComboBox Skin="Simple" ID="drpDonVi" Filter="Contains" CssClass="custom-radcombox drp" Style="font-size: 14px;"
                            InputCssClass="form-control" AllowCustomText="true" runat="server" Width="200px"
                            Height="250px" EmptyMessage="Chọn đơn vị" ShowWhileLoading="true" LoadingMessage="Đang tải..." OnSelectedIndexChanged="drpDonVi_SelectedIndexChanged" 
                            Localization-NoMatches="Không tìm thấy" AutoPostBack="True">                           
                    </telerik:RadComboBox>
                </div>
        <asp:LinkButton ID="btn_ThemMoi" Visible="true" runat="server" OnClick="btn_ThemMoi_Click" CssClass="btn btn-primary waves-effect none-radius none-shadow btn-sm min-width-100 mr-t3 mr-b6 fright"><i class="glyphicon glyphicon-plus"></i> Thêm mới</asp:LinkButton>
    </div>
</asp:Panel>

<asp:Panel ID="pnlDanhSach" runat="server" CssClass="danhsach">
    <asp:Panel CssClass="baoloi" runat="server" ID="pnlThongBao" Visible="false">
        <asp:Label ID="lblThongBao" runat="server" Text=""></asp:Label>
    </asp:Panel>

    <asp:DataGrid  runat="server" ID="dgDanhSach" oninit="dgCustom_Init" AutoGenerateColumns="False" OnItemDataBound="dgDanhSach_ItemDataBound" OnPageIndexChanged="dgDanhSach_PageIndexChanged" AllowPaging="True" AllowCustomPaging="False" OnItemCreated="dgDanhSach_ItemCreated" >
        <HeaderStyle CssClass="tieude" />
        <Columns>
            <asp:TemplateColumn HeaderText="Stt">
                <ItemStyle Width="4%" HorizontalAlign="Center" />
                <ItemTemplate>
                    <%# STT() %>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:BoundColumn HeaderText="Tên đăng nhập" DataField="Username" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
            </asp:BoundColumn>

            <asp:BoundColumn HeaderText="Tên người dùng" DataField="ND_TEN">
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="Đơn vị" DataField="ND_DONVI">
                <ItemStyle Width="30%"/>
            </asp:BoundColumn>

            <asp:TemplateColumn HeaderText="Sửa" Visible="true">
                <ItemStyle Width="5%" HorizontalAlign="Center"/>
                <ItemTemplate>
                    <a onserverclick="dgDanhSach_Sua" class="icon-sua" href='<%# Eval("ND_ID").ToString() %>' oncontextmenu="return false" runat="server"></span></a>
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="Xóa" Visible="true">
                <ItemStyle Width="5%" HorizontalAlign="Center"/>
                <ItemTemplate>
                    <a onserverclick="dgDanhSach_Xoa" class="icon-xoa" onclick="return ConfirmDelete();" href='<%# Eval("UserID").ToString() %>' oncontextmenu="return false" runat="server"></a>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
        <PagerStyle Mode="NumericPages" CssClass="paping" PageButtonCount="9999"></PagerStyle>
    </asp:DataGrid>
</asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript" language="javascript">
    function ConfirmDelete() {
        return confirm("Bạn muốn xóa dữ liệu này?");
    }
</script>

