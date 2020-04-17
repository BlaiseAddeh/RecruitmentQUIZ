using RecruitmentQUIZ.Models;
using RecruitmentQUIZ.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentQUIZ.Controllers
{
    public class LoginController : Controller
    {
        IUser iuser = new UserEntityFrameworkRepo();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User users)
        {
            if (iuser.CheckNonEmptyUserInfo(users))
            {
                User myUser = iuser.GetUser(users.Login, users.Password);
                if (myUser !=null)
                {                   
                    if (myUser.Resultat != null)
                    {
                        return RedirectToAction("DejaJouer", "Login");
                    }
                    Session["user"] = myUser;                   
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }  
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DejaJouer()
        {            
            return View();
        }

    }
}