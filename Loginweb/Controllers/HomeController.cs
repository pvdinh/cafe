using Loginweb.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Loginweb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        QuanLyCafeEntities3 db = new QuanLyCafeEntities3();
        ClassData data = new ClassData();
        private bool position_deletestaff;
        public ActionResult Index(int? id)
        {
            if (id != null && db.tablefoods.ToList().Where(s => s.id == id && string.Compare(s.status, "trống", true) == 0).FirstOrDefault() != null)
            {
                db.customer_table(id);
            }
            using (QuanLyCafeEntities3 dbb = new QuanLyCafeEntities3())
            {
                data.alltablefoods = dbb.tablefoods.SqlQuery("select * from tablefood").ToList();
            }
            return View(data);
        }

        public ActionResult product()
        {
            data.listpathimg = setpathimg();
            data.allfoods = db.foods.ToList();
            return View(data);
        }

        public ActionResult setting(int page=1, int pagesize =4)
        {
            List<staff> allstaff = new List<staff>();
            allstaff = db.staffs.ToList();
            ViewBag.count = allstaff[allstaff.Count() - 1].idaccount + 1;
            var model = new ClassData();
            var pagelist= model.pagestaff(page,pagesize);
            return View(pagelist);
        }

        //public ActionResult page(int? page)
        //{
        //    data.allstaffs = db.staffs.ToList();
        //    int pagesize = 4;
        //    int pagenumber = (page ?? 1);
        //    return PartialView("/Views/Shares/PageList.cshtml", data.allstaffs.ToPagedList(pagenumber, pagesize));
        //}
        public ActionResult seletestaff(int id)
        {

            using (QuanLyCafeEntities3 db = new QuanLyCafeEntities3())
            {
                try
                {
                    data.allstaffs = db.staffs.ToList();
                    if (data.allstaffs[data.allstaffs.Count() -1].idaccount == id)
                    {
                        position_deletestaff = true;
                        db.deletestaffaccount(id);
                    }
                    else
                    { 
                        position_deletestaff = false;
                        db.deletestaffaccount(id);
                    }
                }
                catch
                {
                    return RedirectToAction("setting", "Home");
                }
            }
            return RedirectToAction("setting", "Home");
        }

        public ActionResult editstaff(int id)
        {
            staff x = new staff();
            ClassData edit = new ClassData();
            edit.allstaffs = new List<staff>();
            x = db.staffs.ToList().Where(s => s.idstaff == id).FirstOrDefault();
            edit.allstaffs.Add(x);
            return View(edit);
        }

        [HttpPost]
        public ActionResult savestaff(staff sstaff)
        {

            using (QuanLyCafeEntities3 db = new QuanLyCafeEntities3())
            {
                var nv = db.staffs.Find(sstaff.idstaff);
                nv.name = sstaff.name;
                nv.email = sstaff.email;
                nv.position = sstaff.position;
                nv.status = sstaff.status;
                nv.idaccount = sstaff.idaccount;
                db.SaveChanges();
            }

            return RedirectToAction("setting", "Home");
        }

        [HttpPost]
        public ActionResult addstaff(staff astaff)
        {
            using (QuanLyCafeEntities3 db = new QuanLyCafeEntities3())
            {
                data.allstaffs = db.staffs.ToList();
                int idaccount = data.allstaffs[data.allstaffs.Count()-1].idaccount + 1;
                string username = "user" + idaccount.ToString();
                string password = "12345678";
                string type = "staff";
                if (position_deletestaff == true)
                {
                    db.Addstaffaccountlatest(astaff.name, astaff.status, astaff.position, astaff.email, idaccount, username, password, type);
                }
                else
                {
                    db.Addstaffaccountlatest(astaff.name, astaff.status, astaff.position, astaff.email, idaccount, username, password, type);
                }
            }
            return RedirectToAction("setting", "Home");
        }

        public List<string> setpathimg()
        {
            List<string> path = new List<string>();
            path.Add("/assets/icon/bạc sỉu.jpg");
            path.Add("/assets/icon/americano_large.jpg");
            path.Add("/assets/icon/capuchino.jpg");
            path.Add("/assets/icon/caramel machiato.jpg");
            path.Add("/assets/icon/cold brew cam sả.jpg");
            path.Add("/assets/icon/cold brew phúc bồn tử.jpg");
            path.Add("/assets/icon/cold brew sữa tươi macchiato.jpg");
            path.Add("/assets/icon/cold brew sữa tươi.jpg");
            path.Add("/assets/icon/cold brew truyền thống.jpg");
            path.Add("/assets/icon/cà phê sữa.jpg");
            path.Add("/assets/icon/cà phê đen.jpg");
            path.Add("/assets/icon/espresso.jpg");
            return path;
        }
    }
}