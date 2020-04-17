using RecruitmentQUIZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentQUIZ.Repositories
{
	public class OptionReponseEntityFrameworkRepo : IOptionReponse
	{
		RecruitQuizDBEntities _db;
		public OptionReponseEntityFrameworkRepo()
		{
			_db = new RecruitQuizDBEntities();
		}

		public void AjouterOptionReponse(OptionReponse optionReponse)
		{
			_db.OptionReponses.Add(optionReponse);
			_db.SaveChanges();
		}


		public IEnumerable<OptionReponse> GetAllOptionReponseByQuestionID(string id)
		{
			return _db.OptionReponses.Where(x => x.QuestionID.ToString() == id).ToList();
		}				

		public OptionReponse GetOptionReponse(int OptionReponseID)
		{
			return _db.OptionReponses.FirstOrDefault(x => x.OptionReponseID == OptionReponseID);
		}

		public Question GetQuestionByOptionReponseID(int OptionReponseID)
		{
			OptionReponse optionRep = GetOptionReponse(OptionReponseID);
			return _db.Questions.FirstOrDefault(x => x.QuestionID == optionRep.QuestionID);
		}

		public void SupprimerOptionReponse(OptionReponse optionReponse)
		{
			_db.OptionReponses.Remove(optionReponse);
			_db.SaveChanges();
		}

		
		public void UpdateOptionReponse(OptionReponse optionReponse)
		{
		     OptionReponse  myoptrep = GetOptionReponse(optionReponse.OptionReponseID);
			_db.OptionReponses.Attach(myoptrep);
			myoptrep.Libelle = optionReponse.Libelle;
			_db.SaveChanges();
		}
				
	}
}