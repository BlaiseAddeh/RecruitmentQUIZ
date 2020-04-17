using RecruitmentQUIZ.Models;
using RecruitmentQUIZ.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentQUIZ.Controllers
{
	public class HomeController : Controller
	{
		IQuestion iquestion = new QuestionEntityFrameworkRepo();
		IReponseQuest ireponseQuest = new ReponseEntityFrameworkRepo();
		IResultat iresultat = new ResultatEntityFrameworkRepo();
		IUser iuser = new UserEntityFrameworkRepo();

		public ActionResult Index()
		{
			if (Session["user"]==null)
			{
				return RedirectToAction("Login", "Login");
			}
			else
			{
				var user = Session["user"] as User;
				if (user.Resultat !=null)
				{
					return RedirectToAction("DejaJouer", "Login");
				}
			}
			return View();
		}

		public ActionResult Start()
		{
			var us = Session["user"] as User;


			if (Session["Load"] == null)
			{
				if (Session["user"] == null)
				{
					return RedirectToAction("Login", "Login");
				}
				else
				{
					Session["Load"] = "OK";
					//shuffle
					var rnd = new Random();
				
					IEnumerable<Question> LstQuest = iquestion.GetAllQuestionsForProjetID(us.ProjetID.ToString()).OrderBy(item => rnd.Next()).OrderBy(item => rnd.Next()).Take(int.Parse(System.Configuration.ConfigurationManager.AppSettings["NbreQuestion"]));
					return View(LstQuest);
				}
			}
			else
			{
				var user = iuser.GetUser(us.Login, us.Password);

				if (user.Resultat == null && Session["Load"] != null)
				{
					float TotalReponseChoix = 0;
					Resultat resultat = new Resultat();
					resultat.TotalPoint = 0;
					resultat.TotalObtenu = TotalReponseChoix;
					resultat.ProjetID = user.ProjetID;
					resultat.ExamQuestions = "RELOAD";
					resultat.ExamReponseOptions = "RELOAD";

					iresultat.AjouterResultat(user.UserID, resultat);
				}			

				return RedirectToAction("EndSection","Home");
			}
					
		}

		public JsonResult SaveExam(ExamResponses questionResponse)
		{
			var userTemp = Session["user"] as User;
			var user = iuser.GetUser(userTemp.Login, userTemp.Password);			
			if (user.Resultat != null)
			{				
				return Json(data: new { message = "Désolé vous avez déjà été évaluer pour ce questionnaire !", success = false }, JsonRequestBehavior.AllowGet);
			}
			else
			{
				ExamResponses exrep = questionResponse;

				string[] tblQuestID = exrep.LstQuestionID.Split('£');
				float TotalExam = 0;

				for (int i = 0; i < tblQuestID.Length; i++)
				{
					if (!string.IsNullOrEmpty(tblQuestID[i]))
					{
						IEnumerable<Reponse> LstReponse = ireponseQuest.GetReponsesByQuestionID(tblQuestID[i].Trim());
						foreach (Reponse item in LstReponse)
						{
							TotalExam = TotalExam + item.NbrePoints;
						}
					}
				}

				float TotalReponseChoix = 0;
				TotalReponseChoix += CalculTotalReponse(exrep.LstMultiChoiceOptionId);
				TotalReponseChoix += CalculTotalReponse(exrep.LstSingleChoiceOptionId);
				Resultat resultat = new Resultat();
				resultat.TotalPoint = TotalExam;
				resultat.TotalObtenu = TotalReponseChoix;
				resultat.ProjetID = user.ProjetID;
				resultat.ExamQuestions = questionResponse.LstQuestionID;
				resultat.ExamReponseOptions = questionResponse.LstMultiChoiceOptionId + "~" + questionResponse.LstSingleChoiceOptionId;

				iresultat.AjouterResultat(user.UserID, resultat);

				return Json(data: new { message = resultat.ResultatID, success = true }, JsonRequestBehavior.AllowGet);
			}			
		}

		public ActionResult RedirectToDejaEvaluer()
		{
			return RedirectToAction("DejaJouer", "Login");
		}

		private float CalculTotalReponse(string strOpt)
		{
			float TotalReponseOptionChoix = 0;
			if (!string.IsNullOrEmpty(strOpt))
			{
				string[] tblQuestIDOptID = strOpt.Split('_');

				for (int i = 0; i < tblQuestIDOptID.Length; i++)
				{
					if (!string.IsNullOrEmpty(tblQuestIDOptID[i]))
					{
						string questid = tblQuestIDOptID[i].Split('#')[0];
						string optid = tblQuestIDOptID[i].Split('#')[1];
						if (ireponseQuest.VerifierSiReponseCorrect(questid, optid))
						{
							TotalReponseOptionChoix = ireponseQuest.RetournerValeurReponseCorrecte(questid, optid);
						}
					}
				}
			}

			return TotalReponseOptionChoix;
		}


		public ActionResult EndSection()
		{
			if (Session["user"] == null)
			{
				return RedirectToAction("Login", "Login");
			}
			else
			{
				ViewBag.Message = "Votre section pour cet examen est terminée pour cause de non respect d'instruction." +
					              " VOUS AVEZ RECHARGE LA PAGE DE TEST !.";

				return View();
			}
		}

		public ActionResult ShowResume(string ResultatID)
		{
			Resultat result = iresultat.GetResultat(ResultatID);
			return View(result);
		}

		/*
		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	  */

		public class ExamResponses
		{
			public string LstQuestionID { get; set; }
			public string LstMultiChoiceOptionId { get; set; }
			public string LstSingleChoiceOptionId { get; set; }
		}


	}
}