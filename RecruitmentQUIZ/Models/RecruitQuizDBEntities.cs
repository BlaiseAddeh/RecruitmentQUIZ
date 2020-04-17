using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RecruitmentQUIZ.Models
{
	public class RecruitQuizDBEntities : DbContext
	{
		public RecruitQuizDBEntities() : base("name=RecruitQuizDBEntities")
		{
		}

		public virtual DbSet<Projet> Projets { get; set; }
		public virtual DbSet<Question> Questions { get; set; }
		public virtual DbSet<Reponse> Reponses { get; set; }
		public virtual DbSet<OptionReponse> OptionReponses { get; set; }
		public virtual DbSet<Resultat> Resultats { get; set; }
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<SecurityLevel> SecurityLevels { get; set; }

	}
}