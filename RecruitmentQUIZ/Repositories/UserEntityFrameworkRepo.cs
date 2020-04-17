using RecruitmentQUIZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentQUIZ.Repositories
{
	public class UserEntityFrameworkRepo : IUser
	{
		RecruitQuizDBEntities _db;
		public UserEntityFrameworkRepo()
		{
			_db = new RecruitQuizDBEntities();
		}

		public bool CheckNonEmptyUserInfo(User user)
		{
			if (!string.IsNullOrEmpty(user.Login) && !string.IsNullOrEmpty(user.Password))
				return true;
			else
				return false;
		}

		public User GetUser(string login, string password)
		{
			return _db.Users.FirstOrDefault(x => x.Login == login && x.Password == password);	
		}
	}
}