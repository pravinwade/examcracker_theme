using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models;
using Demo.Models.DBHelper;
using System.Web.Security;
namespace Demo.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            CheckLogin objCheck= new CheckLogin();
            var chk = objCheck.LoginDetails(login);
            if (chk != null)
            {
                if (chk.Count > 0)
                {
                    Session["username"] = chk[0].UserName;
                    Session["roleid"] = chk[0].Roleid;
                    Session["email"] = chk[0].Email;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                //if user not valid then simply showing error message
                Response.Write("Not Valid User");
                
            }return RedirectToAction("Login"); 
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Index");
        }
	}
}