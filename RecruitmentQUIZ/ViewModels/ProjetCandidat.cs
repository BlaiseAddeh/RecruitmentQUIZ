using RecruitmentQUIZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentQUIZ.ViewModels
{
	public class ProjetCandidat
	{
		public Projet LeProjet { get; set; }
		public IEnumerable<User> Candidats { get; set; }
	}
}