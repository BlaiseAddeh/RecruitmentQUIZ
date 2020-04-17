using RecruitmentQUIZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentQUIZ.Repositories
{
	public interface IReponseQuest
	{
		void AjouterReponse(Reponse reponse);

		Reponse GetReponse(string reponseId);

		IEnumerable<Reponse> GetReponsesByQuestionID(string questionID);
		IEnumerable<Reponse> GetAllResponseForQuestionID(int QuestionID);
		void AddUpdateReponse(string questionID, List<string> LstLibelleOptionReponse);
		bool VerifierSiReponseCorrect(string questionID, string optionID);
		float RetournerValeurReponseCorrecte(string questionID, string optionID);
	}
}
