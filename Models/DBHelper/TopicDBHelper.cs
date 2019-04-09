using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Demo.Models.DBHelper
{
    public class TopicDBHelper
    {
        public string Create(TopicViewModel objTop)
        {
            SqlConnection con = null;

            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("InsertTopic", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TopicName", objTop.TopicName);
                cmd.Parameters.AddWithValue("@TopicDescription", objTop.TopicDescription);
                cmd.Parameters.AddWithValue("@SubjectId", objTop.SubjectID);
                cmd.Parameters.AddWithValue("@SectionId", objTop.SectionID);
                cmd.Parameters.AddWithValue("@LessonID", objTop.LessonID);

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

        public List<TopicViewModel> Details()
        {
            SqlConnection con = null;
            int? topid = null;
            DataSet ds = null;
            List<TopicViewModel> topList = null;
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("GetTopic", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubjectID", topid);
                cmd.Parameters.AddWithValue("@SectionID", topid);
                cmd.Parameters.AddWithValue("@LessonID", topid);
                cmd.Parameters.AddWithValue("@TopicId", topid); //string.Empty
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                topList = new List<TopicViewModel>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    TopicViewModel tobj = new TopicViewModel();
                    tobj.TopicID = Convert.ToInt32(ds.Tables[0].Rows[i]["TopicID"].ToString());
                    tobj.TopicName = ds.Tables[0].Rows[i]["TopicName"].ToString(); 
                    tobj.TopicDescription = ds.Tables[0].Rows[i]["TopicDescription"].ToString(); 
                    tobj.LessonID = Convert.ToInt32(ds.Tables[0].Rows[i]["LessonID"].ToString());
                    tobj.LessonName = ds.Tables[0].Rows[i]["LessonName"].ToString(); 
                    tobj.SectionID = Convert.ToInt32(ds.Tables[0].Rows[i]["SectionID"].ToString());
                    tobj.SectionName = ds.Tables[0].Rows[i]["SectionName"].ToString(); 
                    tobj.SubjectID = Convert.ToInt32(ds.Tables[0].Rows[i]["SubjectID"].ToString());
                    tobj.SubjectName = ds.Tables[0].Rows[i]["SubjectName"].ToString();

                    SubjectDBHelper sobj = new SubjectDBHelper();
                    SectionDBHelper scobj = new SectionDBHelper();
                    LessonDBHelper lobj = new LessonDBHelper();

                    tobj.Subjects = sobj.GetSubjects();
                    tobj.Sections = scobj.GetSections();
                    tobj.Lessons = lobj.GetLessons();

                    topList.Add(tobj);
                }
                return topList;
            }
            catch
            {
                return topList;
            }
            finally
            {
                con.Close();
            }
        }

        public bool Update(TopicViewModel objTop)
        {
            SqlConnection con = null;

            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("UpdateTopic", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TopicID", objTop.TopicID);
                cmd.Parameters.AddWithValue("@TopicName", objTop.TopicName);
                cmd.Parameters.AddWithValue("@TopicDescription", objTop.TopicDescription);
                cmd.Parameters.AddWithValue("@SubjectId", objTop.SubjectID);
                cmd.Parameters.AddWithValue("@SectionId", objTop.SectionID);
                cmd.Parameters.AddWithValue("@LessonID", objTop.LessonID);

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
                SqlCommand cmd = new SqlCommand("DeleteTopic", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TopicID", id);

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

        public List<SelectListItem> GetTopics()
        {
            SqlConnection con = null;
            DataSet ds = null;
            int? topid = null;
            List<SelectListItem> items = new List<SelectListItem>();

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
            try
            {
                SqlCommand cmd = new SqlCommand("GetTopic", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubjectID", topid);
                cmd.Parameters.AddWithValue("@SectionID", topid);
                cmd.Parameters.AddWithValue("@LessonID", topid);
                cmd.Parameters.AddWithValue("@TopicId", topid); 
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = ds.Tables[0].Rows[i]["TopicName"].ToString(),
                        Value = ds.Tables[0].Rows[i]["TopicId"].ToString()

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