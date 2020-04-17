using RecruitmentQUIZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentQUIZ.ViewModels
{
	public class ProjetsViewModel
	{
		public IEnumerable<Projet> Projets { get; set; }
		public Projet SelectedProjet { get; set; }
		public string DisplayMode { get; set; }
	}
}