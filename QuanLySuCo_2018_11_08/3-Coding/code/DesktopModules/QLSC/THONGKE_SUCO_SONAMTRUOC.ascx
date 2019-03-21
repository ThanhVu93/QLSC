<%@ Control Language="C#" AutoEventWireup="true" CodeFile="THONGKE_SUCO_SONAMTRUOC.ascx.cs" Inherits="QLSC.THONGKE_SUCO_SONAMTRUOC" %>
<!DOCTYPE html>
<html>
<head>
    <style>
        #suco {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
            text-align:center!important;
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
                text-align:center !important;
            }
            .center{
                text-align: center !important;
            }
            .text_right{
                text-align: center !important;
            }
    </style>
</head>
<body>

    <table id="suco">
        <tr>
            <th width="4%">Stt</th>
            <th class="center" colspan="2" width="16%">SỰ CỐ DO ĐỘNG VẬT<br /> (vụ)</th>
            <th  width="8">SO SÁNH CÙNG KỲ NĂM 2018</th>

            <th colspan="2" width="16%">SỰ CỐ DO SÉT ĐÁNH<br /> (vụ)</th>
            <th width="8%">SO SÁNH CÙNG KỲ NĂM 2018</th>

            <th colspan="2" width="16%">SỰ CỐ DO PHÓNG ĐIỆN<br /> (vụ)</th>
            <th width="8%">SO SÁNH CÙNG KỲ NĂM 2018</th>

            <th colspan="2" width="16%">SỰ CỐ DO VI PHẠM HLATLĐCA<br /> (vụ)</th>
            <th width="8%">SO SÁNH CÙNG KỲ NĂM 2018</th>
        </tr>
        <tr>
            <td rowspan="2" class="center">1</td>
            <td>T1/2019</td>
            <td>T1/2018</td>
            <td></td>
            <td>T1/2019</td>
            <td>T1/2018</td>
            <td></td>
            <td>T1/2019</td>
            <td>T1/2018</td>
            <td></td>
            <td>T1/2019</td>
            <td>T1/2018</td>
            <td></td>
        </tr>
        <tr>
            <td class="text_right">01</td>
            <td class="text_right">01</td>
            <td >Không SC</td>
            <td class="text_right">01</td>
            <td class="text_right">01</td>
            <td >Không sự cố</td>
            <td class="text_right">0</td>
            <td class="text_right">0</td>
            <td >Không sự cố</td>
            <td class="text_right">1</td>
            <td class="text_right">2</td>
            <td>Tăng 1 vụ</td>
        </tr>
          <tr>
            <td rowspan="2" class="center">2</td>
            <td>T2/2019</td>
            <td>T2/2018</td>
            <td></td>
            <td>T2/2019</td>
            <td>T2/2018</td>
            <td></td>
            <td>T2/2019</td>
            <td>T2/2018</td>
            <td></td>
            <td>T2/2019</td>
            <td>T2/2018</td>
            <td></td>
        </tr>
        <tr>
            <td class="text_right">01</td>
            <td class="text_right">01</td>
            <td >Không SC</td>
            <td class="text_right">01</td>
            <td class="text_right">01</td>
            <td >Không sự cố</td>
            <td class="text_right">0</td>
            <td class="text_right">0</td>
            <td >Không sự cố</td>
            <td class="text_right">1</td>
            <td class="text_right">2</td>
            <td>Tăng 1 vụ</td>
        </tr>

        <tr>
            <td rowspan="2" class="center">3</td>
            <td>T3/2019</td>
            <td>T3/2018</td>
            <td></td>
            <td>T3/2019</td>
            <td>T3/2018</td>
            <td></td>
            <td>T3/2019</td>
            <td>T3/2018</td>
            <td></td>
            <td>T3/2019</td>
            <td>T3/2018</td>
            <td></td>
        </tr>
        <tr>
            <td class="text_right">01</td>
            <td class="text_right">01</td>
            <td >Không SC</td>
            <td class="text_right">01</td>
            <td class="text_right">01</td>
            <td >Không sự cố</td>
            <td class="text_right">0</td>
            <td class="text_right">0</td>
            <td >Không sự cố</td>
            <td class="text_right">1</td>
            <td class="text_right">2</td>
            <td>Tăng 1 vụ</td>
        </tr>

        <tr>
            <td rowspan="2" class="center">4</td>
            <td>T4/2019</td>
            <td>T4/2018</td>
            <td></td>
            <td>T4/2019</td>
            <td>T4/2018</td>
            <td></td>
            <td>T4/2019</td>
            <td>T4/2018</td>
            <td></td>
            <td>T4/2019</td>
            <td>T4/2018</td>
            <td></td>
        </tr>
        <tr>
            <td class="text_right">01</td>
            <td class="text_right">01</td>
            <td >Không SC</td>
            <td class="text_right">01</td>
            <td class="text_right">01</td>
            <td >Không sự cố</td>
            <td class="text_right">0</td>
            <td class="text_right">0</td>
            <td >Không sự cố</td>
            <td class="text_right">1</td>
            <td class="text_right">2</td>
            <td>Tăng 1 vụ</td>
        </tr>


      
      
    </table>

</body>
</html>
