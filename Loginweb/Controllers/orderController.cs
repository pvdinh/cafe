using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Loginweb.Models;

namespace Loginweb.Controllers
{
    public class orderController : Controller
    {
        QuanLyCafeEntities3 db = new QuanLyCafeEntities3();
        ClassData data = new ClassData();
        // GET: order
        public ActionResult Index(int id)
        {
            Session["id"] = id;

            //get list product
            data.listpathimg = new List<string>();
            data.listpathimg = setpathimg();

            //get info table when click
            tablefood x = new tablefood();
            data.alltablefoods = new List<tablefood>();
            x = db.tablefoods.ToList().Where(s => s.id == id).FirstOrDefault();
            data.alltablefoods.Add(x);

            //    var y = db.foods.ToList().Where(s => s.id == id).FirstOrDefault();
            //    ViewBag.foodinfo = new List<food>()
            //    {
            //        new food()
            //    {
            //        id = y.id,
            //        name = y.name,
            //        price = y.price
            //    },new food()
            //    {
            //        id = 1,
            //        name = "aa",
            //        price = 111
            //    },
            //};
            data.allfoods = db.foods.ToList();
            return View(data);
        }

        ClassData listfood = new ClassData();

        public ActionResult cart()
        {
            using (QuanLyCafeEntities3 dbb = new QuanLyCafeEntities3())
            {
                listfood.allbillinfos = dbb.billinfoes.ToList();
                listfood.allfoods = new List<food>();
                foreach (billinfo item in listfood.allbillinfos)
                {
                    var x = dbb.foods.ToList().Where(s => s.id == item.idfood).FirstOrDefault();
                    listfood.allfoods.Add(x);
                }
            }
            return PartialView("/views/shares/_cartproduct.cshtml", listfood);
        }
        public ActionResult cartt(int id)
        {
            using (QuanLyCafeEntities3 dbb = new QuanLyCafeEntities3())
            {
                var x = dbb.billinfoes.ToList().Where(s => s.idfood == id).FirstOrDefault();
                if (x == null)
                {
                    billinfo y = new billinfo();
                    y.idbill = 1;
                    y.idfood = id;
                    y.count = 1;
                    dbb.billinfoes.Add(y);
                    dbb.SaveChanges();
                }
                else
                {
                    x.count++;
                    dbb.SaveChanges();
                }
            }
            using (QuanLyCafeEntities3 dbb = new QuanLyCafeEntities3())
            {
                listfood.allbillinfos = dbb.billinfoes.ToList();
                listfood.allfoods = new List<food>();
                foreach (billinfo item in listfood.allbillinfos)
                {
                    var x = dbb.foods.ToList().Where(s => s.id == item.idfood).FirstOrDefault();
                    listfood.allfoods.Add(x);
                }
            }
            //return PartialView("/views/shares/_cartproduct.cshtml", listfood);
            return RedirectToAction("Index", "order", new { id = Session["id"] });
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

        public ActionResult delproduct(int id)
        {
            using (QuanLyCafeEntities3 dbb = new QuanLyCafeEntities3())
            {
                billinfo x = new billinfo();
                x = dbb.billinfoes.ToList().Find(s => s.idfood == id);
                dbb.billinfoes.Remove(x);
                dbb.SaveChanges();
            }

            using (QuanLyCafeEntities3 dbb = new QuanLyCafeEntities3())
            {
                listfood.allbillinfos = dbb.billinfoes.ToList();
                listfood.allfoods = new List<food>();
                foreach (billinfo item in listfood.allbillinfos)
                {
                    var x = dbb.foods.ToList().Where(s => s.id == item.idfood).FirstOrDefault();
                    listfood.allfoods.Add(x);
                }
            }

            return PartialView("/views/shares/_cartproduct.cshtml", listfood);
        }
    }
}