using Loginweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loginweb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Result(Loginweb.Models.account usermodel)
        {
            using(QuanLyCafe1Entities2 db = new QuanLyCafe1Entities2())
            {
                var userdetail = db.accounts.Where(x => x.id == usermodel.id && x.password == usermodel.password).FirstOrDefault();
                if (userdetail == null)
                {
                    usermodel.LoginErrorMessage = "Wrong username or password.";
                    return View("Index", usermodel);
                }
                else
                {
                    Session["type"] = userdetail.type;
                    Session["idaccount"] = userdetail.id;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}