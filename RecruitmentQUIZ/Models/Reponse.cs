using System;

namespace RecruitmentQUIZ.Models
{
	public class Reponse
	{
		public int ReponseID { get; set; }
		public string Libelle { get; set; }
		public float NbrePoints { get; set; }
		public Nullable<int> QuestionID { get; set; }
		public virtual Question Question { get; set; }

	}
}