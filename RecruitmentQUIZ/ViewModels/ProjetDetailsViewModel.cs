using RecruitmentQUIZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentQUIZ.ViewModels
{
	public class ProjetDetailsViewModel
	{
		public Projet LeProjet { get; set; }
		public IEnumerable<Question> Questions { get; set; }
		public Question SelectedQuestion { get; set; }
		public string DisplayMode { get; set; }
	}
}