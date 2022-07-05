using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Text;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.IO;
using HRGDReguestsystem_IS.Models;

namespace HRGDReguestsystem_IS.Repository
{
    public class Common
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString);
            return con;
        }
        public static DataTable ExecuteQuery(string query)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader read;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandTimeout = 36000000;
            con.Open();
            DataTable dt = new DataTable();
            read = cmd.ExecuteReader();
            dt.Load(read);
            con.Close();
            return dt;
        }
        public static string SqlDate2(string value)
        {
            var SqlDateTime = string.IsNullOrEmpty(value) ? "" : Convert.ToDateTime(value).ToString("yyyy-MM-dd");

            return SqlDateTime;
        }
        public static void ExecuteNonQuery(string Query)
        {
            try
            {
                SqlConnection con = GetConnection();
                SqlCommand cmd = new SqlCommand(Query, con);
                con.OpenAsync();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region GetRole
        public static string GetRole(string EmployeeID)
        {
            string Result = string.Empty;

            try
            {
                DataTable dt = new DataTable();
                dt = ExecuteQuery("Select Distinct R.ROLE From MST_Privilege P LEFT JOIN MST_ROLE R ON P.RoleID=R.ID Where IDEmployee = '" + EmployeeID + "'");

                if (dt.Rows.Count > 0)
                {
                    Result = dt.Rows[0][0].ToString();
                }
                else
                {
                    Result = "USER";
                }

                return Result;
            }
            catch (Exception ex)
            {
                return Result;
            }
        }
        #endregion

        #region GetEmployeeDetail
        public static string GetEmployeeDetail(string EmployeeID, string param)
        {
            string Result = string.Empty;

            try
            {
                DataTable dt = new DataTable();
                dt = ExecuteQuery("Select * from mst_employee Where [EmployeeID] = '" + EmployeeID + "'");

                if (dt.Rows.Count > 0)
                {
                    if (param == "Entity")
                    {
                        Result = dt.Rows[0]["Entities"].ToString();
                    }
                    else if (param == "JoinDate")
                    {
                        Result = Convert.ToDateTime(dt.Rows[0]["Employee Hire Date"].ToString()).ToString("dd/MM/yyyy");
                    }

                }
                else
                {
                    Result = null;
                }

                return Result;
            }
            catch (Exception ex)
            {
                return Result;
            }
        }
        #endregion

        #region Encrypt
        public static string Encrypt(string str)
        {
            string EncrptKey = "2013;[pnuLIT)WebCodeExpert";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byKey = System.Text.Encoding.UTF8.GetBytes(EncrptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        #endregion


    }


}