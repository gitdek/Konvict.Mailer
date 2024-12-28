using System;
using System.Data;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
using MySql;

namespace Injector
{
    public class CampaignMngtDb
    {
        private string _connString = "";

        public CampaignMngtDb(string initString)
        {
            string[] parameters = initString.Replace("\r\n", "\n").Split('\n');
            foreach (string param in parameters)
            {
                if (param.ToLower().IndexOf("connectionstring=") > -1)
                {
                    _connString = param.Substring(17);
                }
                else
                {
                    _connString = param.ToString();
                }
            }


            MySqlConnectionStringBuilder conBuild = new MySqlConnectionStringBuilder(_connString);

            string database = conBuild.Database;
            conBuild.Database = "";

            using (MySqlConnection con = new MySqlConnection(conBuild.ToString()))
            {
                con.Open();

                // See if database exists
                try
                {
                    con.ChangeDatabase(database);
                }
                catch
                {
                    throw new Exception("Database '" + database + "' doesn''t exist !");
                }
            }
        }


        #region Redirects related

        #region method GetFolders

        /// <summary>
        /// Gets redirects folder list.
        /// </summary>
        /// <returns></returns>
        public DataView GetFolders()
        {
            using (WSqlCommand sqlCmd = new WSqlCommand(_connString, "lspr_GetFolders"))
            {
                DataSet ds = sqlCmd.Execute();
                ds.Tables[0].TableName = "Redirects";

                return ds.Tables["Redirects"].DefaultView;
            }
        }

        #endregion

        #region method GetFolder

        /// <summary>
        /// Gets redirects folder list.
        /// </summary>
        /// <returns></returns>
        public DataView GetFolder(string folderName, out int result)
        {
            if (folderName == null || folderName.Length == 0)
            {
                result = -1;
                return null;
            }

            using (WSqlCommand sqlCmd = new WSqlCommand(_connString, "lspr_GetFolder"))
            {
                sqlCmd.AddParameter("FolderName", MySqlDbType.VarChar, folderName);

                MySqlParameter param = new MySqlParameter("Result", MySqlDbType.Int32);
                param.Direction = ParameterDirection.Output;
                sqlCmd.AddParameter(param);

                DataSet ds = sqlCmd.Execute();

                if (int.TryParse(param.Value.ToString(), out result))
                {
                    ds.Tables[0].TableName = "Redirects";
                    return ds.Tables["Redirects"].DefaultView;
                }
                else
                {
                    result = -1;
                    return null;
                }
            }
        }

        #endregion

        #region method AddRedirect

        public int AddRedirect(string folderName, string redirectTo)
        {
            if (folderName == null || folderName.Length == 0)
                return -1;

            if (redirectTo == null || redirectTo.Length == 0)
                return -1;

            using (WSqlCommand sqlCmd = new WSqlCommand(_connString, "lspr_AddRedirect"))
            {
                sqlCmd.AddParameter("FolderName", MySqlDbType.VarChar, folderName);
                sqlCmd.AddParameter("RedirectTo", MySqlDbType.VarChar, redirectTo);

                MySqlParameter param = new MySqlParameter("Result", MySqlDbType.Int32);
                param.Direction = ParameterDirection.Output;
                sqlCmd.AddParameter(param);
                sqlCmd.Execute();

                int result;
                if (!int.TryParse(param.Value.ToString(), out result))
                    result =  - 1;

                return result;
            }
        }

        #endregion

        #region method DeleteRedirect

        public void DeleteRedirect(string folderName, out int result)
        {
            if (folderName == null || folderName.Length == 0)
                result = -1;


            using (WSqlCommand sqlCmd = new WSqlCommand(_connString, "lspr_RedirectDeleteFolder"))
            {
                sqlCmd.AddParameter("FolderName", MySqlDbType.VarChar, folderName);
                MySqlParameter param = new MySqlParameter("Result", MySqlDbType.Int32);
                param.Direction = ParameterDirection.Output;
                sqlCmd.AddParameter(param);
                sqlCmd.Execute();

                if (!int.TryParse(param.Value.ToString(), out result))
                    result = -1;
            }
        }

