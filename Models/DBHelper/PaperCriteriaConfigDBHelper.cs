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
    public class PaperCriteriaConfigDBHelper
    {
        public List<QuestionViewModel> Details()
        {
            SqlConnection con = null;
            //DataSet ds = null;
            List<QuestionViewModel> quesList = null;
            List<SelectListItem> items = new List<SelectListItem>();
            //try
            //{
            //    con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
            //    SqlCommand cmd = new SqlCommand("GetQuestionPaperCriteria", con);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@QuestionId", null);

            //    con.Open();
            //    SqlDataAdapter da = new SqlDataAdapter();
            //    da.SelectCommand = cmd;
            //    ds = new DataSet();
            //    da.Fill(ds);
            //    quesList = new List<QuestionViewModel>();
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        QuestionViewModel cobj = new QuestionViewModel();
            //        cobj.QuestionID = Convert.ToInt32(ds.Tables[0].Rows[i]["QuestionID"].ToString());
            //        cobj.subjectName = ds.Tables[0].Rows[i]["SubjectName"].ToString();
            //        cobj.section = ds.Tables[0].Rows[i]["SectionName"].ToString();
            //        cobj.lesson = ds.Tables[0].Rows[i]["LessonName"].ToString();
            //        cobj.topic = ds.Tables[0].Rows[i]["TopicName"].ToString();
            //        cobj.QuestionText = ds.Tables[0].Rows[i]["QuestionText"].ToString();
            //        cobj.MarksForQuestion = Convert.ToInt32(ds.Tables[0].Rows[i]["MarksForQuestion"].ToString());
            //        cobj.questiondifflevel = ds.Tables[0].Rows[i]["QuestionDiffLevel"].ToString();

            //        quesList.Add(cobj);
            //    }
            //    return quesList;
            //}
            //catch
            //{
                return quesList;
            //}
            //finally
            //{
            //    con.Close();
            //}
        }

        public PaperCriteriaConfigViewModel GetQuestionPaperCriteria(int examTypeid, string criteria)
        {
            SqlConnection con = null;
            DataSet ds = null;
            PaperCriteriaConfigViewModel pcConfig = new PaperCriteriaConfigViewModel();
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("GetQuestionPaperCriteria", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamTypeId", examTypeid);
                cmd.Parameters.AddWithValue("@CriteriaType", criteria);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    pcConfig.ExamTypeID = Convert.ToInt32(examTypeid);
                    pcConfig.DefaultCriteria = Convert.ToBoolean(criteria);

                    InstituteDBHelper insObj = new InstituteDBHelper();
                    SubjectDBHelper subObj = new SubjectDBHelper();
                    SectionDBHelper scobj = new SectionDBHelper();
                    LessonDBHelper lobj = new LessonDBHelper();
                    TopicDBHelper topObj = new TopicDBHelper();                    
                    QuesDiffLevelDBHelper qdlh = new QuesDiffLevelDBHelper();
                    QuesSourceDBHelper qsd = new QuesSourceDBHelper();
                    ExamTypeDBHelper etdh = new ExamTypeDBHelper();

                    pcConfig.ExamTypes = etdh.GetExamTypes();
                    pcConfig.Institutes = insObj.GetInstitutes();
                    pcConfig.Subjects = subObj.GetSubjects();
                    pcConfig.Sections = scobj.GetSections();
                    pcConfig.Lessons = lobj.GetLessons();
                    pcConfig.Topics = topObj.GetTopics();
                    pcConfig.QuestionDiffLevels = qdlh.GetQuesDiffLevels();
                    pcConfig.QuestionSources = qsd.GetQuestionSources();
                    



                    //cobj.section = ds.Tables[0].Rows[i]["SectionName"].ToString();
                    //cobj.lesson = ds.Tables[0].Rows[i]["LessonName"].ToString();
                    //cobj.topic = ds.Tables[0].Rows[i]["TopicName"].ToString();
                    //cobj.QuestionText = ds.Tables[0].Rows[i]["QuestionText"].ToString();
                    //cobj.MarksForQuestion = Convert.ToInt32(ds.Tables[0].Rows[i]["MarksForQuestion"].ToString());
                    //cobj.questiondifflevel = ds.Tables[0].Rows[i]["QuestionDiffLevel"].ToString();


                }
                return pcConfig;
            }
            catch
            {
                return pcConfig;
            }
            finally
            {
                con.Close();
            }
        }

        public PaperCriteriaConfigViewModel GetQPaperExamType()
        {
            SqlConnection con = null;
            PaperCriteriaConfigViewModel pcConfig = new PaperCriteriaConfigViewModel();

            try
            {
                ExamTypeDBHelper eObj = new ExamTypeDBHelper();
                pcConfig.ExamTypes = eObj.GetExamTypes();

                //InstituteDBHelper iObj = new InstituteDBHelper();
                //pcConfig.Institutes = iObj.GetInstitutes();

                return pcConfig;
            }
            catch
            {
                return pcConfig;
            }
           
        }

        //public PaperCriteriaConfigViewModel GetInstitutesByExamID(int )
        //{
        //    PaperCriteriaConfigViewModel pcConfig = new PaperCriteriaConfigViewModel();

        //    try
        //    {
        //        InstituteDBHelper iObj = new InstituteDBHelper();
        //        pcConfig.Institutes = iObj.GetInstitutes();

        //        return pcConfig;
        //    }
        //    catch
        //    {
        //        return pcConfig;
        //    }

        //}
    }
}