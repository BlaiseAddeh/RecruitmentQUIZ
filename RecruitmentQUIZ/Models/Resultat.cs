using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RecruitmentQUIZ.Models
{
	public class Resultat
	{
		[ForeignKey("User")]
		public int ResultatID { get; set; }		
		public float TotalPoint { get; set; }
		public float TotalObtenu { get; set; }
		public string ExamQuestions { get; set; }
		public string ExamReponseOptions { get; set; }
		public virtual User User { get; set; }

		[ForeignKey("Projet")]
		public Nullable<int> ProjetID { get; set; }
		public virtual Projet Projet { get; set; }
	}
}