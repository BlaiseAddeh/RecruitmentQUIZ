using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentQUIZ.Repositories
{
	public interface IUtilityFonctions
	{
		int GetNbreProjet();
		int getNbreCandidatEnBase();
		int getNbreCandidatByProjetID(int projetID);
	}
}
