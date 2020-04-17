﻿using RecruitmentQUIZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentQUIZ.Repositories
{
	public class ProjetEntityFrameworkRepo : IProjet
	{
		RecruitQuizDBEntities _db;
		public ProjetEntityFrameworkRepo()
		{
			_db = new RecruitQuizDBEntities();
		}
		public void AjouterProjet(Projet projet)
		{
			_db.Projets.Add(projet);
			_db.SaveChanges();
		}

		public IEnumerable<Projet> GetAllProjets()
		{
			return _db.Projets.ToList();
		}

		public Projet GetProjet(int ProjetID)
		{
			return _db.Projets.FirstOrDefault(x => x.ProjectID == ProjetID);
		}

		public void SupprimerProjet(Projet projet)
		{
			_db.Projets.Remove(projet);
			_db.SaveChanges();
		}

		public void UpdateProjet(Projet projet)
		{
			Projet myproj = GetProjet(projet.ProjectID);
			_db.Projets.Attach(myproj);
			myproj.Libelle = projet.Libelle;
			_db.SaveChanges();
		}
	}
}