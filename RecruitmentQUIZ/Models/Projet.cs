using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecruitmentQUIZ.Models
{
	public class Projet
	{
		public Projet()
		{
			this.Questions = new HashSet<Question>();
			this.Resultats = new HashSet<Resultat>();
			this.Users = new HashSet<User>();
		}

		[Key]
		public int ProjectID { get; set; }
		public string Libelle { get; set; }

		public virtual ICollection<Question> Questions { get; set; }
		public virtual ICollection<Resultat> Resultats { get; set; }
		public virtual ICollection<User> Users { get; set; }
	}
}