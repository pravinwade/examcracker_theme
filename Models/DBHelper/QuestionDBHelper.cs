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
    public class QuestionDBHelper
    {
        public List<QuestionViewModel> Details()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<QuestionViewModel> quesList = null;
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("GetQuestion", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QuestionId", null);
                
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                quesList = new List<QuestionViewModel>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    QuestionViewModel cobj = new QuestionViewModel();
                    cobj.QuestionID = Convert.ToInt32(ds.Tables[0].Rows[i]["QuestionID"].ToString());
                    cobj.subjectName = ds.Tables[0].Rows[i]["SubjectName"].ToString();
                    cobj.section = ds.Tables[0].Rows[i]["SectionName"].ToString();
                    cobj.lesson = ds.Tables[0].Rows[i]["LessonName"].ToString();
                    cobj.topic = ds.Tables[0].Rows[i]["TopicName"].ToString();
                    cobj.QuestionText = ds.Tables[0].Rows[i]["QuestionText"].ToString();
                    cobj.MarksForQuestion = Convert.ToInt32(ds.Tables[0].Rows[i]["MarksForQuestion"].ToString());
                    cobj.questiondifflevel= ds.Tables[0].Rows[i]["QuestionDiffLevel"].ToString();

                    quesList.Add(cobj);
                }
                return quesList;
            }
            catch
            {
                return quesList;
            }
            finally
            {
                con.Close();
            }
        }
        
        public string Create(QuestionViewModel objQues)
        {
            SqlConnection con = null;

            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("InsertQuestion", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SubjectID", objQues.SubjectID);
                cmd.Parameters.AddWithValue("@SectionID", objQues.SectionID);
                cmd.Parameters.AddWithValue("@LessonID", objQues.LessonID);
                cmd.Parameters.AddWithValue("@TopicID", objQues.TopicID);
                cmd.Parameters.AddWithValue("@QuestionTypeID", objQues.QuestionTypeID);
                cmd.Parameters.AddWithValue("@QuesDiffLevelID", objQues.QuesDiffLevelID);
                cmd.Parameters.AddWithValue("@QuestionText", objQues.QuestionText);
                cmd.Parameters.AddWithValue("@QuestionImage", objQues.QuestionImage);
                cmd.Parameters.AddWithValue("@MarksForQuestion", objQues.MarksForQuestion);
                cmd.Parameters.AddWithValue("@IsMultiSelect", objQues.IsMultiSelect);
                cmd.Parameters.AddWithValue("@QuestionSourceID", objQues.QuestionSourceId);

                DataTable dt = new DataTable();

                dt.Columns.Add("ExamTypeId", typeof(Int32));
                for (int i = 0; i < objQues.ExamTypes.Count; i++)
                {
                    if (objQues.ExamTypes[i].Selected == true)
                    {
                        dt.Rows.Add(objQues.ExamTypes[i].Value);
                    }
                }

                cmd.Parameters.AddWithValue("@QuestionExamTableType", dt);

                DataTable dt1 = new DataTable();
                dt1.Columns.Add("QuestionId", typeof(Int32));
                dt1.Columns.Add("AnswerId", typeof(Int32));
                dt1.Columns.Add("SeqOfAnswer", typeof(Int32));
                dt1.Columns.Add("AnswerText", typeof(String));
                dt1.Columns.Add("AnswerImage", typeof(String));
                dt1.Columns.Add("CorrectAnswer", typeof(Boolean));

                for (int i = 0; i < objQues.Answers.Count; i++)
                {
                    dt1.Rows.Add(objQues.Answers[i].QuestionID, objQues.Answers[i].AnswerID, objQues.Answers[i].SeqOfAnswer, objQues.Answers[i].AnswerText, objQues.Answers[i].AnswerImage, objQues.Answers[i].CorrectAnswer);
                }
                cmd.Parameters.AddWithValue("@AnswerTableType", dt1);
                
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

        public bool Update(QuestionViewModel objQues)
        {
            SqlConnection con = null;

            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("UpdateQuestion", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QuestionID", objQues.QuestionID);
                cmd.Parameters.AddWithValue("@SubjectID", objQues.SubjectID);
                cmd.Parameters.AddWithValue("@SectionID", objQues.SectionID);
                cmd.Parameters.AddWithValue("@LessonID", objQues.LessonID);
                cmd.Parameters.AddWithValue("@TopicID", objQues.TopicID);
                cmd.Parameters.AddWithValue("@QuestionTypeID", objQues.QuestionTypeID);
                cmd.Parameters.AddWithValue("@QuesDiffLevelID", objQues.QuesDiffLevelID);
                cmd.Parameters.AddWithValue("@QuestionText", objQues.QuestionText);
                cmd.Parameters.AddWithValue("@QuestionImage", objQues.QuestionImage);
                cmd.Parameters.AddWithValue("@MarksForQuestion", objQues.MarksForQuestion);
                cmd.Parameters.AddWithValue("@IsMultiSelect", objQues.IsMultiSelect);
                cmd.Parameters.AddWithValue("@QuestionSourceID", objQues.QuestionSourceId);

                DataTable dt = new DataTable();

                dt.Columns.Add("ExamTypeId", typeof(Int32));
                for (int i = 0; i < objQues.ExamTypes.Count; i++)
                {
                    if (objQues.ExamTypes[i].Selected == true)
                    {
                        dt.Rows.Add(objQues.ExamTypes[i].Value);
                    }
                }

                cmd.Parameters.AddWithValue("@QuestionExamTableType", dt);

                DataTable dt1 = new DataTable();
                dt1.Columns.Add("QuestionId", typeof(Int32));
                dt1.Columns.Add("AnswerId", typeof(Int32));
                dt1.Columns.Add("SeqOfAnswer", typeof(Int32));
                dt1.Columns.Add("AnswerText", typeof(String));
                dt1.Columns.Add("AnswerImage", typeof(String));
                dt1.Columns.Add("CorrectAnswer", typeof(Boolean));

                for (int i = 0; i < objQues.Answers.Count; i++)
                {
                    dt1.Rows.Add(objQues.Answers[i].QuestionID, objQues.Answers[i].AnswerID, objQues.Answers[i].SeqOfAnswer, objQues.Answers[i].AnswerText, objQues.Answers[i].AnswerImage, objQues.Answers[i].CorrectAnswer);
                }
                cmd.Parameters.AddWithValue("@AnswerTableType", dt1);

                con.Open();
                result = cmd.ExecuteScalar().ToString();
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
                SqlCommand cmd = new SqlCommand("DeleteQuestion", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QuestionID", id);

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

        public QuestionViewModel GetQuestionAnswerByID(int id)
        {
            SqlConnection con = null;
            DataSet ds = null;
            QuestionViewModel ques = new QuestionViewModel();
            List<AnswerViewModel> ansList = new List<AnswerViewModel>();
            //List<QuestionExamMapping> queExaList = new List<QuestionExamMapping>();

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("GetQuestionAnswerById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QuestionId", id);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ques.QuestionID = Convert.ToInt32(ds.Tables[0].Rows[i]["QuestionID"]);
                    ques.SubjectID = Convert.ToInt32(ds.Tables[0].Rows[i]["SubjectID"]);
                    ques.subjectName = Convert.ToString(ds.Tables[0].Rows[i]["SubjectName"]);
                    ques.SectionID = Convert.ToInt32(ds.Tables[0].Rows[i]["SectionID"]);
                    ques.section = Convert.ToString(ds.Tables[0].Rows[i]["SectionName"]);
                    ques.LessonID = Convert.ToInt32(ds.Tables[0].Rows[i]["LessonID"]);
                    ques.lesson = Convert.ToString(ds.Tables[0].Rows[i]["LessonName"]);
                    ques.TopicID = Convert.ToInt32(ds.Tables[0].Rows[i]["TopicID"]);
                    ques.topic = Convert.ToString(ds.Tables[0].Rows[i]["TopicName"]);
                    ques.QuestionText = Convert.ToString(ds.Tables[0].Rows[i]["QuestionText"]);
                    ques.QuestionImage = Convert.ToString(ds.Tables[0].Rows[i]["QuestionImage"]);
                    ques.MarksForQuestion = Convert.ToInt32(ds.Tables[0].Rows[i]["MarksForQuestion"].ToString());
                    ques.QuesDiffLevelID = Convert.ToInt32(ds.Tables[0].Rows[i]["QuesDiffLevelID"]);  
                    ques.questiondifflevel = ds.Tables[0].Rows[i]["QuestionDiffLevel"].ToString();
                    ques.QuestionTypeID = Convert.ToInt32(ds.Tables[0].Rows[i]["QuestionTypeID"]);
                    ques.QuestionTypeName = Convert.ToString(ds.Tables[0].Rows[i]["QuestionType"]);                    
                    ques.QuestionSourceId = Convert.ToInt32(ds.Tables[0].Rows[i]["QuestionSourceId"]);
                    ques.QuestionSourceName = Convert.ToString(ds.Tables[0].Rows[i]["QuestionSource"]);
                    if(ques.QuestionImage=="")
                        ques.QuestionImage = "~/Image/test1.jpg";


                }
               
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    AnswerViewModel ans = new AnswerViewModel();
                    ans.AnswerID = Convert.ToInt32(ds.Tables[1].Rows[i]["AnswerID"].ToString()); ;
                    ans.QuestionID = Convert.ToInt32(ds.Tables[1].Rows[i]["QuestionID"].ToString());
                    ans.SeqOfAnswer = Convert.ToInt32(ds.Tables[1].Rows[i]["SeqOfAnswer"].ToString());
                    ans.AnswerText = Convert.ToString(ds.Tables[1].Rows[i]["AnswerText"]);
                    ans.AnswerImage = Convert.ToString(ds.Tables[1].Rows[i]["AnswerImage"]);
                    if (ans.AnswerImage == "")
                        ans.AnswerImage = "~/Image/no_image1.jpg";
                    ans.CorrectAnswer = Convert.ToBoolean(ds.Tables[1].Rows[i]["CorrectAnswer"]);
                    ans.Active = Convert.ToBoolean(ds.Tables[1].Rows[i]["Active"]);

                    ansList.Add(ans);

                    if ( i == 0 )
                        ques.Answer1 = ans;
                    else if (i == 1)
                        ques.Answer2 = ans;
                    else if (i == 2)
                        ques.Answer3 = ans;
                    else if (i == 3)
                        ques.Answer4 = ans;
                }

                ques.Answers = ansList;

                SubjectDBHelper subObj = new SubjectDBHelper();
                SectionDBHelper scobj = new SectionDBHelper();
                LessonDBHelper lobj = new LessonDBHelper();
                TopicDBHelper topObj = new TopicDBHelper();
                QuestionTypeDBHelper qtObj = new QuestionTypeDBHelper();
                QuesDiffLevelDBHelper qdlh = new QuesDiffLevelDBHelper();
                QuesSourceDBHelper qsd = new QuesSourceDBHelper();
                ExamTypeDBHelper etdh = new ExamTypeDBHelper();

                ques.Subjects = subObj.GetSubjects();
                ques.Sections = scobj.GetSections();
                ques.Lessons = lobj.GetLessons();
                ques.Topics = topObj.GetTopics();
                ques.QuestionTypes = qtObj.GetQuestionTypes();
                ques.QuestionDiffLevels = qdlh.GetQuesDiffLevels();
                ques.QuestionSources = qsd.GetQuestionSources();
                ques.ExamTypes = etdh.GetExamTypes();

                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    for (int j = 0; j < ques.ExamTypes.Count; j++)
                    {
                        if (ques.ExamTypes[j].Value == ds.Tables[2].Rows[i]["ExamTypeId"].ToString())
                        {
                            ques.ExamTypes[j].Selected = true; 
                        }
                    }
                }

                return ques;
            }
            catch
            {
                return ques;
            }
            finally
            {
                con.Close();
            }
        }
    }
}