<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QLHD_TRANGCHU_DS.ascx.cs" Inherits="QLHD.QLHD_TRANGCHU_DS" %>
<%@ Register TagPrefix="dnnsc" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script type="text/javascript" src="<%=vPathCommonJS %>"></script>
<asp:UpdatePanel runat="server" ID="uppnl">
    <ContentTemplate>
        <asp:Panel ID="pnlFormDanhSach" runat="server" CssClass="form">
            <div class="form-inline">
                <asp:Button ID="btn_TK_Tim" runat="server" CssClass="btn_orange btn-sm none-radius  btn min-width-100" Text="Tìm" OnClick="btn_TK_Tim_Click" Visible="false" />
                <div class="col-md-5 pd-l0">
                    <asp:TextBox ID="txt_TK_NoiDung" OnTextChanged="txt_TK_NoiDung_TextChanged" runat="server" AutoPostBack="true" Visible="true" placeholder="Số hợp đồng, Tên công trình" MaxLength="200" Width="100%" CssClass="form-control mr-b10"></asp:TextBox>
                </div>
                <div class="col-md-2 pd-l0">
                    <asp:DropDownList ID="drpTrangThai" Visible="false" OnSelectedIndexChanged="drpTrangThai_SelectedIndexChanged" AutoPostBack="True" runat="server" CssClass="form-control" Width="100%">
                        <asp:ListItem Value="-1">Tất cả hợp đồng</asp:ListItem>
                        <asp:ListItem Value="3">Quá hạn</asp:ListItem>
                        <asp:ListItem Value="2">Đang thực hiện</asp:ListItem>
                        <asp:ListItem Value="1">Kết thúc</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group fright">
                    <asp:LinkButton ID="btn_ThemMoi" Visible="false" OnClick="btn_ThemMoi_Click" runat="server" CssClass="btn btn-primary waves-effect none-radius none-shadow btn-sm min-width-100 mr-t3 mr-b6 fright"><i class="glyphicon glyphicon-plus"></i> Thêm mới</asp:LinkButton>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlDanhSach" runat="server" CssClass="danhsach">
            <asp:Panel CssClass="baoloi" runat="server" ID="pnThongBao" Visible="false">
                <asp:Label ID="lblThongBao" runat="server" Text=""></asp:Label>
            </asp:Panel>
            <asp:DataGrid runat="server" ID="dgDanhSach" AutoGenerateColumns="False" OnInit="dgCustom_Init" OnItemDataBound="dgDanhSach_ItemDataBound" OnPageIndexChanged="dgDanhSach_PageIndexChanged" AllowPaging="True" AllowCustomPaging="true" OnItemCreated="dgDanhSach_ItemCreated" AllowSorting="True">
                <HeaderStyle CssClass="tieude" />
                <Columns>
                    <asp:TemplateColumn>
                        <ItemStyle Width="1%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container,"DataItem.COLOR")%>
                        </ItemTemplate>
                        <HeaderStyle Width="1%" HorizontalAlign="Center" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="#">
                        <ItemStyle Width="3%" HorizontalAlign="Center" />
                        <HeaderStyle Width="3%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%#STT()%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="HD_ID" HeaderText="" Visible="false"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Số HĐ">
                        <ItemStyle Width="8%" HorizontalAlign="Left" />
                        <HeaderStyle Width="8%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container,"DataItem.HD_SO").ToString()%><br />
                            <%#DataBinder.Eval(Container,"DataItem.HD_TRANGTHAI").ToString() != "1"?
                            (DataBinder.Eval(Container,"DataItem.HD_TRANGTHAI").ToString() == "3"?
                            ("<span class='quahan'>(" + DataBinder.Eval(Container,"DataItem._COUNT_SONGAY_HD").ToString() + " ngày)</span>")
                            :(DataBinder.Eval(Container,"DataItem.NHACNHO").ToString() == "1"?("<span class='dangthuchien'>(" + DataBinder.Eval(Container,"DataItem._COUNT_SONGAY_HD").ToString() + " ngày)</span>"):("(" +DataBinder.Eval(Container,"DataItem._COUNT_SONGAY_HD").ToString() + " ngày)"))):
                            ""
                            %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tên công trình">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a id="hplChiTiet" title="Xem chi tiết" oncontextmenu="return false" runat="server" style="color: #1f91f3;">
                                <%# DataBinder.Eval(Container,"DataItem.HD_TENCONGTRINH").ToString()%>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Ngày hiệu lực">
                        <ItemStyle Width="7%" HorizontalAlign="Center" />
                        <HeaderStyle Width="7%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# DateTime.Parse(DataBinder.Eval(Container,"DataItem.HD_HIEULUC_HD").ToString()).ToString("dd/MM/yyyy") %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="HD_TENDONVITHICONG" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderText="Đối tác" ItemStyle-Width="14%" HeaderStyle-Width="14%"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Giá trị (VNĐ)">
                        <ItemStyle Width="11%" HorizontalAlign="Right" />
                        <HeaderStyle Width="11%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <strong><%#String.Format("{0:#,0.###}",DataBinder.Eval(Container,"DataItem.HD_GIATRI"))%></strong>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Thi công">
                        <ItemStyle Width="6%" HorizontalAlign="Center" />
                        <HeaderStyle Width="6%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container,"DataItem.HD_TRANGTHAI").ToString() != "1"?
                            (DataBinder.Eval(Container,"DataItem.HD_TRANGTHAI").ToString() == "3"?
                            ("<span class='quahan'>" + DataBinder.Eval(Container,"DataItem._COUNT_SONGAY_THICONG").ToString() + " ngày</span>")
                            :(DataBinder.Eval(Container,"DataItem.NHACNHOTHICONG").ToString() == "1"?("<span class='dangthuchien'>" + DataBinder.Eval(Container,"DataItem._COUNT_SONGAY_THICONG").ToString() + " ngày</span>"):(DataBinder.Eval(Container,"DataItem._COUNT_SONGAY_THICONG").ToString() + " ngày"))):
                            ""
                            %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Thanh toán">
                        <ItemStyle Width="6%" HorizontalAlign="Center" />
                        <HeaderStyle Width="6%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container,"DataItem.HD_TRANGTHAI").ToString() != "1"?
                            (DataBinder.Eval(Container,"DataItem.HD_TRANGTHAI").ToString() == "3"?
                            ("<span class='quahan'>" + DataBinder.Eval(Container,"DataItem._COUNT_SONGAY_BLTHANHTOAN").ToString() + " ngày</span>")
                            :(DataBinder.Eval(Container,"DataItem.NHACNHOTHANHTOAN").ToString() == "1"?("<span class='dangthuchien'>" + DataBinder.Eval(Container,"DataItem._COUNT_SONGAY_BLTHANHTOAN").ToString() + " ngày</span>"):(DataBinder.Eval(Container,"DataItem._COUNT_SONGAY_BLTHANHTOAN").ToString() + " ngày"))):
                            ""
                            %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Vật tư">
                        <ItemStyle Width="6%" HorizontalAlign="Center" />
                        <HeaderStyle Width="6%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container,"DataItem.HD_TRANGTHAI").ToString() != "1"?
                            (DataBinder.Eval(Container,"DataItem.HD_TRANGTHAI").ToString() == "3"?
                            ("<span class='quahan'>" + DataBinder.Eval(Container,"DataItem._COUNT_SONGAY_BLVATTU").ToString() + " ngày</span>")
                            :(DataBinder.Eval(Container,"DataItem.HD_BLTHANHTOANVATTU_TGNHAC").ToString() == "1"?("<span class='dangthuchien'>" + DataBinder.Eval(Container,"DataItem._COUNT_SONGAY_BLVATTU").ToString() + " ngày</span>"):(DataBinder.Eval(Container,"DataItem._COUNT_SONGAY_BLVATTU").ToString() + " ngày"))):
                            ""
                            %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tạm ứng">
                        <ItemStyle Width="6%" HorizontalAlign="Center" />
                        <HeaderStyle Width="6%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container,"DataItem.HD_TRANGTHAI").ToString() != "1"?
                            (DataBinder.Eval(Container,"DataItem.HD_TRANGTHAI").ToString() == "3"?
                            ("<span class='quahan'>" + DataBinder.Eval(Container,"DataItem._COUNT_SONGAY_BL_TAMUNG").ToString() + " ngày</span>")
                            :(DataBinder.Eval(Container,"DataItem.HD_BLTAMUNG_TGNHAC").ToString() == "1"?("<span class='dangthuchien'>" + DataBinder.Eval(Container,"DataItem._COUNT_SONGAY_BL_TAMUNG").ToString() + " ngày</span>"):(DataBinder.Eval(Container,"DataItem._COUNT_SONGAY_BL_TAMUNG").ToString() + " ngày"))):
                            ""
                            %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Gia hạn">
                        <ItemStyle Width="3%" HorizontalAlign="Center" />
                        <HeaderStyle Width="3%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <img class="img_giahan" runat="server" src="~/DesktopModules/QLHD/image/time5.png" />
                            <sup class="custome_sup"><%#DataBinder.Eval(Container,"DataItem.HD_SOLANGIAHAN").ToString()%></sup>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Kết thúc">
                        <ItemStyle Width="3%" HorizontalAlign="Center" />
                        <HeaderStyle Width="3%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <img class="img_ketthuc" runat="server" src='<%#DataBinder.Eval(Container,"DataItem.HD_TRANGTHAI").ToString() != "1"? "~/DesktopModules/QLHD/image/check_wait.png":
                        "~/DesktopModules/QLHD/image/check_done.png"
                        %>' />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Sửa" Visible="false">
                        <ItemStyle Width="3%" HorizontalAlign="Center" />
                        <HeaderStyle Width="3%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a onserverclick="dgSua" class="icon-sua" oncontextmenu="return false" href='<%#DataBinder.Eval(Container,"DataItem.HD_ID")%>' runat="server"></a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Xóa" Visible="false">
                        <ItemStyle Width="3%" HorizontalAlign="Center" />
                        <HeaderStyle Width="3%" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a onserverclick="dgXoa" class="icon-xoa" onclick="return ConfirmDelete();" oncontextmenu="return false" href='<%#DataBinder.Eval(Container,"DataItem.HD_ID")%>' runat="server"></a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn HeaderText="" DataField="HD_TRANGTHAI" Visible="false"></asp:BoundColumn>
                </Columns>
                <PagerStyle Mode="NumericPages" CssClass="paping" PageButtonCount="9999"></PagerStyle>
            </asp:DataGrid>
        </asp:Panel>
        <div class="col-sm-12 note_form" style="line-height: 20px; display:none">
            <b>Ghi chú: </b>
            <span style=""><i class="fa fa-stop icon_ketthuc"></i>Hợp đồng đã kết thúc</span>
            <span style=""><i class="fa fa-stop icon_chuaketthuc"></i>Hợp đồng đang thực hiện</span>
            <span style=""><i class="fa fa-stop icon_quahan"></i>Hợp đồng quá hạn</span>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<style>
    .content-wrapper{
        margin-top:40px;
    }
    body .LoginTemplate{
        display: contents !important;
    }
    body .LoginTemplate .panel-heading .panel-title{
        margin-top: 70px;
    }
    .img_giahan {
        width: 65%;
    }

    .img_ketthuc {
        width: 65%;
    }

    .custome_sup {
        font-size: 100%;
        margin-left: -7px;
        color: red;
    }

    .icon_quahan {
        color: red;
    }

    .dangthuchien {
        color: Green;
    }

    .quahan {
        color: red
    }
</style>
<script>
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
