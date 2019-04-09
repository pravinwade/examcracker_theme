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
    public class InstituteDBHelper
    {
        public string Create(InstituteViewModel objIns)
        {
            SqlConnection con = null;

            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("InsertInstitute", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@InstituteName", objIns.InstituteName);
                cmd.Parameters.AddWithValue("@InstituteDescription", objIns.InstituteDescription);
                cmd.Parameters.AddWithValue("@Address", objIns.Address);
                cmd.Parameters.AddWithValue("@City", objIns.City);
                cmd.Parameters.AddWithValue("@StateId", objIns.StateId);
                cmd.Parameters.AddWithValue("@Phone", objIns.Phone);
                cmd.Parameters.AddWithValue("@Mobile", objIns.Mobile);
                cmd.Parameters.AddWithValue("@EmailId", objIns.EmailID);

                DataTable dt = new DataTable();

                dt.Columns.Add("ExamTypeId", typeof(Int32));

                for (int i = 0; i < objIns.ExamTypes.Count; i++)
                {
                    if (objIns.ExamTypes[i].Selected == true)
                    {
                        dt.Rows.Add(objIns.ExamTypes[i].Value);
                    }
                }

                cmd.Parameters.AddWithValue("@InstituteExamTableType", dt);
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

        public List<InstituteViewModel> Details()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<InstituteViewModel> instList = null;
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("GetAllInstitutes", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                instList = new List<InstituteViewModel>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    InstituteViewModel iobj = new InstituteViewModel();
                    iobj.InstituteID = Convert.ToInt32(ds.Tables[0].Rows[i]["InstituteID"].ToString());
                    iobj.InstituteName = ds.Tables[0].Rows[i]["InstituteName"].ToString();
                    iobj.InstituteDescription = ds.Tables[0].Rows[i]["InstituteDescription"].ToString();
                    iobj.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    iobj.City = ds.Tables[0].Rows[i]["City"].ToString();
                    iobj.StateId = Convert.ToInt32(ds.Tables[0].Rows[i]["StateId"]);
                    iobj.Phone = ds.Tables[0].Rows[i]["Phone"].ToString();
                    iobj.Mobile = ds.Tables[0].Rows[i]["Mobile"].ToString();                    
                    
                    iobj.EmailID = ds.Tables[0].Rows[i]["EmailID"].ToString();
                    StateDBHelper sdh = new StateDBHelper();
                    iobj.States = sdh.GetStates();
                    instList.Add(iobj);
                }
                return instList;
            }
            catch
            {
                return instList;
            }
            finally
            {
                con.Close();
            }
        }

        public bool Update(InstituteViewModel objInst)
        {
            SqlConnection con = null;

            string result = "";
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("UpdateInstitute", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@InstituteID", objInst.InstituteID);
                cmd.Parameters.AddWithValue("@InstituteName", objInst.InstituteName);
                cmd.Parameters.AddWithValue("@InstituteDescription", objInst.InstituteDescription);
                cmd.Parameters.AddWithValue("@Address", objInst.Address);
                cmd.Parameters.AddWithValue("@City", objInst.City);
                cmd.Parameters.AddWithValue("@StateId", objInst.StateId);
                cmd.Parameters.AddWithValue("@Phone", objInst.Phone);
                cmd.Parameters.AddWithValue("@Mobile", objInst.Mobile);
                cmd.Parameters.AddWithValue("@EmailId", objInst.EmailID);

                DataTable dt = new DataTable();

                dt.Columns.Add("ExamTypeId", typeof(Int32));

                for (int i = 0; i < objInst.ExamTypes.Count; i++)
                {
                    if (objInst.ExamTypes[i].Selected == true)
                    {
                        dt.Rows.Add(objInst.ExamTypes[i].Value);
                    }
                }

                cmd.Parameters.AddWithValue("@InstituteExamTableType", dt);

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
                SqlCommand cmd = new SqlCommand("DeleteInstitute", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@InstituteID", id);

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
        public List<SelectListItem> GetInstitutes()
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<SelectListItem> items = new List<SelectListItem>();

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllInstitutes", con);
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
                        Text = ds.Tables[0].Rows[i]["InstituteName"].ToString(),
                        Value = ds.Tables[0].Rows[i]["InstituteID"].ToString()
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
        public List<SelectListItem> GetInstitutesByExamID(int examID)
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<SelectListItem> items = new List<SelectListItem>();

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
            try
            {
                SqlCommand cmd = new SqlCommand("GetInstitutesByExamID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamTypeID", examID);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = ds.Tables[0].Rows[i]["InstituteName"].ToString(),
                        Value = ds.Tables[0].Rows[i]["InstituteID"].ToString()
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
        public InstituteViewModel GetInstituteById(int id)
        {
            SqlConnection con = null;
            DataSet ds = null;
            InstituteViewModel iobj = new InstituteViewModel();
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("GetInstituteById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteId", id);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    
                    iobj.InstituteID = Convert.ToInt32(ds.Tables[0].Rows[i]["InstituteID"].ToString());
                    iobj.InstituteName = ds.Tables[0].Rows[i]["InstituteName"].ToString();
                    iobj.InstituteDescription = ds.Tables[0].Rows[i]["InstituteDescription"].ToString();
                    iobj.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    iobj.City = ds.Tables[0].Rows[i]["City"].ToString();
                    iobj.StateId = Convert.ToInt32(ds.Tables[0].Rows[i]["StateId"]);
                    iobj.Phone = ds.Tables[0].Rows[i]["Phone"].ToString();
                    iobj.Mobile = ds.Tables[0].Rows[i]["Mobile"].ToString();

                    iobj.EmailID = ds.Tables[0].Rows[i]["EmailID"].ToString();
                    StateDBHelper sdh = new StateDBHelper();
                    iobj.States = sdh.GetStates();
                    ExamTypeDBHelper etdb = new ExamTypeDBHelper();
                    iobj.ExamTypes = etdb.GetExamTypes();

                    items = GetInstituteExamTypes(iobj.InstituteID);

                    for (int j = 0; j < iobj.ExamTypes.Count; j++)
                    {
                        for (int k = 0; k < items.Count; k++)
                        {
                            if (iobj.ExamTypes[j].Value == items[k].Value)
                            {
                                iobj.ExamTypes[j].Selected = true; ;
                            }
                        }
                    }
                }
                return iobj;
            }
            catch
            {
                return iobj;
            }
            finally
            {
                con.Close();
            }
        }

        public List<SelectListItem> GetInstituteExamTypes(int InstituteID)
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<SelectListItem> items = new List<SelectListItem>();

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
            try
            {
                SqlCommand cmd = new SqlCommand("GetInstituteExamTypes", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstituteID", InstituteID);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    items.Add(new SelectListItem
                    {
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