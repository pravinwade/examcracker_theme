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
    public class CandidateDBHelper
    {
        public string Create(CandiateViewModel objCand)
        {
            SqlConnection con = null;

            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("InsertCandidate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@CandidateID", 0);  
                cmd.Parameters.AddWithValue("@FName", objCand.FName);
                cmd.Parameters.AddWithValue("@MName", objCand.MName);
                cmd.Parameters.AddWithValue("@LName", objCand.LName);
                cmd.Parameters.AddWithValue("@Address", objCand.Address);
                cmd.Parameters.AddWithValue("@City", objCand.City);
                cmd.Parameters.AddWithValue("@StateId", objCand.StateID);
                cmd.Parameters.AddWithValue("@AdharCardNo", objCand.AdharCardNo);
                cmd.Parameters.AddWithValue("@DateOfBirth", objCand.DateOfBirth);
                cmd.Parameters.AddWithValue("@Mobile", objCand.Mobile);
                cmd.Parameters.AddWithValue("@Gender", objCand.Gender);
                cmd.Parameters.AddWithValue("@Photo", objCand.Photo);
                cmd.Parameters.AddWithValue("@EmailId", objCand.EmailID);
                cmd.Parameters.AddWithValue("@CollegeName", objCand.CollegeName);
                cmd.Parameters.AddWithValue("@InstituteId", objCand.InstituteID);
                cmd.Parameters.AddWithValue("@Class", objCand.ClassID);

                DataTable dt = new DataTable();

                dt.Columns.Add("ExamTypeId", typeof(Int32));

                //Data  

                for (int i = 0; i < objCand.ExamTypes.Count; i++)
                {
                    if (objCand.ExamTypes[i].Selected == true)
                    {
                        dt.Rows.Add(objCand.ExamTypes[i].Value);
                    }
                }

                cmd.Parameters.AddWithValue("@CandExamTableType", dt);

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

        public List<CandiateViewModel> Details()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<CandiateViewModel> candList = null;
            int? CandidateID = null;
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("GetCandidate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CandidateID", CandidateID);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                candList = new List<CandiateViewModel>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CandiateViewModel cobj = new CandiateViewModel();
                    cobj.CandidateID = Convert.ToInt32(ds.Tables[0].Rows[i]["CandidateID"]);
                    cobj.FName = Convert.ToString(ds.Tables[0].Rows[i]["FName"]);
                    cobj.MName = Convert.ToString(ds.Tables[0].Rows[i]["MName"]);
                    cobj.LName = Convert.ToString(ds.Tables[0].Rows[i]["LName"]);
                    cobj.Address = Convert.ToString(ds.Tables[0].Rows[i]["Address"]);
                    cobj.City = Convert.ToString(ds.Tables[0].Rows[i]["City"]);
                    cobj.AdharCardNo = Convert.ToString(ds.Tables[0].Rows[i]["AdharCardNo"]);
                    if ((ds.Tables[0].Rows[i]["DateOfBirth"]) != DBNull.Value)
                        cobj.DateOfBirth = Convert.ToDateTime(ds.Tables[0].Rows[i]["DateOfBirth"]);
                    else
                        cobj.DateOfBirth = null;
                    cobj.Mobile = Convert.ToString(ds.Tables[0].Rows[i]["Mobile"]);
                    cobj.Gender = Convert.ToChar(ds.Tables[0].Rows[i]["Gender"]);
                    cobj.Photo = Convert.ToString(ds.Tables[0].Rows[i]["Photo"]);
                    cobj.EmailID = Convert.ToString(ds.Tables[0].Rows[i]["EmailID"]);
                    cobj.CollegeName = Convert.ToString(ds.Tables[0].Rows[i]["CollegeName"]);
                    cobj.ClassID = Convert.ToInt32(ds.Tables[0].Rows[i]["Class"]);
                    cobj.Active = Convert.ToBoolean(ds.Tables[0].Rows[i]["Active"]);
                    cobj.StateID = Convert.ToInt32(ds.Tables[0].Rows[i]["StateID"]);
                    cobj.InstituteID = Convert.ToInt32(ds.Tables[0].Rows[i]["InstituteID"]);
                    cobj.Institute = Convert.ToString(ds.Tables[0].Rows[i]["Institute"]);

                    ExamTypeDBHelper etdb = new ExamTypeDBHelper();
                    StateDBHelper sdh = new StateDBHelper();
                    ClassDBHelper cdh = new ClassDBHelper();
                    InstituteDBHelper idh = new InstituteDBHelper();

                    cobj.ExamTypes = etdb.GetExamTypes();
                    cobj.States = sdh.GetStates();
                    cobj.Class = cdh.GetClass();
                    cobj.Institutes = idh.GetInstitutes();

                    items = GetCandidateExamTypes(cobj.CandidateID);

                    for (int j = 0; j < items.Count; j++)
                    {
                        if (cobj.ExamTypes[j].Value == items[j].Value)
                        {
                            cobj.ExamTypes[j].Selected = true; ;
                        }
                    }

                    candList.Add(cobj);
                }
                return candList;
            }
            catch
            {
                return candList;
            }
            finally
            {
                con.Close();
            }
        }

        public bool Update(CandiateViewModel objCand)
        {
            SqlConnection con = null;

            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("UpdateCandidate", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CandidateID", objCand.CandidateID);
                cmd.Parameters.AddWithValue("@FName", objCand.FName);
                cmd.Parameters.AddWithValue("@MName", objCand.MName);
                cmd.Parameters.AddWithValue("@LName", objCand.LName);
                cmd.Parameters.AddWithValue("@City", objCand.City);
                cmd.Parameters.AddWithValue("@Address", objCand.Address);
                cmd.Parameters.AddWithValue("@StateId", objCand.StateID);
                cmd.Parameters.AddWithValue("@AdharCardNo", objCand.AdharCardNo);
                cmd.Parameters.AddWithValue("@DateOfBirth", objCand.DateOfBirth);
                cmd.Parameters.AddWithValue("@Mobile", objCand.Mobile);
                cmd.Parameters.AddWithValue("@Gender", objCand.Gender);
                cmd.Parameters.AddWithValue("@Photo", objCand.Photo);
                cmd.Parameters.AddWithValue("@EmailId", objCand.EmailID);
                cmd.Parameters.AddWithValue("@CollegeName", objCand.CollegeName);
                cmd.Parameters.AddWithValue("@InstituteId", objCand.InstituteID);
                cmd.Parameters.AddWithValue("@Class", objCand.ClassID);

                DataTable dt = new DataTable();

                dt.Columns.Add("ExamTypeId", typeof(Int32));

                //Data  

                for (int i = 0; i < objCand.ExamTypes.Count; i++)
                {
                    if (objCand.ExamTypes[i].Selected == true)
                    {
                        dt.Rows.Add(objCand.ExamTypes[i].Value);
                    }
                }

                cmd.Parameters.AddWithValue("@CandExamTableType", dt);

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
                SqlCommand cmd = new SqlCommand("DeleteCandidate", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CandidateID", id);

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

        public List<SelectListItem> GetCandidateExamTypes(int CandidateID)
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<SelectListItem> items = new List<SelectListItem>();

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
            try
            {
                SqlCommand cmd = new SqlCommand("GetCandidateExamTypes", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CandidateID", CandidateID);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    items.Add(new SelectListItem
                    {
                        //Text = ds.Tables[0].Rows[i]["ExamName"].ToString(),
                        Value = ds.Tables[0].Rows[i]["ExamTypeID"].ToString(),
                        Selected = true

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