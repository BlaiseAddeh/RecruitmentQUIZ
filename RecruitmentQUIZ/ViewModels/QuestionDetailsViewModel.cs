using RecruitmentQUIZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentQUIZ.ViewModels
{
	public class QuestionDetailsViewModel
	{
		public Question LaQuestion { get; set; }
		public IEnumerable<OptionReponse> OptionReponses { get; set; }
		public OptionReponse SelectedOptionReponse { get; set; }
		public string DisplayMode { get; set; }

		public bool VerifierSiOptionEstReponseCorrecte(Question LaQuestion, OptionReponse optionReponse )
		{
			bool ret = false;

			if (LaQuestion.Reponses.Where(x =>x.Libelle.Trim() == optionReponse.Libelle.Trim()).Count() == 1)
			{
				ret = true;
			}
			return ret;
		}

	}
}