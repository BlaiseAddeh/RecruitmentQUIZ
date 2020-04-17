using RecruitmentQUIZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentQUIZ.Repositories
{
	public interface IResultat
	{
		void AjouterResultat(int UserID, Resultat resultat);
		Resultat GetResultat(string resultatId);
	}
}
