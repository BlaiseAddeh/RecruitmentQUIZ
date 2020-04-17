using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RecruitmentQUIZ.Models
{
	public class User
	{
		public int UserID { get; set; }	
		public string Login { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }

		public Nullable<int> SecurityLevelID { get; set; }
		public virtual SecurityLevel SecurityLevel { get; set; }

		[ForeignKey("Projet")]
		public Nullable<int> ProjetID { get; set; }
		public virtual Projet Projet { get; set; }

		public virtual Resultat Resultat { get; set; }

	}
}