using System;
using System.Collections.Generic;
using System.Data;
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
		DataTable ConvertCSVtoDataTable(string strFilePath);
		DataTable ConvertXSLXtoDataTable(string strFilePath, string connString);
	}
}
