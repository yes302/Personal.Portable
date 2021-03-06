﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionLib
{
    /// <summary>
    /// using mssql to get data informatin
    /// </summary>
    public class MSSQL : baseBasic
    {
        public MSSQL(string ConnectionString)
        {
            baseConnectionString = ConnectionString;
        }
        /// <summary>
        /// Get data for script
        /// </summary>
        /// <param name="SPName">Store procedure name</param>
        /// <param name="Params">Parameter array</param>
        /// <returns>retrun dataset</returns>
        public DataSet GetDSForScript(string Script, SqlParameter[] Params)
        {
            try
            {
                // Get data from mssql 
                using (baseConnection = new SqlConnection(baseConnectionString))
                {
                    // Sqlconnection setting
                    if (baseConnection.State == ConnectionState.Closed)
                        baseConnection.Open();

                    using (baseCommand = new SqlCommand())
                    {
                        // Command setting
                        baseCommand.CommandType = CommandType.Text;
                        baseCommand.CommandText = Script;
                        baseCommand.Connection = baseConnection;

                        // Command parameter setting
                        if (Params != null && Params.Length > 0)
                        {
                            for (int index = 0; index < Params.Length; ++index)
                                baseCommand.Parameters.Add(Params[index]);
                        }
                    }

                    baseDS = new DataSet();

                    using (baseDA = new SqlDataAdapter(baseCommand))
                    {
                        // Command and DB to connect
                        baseDA.Fill(baseDS);
                    }

                    baseCommand.Parameters.Clear();
                }
                return baseDS;
            }
            finally
            {
                baseDS.Dispose();
            }
        }

        /// <summary>
        /// Get data for store procedure
        /// </summary>
        /// <param name="SPName">Store procedure name</param>
        /// <param name="Params">Parameter array</param>
        /// <returns>retrun dataset</returns>
        public DataSet GetDSForSP(string SPName, SqlParameter[] Params)
        {
            try
            {
                // Get data from mssql 
                using (baseConnection = new SqlConnection(baseConnectionString))
                {
                    // Sqlconnection setting
                    if (baseConnection.State == ConnectionState.Closed)
                        baseConnection.Open();

                    using (baseCommand = new SqlCommand())
                    {
                        // Command setting
                        baseCommand.CommandType = CommandType.StoredProcedure;
                        baseCommand.CommandText = SPName;
                        baseCommand.Connection = baseConnection;

                        // Command parameter setting
                        if (Params != null && Params.Length > 0)
                        {
                            for (int index = 0; index < Params.Length; ++index)
                                baseCommand.Parameters.Add(Params[index]);
                        }

                        baseDS = new DataSet();

                        using (baseDA = new SqlDataAdapter(baseCommand))
                        {
                            // Command and DB to connect
                            baseDA.Fill(baseDS);
                        }
                    }

                    baseCommand.Parameters.Clear();
                }

                return baseDS;
            }
            finally
            {
                baseDS.Dispose();
            }
        }

        /// <summary>
        /// Affect data for SP
        /// </summary>
        /// <param name="SPName"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public int ExecuteNonQueryForSP(string SPName, SqlParameter[] Params)
        {
            // Affect data for MSSQL
            using (baseConnection = new SqlConnection(baseConnectionString))
            {
                // Sqlconnection setting
                if (baseConnection.State == ConnectionState.Closed)
                    baseConnection.Open();

                using (baseCommand = new SqlCommand())
                {
                    // Command setting
                    baseCommand.CommandType = CommandType.StoredProcedure;
                    baseCommand.CommandText = SPName;
                    baseCommand.Connection = baseConnection;

                    // Command parameter setting
                    if (Params != null && Params.Length > 0)
                    {
                        for (int index = 0; index < Params.Length; ++index)
                            baseCommand.Parameters.Add(Params[index]);
                    }
                }

                return baseCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Affect data for SP
        /// </summary>
        /// <param name="SPName"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public int ExecuteNonQueryForScript(string SPName, SqlParameter[] Params)
        {
            // Affect data for MSSQL
            using (baseConnection = new SqlConnection(baseConnectionString))
            {
                // Sqlconnection setting
                if (baseConnection.State == ConnectionState.Closed)
                    baseConnection.Open();

                using (baseCommand = new SqlCommand())
                {
                    // Command setting
                    baseCommand.CommandType = CommandType.Text;
                    baseCommand.CommandText = SPName;
                    baseCommand.Connection = baseConnection;

                    // Command parameter setting
                    if (Params != null && Params.Length > 0)
                    {
                        for (int index = 0; index < Params.Length; ++index)
                            baseCommand.Parameters.Add(Params[index]);
                    }
                }

                return baseCommand.ExecuteNonQuery();
            }
        }
    }
}
