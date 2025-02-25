﻿using System;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace Injector
{
    /// <summary>
    /// WSqlCommand.
    /// </summary>
    internal class WSqlCommand : IDisposable
    {
        private MySqlCommand m_SqlCmd = null;
        private string m_connStr = "";

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        /// <param name="commandText">Command text.</param>
        public WSqlCommand(string connectionString, string commandText)
        {
            m_connStr = connectionString;
            m_SqlCmd = new MySqlCommand(commandText);
            m_SqlCmd.CommandType = CommandType.StoredProcedure;
            m_SqlCmd.CommandTimeout = 180;
        }

        #region method Dispose

        public void Dispose()
        {
            if (m_SqlCmd != null)
            {
                m_SqlCmd.Dispose();
            }
        }

        #endregion

        #region method AddParameter

        /// <summary>
        /// Adds parameter to Sql Command.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="dbType">Parameter datatype.</param>
        /// <param name="value">Parameter value.</param>
        public void AddParameter(string name, MySqlDbType dbType, object value)
        {
            //MySqlDbType dbTyp = dbType;
            //object val = value;

            //if (dbType == DbType.Guid)
            //{
            //    dbTyp = OleDbType.VarChar;
            //    string guid = val.ToString();
            //    if (guid.Length < 1)
            //    {
            //        return;
            //    }
            //}

            //m_SqlCmd.Parameters.Add(name, dbTyp).Value = val;

            this.AddParameter(name, dbType, value, ParameterDirection.Input);
        }

        public void AddParameter(MySqlParameter param)
        {
            m_SqlCmd.Parameters.Add(param);
        }

        public void AddParameter(string name, MySqlDbType dbType, object value, ParameterDirection direction)
        {
            MySqlParameter param = new MySqlParameter(name, dbType);
            param.Value = value;
            param.Direction = direction;
            m_SqlCmd.Parameters.Add(param);
        }

        #endregion

        #region method Execute

        /// <summary>
        /// Executes command.
        /// </summary>
        /// <returns></returns>
        public DataSet Execute()
        {
            DataSet dsRetVal = null;

            using (MySqlConnection con = new MySqlConnection(m_connStr))
            {
                con.Open();
                m_SqlCmd.Connection = con;

                dsRetVal = new DataSet();

                MySqlDataAdapter adapter = new MySqlDataAdapter(m_SqlCmd);
                adapter.Fill(dsRetVal);

                adapter.Dispose();
            }

            return dsRetVal;
        }

        #endregion


        #region Properties Implementaion

        /// <summary>
        /// Gets or sets command timeout time.
        /// </summary>
        public int CommandTimeout
        {
            get { return m_SqlCmd.CommandTimeout; }

            set { m_SqlCmd.CommandTimeout = value; }
        }

        /// <summary>
        /// Gets or sets command type.
        /// </summary>
        public CommandType CommandType
        {
            get { return m_SqlCmd.CommandType; }
            set { m_SqlCmd.CommandType = value; }
        }

        #endregion

    }
}