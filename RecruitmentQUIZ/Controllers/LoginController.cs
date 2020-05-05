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
        IResultat iresultat = new ResultatEntityFrameworkRepo();

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
                    if (myUser.SecurityLevel.Libelle.Equals("Admin", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return RedirectToAction("IndexAdmin", "Home");
                    }
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
            var us = Session["user"] as User;
            var user = iuser.GetUser(us.Login, us.Password);

            if (user.Resultat == null && Session["Load"] != null)
            {
                float TotalReponseChoix = 0;
                Resultat resultat = new Resultat();
                resultat.TotalPoint = 0;
                resultat.TotalObtenu = TotalReponseChoix;
                resultat.ProjetID = user.ProjetID;
                resultat.ExamQuestions = "LOGOUT";
                resultat.ExamReponseOptions = "LOGOUT";

                iresultat.AjouterResultat(user.UserID, resultat);
            }

            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DejaJouer()
        {            
            return View();
        }

    }
}