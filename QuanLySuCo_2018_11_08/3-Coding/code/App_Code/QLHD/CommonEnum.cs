using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace QLHD
{
    /// <summary>
    /// Class contain common enums in project
    /// </summary>
    public class CommonEnum
    {
        public enum FileExtension
        {
            Doc,
            Docx,
            Xls,
            Xlsx,
            Pdf,
            Rar,
            Zip,
            Jpg,
            Jpeg,
            Png
        }

        public enum TapTinObjectLoai
        {
            BieuMau
        }

        
        public enum NhomThoiGian { tuan, thang, quy, nam };

    }
}