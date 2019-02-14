using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace QLHD
{
    /// <summary>
    /// Chứa các hàm hỗ trợ cho việc xử lý ngày giờ
    /// </summary>
    public class DateTimeUtils
    {

        /// <summary>
        /// Chia khoảng thời gian từ ngày đến ngày thành các nhóm
        /// </summary>
        /// <param name="tuNgay"></param>
        /// <param name="denNgay"></param>
        /// <param name="loaiThoiGian"></param>
        /// <returns>Datatable với 4 cột: Key, TieuDe, TuNgay, DenNgay</returns>
        public static DataTable ChiaThoiGian(DateTime tuNgay, DateTime denNgay, CommonEnum.NhomThoiGian nhomThoiGian)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Key");
            dt.Columns.Add("TieuDe");
            dt.Columns.Add("TuNgay");
            dt.Columns.Add("DenNgay");
            switch (nhomThoiGian)
            {
                case CommonEnum.NhomThoiGian.tuan:
                    // Tìm danh sách tuần, ds này luôn có ngày bđ là thứ 2, ngày kt là chủ nhật
                    for (DateTime date = tuNgay.FirstDayOfWeek(DayOfWeek.Monday); date <= denNgay; date = date.AddDays(7))
                    {
                        DataRow row = dt.NewRow();
                        row["Key"] = string.Format("Tuan{0}", date.ToString(ClassParameter.dateFormat));
                        row["TieuDe"] = string.Format("Tu.{0}", date.ToString(ClassParameter.dateFormat));
                        row["TuNgay"] = date;
                        row["DenNgay"] = date.LastDayOfWeek(DayOfWeek.Monday);
                        dt.Rows.Add(row);
                    }
                    // Cập nhật lại ngày bđ của tuần đầu tiên và ngày kt của tuần cuối cùng trong danh sách
                    // cho đúng với mốc thời gian đang tìm kiếm
                    if (dt.Rows.Count > 0)
                    {
                        dt.Rows[0]["TuNgay"] = tuNgay;
                        dt.Rows[dt.Rows.Count - 1]["DenNgay"] = denNgay;
                    }
                    break;

                case CommonEnum.NhomThoiGian.thang:
                    // Tìm danh sách tháng, ds này luôn có ngày bđ là 1, ngày kt là ngày cuối tháng
                    for (DateTime date = tuNgay.FirstDayOfMonth(); date <= denNgay; date = date.AddMonths(1))
                    {
                        DataRow row = dt.NewRow();
                        row["Key"] = string.Format("Thang{0}Nam{1}", date.Month, date.Year);
                        row["TieuDe"] = string.Format("T.{0}/{1}", date.Month, date.Year);
                        row["TuNgay"] = date;
                        row["DenNgay"] = date.LastDayOfMonth();
                        dt.Rows.Add(row);
                    }
                    // Cập nhật lại ngày bđ của tháng đầu tiên và ngày kt của tháng cuối cùng trong danh sách
                    // cho đúng với mốc thời gian đang tìm kiếm
                    if (dt.Rows.Count > 0)
                    {
                        dt.Rows[0]["TuNgay"] = tuNgay;
                        dt.Rows[dt.Rows.Count - 1]["DenNgay"] = denNgay;
                    }
                    break;

                case CommonEnum.NhomThoiGian.quy:
                    // Tìm danh sách quý, ds này luôn có ngày bđ của quý là 1, ngày kt là ngày cuối quý
                    for (DateTime date = tuNgay.FirstDayOfQuarter(); date <= denNgay; date = date.AddMonths(3))
                    {
                        DataRow row = dt.NewRow();
                        row["Key"] = string.Format("Quy{0}Nam{1}", date.GetQuarter(), date.Year);
                        row["TieuDe"] = string.Format("Q.{0}/{1}", date.GetQuarter(), date.Year);
                        row["TuNgay"] = date;
                        row["DenNgay"] = date.LastDayOfQuarter();
                        dt.Rows.Add(row);
                    }
                    // Cập nhật lại ngày bđ của quý đầu tiên và ngày cuối của quý kt trong danh sách
                    // cho đúng với mốc thời gian đang tìm kiếm
                    if (dt.Rows.Count > 0)
                    {
                        dt.Rows[0]["TuNgay"] = tuNgay;
                        dt.Rows[dt.Rows.Count - 1]["DenNgay"] = denNgay;
                    }
                    break;

                default: // năm
                         // Tìm danh sách năm, ds này luôn có ngày bđ là 1/1, ngày kt là 31/12
                    for (DateTime date = tuNgay.FirstDayOfYear(); date <= denNgay; date = date.AddYears(1))
                    {
                        DataRow row = dt.NewRow();
                        row["Key"] = string.Format("Nam{0}", date.Year);
                        row["TieuDe"] = string.Format("{0}", date.Year);
                        row["TuNgay"] = date;
                        row["DenNgay"] = date.LastDayOfYear();
                        dt.Rows.Add(row);
                    }
                    // Cập nhật lại ngày bđ của năm đầu tiên và ngày kt của năm cuối cùng trong danh sách
                    // cho đúng với mốc thời gian đang tìm kiếm
                    if (dt.Rows.Count > 0)
                    {
                        dt.Rows[0]["TuNgay"] = tuNgay;
                        dt.Rows[dt.Rows.Count - 1]["DenNgay"] = denNgay;
                    }
                    break;
            }

            return dt;
        }

    }
}