        #endregion

        #region method RenameFolder

        public int RenameFolder(string folderName, string newFolder)
        {
            if (folderName == null || folderName.Length == 0)
                return -1;

            if (newFolder == null || newFolder.Length == 0)
                return -1;

            using (WSqlCommand sqlCmd = new WSqlCommand(_connString, "lspr_RenameFolder"))
            {
                sqlCmd.AddParameter("FolderName", MySqlDbType.VarChar, folderName);
                sqlCmd.AddParameter("NewFolder", MySqlDbType.VarChar, newFolder);

                MySqlParameter param = new MySqlParameter("Result", MySqlDbType.Int32);
                param.Direction = ParameterDirection.Output;
                sqlCmd.AddParameter(param);
                sqlCmd.Execute();

                int result;
                if (!int.TryParse(param.Value.ToString(), out result))
                    result = -1;

                return result;
            }
        }

        #endregion

        #region method UpdateRedirectTo

        public int UpdateRedirectTo(string folderName, string newRedirectTo)
        {
            if (folderName == null || folderName.Length == 0)
                return -1;

            if (newRedirectTo == null || newRedirectTo.Length == 0)
                return -1;

            using (WSqlCommand sqlCmd = new WSqlCommand(_connString, "lspr_UpdateRedirectTo"))
            {
                sqlCmd.AddParameter("FolderName", MySqlDbType.VarChar, folderName);
                sqlCmd.AddParameter("NewRedirectTo", MySqlDbType.VarChar, newRedirectTo);

                MySqlParameter param = new MySqlParameter("Result", MySqlDbType.Int32);
                param.Direction = ParameterDirection.Output;
                sqlCmd.AddParameter(param);
                sqlCmd.Execute();

                int result;
                if (!int.TryParse(param.Value.ToString(), out result))
                    result = -1;

                return result;
            }
        }

        #endregion

        #region method RedirectToExists

        public int RedirectToExists(string redirectTo)
        {
            if (string.IsNullOrEmpty(redirectTo))
            {
                throw new Exception("You must specify redirectTo");
            }

            using (WSqlCommand sqlCmd = new WSqlCommand(_connString, "lspr_ExistsRedirectTo"))
            {
                sqlCmd.AddParameter("RedirectTo", MySqlDbType.VarChar, redirectTo);

                MySqlParameter param = new MySqlParameter("Result", MySqlDbType.Int32);
                param.Direction = ParameterDirection.Output;
                sqlCmd.AddParameter(param);
                sqlCmd.Execute();

                int result;
                if (!int.TryParse(param.Value.ToString(), out result))
                    result = -1;

                return result;
            }
        }

        #endregion

        #endregion


        #region Hits related


        #region method GetCampaignHits

        /// <summary>
        /// Gets hits for specified campaign.
        /// </summary>
        /// <returns></returns>
        public DataView GetCampaignHits(string campaignID)
        {
            using (WSqlCommand sqlCmd = new WSqlCommand(_connString, "lspr_GetCampaignHits"))
            {
                sqlCmd.AddParameter("CampaignID", MySqlDbType.VarChar, campaignID);

                DataSet ds = sqlCmd.Execute();
                ds.Tables[0].TableName = "Hits";

                return ds.Tables["Hits"].DefaultView;
            }
        }

        #endregion

        #region method GetCampaignHitsCount

