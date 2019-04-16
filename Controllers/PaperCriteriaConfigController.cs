using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models.DBHelper;
using Demo.Models;
using System.Configuration;
using System.Data.SqlClient;
namespace Demo.Controllers
{
    public class PaperCriteriaConfigController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if ((Session["username"]== null) || ( Session["roleid"] == null))
            {
                ViewBag.Error = "Error";
                RedirectToAction("Index", "Login");
            }
            PaperCriteriaConfigViewModel model = new PaperCriteriaConfigViewModel();
            model.ExamTypes = PopulateDropDown("SELECT ExamTypeID, ExamName FROM ExamType", "ExamName", "ExamTypeID");
            return View(model);
        }

        [HttpPost]
        public JsonResult AjaxMethod(string type, int value)
        {
            PaperCriteriaConfigViewModel model = new PaperCriteriaConfigViewModel();
            switch (type)
            {
                case "ExamTypeID":
                    //model.States = PopulateDropDown("SELECT InstituteID, InstituteName FROM States WHERE ExamTypeID = " + value, "InstituteName", "InstituteID");
                    model.Institutes = PopulateDropDown("select i.InstituteID, i.InstituteName from dbo.Institute i join dbo.InstituteExamTypes ie on ie.instituteid=i.instituteid where ie.examtypeid= " + value, "InstituteName", "InstituteID");
                    break;
                case "InstituteID":
                    model.Candidates = PopulateDropDown("select CandidateID, FName+' '+LName as CandidateName from dbo.Candidate where InstituteID= " + value, "CandidateName", "CandidateID");
                    break;
            }
            return Json(model);
        }

        [HttpPost]
        public ActionResult Index(int ExamTypeID, int InstituteID, int CandidateID)
        {
            PaperCriteriaConfigViewModel model = new PaperCriteriaConfigViewModel();
            model.ExamTypes = PopulateDropDown("SELECT ExamTypeID, ExamName FROM ExamType", "ExamName", "ExamTypeID");
            model.Institutes = model.Institutes = PopulateDropDown("select i.InstituteID, i.InstituteName from dbo.Institute i join dbo.InstituteExamTypes ie on ie.instituteid=i.instituteid where ie.examtypeid= " + ExamTypeID, "InstituteName", "InstituteID");
            model.Candidates = PopulateDropDown("select CandidateID, FName+' '+LName as CandidateName from dbo.Candidate where InstituteID= " + InstituteID, "CandidateName", "CandidateID");
            return View(model);
        }

        private static List<SelectListItem> PopulateDropDown(string query, string textColumn, string valueColumn)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = sdr[textColumn].ToString(),
                                Value = sdr[valueColumn].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items;
        }

        //public JsonResult GetCriteriaDetails()
        //{
        //    PaperCriteriaConfigViewModel model = new PaperCriteriaConfigViewModel();
        //    PaperCriteriaConfigDBHelper objDB = new PaperCriteriaConfigDBHelper();
        //    model = objDB.GetQuestionPaperCriteria(1, "D");
        //    //PaperCriteriaConfigViewModel GetQuestionPaperCriteria(
        //    //return View(obj.GetQuestionPaperCriteria(1, "D"));
        //    return Json(model);
        //}

        //public PartialViewResult GetCriteriaDetails()
        //{
        //    PaperCriteriaConfigViewModel model = new PaperCriteriaConfigViewModel();
        //    PaperCriteriaConfigDBHelper objDB = new PaperCriteriaConfigDBHelper();
        //    model= objDB.GetQuestionPaperCriteria(1, "D");
        //    return PartialView(model);
        //}
	}

    }

