<%@ Control Language="C#" AutoEventWireup="true" CodeFile="THONGKE_SUCO_THEOTHANG.ascx.cs" Inherits="QLSC.THONGKE_SUCO_THEOTHANG" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html>
<html>
<head>
</head>
<body>
    <div class="form-inline header_tk">
            <div>Thống kê sự cố do động vật, do sét đánh, do thiết bị phóng điện và  do vi phạm lành lang an toàn lưới điện cao áp (HLATLĐCA)</div>
        </div>
    <div class="form-inline">
        
        <%--<asp:LinkButton ID="btn_XuatExcel" Visible="true" OnClientClick="InThongKe()'" runat="server" CssClass="btn btn-primary waves-effect none-radius none-shadow btn-sm min-width-100 mr-t3 mr-b6 fright mr-r10"><i class="glyphicon glyphicon-print"></i> In báo cáo</asp:LinkButton>--%>
        <div class="form-group pd-l10" style="margin-bottom: -3px; float:right;">
            <button class="btn btn-primary waves-effect none-radius none-shadow btn-sm min-width-100 mr-b6 fright mr-r10 hv" onclick="InThongKe();"><i class='fa fa-print' style="font-size: 15px"></i>In thống kê </button>
            <telerik:RadComboBox Skin="Simple" ID="drpDonVi" Filter="Contains" CssClass="custom-radcombox drp pd-r20" Style="font-size: 14px;"
                InputCssClass="form-control" AllowCustomText="true" runat="server" Width="200px"
                Height="250px" EmptyMessage="Chọn tháng" ShowWhileLoading="true" LoadingMessage="Đang tải..."
                Localization-NoMatches="Không tìm thấy" AutoPostBack="True">
                <Items>
                    <telerik:RadComboBoxItem Value="1" Text="Tháng 1" Selected="true" />
                    <telerik:RadComboBoxItem Value="2" Text="Tháng 2" />
                    <telerik:RadComboBoxItem Value="3" Text="Tháng 3" />
                    <telerik:RadComboBoxItem Value="4" Text="Tháng 4" />
                    <telerik:RadComboBoxItem Value="5" Text="Tháng 5" />
                    <telerik:RadComboBoxItem Value="6" Text="Tháng 6" />
                    <telerik:RadComboBoxItem Value="7" Text="Tháng 7" />
                    <telerik:RadComboBoxItem Value="8" Text="Tháng 8" />
                    <telerik:RadComboBoxItem Value="9" Text="Tháng 9" />
                    <telerik:RadComboBoxItem Value="10" Text="Tháng 10" />
                    <telerik:RadComboBoxItem Value="11" Text="Tháng 11" />
                    <telerik:RadComboBoxItem Value="12" Text="Tháng 12" />
                </Items>
            </telerik:RadComboBox>
        </div>
        
    </div>
    <div runat="server" id="pPrint">

        <style>
            #suco {
                font-family: Tahoma, Arial, Helvetica, sans-serif;
                border-collapse: collapse;
                width: 100%;
                text-align: center !important;
            }

                #suco td, #customers th {
                    border: 1px solid #ddd;
                    padding: 8px;
                }

            /*#suco tr:nth-child(even) {
                background-color: #f2f2f2;*/
            }

            #suco tr:hover {
                background-color: #ddd;
            }

            #suco th {
                border: 1px solid #bdbaba;
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #ddd;
                color: black;
                text-align: center !important;
                padding-left: 5px;
                padding-right: 5px;
            }

            .center {
                text-align: center !important;
            }

            .text_right {
                text-align: center !important;
            }

            .header_tk {
                text-transform: uppercase;
                margin-bottom: 3px;
                margin-top: 3px;
                color: #3c8dbc;
                font-size: 14px;
                padding-left: 10px;
                font-weight: bold !important;
                text-align: center;
                color: #3c8dbc;
            }

            .btn-primary:active {
                color: #fff;
                background-color: #337ab7;
                border-color: #204d74;
            }
        </style>
        <table id="suco">
            <div class="form-inline header_tk">
                <div id="title"></div>
            </div>
            <asp:Literal runat="server" ID="lbContent"></asp:Literal>
        </table>
    </div>
</body>
</html>
<script type="text/javascript">
    function InThongKe() {
        var divElements = document.getElementById('<%=pPrint.ClientID%>').innerHTML;
        var oldContent = document.body.innerHTML;
        document.getElementById("title").textContent = "Thống kê sự cố do động vật, do sét đánh, do thiết bị phóng điện và  do vi phạm lành lang an toàn lưới điện cao áp (HLATLĐCA)";
        document.body.innerHTML =
                "<html><head><title></title></head><body>" +
                divElements
                + "</body></html>";
        window.print();
        document.getElementById("title").textContent = "";
        var url = '<%=DotNetNuke.Common.Globals.NavigateURL()%>';
        window.location = url;
    }
</script>