        /// <summary>
        /// Gets hits count for specified campaign.
        /// </summary>
        /// <returns></returns>
        public int GetCampaignHitsCount(string campaignID)
        {
            using (WSqlCommand sqlCmd = new WSqlCommand(_connString, "lspr_GetCampaignHitsCount"))
            {
                sqlCmd.AddParameter("CampaignID", MySqlDbType.VarChar, campaignID);
                MySqlParameter param = new MySqlParameter("Result", MySqlDbType.Int32);
                param.Direction = ParameterDirection.Output;
                sqlCmd.AddParameter(param);
                sqlCmd.Execute();

                int result;
                if (!int.TryParse(param.Value.ToString(), out result))
                    result = -1;

                return result;
            }
        }

        #endregion

        


        #region method GetMailingListHits

        /// <summary>
        /// Gets hits for specified mailing list.
        /// </summary>
        /// <returns></returns>
        public DataView GetMailingListHits(string listID)
        {
            using (WSqlCommand sqlCmd = new WSqlCommand(_connString, "lspr_GetMailingListHits"))
            {
                sqlCmd.AddParameter("ListID", MySqlDbType.VarChar, listID);

                DataSet ds = sqlCmd.Execute();
                ds.Tables[0].TableName = "Hits";

                return ds.Tables["Hits"].DefaultView;
            }
        }

        #endregion

        #region method GetSubjectHits

        /// <summary>
        /// Gets hits for specified subject.
        /// </summary>
        /// <returns></returns>
        public DataView GetSubjectHits(string subjectID)
        {
            using (WSqlCommand sqlCmd = new WSqlCommand(_connString, "lspr_GetSubjectHits"))
            {
                sqlCmd.AddParameter("SubjectID", MySqlDbType.VarChar, subjectID);

                DataSet ds = sqlCmd.Execute();
                ds.Tables[0].TableName = "Hits";

                return ds.Tables["Hits"].DefaultView;
            }
        }

        #endregion

        #region method GetMessageHits

        /// <summary>
        /// Gets hits for specified message.
        /// </summary>
        /// <returns></returns>
        public DataView GetMessageHits(string messageID)
        {
            using (WSqlCommand sqlCmd = new WSqlCommand(_connString, "lspr_GetMessageHits"))
            {
                sqlCmd.AddParameter("MessageID", MySqlDbType.VarChar, messageID);

                DataSet ds = sqlCmd.Execute();
                ds.Tables[0].TableName = "Hits";

                return ds.Tables["Hits"].DefaultView;
            }
        }

        #endregion


        #region method GetMessageHits

        /// <summary>
        /// Gets hits per campaign.
        /// </summary>
        /// <returns></returns>
        public DataView GetHitsPerCampaign()
        {
            using (WSqlCommand sqlCmd = new WSqlCommand(_connString, "lspr_GetHitsPerCampaign"))
            {
                DataSet ds = sqlCmd.Execute();
                ds.Tables[0].TableName = "Hits";

                return ds.Tables["Hits"].DefaultView;
            }
        }

        #endregion

        #region Opens related




        #endregion


        #endregion


        #region Opens related

        #region method GetCampaignOpens

        /// <summary>
        /// Gets opens for specified campaign.
        /// </summary>
        /// <returns></returns>
        public DataView GetCampaignOpens(string campaignID)
        {
            using (WSqlCommand sqlCmd = new WSqlCommand(_connString, "lspr_GetCampaignOpens"))
            {
                sqlCmd.AddParameter("CampaignID", MySqlDbType.VarChar, campaignID);

                DataSet ds = sqlCmd.Execute();
                ds.Tables[0].TableName = "Opens";

                return ds.Tables["Opens"].DefaultView;
            }
        }

        #endregion

        #region method GetCampaignOpensCount

        /// <summary>
        /// Gets opens count for specified campaign.
        /// </summary>
        /// <returns></returns>
        public int GetCampaignOpensCount(string campaignID)
        {
            using (WSqlCommand sqlCmd = new WSqlCommand(_connString, "lspr_GetCampaignOpensCount"))
            {
                sqlCmd.AddParameter("CampaignID", MySqlDbType.VarChar, campaignID);
                MySqlParameter param = new MySqlParameter("Result", MySqlDbType.Int32);
                param.Direction = ParameterDirection.Output;
                sqlCmd.AddParameter(param);
                sqlCmd.Execute();

                int result;
                if (!int.TryParse(param.Value.ToString(), out result))
                    result = -1;

                return result;
            }
        }

