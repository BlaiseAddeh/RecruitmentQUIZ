using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentQUIZ.Models
{
	public class OptionReponse
	{
		public int OptionReponseID { get; set; }
		public string Libelle { get; set; }
		public Nullable<int> QuestionID { get; set; }
		public virtual Question Question { get; set; }
	}
}