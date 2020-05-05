using RecruitmentQUIZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentQUIZ.Repositories
{
	public class UtilityFonctionsRepo : IUtilityFonctions
	{
		RecruitQuizDBEntities _db;

		public UtilityFonctionsRepo()
		{
			_db = new RecruitQuizDBEntities();
		}

		public int getNbreCandidatByProjetID(int projetID)
		{
			return _db.Users.Where(x => x.ProjetID == projetID).Count();
		}

		public int getNbreCandidatEnBase()
		{
			return _db.Users.Where(x =>x.ProjetID !=null).Count();
		}

		public int GetNbreProjet()
		{
			return _db.Projets.Count();
		}

	}
}