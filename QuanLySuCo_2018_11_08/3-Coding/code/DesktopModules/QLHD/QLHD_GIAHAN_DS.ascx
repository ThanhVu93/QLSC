<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QLHD_GIAHAN_DS.ascx.cs" Inherits="QLHD.QLHD_GIAHAN_DS" %>
<%@ Register TagPrefix="dnnsc" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script type="text/javascript" src="<%=vPathCommonJS %>"></script>
<asp:UpdatePanel runat="server" ID="uppnl">
    <ContentTemplate>
         <asp:Panel ID="pnlFormDanhSach" runat="server" CssClass="form">
            <div class="form-inline">
                 <asp:Button ID="btn_TK_Tim" runat="server" CssClass="btn_orange btn-sm none-radius  btn min-width-100" Text="Tìm" Visible="false" />
                <div class="form-group fright">
                    <asp:LinkButton ID="btn_XuatExcel" Visible="false" runat="server" CssClass="btn btn-default waves-effect none-radius none-shadow btn-sm min-width-100 mr-t3 mr-b6 mr-r3"><i class="glyphicon glyphicon-export"></i> Xuất Excel</asp:LinkButton>
                    <asp:LinkButton ID="btn_ThemMoi" OnClick="btn_ThemMoi_Click" runat="server" CssClass="btn btn-primary waves-effect none-radius none-shadow btn-sm min-width-100 mr-t3 mr-b6 fright"><i class="glyphicon glyphicon-plus"></i> Thêm mới</asp:LinkButton>
                    <asp:LinkButton ID="btnBoQua" runat="server" OnClick="btnBoQua_Click" CssClass="btn btn-sm btn-default waves-effect none-radius none-shadow min-width-100 mr-t3 mr-b6 mr-r3" CausesValidation="false"><i class='fa fa-mail-reply-all'></i> Trở về</asp:LinkButton>

                </div>
            </div>
        </asp:Panel>
                <asp:Panel ID="pnlDanhSach" runat="server" CssClass="danhsach">
            <asp:Panel CssClass="baoloi" runat="server" ID="pnThongBao" Visible="false">
                <asp:Label ID="lblThongBao" runat="server" Text=""></asp:Label>
            </asp:Panel>
            <asp:DataGrid runat="server" ID="dgDanhSach" OnInit="dgCustom_Init" UseAccessibleHeader="true" AutoGenerateColumns="False" OnItemDataBound="dgDanhSach_ItemDataBound" OnPreRender="dgDanhSach_PreRender" OnPageIndexChanged="dgDanhSach_PageIndexChanged" AllowPaging="True" AllowCustomPaging="true" OnItemCreated="dgDanhSach_ItemCreated" AllowSorting="True">
                <HeaderStyle CssClass="tieude" />
                <Columns>
                    <asp:TemplateColumn HeaderText="#">
                        <ItemStyle Width="3%" HorizontalAlign="Center" />
                        <HeaderStyle Width="3%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%#STT()%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Bắt đầu <br/> & kết thúc">
                        <ItemStyle Width="4%" HorizontalAlign="Center" />
                        <HeaderStyle Width="4%" HorizontalAlign="Center" />
                        <ItemTemplate>
                           <%# DateTime.Parse(DataBinder.Eval(Container,"DataItem.HD_HIEULUC_HD").ToString()).ToString("dd/MM/yyyy") %><br/>
                           <%# DateTime.Parse(DataBinder.Eval(Container,"DataItem.HD_NGAYHETHAN_HD").ToString()).ToString("dd/MM/yyyy") %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Tg nhắc (ngày)">
                        <ItemStyle Width="4%" HorizontalAlign="Center" />
                        <HeaderStyle Width="4%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container,"DataItem.HD_TGNHAC").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Bắt đầu <br/> & kết thúc">
                        <ItemStyle Width="4%" HorizontalAlign="Center" />
                        <HeaderStyle Width="4%" HorizontalAlign="Center" />
                        <ItemTemplate>
                             <%# DateTime.Parse(DataBinder.Eval(Container,"DataItem.HD_NGAYKHOICONG").ToString()).ToString("dd/MM/yyyy") %><br />
                            <%# DateTime.Parse(DataBinder.Eval(Container,"DataItem.HD_NGAYHETHANTHICONG").ToString()).ToString("dd/MM/yyyy") %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Tg nhắc (ngày)">
                        <ItemStyle Width="4%" HorizontalAlign="Center" />
                        <HeaderStyle Width="4%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container,"DataItem.HD_THICONG_TGNHAC").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Bắt đầu <br/> & kết thúc">
                        <ItemStyle Width="4%" HorizontalAlign="Center" />
                        <HeaderStyle Width="4%" HorizontalAlign="Center" />
                        <ItemTemplate>
                             <%# DateTime.Parse(DataBinder.Eval(Container,"DataItem.HD_BLTHUCHIENHOPDONG_TUNGAY").ToString()).ToString("dd/MM/yyyy") %><br />
                            <%# DateTime.Parse(DataBinder.Eval(Container,"DataItem.HD_BLTHUCHIENHOPDONG_DENNGAY").ToString()).ToString("dd/MM/yyyy") %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Tg nhắc (ngày)">
                        <ItemStyle Width="4%" HorizontalAlign="Center" />
                        <HeaderStyle Width="4%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container,"DataItem.HD_BLTHUCHIENHOPDONG_TGNHAC").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Bắt đầu <br/> & kết thúc">
                        <ItemStyle Width="4%" HorizontalAlign="Center" />
                        <HeaderStyle Width="4%" HorizontalAlign="Center" />
                        <ItemTemplate>
                             <%# DateTime.Parse(DataBinder.Eval(Container,"DataItem.HD_BLTHANHTOANVATTU_TUNGAY").ToString()).ToString("dd/MM/yyyy") %><br />
                            <%# DateTime.Parse(DataBinder.Eval(Container,"DataItem.HD_BLTHANHTOANVATTU_DENNGAY").ToString()).ToString("dd/MM/yyyy") %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Tg nhắc (ngày)">
                        <ItemStyle Width="4%" HorizontalAlign="Center" />
                        <HeaderStyle Width="4%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container,"DataItem.HD_BLTHANHTOANVATTU_TGNHAC").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Bắt đầu <br/> & kết thúc">
                        <ItemStyle Width="4%" HorizontalAlign="Center" />
                        <HeaderStyle Width="4%" HorizontalAlign="Center" />
                        <ItemTemplate>
                             <%# DateTime.Parse(DataBinder.Eval(Container,"DataItem.HD_BLTAMUNG_TUNGAY").ToString()).ToString("dd/MM/yyyy") %><br />
                            <%# DateTime.Parse(DataBinder.Eval(Container,"DataItem.HD_BLTAMUNG_DENNGAY").ToString()).ToString("dd/MM/yyyy") %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="Tg nhắc (ngày)">
                        <ItemStyle Width="4%" HorizontalAlign="Center" />
                        <HeaderStyle Width="4%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container,"DataItem.HD_BLTAMUNG_TGNHAC").ToString()%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Sửa">
                        <ItemStyle Width="3%" HorizontalAlign="Center" />
                        <HeaderStyle Width="3%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a onserverclick="dgSua" class="icon-sua" oncontextmenu="return false" href='<%#DataBinder.Eval(Container,"DataItem.GIAHAN_ID")%>' runat="server"></a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Xóa">
                        <ItemStyle Width="3%" HorizontalAlign="Center" />
                        <HeaderStyle Width="3%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a onserverclick="dgXoa" class="icon-xoa" onclick="return ConfirmDelete();" oncontextmenu="return false" href='<%#DataBinder.Eval(Container,"DataItem.GIAHAN_ID")%>' runat="server"></a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle Mode="NumericPages" CssClass="paping" PageButtonCount="9999"></PagerStyle>
            </asp:DataGrid>
        </asp:Panel>

    </ContentTemplate>
</asp:UpdatePanel>