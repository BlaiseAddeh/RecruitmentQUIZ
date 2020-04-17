using RecruitmentQUIZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentQUIZ.Repositories
{
	public interface IQuestion
	{
		void AjouterQuestion(Question question);
		Question GetQuestion(int QuestionID);
		void UpdateQuestion(Question question);
		Projet GetProjetByQuestionID(int QuestionID);
		void SupprimerQuestion(Question question);
		IEnumerable<Question> GetAllQuestionsForProjetID(string projetID);
	}
}
