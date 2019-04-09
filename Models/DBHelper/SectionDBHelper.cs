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
    public class SectionDBHelper
    {
        public string Create(SectionViewModel objSec)
        {
            SqlConnection con = null;

            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("InsertSection", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SectionName", objSec.SectionName);
                cmd.Parameters.AddWithValue("@Description", objSec.Description);
                cmd.Parameters.AddWithValue("@SubjectId", objSec.SubjectID);

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

        public List<SectionViewModel> Details()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<SectionViewModel> subList = null;
            int? sectionId = null, subjectId = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("GetSection", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubjectID", subjectId);
                cmd.Parameters.AddWithValue("@SectionID", sectionId);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                subList = new List<SectionViewModel>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    SectionViewModel objSec = new SectionViewModel();
                    SubjectDBHelper objsDB = new SubjectDBHelper();

                    objSec.SectionID = Convert.ToInt32(ds.Tables[0].Rows[i]["SectionID"].ToString());
                    objSec.SectionName = ds.Tables[0].Rows[i]["SectionName"].ToString();
                    objSec.Description = ds.Tables[0].Rows[i]["Description"].ToString();
                    objSec.SubjectID = Convert.ToInt32(ds.Tables[0].Rows[i]["SubjectID"].ToString());
                    objSec.SubjectName = ds.Tables[0].Rows[i]["SubjectName"].ToString();
                    objSec.Subjects = objsDB.GetSubjects();

                    subList.Add(objSec);
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

        public bool Update(SectionViewModel objSec)
        {
            SqlConnection con = null;

            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("UpdateSection", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SectionID", objSec.SectionID);
                cmd.Parameters.AddWithValue("@SectionName", objSec.SectionName);
                cmd.Parameters.AddWithValue("@Description", objSec.Description);
                cmd.Parameters.AddWithValue("@SubjectId", objSec.SubjectID);


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
                SqlCommand cmd = new SqlCommand("DeleteSection", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SectionId", id);

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

        public List<SelectListItem> GetSections()
        {
            SqlConnection con = null;
            DataSet ds = null;
            int? sectionId = null, subjectId= null;
            List<SelectListItem> items = new List<SelectListItem>();

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
            try
            {
                SqlCommand cmd = new SqlCommand("GetSection", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SectionId", sectionId);
                cmd.Parameters.AddWithValue("@SubjectID", subjectId);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = ds.Tables[0].Rows[i]["SectionName"].ToString(),
                        Value = ds.Tables[0].Rows[i]["SectionID"].ToString()

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