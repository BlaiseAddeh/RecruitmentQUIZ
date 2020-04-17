using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentQUIZ.Models
{
	public class Question
	{
		public Question()
		{
			this.Reponses = new HashSet<Reponse>();
			this.OptionReponses = new HashSet<OptionReponse>();
		}

		public int QuestionID { get; set; }
		public bool EstMultiChoix { get; set; }
		public string Libelle { get; set; }

		[ForeignKey("Projet")]
		public Nullable<int> ProjetID { get; set; }
		public virtual Projet Projet { get; set; }

		public virtual ICollection<Reponse> Reponses { get; set; }
		public virtual ICollection<OptionReponse> OptionReponses { get; set; }
		

	}
}