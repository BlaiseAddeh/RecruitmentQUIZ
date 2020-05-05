using RecruitmentQUIZ.Models;
using RecruitmentQUIZ.Repositories;
using RecruitmentQUIZ.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentQUIZ.Controllers
{
    public class QuestionController : Controller
    {
        IQuestion iquestion = new QuestionEntityFrameworkRepo();
        IProjet iprojet = new ProjetEntityFrameworkRepo();
        // GET: Question
        public ActionResult Index(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            Session["projetid"] = id;
            ProjetDetailsViewModel model = new ProjetDetailsViewModel();
            Projet myProjet = new Projet();
            myProjet = iprojet.GetProjet(int.Parse(id));
            model.LeProjet = myProjet;
            model.Questions = myProjet.Questions;
            model.SelectedQuestion = null;
            return View(model);
        }


        [HttpPost]
        public ActionResult New(string id)
        {
            ProjetDetailsViewModel model = new ProjetDetailsViewModel();
            Projet myProjet = new Projet();
            myProjet = iprojet.GetProjet(int.Parse(id));
            model.LeProjet = myProjet;
            model.Questions = myProjet.Questions;
            model.SelectedQuestion = null;
            model.DisplayMode = "WriteOnly";
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Insert(Question obj)
        {
            obj.ProjetID = int.Parse(Session["projetid"].ToString());
            iquestion.AjouterQuestion(obj);
            ProjetDetailsViewModel model = new ProjetDetailsViewModel();            
            Projet myProjet = iquestion.GetProjetByQuestionID(obj.QuestionID);
            model.LeProjet = myProjet;
            model.Questions = myProjet.Questions.ToList();
            model.SelectedQuestion = obj;
            model.DisplayMode = "ReadOnly";
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Select(string id)
        {
            ProjetDetailsViewModel model = new ProjetDetailsViewModel();
            Projet myProjet = iquestion.GetProjetByQuestionID(int.Parse(id));
            model.LeProjet = myProjet;
            model.Questions = myProjet.Questions.ToList();
            model.SelectedQuestion = myProjet.Questions.ToList().FirstOrDefault(x =>x.QuestionID.ToString()== id);
            model.DisplayMode = "ReadOnly";
            return View("Index", model);

        }

        [HttpPost]
        public ActionResult Edit(string id)
        {
            ProjetDetailsViewModel model = new ProjetDetailsViewModel();
            Projet myProjet = iquestion.GetProjetByQuestionID(int.Parse(id));
            model.LeProjet = myProjet;
            model.Questions = myProjet.Questions.ToList();
            model.SelectedQuestion = myProjet.Questions.ToList().FirstOrDefault(x => x.QuestionID.ToString() == id);
            model.DisplayMode = "ReadWrite";
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Update(Question obj)
        {
            Question existing = iquestion.GetQuestion(obj.QuestionID);
            iquestion.UpdateQuestion(obj);
            ProjetDetailsViewModel model = new ProjetDetailsViewModel();
            Projet myProjet = iquestion.GetProjetByQuestionID(obj.QuestionID);
            model.LeProjet = myProjet;
            model.Questions = myProjet.Questions.ToList();
            model.SelectedQuestion = existing;
            model.DisplayMode = "ReadOnly";
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Desactiver(string id)
        {
           // A IMPLEMENTER

            ProjetDetailsViewModel model = new ProjetDetailsViewModel();
            model.LeProjet = iquestion.GetProjetByQuestionID(int.Parse(id));
            model.SelectedQuestion = null;
            model.DisplayMode = "";
            return View("Index", model);

        }
              
       
    }
}