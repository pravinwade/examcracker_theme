using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Models.DBHelper
{
    public class QuesDiffLevelDBHelper
    {
        public List<SelectListItem> GetQuesDiffLevels()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<SelectListItem> items = new List<SelectListItem>();

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
            try
            {
                SqlCommand cmd = new SqlCommand("GetQuestionDiffLevels", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = ds.Tables[0].Rows[i]["QuesDiffLevel"].ToString(),
                        Value = ds.Tables[0].Rows[i]["QuesDiffLevelID"].ToString()

                    });
                }
                return items;
            }

            catch
            {
                return items;
            }
            finally
            {
                con.Close();
            }
        }
    }
}