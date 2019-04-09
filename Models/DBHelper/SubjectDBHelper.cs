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
    public class SubjectDBHelper
    {
        public string Create(SubjectViewModel objSub)
        {
            SqlConnection con = null;

            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("InsertSubject", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SubjectName", objSub.SubjectName);
                cmd.Parameters.AddWithValue("@Description", objSub.Description);
                cmd.Parameters.AddWithValue("@ClassId", objSub.ClassID);

                
                con.Open();
                result = cmd.ExecuteScalar().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }

        public List<SubjectViewModel> Details()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<SubjectViewModel> subList = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("GetSubject", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                subList = new List<SubjectViewModel>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    SubjectViewModel subObj = new SubjectViewModel();
                    subObj.SubjectID = Convert.ToInt32(ds.Tables[0].Rows[i]["SubjectID"].ToString());
                    subObj.SubjectName = ds.Tables[0].Rows[i]["SubjectName"].ToString();
                    subObj.Description = ds.Tables[0].Rows[i]["Description"].ToString();
                    subObj.ClassID = Convert.ToInt32(ds.Tables[0].Rows[i]["ClassId"]);
                    subObj.ClassDetails = ds.Tables[0].Rows[i]["ClassDetails"].ToString();

                    ClassDBHelper cdb = new ClassDBHelper();
                    subObj.Class = cdb.GetClass();

                    subList.Add(subObj);
                }
                return subList;
            }
            catch
            {
                return subList;
            }
            finally
            {
                con.Close();
            }
        }

        public bool Update(SubjectViewModel objSub)
        {
            SqlConnection con = null;

            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("UpdateSubject", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SubjectId", objSub.SubjectID);
                cmd.Parameters.AddWithValue("@SubjectName", objSub.SubjectName);
                cmd.Parameters.AddWithValue("@Description", objSub.Description);
                cmd.Parameters.AddWithValue("@ClassId", objSub.ClassID);

                con.Open();
                result = cmd.ExecuteNonQuery().ToString();


                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }


        }

        public bool Delete(int id)
        {
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("DeleteSubject", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SubjectId", id);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i >= 1)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public List<SelectListItem> GetSubjects()
        {
            SqlConnection con = null;
            DataSet ds = null;
            int? subid = null;
            List<SelectListItem> items = new List<SelectListItem>();

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
            try
            {
                SqlCommand cmd = new SqlCommand("GetSubject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubjectId", subid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = ds.Tables[0].Rows[i]["SubjectName"].ToString(),
                        Value = ds.Tables[0].Rows[i]["SubjectId"].ToString()
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