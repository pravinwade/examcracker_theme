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
    public class LessonDBHelper
    {
        public string Create(LessonViewModel objLes)
        {
            SqlConnection con = null;

            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("InsertLesson", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LessonName", objLes.LessonName);
                cmd.Parameters.AddWithValue("@Description", objLes.Description);
                cmd.Parameters.AddWithValue("@SubjectId", objLes.SubjectID);
                cmd.Parameters.AddWithValue("@SectionId", objLes.SubjectID);
               

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

        public List<LessonViewModel> Details()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<LessonViewModel> lessonList = null;
            int? lessonId = null, sectionId = null,subjectId=null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("GetLesson", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubjectID", subjectId);
                cmd.Parameters.AddWithValue("@SectionID", sectionId);
                cmd.Parameters.AddWithValue("@LessonID", lessonId);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                lessonList = new List<LessonViewModel>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    LessonViewModel objLes = new LessonViewModel();
                    //SubjectDBHelper sobj = new SubjectDBHelper();
                    //SectionDBHelper scobj = new SectionDBHelper();

                    objLes.LessonID = Convert.ToInt32(ds.Tables[0].Rows[i]["LessonID"].ToString());
                    objLes.SectionID = Convert.ToInt32(ds.Tables[0].Rows[i]["SectionID"].ToString());
                    objLes.LessonName = ds.Tables[0].Rows[i]["LessonName"].ToString();
                    objLes.Description = ds.Tables[0].Rows[i]["Description"].ToString();
                    objLes.SubjectID = Convert.ToInt32(ds.Tables[0].Rows[i]["SubjectID"].ToString());
                    objLes.SectionName = ds.Tables[0].Rows[i]["SectionName"].ToString();
                    objLes.SubjectName = ds.Tables[0].Rows[i]["SubjectName"].ToString();

                    //objLes.Subjects = sobj.GetSubjects();
                    //objLes.Sections = scobj.GetSections();

                    lessonList.Add(objLes);
                }
                return lessonList;
            }
            catch
            {
                return lessonList;
            }
            finally
            {
                con.Close();
            }
        }

        public bool Update(LessonViewModel objLes)
        {
            SqlConnection con = null;

            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("UpdateLesson", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LessonID", objLes.LessonID);
                cmd.Parameters.AddWithValue("@LessonName", objLes.LessonName);
                cmd.Parameters.AddWithValue("@Description", objLes.Description);
                cmd.Parameters.AddWithValue("@SubjectId", objLes.SubjectID);
                cmd.Parameters.AddWithValue("@SectionId", objLes.SubjectID);
                


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
                SqlCommand cmd = new SqlCommand("DeleteLesson", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LessonID", id);

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
        public List<SelectListItem> GetLessons()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<SelectListItem> items = new List<SelectListItem>();

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
            try
            {
                SqlCommand cmd = new SqlCommand("GetLesson", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubjectID", null);
                cmd.Parameters.AddWithValue("@SectionID", null);
                cmd.Parameters.AddWithValue("@LessonID", null);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = ds.Tables[0].Rows[i]["lessonName"].ToString(),
                        Value = ds.Tables[0].Rows[i]["LessonID"].ToString()

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

        public List<LessonViewModel> GetSearchDetails()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<LessonViewModel> lessonList = null;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("GetLesson", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                lessonList = new List<LessonViewModel>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    LessonViewModel objLes = new LessonViewModel();

                    objLes.LessonID = Convert.ToInt32(ds.Tables[0].Rows[i]["LessonID"].ToString());
                    objLes.SectionID = Convert.ToInt32(ds.Tables[0].Rows[i]["SectionID"].ToString());
                    objLes.LessonName = ds.Tables[0].Rows[i]["LessonName"].ToString();
                    objLes.Description = ds.Tables[0].Rows[i]["Description"].ToString();
                    objLes.SubjectID = Convert.ToInt32(ds.Tables[0].Rows[i]["SubjectID"].ToString());
                    objLes.SectionName = ds.Tables[0].Rows[i]["SectionName"].ToString();
                    objLes.SubjectName = ds.Tables[0].Rows[i]["SubjectName"].ToString();

                    lessonList.Add(objLes);
                }
                return lessonList;
            }
            catch
            {
                return lessonList;
            }
            finally
            {
                con.Close();
            }
        }

        public LessonViewModel GetLessonById(int id)
        {
            SqlConnection con = null;
            DataSet ds = null;
            LessonViewModel objLes = new LessonViewModel();

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("GetLessonByID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LessonId", id);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {                    
                    objLes.LessonID = Convert.ToInt32(ds.Tables[0].Rows[i]["LessonID"].ToString());
                    objLes.SectionID = Convert.ToInt32(ds.Tables[0].Rows[i]["SectionID"].ToString());
                    objLes.LessonName = ds.Tables[0].Rows[i]["LessonName"].ToString();
                    objLes.Description = ds.Tables[0].Rows[i]["Description"].ToString();
                    objLes.SubjectID = Convert.ToInt32(ds.Tables[0].Rows[i]["SubjectID"].ToString());
                    objLes.SectionName = ds.Tables[0].Rows[i]["SectionName"].ToString();
                    objLes.SubjectName = ds.Tables[0].Rows[i]["SubjectName"].ToString();

                    SubjectDBHelper sobj = new SubjectDBHelper();
                    SectionDBHelper scobj = new SectionDBHelper();

                    objLes.Subjects = sobj.GetSubjects();
                    objLes.Sections = scobj.GetSections();
                }
                return objLes;
            }
            catch
            {
                return objLes;
            }
            finally
            {
                con.Close();
            }
        }
    }
}