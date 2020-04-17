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
    public class OptionReponseController : Controller
    {
        IOptionReponse ioptionReponse = new OptionReponseEntityFrameworkRepo();
        IQuestion  iquestion = new QuestionEntityFrameworkRepo();
        IReponseQuest ireponseQuest = new ReponseEntityFrameworkRepo();

        // GET: OptionReponse
        public ActionResult Index(string id)
        {
            Session["questionid"] = id;
            QuestionDetailsViewModel model = new QuestionDetailsViewModel();
            Question myQuestion = new Question();
            myQuestion = iquestion.GetQuestion(int.Parse(id));
            model.LaQuestion = myQuestion;
            model.OptionReponses = myQuestion.OptionReponses;
            model.SelectedOptionReponse = null;
            return View(model);
        }

        [HttpPost]
        public ActionResult New(string id)
        {
            QuestionDetailsViewModel model = new QuestionDetailsViewModel();
            Question myQuestion = new Question();
            myQuestion = iquestion.GetQuestion(int.Parse(id));
            model.LaQuestion = myQuestion;
            model.OptionReponses = myQuestion.OptionReponses;
            model.SelectedOptionReponse = null;
            model.DisplayMode = "WriteOnly";
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Insert(OptionReponse obj)
        {
            obj.QuestionID = int.Parse(Session["questionid"].ToString());
            ioptionReponse.AjouterOptionReponse(obj);
            QuestionDetailsViewModel model = new QuestionDetailsViewModel();
            Question myQuestion = ioptionReponse.GetQuestionByOptionReponseID(obj.OptionReponseID);
            model.LaQuestion = myQuestion;
            model.OptionReponses = myQuestion.OptionReponses.ToList();
            model.SelectedOptionReponse = obj;
            model.DisplayMode = "ReadOnly";
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Select(string id)
        {
            QuestionDetailsViewModel model = new QuestionDetailsViewModel();
            Question myQuestion = ioptionReponse.GetQuestionByOptionReponseID(int.Parse(id));
            model.LaQuestion = myQuestion;
            model.OptionReponses = myQuestion.OptionReponses.ToList();
            model.SelectedOptionReponse = myQuestion.OptionReponses.ToList().FirstOrDefault(x => x.OptionReponseID.ToString() == id);
            model.DisplayMode = "ReadOnly";
            return View("Index", model);

        }

        [HttpPost]
        public ActionResult Edit(string id)
        {
            QuestionDetailsViewModel model = new QuestionDetailsViewModel();
            Question myQuestion  = ioptionReponse.GetQuestionByOptionReponseID(int.Parse(id));
            model.LaQuestion = myQuestion;
            model.OptionReponses = myQuestion.OptionReponses.ToList();
            model.SelectedOptionReponse = myQuestion.OptionReponses.ToList().FirstOrDefault(x => x.OptionReponseID.ToString() == id);
            model.DisplayMode = "ReadWrite";
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Update(OptionReponse obj)
        {
            OptionReponse existing = ioptionReponse.GetOptionReponse(obj.OptionReponseID);
            ioptionReponse.UpdateOptionReponse(obj);
            QuestionDetailsViewModel model = new QuestionDetailsViewModel();
            Question myQuestion = ioptionReponse.GetQuestionByOptionReponseID(obj.OptionReponseID);
            model.LaQuestion = myQuestion;
            model.OptionReponses = myQuestion.OptionReponses.ToList();
            model.SelectedOptionReponse = existing;
            model.DisplayMode = "ReadOnly";
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Desactiver(string id)
        {
            // A IMPLEMENTER
            QuestionDetailsViewModel model = new QuestionDetailsViewModel();
            model.LaQuestion = ioptionReponse.GetQuestionByOptionReponseID(int.Parse(id));
            model.SelectedOptionReponse = null;
            model.DisplayMode = "";
            return View("Index", model);
        }

        public ActionResult Reponse(string id)
        {
            Question quest = iquestion.GetQuestion(int.Parse(id));
            return View(quest);
        }

        public JsonResult SaveQuestionAnswer(QuestionResponse questionResponse)
        {
            List<string> LstLibelleOptionReponse  = new List<string>(); 

            Question myQuest = iquestion.GetQuestion(int.Parse(questionResponse.QuestionID));
            if (!myQuest.EstMultiChoix)
            {
                string lblresponse = ioptionReponse.GetOptionReponse(int.Parse(questionResponse.LstOptionId)).Libelle;
                LstLibelleOptionReponse.Add(lblresponse);
            }
            else
            { 
                string[] tbOptID = questionResponse.LstOptionId.Split('_');
                for (int i = 0; i < tbOptID.Length; i++)
                {
                    if (!string.IsNullOrEmpty(tbOptID[i]))
                    {
                        LstLibelleOptionReponse.Add(ioptionReponse.GetOptionReponse(int.Parse(tbOptID[i])).Libelle);
                    }
                }
            }

            ireponseQuest.AddUpdateReponse(questionResponse.QuestionID, LstLibelleOptionReponse);

            return Json(data: new { message = "Data Successfully Added.", success = true }, JsonRequestBehavior.AllowGet);
        }

        public class QuestionResponse
        {
            public string QuestionID { get; set; }
            public string LstOptionId { get; set; }
        }

    }
}