        #endregion


        #region method GetMailingListOpens

        /// <summary>
        /// Gets opens for specified mailing list.
        /// </summary>
        /// <returns></returns>
        public DataView GetMailingListOpens(string listId)
        {
            using (WSqlCommand sqlCmd = new WSqlCommand(_connString, "lspr_GetMailingListOpens"))
            {
                sqlCmd.AddParameter("ListID", MySqlDbType.VarChar, listId);

                DataSet ds = sqlCmd.Execute();
                ds.Tables[0].TableName = "Opens";

                return ds.Tables["Opens"].DefaultView;
            }
        }

        #endregion

        #region method GetMailingListOpens

        /// <summary>
        /// Gets opens for specified subject.
        /// </summary>
        /// <returns></returns>
        public DataView GetSubjectOpens(string subjectId)
        {
            using (WSqlCommand sqlCmd = new WSqlCommand(_connString, "lspr_GetSubjectOpens"))
            {
                sqlCmd.AddParameter("SubjectID", MySqlDbType.VarChar, subjectId);

                DataSet ds = sqlCmd.Execute();
                ds.Tables[0].TableName = "Opens";

                return ds.Tables["Opens"].DefaultView;
            }
        }

        #endregion

        #region method GetMailingListOpens

        /// <summary>
        /// Gets opens for specified message.
        /// </summary>
        /// <returns></returns>
        public DataView GetMessageOpens(string messageId)
        {
            using (WSqlCommand sqlCmd = new WSqlCommand(_connString, "lspr_GetMessageOpens"))
            {
                sqlCmd.AddParameter("MessageID", MySqlDbType.VarChar, messageId);

                DataSet ds = sqlCmd.Execute();
                ds.Tables[0].TableName = "Opens";

                return ds.Tables["Opens"].DefaultView;
            }
        }

        #endregion


        #region method GetOpensPerCampaign

        /// <summary>
        /// Gets the count of opens of all campaigns and groups the results.
        /// <code>
        /// SELECT campaign, COUNT(*) FROM opens GROUP BY campaign;
        /// </code>
        /// </summary>
        /// <returns></returns>
        public DataView GetOpensPerCampaign()
        {
            using (WSqlCommand sqlCmd = new WSqlCommand(_connString, "lspr_GetOpensPerCampaign"))
            {
                DataSet ds = sqlCmd.Execute();
                ds.Tables[0].TableName = "Opens";

                return ds.Tables["Opens"].DefaultView;
            }
        }

        #endregion

        #region method GetOpensCount

        /// <summary>
        /// Gets the count of opens of all campaigns.
        /// </summary>
        /// <returns></returns>
        public int GetOpensCount()
        {
            using (WSqlCommand sqlCmd = new WSqlCommand(_connString, "lspr_GetOpensCount"))
            {
                MySqlParameter param = new MySqlParameter("Result", MySqlDbType.Int32);
                param.Direction = ParameterDirection.ReturnValue;
                sqlCmd.AddParameter(param);

                sqlCmd.Execute();

                int result;
                if (!int.TryParse(param.Value.ToString(), out result))
                    result = 0;

                return result;
            }
        }

        #endregion



        /*
         *
         
            1.
            parameters[1] = new SqlParameter("@Return_Value", SqlDbType.Int);
            2.
            parameters[1].Value = -1;
            3.
            parameters[1].Direction = ParameterDirection.ReturnValue;
            4.

            5.
            if(Int32.Parse(parameters[1].Value.ToString()) != -1)
            6.
            {
            7.
            //Success
            8.
      }

         
         */
        #endregion


    }
}
