using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Demo.Models.DBHelper
{
    public class Dashboard
    {
        public int GetCountForDashboard(string entity)
        {
            SqlConnection con = null;
            DataSet ds = null;
            int cnt = 0;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("GetCountForDashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@entity", entity);
                con.Open();
                
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cnt = Convert.ToInt32(reader["cnt"].ToString());
                }
                return cnt;
            }
            catch
            {
                return cnt;
            }
            finally
            {
                con.Close();
            }
        }
    }
}