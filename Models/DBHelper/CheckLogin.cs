using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models;

namespace Demo.Models.DBHelper
{
    public class CheckLogin
    {
        public List<LoginViewModel> LoginDetails(LoginViewModel objUser)
        {
            SqlConnection con = null;
            DataSet ds = null;
            List<LoginViewModel> userList = null;
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ToString());
                SqlCommand cmd = new SqlCommand("CheckLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", objUser.UserName);
                cmd.Parameters.AddWithValue("@Password", objUser.Password);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                userList = new List<LoginViewModel>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    LoginViewModel cobj = new LoginViewModel();
                    cobj.Roleid= Convert.ToInt32(ds.Tables[0].Rows[i]["Roleid"].ToString());
                    cobj.Rolename= ds.Tables[0].Rows[i]["RoleName"].ToString();
                    cobj.Userid= Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"].ToString());
                    cobj.UserName= ds.Tables[0].Rows[i]["Username"].ToString();
                    cobj.Email= ds.Tables[0].Rows[i]["Email"].ToString();
                    cobj.CandidateName= ds.Tables[0].Rows[i]["CandidateName"].ToString();
                    
                    userList.Add(cobj);
                }
                return userList;
            }
            catch
            {
                return userList;
            }
            finally
            {
                con.Close();
            }
        }

    }
}