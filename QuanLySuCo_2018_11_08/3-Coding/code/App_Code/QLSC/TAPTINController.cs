using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Chứa các phương thức tương tác với table PSW_NHOMHANGHOA
/// </summary>
namespace QLSC
{
    public class TAPTINController
    {
        QLSCDataContext context = new QLSCDataContext();

        public TAPTINController()
        {}

        public QLSC_TAPTIN Get_TapTin(int ID)
        {
            var obj = (from tt in context.QLSC_TAPTINs
                       where tt.FILE_ID == ID
                       select tt).FirstOrDefault();

            return obj;
        }
        public List<QLSC_TAPTIN> Get_TapTin_By_ObjectID(int ID)
        {
            var obj = (from tt in context.QLSC_TAPTINs
                       where tt.OBJECT_ID == ID
                       select tt).ToList();

            return obj;
        }
        public QLSC_TAPTIN Get_TapTin_By_ObjectID_BieuMau(int ID)
        {
            var obj = (from tt in context.QLSC_TAPTINs
                       where tt.OBJECT_ID == ID
                       select tt).FirstOrDefault();

            return obj;
        }
        public List<QLSC_TAPTIN> Get_TapTin_By_ObjectID_LoaiID(int ID, int Loai)
        {
            var obj = (from tt in context.QLSC_TAPTINs
                       where tt.OBJECT_ID == ID && tt.OBJECT_LOAI == Loai
                       select tt).ToList();

            return obj;
        }
        public void ThemTapTin(QLSC_TAPTIN objTapTin)
        {
            context.QLSC_TAPTINs.InsertOnSubmit(objTapTin);
            context.SubmitChanges();
        }

        public void CapNhatTapTin(QLSC_TAPTIN objTapTin)
        {
            var obj = Get_TapTin(objTapTin.FILE_ID);
            obj.FILE_NAME = objTapTin.FILE_NAME;
            obj.FILE_MOTA = objTapTin.FILE_MOTA;
            obj.FILE_EXT = objTapTin.FILE_EXT;
            obj.FILE_NGAYCAPNHAT = objTapTin.FILE_NGAYCAPNHAT;
            obj.FILE_SIZE = objTapTin.FILE_SIZE;
            obj.FILE_USERID_CAPNHAT = objTapTin.FILE_USERID_CAPNHAT;
            obj.OBJECT_ID = objTapTin.OBJECT_ID;
            obj.OBJECT_LOAI = objTapTin.OBJECT_LOAI;

            context.SubmitChanges();
        }

        public void XOA_TAPTIN(int ID)
        {
            var it = (from p in context.QLSC_TAPTINs
                      where p.FILE_ID == ID
                      select p).Single();
            context.QLSC_TAPTINs.DeleteOnSubmit(it);
            context.SubmitChanges();
        }
    }
}