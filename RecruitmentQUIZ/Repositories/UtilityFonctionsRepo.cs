using RecruitmentQUIZ.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;

namespace RecruitmentQUIZ.Repositories
{
    public class UtilityFonctionsRepo : IUtilityFonctions
	{
		RecruitQuizDBEntities _db;

		public UtilityFonctionsRepo()
		{
			_db = new RecruitQuizDBEntities();
		}

		public int getNbreCandidatByProjetID(int projetID)
		{
			return _db.Users.Where(x => x.ProjetID == projetID && x.EstActif == true).Count();
		}

		public int getNbreCandidatEnBase()
		{
			return _db.Users.Where(x =>x.ProjetID != null && x.EstActif == true).Count();
		}

		public int GetNbreProjet()
		{
			return _db.Projets.Count();
		}

        public DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    if (rows.Length > 1)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i].Trim();
                        }
                        dt.Rows.Add(dr);
                    }
                }

            }


            return dt;
        }

        public DataTable ConvertXSLXtoDataTable(string strFilePath, string connString)
        {
            OleDbConnection oledbConn = new OleDbConnection(connString);
            DataTable dt = new DataTable();
            try
            {

                oledbConn.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn))
                {
                    OleDbDataAdapter oleda = new OleDbDataAdapter();
                    oleda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    oleda.Fill(ds);

                    dt = ds.Tables[0];
                }
            }
            catch
            {
            }
            finally
            {

                oledbConn.Close();
            }

            return dt;

        }
    }

}