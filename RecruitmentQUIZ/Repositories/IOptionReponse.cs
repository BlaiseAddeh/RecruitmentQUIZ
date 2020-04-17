using RecruitmentQUIZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentQUIZ.Repositories
{
	public interface IOptionReponse
	{
		void AjouterOptionReponse(OptionReponse optionReponse);
		OptionReponse GetOptionReponse(int OptionReponseID);
		void UpdateOptionReponse(OptionReponse optionReponse);
		void SupprimerOptionReponse(OptionReponse optionReponse);
		Question GetQuestionByOptionReponseID(int OptionReponseID);
		IEnumerable<OptionReponse> GetAllOptionReponseByQuestionID(string id);		
	}
}
