using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentQUIZ.Models
{
	public class SecurityLevel
	{
		public SecurityLevel()
		{
			this.Users = new HashSet<User>();
		}

		public int SecurityLevelID { get; set; }
		public string Libelle { get; set; }
		public virtual ICollection<User> Users { get; set; }
	}
}