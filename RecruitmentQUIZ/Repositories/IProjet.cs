using RecruitmentQUIZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentQUIZ.Repositories
{
	public interface IProjet
	{
		void AjouterProjet(Projet projet);
		Projet GetProjet(int ProjetID);
		void UpdateProjet(Projet projet);
		void SupprimerProjet(Projet projet);
		IEnumerable<Projet> GetAllProjets();		
	}
}
