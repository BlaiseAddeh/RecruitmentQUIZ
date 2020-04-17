using RecruitmentQUIZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentQUIZ.Repositories
{
	public class QuestionEntityFrameworkRepo : IQuestion
	{
		RecruitQuizDBEntities _db;
		public QuestionEntityFrameworkRepo()
		{
			_db = new RecruitQuizDBEntities();
		}

		public void AjouterQuestion(Question question)
		{
			_db.Questions.Add(question);
			_db.SaveChanges();
		}

		public IEnumerable<Question> GetAllQuestionsForProjetID(string projetID)
		{
			return _db.Questions.Where(x => x.ProjetID.ToString()== projetID).ToList();
		}

		public Projet GetProjetByQuestionID(int QuestionID)
		{ 
			Question quest = GetQuestion(QuestionID);
			return _db.Projets.FirstOrDefault(x =>x.ProjectID ==quest.ProjetID);
		}

		public Question GetQuestion(int QuestionID)
		{
			return _db.Questions.FirstOrDefault(x => x.QuestionID == QuestionID);
		}

		public void UpdateQuestion(Question question)
		{
			Question myquest = GetQuestion(question.QuestionID);
			_db.Questions.Attach(myquest);
			myquest.Libelle = question.Libelle;
			myquest.EstMultiChoix = question.EstMultiChoix;
			_db.SaveChanges();
		}

		public void SupprimerQuestion(Question question)
		{
			_db.Questions.Remove(question);
			_db.SaveChanges();
		}
	}
}