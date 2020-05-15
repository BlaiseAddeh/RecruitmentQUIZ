using RecruitmentQUIZ.Models;
using RecruitmentQUIZ.Repositories;
using RecruitmentQUIZ.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
		IProjet iprojet = new ProjetEntityFrameworkRepo();
		IUtilityFonctions iutilityFonctions = new UtilityFonctionsRepo();


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
		
		
		public ActionResult IndexAdmin()
		{
			if (Session["user"] == null)
			{
				return RedirectToAction("Login", "Login");
			}
			
			return View();
		}

		public ActionResult Start()
		{
			var us = Session["user"] as User;

			if (!iuser.VerifierSiLeCandidatADemarrerExamen(us.UserID.ToString())) // Le candidat n'a jamais demarré la page des questions
			{

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

						iuser.MarquerDemarrageExamen(us.UserID.ToString());    // Marquer le demarrage de l'Exam.

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

					return RedirectToAction("EndSection", "Home");
				}


			}
			else      // Le cas où le candidat à au moins une fois demarré la page des questions
			{
					return RedirectToAction("NonRespectInstrction", "Home");
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

		public ActionResult NonRespectInstrction()
		{
			if (Session["user"] == null)
			{
				return RedirectToAction("Login", "Login");
			}
			else
			{
				ViewBag.Message = "Vous avez déjà demarré la page des questionnaires. Donc vous ne respectez pas les règles de l'examen. Désolé pour vous :(";


				return View();
			}
		}


		public ActionResult ShowResume(string ResultatID)
		{
			Resultat result = iresultat.GetResultat(ResultatID);
			return View(result);
		}

		
		public ActionResult Candidats()
		{
			if (Session["user"] == null)
			{
				return RedirectToAction("Login", "Login");
			}
			List<ProjetCandidat> LstProjetCandidats= new List<ProjetCandidat>();
			IEnumerable<Projet> LstProjets = iprojet.GetAllProjets();
			foreach(Projet proj in LstProjets)
			{
				ProjetCandidat projetCandidat = new ProjetCandidat();
				projetCandidat.LeProjet = proj;
				projetCandidat.Candidats = iprojet.GetCandidatsByProjet(proj.ProjectID);
				LstProjetCandidats.Add(projetCandidat);
			}

			return View(LstProjetCandidats);
		}

		public ActionResult ProjetCandidatDetails(int Id)
		{
			if (Session["user"] == null)
			{
				return RedirectToAction("Login", "Login");
			}
			TempData["projetID"] = Id;
			TempData.Keep("projetID");
			IEnumerable<User> LstUser = iprojet.GetCandidatsByProjet(Id);
			return PartialView("_ProjetCandidatDetails", LstUser);
		}

		
		public ActionResult LoadCandidatExcelImportFile(int Id)
		{
			if (Session["user"] == null)
			{
				return RedirectToAction("Login", "Login");
			}
			TempData["projetID"] = Id;
			TempData.Keep("projetID");			
			return PartialView("_LoadCandidatExcelImportFile");
		}

		public FileResult Download()
		{
			try
			{
				byte[] fileBytes = System.IO.File.ReadAllBytes(System.Configuration.ConfigurationManager.AppSettings["ModelFichier"]);
				string fileName = "ModelCandidats.xlsx";
				return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			
		}

		[HttpPost]
		public ActionResult ImportExcel()
		{
			try
			{
				if (Session["user"] == null)
				{
					return RedirectToAction("Login", "Login");
				}

				if (Request.Files["FileUpload1"].ContentLength > 0)
				{
					string extension = System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName).ToLower();
					string query = null;
					string connString = "";
					var projetID = TempData["projetID"];
					TempData.Keep("projetID");

					string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

					string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["FileUpload1"].FileName);
					if (!Directory.Exists(path1))
					{
						Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
					}
					if (validFileTypes.Contains(extension))
					{
						if (System.IO.File.Exists(path1))
						{ System.IO.File.Delete(path1); }
						Request.Files["FileUpload1"].SaveAs(path1);
						if (extension == ".csv")
						{
							DataTable dt = iutilityFonctions.ConvertCSVtoDataTable(path1);
							iuser.AddNewUserInfos(projetID.ToString(), dt);
						}
						//Connection String to Excel Workbook  
						else if (extension.Trim() == ".xls")
						{
							connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
							DataTable dt = iutilityFonctions.ConvertXSLXtoDataTable(path1, connString);
							iuser.AddNewUserInfos(projetID.ToString(), dt);
						}
						else if (extension.Trim() == ".xlsx")
						{
							connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";						
							DataTable dt = iutilityFonctions.ConvertXSLXtoDataTable(path1, connString);							
							iuser.AddNewUserInfos(projetID.ToString(), dt);	
						}
					}
					else
					{
						ViewBag.Error = "Please Upload Files in .xls, .xlsx or .csv format";
					}
				}
				return RedirectToAction("Candidats", "Home");
			}
			catch (Exception ex)
			{
				throw ex;
			}
			
		}


		public ActionResult UpdateCandidatInfos(int Id)
		{
			if (Session["user"] == null)
			{
				return RedirectToAction("Login", "Login");
			}			
			User myUser = iuser.GetUserByID(Id.ToString());
			return PartialView("_UpdateCandidatDetails", myUser);
		}

		
		
		public JsonResult EditCandidatInfos(string Userid, string Username, string Usergender, string Useremail, string Userphone, 
			string Userlangmat, string UserlangWork1, string UserlangWork2, string Userniveauetude)
		{			
			var projetID = TempData["projetID"];
			TempData.Keep("projetID");
			bool result =iuser.UpdateUserInfos(projetID.ToString(), Userid, Username, Usergender, Useremail, Userphone,
			Userlangmat, UserlangWork1, UserlangWork2, Userniveauetude);

			return Json(new { msg = result }, JsonRequestBehavior.AllowGet);

		}


		[HttpPost]
		public ActionResult ProjetCandidatDetailsSearch(string searchvalue)
		{
			if (Session["user"] == null)
			{
				return RedirectToAction("Login", "Login");
			}
			var projetID = TempData["projetID"];
			TempData.Keep("projetID");
			IEnumerable<User> LstUser =null;
			if (!string.IsNullOrEmpty(searchvalue))
			{
				LstUser = iprojet.SearchCandidatsInProject(int.Parse(projetID.ToString()), searchvalue);
			}
			else
			{
				LstUser = iprojet.GetCandidatsByProjet(int.Parse(projetID.ToString()));
			}
			
			return PartialView("_ProjetCandidatDetailsData", LstUser);
		}

		[HttpPost]
		public ActionResult DesactiverCandidat(string userid)
		{
			if (Session["user"] == null)
			{
				return RedirectToAction("Login", "Login");
			}
			var projetID = TempData["projetID"];
			TempData.Keep("projetID");
			IEnumerable<User> LstUser = null;
			if (!string.IsNullOrEmpty(userid))
			{
				LstUser = iuser.DesactiverUser(projetID.ToString(), userid);
			}
			else
			{
				LstUser = iprojet.GetCandidatsByProjet((int)projetID);
			}

			return PartialView("_ProjetCandidatDetailsData", LstUser);

		}
		

		[HttpPost]
		public ActionResult ConvoqueCandidatPourPratique(string userid)
		{
			if (Session["user"] == null)
			{
				return RedirectToAction("Login", "Login");
			}
			var projetID = TempData["projetID"];
			TempData.Keep("projetID");
			IEnumerable<User> LstUser = null;
			if (!string.IsNullOrEmpty(userid))
			{
				LstUser = iuser.ConvoquerUserPourPratique(projetID.ToString(), userid);
			}
			else
			{
				LstUser = iprojet.GetCandidatsByProjet((int)projetID);
			}

			return PartialView("_ProjetCandidatDetailsData", LstUser);

		}

		public ActionResult Resultats()
		{
			if (Session["user"] == null)
			{
				return RedirectToAction("Login", "Login");
			}

			return View();
		}


		public class ExamResponses
		{
			public string LstQuestionID { get; set; }
			public string LstMultiChoiceOptionId { get; set; }
			public string LstSingleChoiceOptionId { get; set; }
		}

	}
}