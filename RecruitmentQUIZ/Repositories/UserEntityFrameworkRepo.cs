using RecruitmentQUIZ.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

		public IEnumerable<User> ConvoquerUserPourPratique(string ProjetId, string UserId)
		{
			User us = _db.Users.Where(x => x.UserID.ToString() == UserId).FirstOrDefault();
			if (us != null)
			{
				long projetid = (long)us.ProjetID;
				us.EstConvoquePourPratique = true;
				_db.SaveChanges();
				return _db.Users.Where(x => x.ProjetID == projetid && x.EstActif == true);
			}
			return _db.Users.Where(x => x.ProjetID.ToString() == ProjetId && x.EstActif == true);
		}

		public IEnumerable<User> DesactiverUser(string ProjetId, string UserId)
		{
			User us = _db.Users.Where(x => x.UserID.ToString() == UserId).FirstOrDefault();
			if (us !=null)
			{
				long projetid = (long)us.ProjetID;
				us.EstActif = false;
				_db.SaveChanges();
				return _db.Users.Where(x => x.ProjetID == projetid && x.EstActif==true);
			}
			return _db.Users.Where(x => x.ProjetID.ToString() == ProjetId && x.EstActif == true);
		}

		public User GetUser(string login, string password)
		{
			return _db.Users.FirstOrDefault(x => x.Login == login && x.Password == password && x.EstActif == true);	
		}

		public User GetUserByID(string Id)
		{
			return _db.Users.Where(x => x.UserID.ToString() == Id).FirstOrDefault();
		}

		public bool UpdateUserInfos(string ProjetId, string Userid, string Username, string Usergender, string Useremail, string Userphone, string Userlangmat, string UserlangWork1, string UserlangWork2, string Userniveauetude)
		{
			User us = _db.Users.Where(x => x.UserID.ToString() == Userid).FirstOrDefault();
			if (us !=null)
			{
				us.Name = Username;
				us.Sexe = Usergender;
				us.Email = Useremail;
				us.Phone = Userphone;
				us.LangueMaternelle = Userlangmat;
				us.LangueTravail1 = UserlangWork1;
				us.LangueTravail2 = UserlangWork2;
				us.NiveauEtude = Userniveauetude;
				_db.SaveChanges();
				return true;
			}

			return false;			
		}


		public bool AddNewUserInfos(string ProjetId, DataTable dt)
		{
			try
			{
				if (dt.Rows.Count > 0)
				{
					foreach (DataRow r in dt.Rows)
					{
						string email = r["Useremail"].ToString();
						string phone = r["Userphone"].ToString();
						if (_db.Users.Where(
							x => x.Email == email &&
							x.Phone == phone &&
							x.ProjetID.ToString() == ProjetId
						).Count() == 0)
						{
							User us = new User();
							us.ProjetID = int.Parse(ProjetId);
							us.Name = r["Username"].ToString();
							us.Login = r["Useremail"].ToString();
							Random random = new Random();
							us.Password = random.Next(100, 1000).ToString();
							us.Sexe = r["Usergender"].ToString();
							us.Email = r["Useremail"].ToString();
							us.Phone = r["Userphone"].ToString();
							us.LangueMaternelle = r["Userlangmat"].ToString();
							us.LangueTravail1 = r["UserlangWork1"].ToString();
							us.LangueTravail2 = r["UserlangWork2"].ToString();
							us.NiveauEtude = r["Userniveauetude"].ToString();
							us.SecurityLevelID = 2;
							us.EstActif = true;
							us.ADemarrerExame = false;
							us.DateDemarrageExam = Convert.ToDateTime("1900-01-01 00:00:00.000");
							us.Nationalite = "";
							us.EstConvoquePourPratique = false;
							us.EstRetenuApresPratique = false;
							_db.Users.Add(us);
						}
					}

					_db.SaveChanges();
					
					return true;
				}

				return false;
			}
			catch (Exception ex)
			{

				throw ex;
			}
			
		}

		public bool VerifierSiLeCandidatADemarrerExamen(string UserId)
		{
			User us = _db.Users.Where(x => x.ADemarrerExame == true).FirstOrDefault();
			if (us != null)
			{				
				return true;
			}
			return false;
		}

		public bool MarquerDemarrageExamen(string UserId)
		{
			User us = _db.Users.Where(x => x.UserID.ToString() == UserId).FirstOrDefault();
			if (us !=null)
			{
				us.ADemarrerExame = true;
				us.DateDemarrageExam = DateTime.Now;
				_db.SaveChanges();
				return true;
			}
			return false;
		}
	}
}