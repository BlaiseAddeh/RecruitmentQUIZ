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
    public class ProjetController : Controller
    {
        IProjet iprojet = new ProjetEntityFrameworkRepo();
        // GET: Projet
        public ActionResult Index()
        {
            ProjetsViewModel model = new ProjetsViewModel();
            model.Projets = iprojet.GetAllProjets();
            model.SelectedProjet = null;
            return View(model);
        }

        [HttpPost]
        public ActionResult New()
        {
            ProjetsViewModel model = new ProjetsViewModel();
            model.Projets = iprojet.GetAllProjets();
            model.SelectedProjet = null;
            model.DisplayMode = "WriteOnly";
            return View("Index", model);
        }


        [HttpPost]
        public ActionResult Insert(Projet obj)
        {          
              iprojet.AjouterProjet(obj);               
              ProjetsViewModel model = new ProjetsViewModel();
              model.Projets = iprojet.GetAllProjets();
              model.SelectedProjet = iprojet.GetProjet(obj.ProjectID);
              model.DisplayMode = "ReadOnly";
              return View("Index", model);            
        }


        [HttpPost]
        public ActionResult Select(string id)
        {           
            ProjetsViewModel model = new ProjetsViewModel();
            model.Projets = iprojet.GetAllProjets();
            model.SelectedProjet = iprojet.GetProjet(int.Parse(id));
            model.DisplayMode = "ReadOnly";
            return View("Index", model);
            
        }

        [HttpPost]
        public ActionResult Edit(string id)
        {
            ProjetsViewModel model = new ProjetsViewModel();
            model.Projets = iprojet.GetAllProjets();
            model.SelectedProjet = iprojet.GetProjet(int.Parse(id));
            model.DisplayMode = "ReadWrite";
            return View("Index", model);            
        }

        [HttpPost]
        public ActionResult Update(Projet obj)
        {
            Projet existing = iprojet.GetProjet(obj.ProjectID);
            iprojet.UpdateProjet(obj);
            ProjetsViewModel model = new ProjetsViewModel();
            model.Projets = iprojet.GetAllProjets();

            model.SelectedProjet = existing;
            model.DisplayMode = "ReadOnly";
            return View("Index", model);            
        }


        [HttpPost]
        public ActionResult Delete(string id)
        {
            iprojet.SupprimerProjet(iprojet.GetProjet(int.Parse(id)));
            ProjetsViewModel model = new ProjetsViewModel();
            model.Projets = iprojet.GetAllProjets();

            model.SelectedProjet = null;
            model.DisplayMode = "";
            return View("Index", model);
            
        }       
    }
}