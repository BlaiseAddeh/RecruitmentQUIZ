using RecruitmentQUIZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentQUIZ.Repositories
{
	public interface IUser
	{
		User GetUser(string login, string password);
		bool CheckNonEmptyUserInfo(User user);
	}
}
