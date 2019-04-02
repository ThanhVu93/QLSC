<%@ Control Language="C#" AutoEventWireup="true" CodeFile="THONGKE_SUCO_SONAMTRUOC.ascx.cs" Inherits="QLSC.THONGKE_SUCO_SONAMTRUOC" %>
<!DOCTYPE html>
<html>
<head>
</head>
<body>
    <div class="form-inline">
        <%--<asp:LinkButton ID="btn_XuatExcel" Visible="true" OnClientClick="InThongKe()'" runat="server" CssClass="btn btn-primary waves-effect none-radius none-shadow btn-sm min-width-100 mr-t3 mr-b6 fright mr-r10"><i class="glyphicon glyphicon-print"></i> In báo cáo</asp:LinkButton>--%>
        <button style=" color: #fff !important;background-color: #337ab7 !important;border-color: #2e6da4 !important;" class="btn btn-primary waves-effect none-radius none-shadow btn-sm min-width-100 mr-t3 mr-b6 fright mr-r10 hv" onclick="InThongKe();"><i class='fa fa-print' style="font-size: 15px"></i>In thống kê </button>
    </div>
    <div runat="server" id="pPrint">
        <div class="form-inline header_tk"><h3>SỰ CỐ NĂM <asp:Literal runat="server" ID="lbNamHienTai"></asp:Literal> SO SÁNH CÙNG KỲ NĂM <asp:Literal runat="server" ID="lbNamTruoc"></asp:Literal></h3></div>
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
            }

            .center {
                text-align: center !important;
            }

            .text_right {
                text-align: center !important;
            }
            .header_tk{
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
            .btn-primary1:hover {
                   /*color: #fff;
    background-color: #286090;
    border-color: #204d74;*/
                    color: #fff !important;
                    background-color: #337ab7 !important;
                    border-color: #2e6da4 !important;
    } 
           
        </style>
        <table id="suco">
            <tr>

                <th width="4%">Stt</th>
                <th class="center" colspan="2" width="16%">SỰ CỐ DO ĐỘNG VẬT<br />
                    (vụ)</th>
                <th width="8">SO SÁNH CÙNG KỲ NĂM 2018</th>

                <th colspan="2" width="16%">SỰ CỐ DO SÉT ĐÁNH<br />
                    (vụ)</th>
                <th width="8%">SO SÁNH CÙNG KỲ NĂM 2018</th>

                <th colspan="2" width="16%">SỰ CỐ DO PHÓNG ĐIỆN<br />
                    (vụ)</th>
                <th width="8%">SO SÁNH CÙNG KỲ NĂM 2018</th>

                <th colspan="2" width="16%">SỰ CỐ DO VI PHẠM HLATLĐCA<br />
                    (vụ)</th>
                <th width="8%">SO SÁNH CÙNG KỲ NĂM 2018</th>
            </tr>
            <asp:Literal runat="server" ID="lbContent"></asp:Literal>
        </table>
    </div>
</body>
</html>
<script type="text/javascript">
    function InThongKe() {
        var divElements = document.getElementById('<%=pPrint.ClientID%>').innerHTML;
        var oldContent = document.body.innerHTML;
        document.body.innerHTML =
                "<html><head><title></title></head><body>" +
                divElements
                + "</body></html>";
        window.print();       
        var url = '<%=DotNetNuke.Common.Globals.NavigateURL()%>';
        window.location = url;
        //document.body.innerHTML = oldContent;
        
    }
</script>
