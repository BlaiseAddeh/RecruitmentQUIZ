using RecruitmentQUIZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentQUIZ.Repositories
{
	public class ReponseEntityFrameworkRepo : IReponseQuest
	{
		RecruitQuizDBEntities _db;
		public ReponseEntityFrameworkRepo()
		{
			_db = new RecruitQuizDBEntities();
		}

		public void AjouterReponse(Reponse reponse)
		{
			_db.Reponses.Add(reponse);
			_db.SaveChanges();
		}

		public IEnumerable<Reponse> GetAllResponseForQuestionID(int QuestionID)
		{
			return _db.Reponses.Where(x => x.QuestionID == QuestionID).ToList();
		}

		public void AddUpdateReponse(string questionID, List<string> LstLibelleOptionReponse)
		{
			List<Reponse> LstRepo = _db.Reponses.Where(x => x.QuestionID.ToString() == questionID).ToList();
			foreach (Reponse item in LstRepo)
			{
				_db.Reponses.Remove(item);
				_db.SaveChanges();
			}	

			foreach (string item in LstLibelleOptionReponse)
			{
				Reponse rep = new Reponse();
				rep.Libelle = item.Trim();
				rep.NbrePoints = 1 / (float.Parse(LstLibelleOptionReponse.Count().ToString()));
				rep.QuestionID = int.Parse(questionID);
				_db.Reponses.Add(rep);
			}

			_db.SaveChanges();
			
		}

		public Reponse GetReponse(string reponseId)
		{
			return _db.Reponses.FirstOrDefault(x => x.ReponseID.ToString() == reponseId);
		}

		public bool VerifierSiReponseCorrect(string questionID, string optionID)
		{
			IEnumerable<Reponse> LstRep = GetAllResponseForQuestionID(int.Parse(questionID));

			OptionReponse OpRep = _db.OptionReponses.FirstOrDefault(x => x.OptionReponseID.ToString() == optionID);

			if (LstRep.Where(x =>x.Libelle == OpRep.Libelle).Count() >  0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public float RetournerValeurReponseCorrecte(string questionID, string optionID)
		{
			IEnumerable<Reponse> LstRep = GetAllResponseForQuestionID(int.Parse(questionID));
			OptionReponse OpRep = _db.OptionReponses.FirstOrDefault(x => x.OptionReponseID.ToString() == optionID);
			Reponse rep = LstRep.First(x => x.Libelle == OpRep.Libelle);
			return rep.NbrePoints;
		}

		public IEnumerable<Reponse> GetReponsesByQuestionID(string questionID)
		{
			return _db.Reponses.Where(x => x.QuestionID.ToString() == questionID);
		}
	}
}