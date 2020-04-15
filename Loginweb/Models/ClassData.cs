using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace Loginweb.Models
{
    public class ClassData
    {
        public List<tablefood> alltablefoods { get; set; }
        public List<staff> allstaffs { get; set; }

        public List<food> allfoods { get; set; }

        public List<bill> allbills { get; set; }

        public List<billinfo> allbillinfos { get; set; }


        public staff addstaff { get; set; }
        public IEnumerable<staff> pagestaff(int page, int pagesize)
        {
            QuanLyCafeEntities3 db = new QuanLyCafeEntities3();
            return db.staffs.ToList().ToPagedList(page,pagesize);
        }
        public List<string> listpathimg { set; get; }

    }
}