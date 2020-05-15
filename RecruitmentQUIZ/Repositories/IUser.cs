using RecruitmentQUIZ.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentQUIZ.Repositories
{
	public interface IUser
	{
		User GetUser(string login, string password);
		User GetUserByID(string Id);
		bool CheckNonEmptyUserInfo(User user);
		IEnumerable<User> DesactiverUser(string ProjetId,string UserId);
		bool UpdateUserInfos(
			string ProjetId, string Userid, string Username, 
			string Usergender, string Useremail, string Userphone,
			string Userlangmat, string UserlangWork1, string UserlangWork2, 
			string Userniveauetude);

		bool AddNewUserInfos(
			string ProjetId,  DataTable dt);

		IEnumerable<User> ConvoquerUserPourPratique(string ProjetId, string UserId);
				
		bool VerifierSiLeCandidatADemarrerExamen(string UserId);
		bool MarquerDemarrageExamen(string UserId);
	}
}
