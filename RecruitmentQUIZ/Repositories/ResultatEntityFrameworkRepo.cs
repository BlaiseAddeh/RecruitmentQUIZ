using RecruitmentQUIZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentQUIZ.Repositories
{
	public class ResultatEntityFrameworkRepo : IResultat
	{
		RecruitQuizDBEntities _db;
		public ResultatEntityFrameworkRepo()
		{
			_db = new RecruitQuizDBEntities();
		}
		public void AjouterResultat(int UserID, Resultat resultat)
		{
			//_db.Resultats.Add(resultat);
			//_db.SaveChanges();

			User myUser = _db.Users.Find(UserID);

			_db.Users.Attach(myUser);
			myUser.Resultat = resultat;
			_db.SaveChanges();

		}

		public Resultat GetResultat(string resultatId)
		{
			return _db.Resultats.FirstOrDefault(x => x.ResultatID.ToString() == resultatId);
		}
	}
}