using DotNetNuke.Instrumentation;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace QLHD
{
    /// <summary>
    /// Cung cấp các hàm hỗ trợ tạo nhanh tập tin Excel
    /// </summary>
    public class ExcelUtils
    {
        /// <summary>
        /// Xuất nội dung DataTable ra excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="headers">Nếu headers null thì sẽ dùng tên cột trong DataTable làm header</param>
        /// <param name="autoFitColumns"></param>
        /// <returns></returns>
        public static byte[] exportDataTableToExcel(DataTable dt, string[] headers, bool autoFitColumns = true)
        {
            byte[] fileBytes = { };
            using (ExcelPackage pck = new ExcelPackage())
            {
                try
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");

                    if (dt != null)
                    {
                        // there is has header
                        if (headers != null)
                        {
                            // create header
                            int header_count = headers.Length;
                            for (int i = 0; i < header_count; i++)
                            {
                                ws.Cells[1, i + 1].Value = headers[i];
                            }
                            // export datatable without datatable's header
                            ws.Cells["A2"].LoadFromDataTable(dt, false);
                        }
                        else
                        {
                            // export datatable with datatable's header
                            ws.Cells["A1"].LoadFromDataTable(dt, true);
                        }

                        // set font
                        ws.Cells.Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Regular));

                        if (autoFitColumns)
                        {
                            ws.Cells[ws.Dimension.Address].AutoFitColumns();
                        }

                        // set border
                        ws.Cells[ws.Dimension.Address].Style.Border.Top.Style =
                                   ws.Cells[ws.Dimension.Address].Style.Border.Bottom.Style =
                                   ws.Cells[ws.Dimension.Address].Style.Border.Left.Style =
                                   ws.Cells[ws.Dimension.Address].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        // Format the header    
                        using (ExcelRange objRange = ws.Cells[1, 1, 1, dt.Columns.Count])
                        {
                            objRange.Style.Font.Bold = true;
                            objRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            objRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            objRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            objRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                    }
                    fileBytes = pck.GetAsByteArray();
                }
                catch (Exception ex)
                {
                    ILog logger = LoggerSource.Instance.GetLogger(typeof(ExcelUtils));
                    logger.Error(ex);
                }

            }
            return fileBytes;
        }
    }
}