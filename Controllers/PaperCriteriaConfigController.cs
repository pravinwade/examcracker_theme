using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models.DBHelper;
using Demo.Models;
namespace Demo.Controllers
{
    public class PaperCriteriaConfigController : Controller
    {
        //
        // GET: /PaperCriteriaConfig/
        public ActionResult Index()
        {
           // loaddata();
            //bindExamTyes();  
            return View();
        }
        //public void bindExamTyes()
        //{
        //    ExamTypeDBHelper ex = new ExamTypeDBHelper();
        //    var examT = ex.GetExamTypes();
        //    List<SelectListItem> li = new List<SelectListItem>();
        //    li.Add(new SelectListItem { Text = "--Select Exam Types--", Value = "0" });

        //    foreach (var m in examT)
        //    {
        //        li.Add(new SelectListItem { Text = m.Text, Value = m.Value.ToString() });
        //        ViewBag.state = li;

        //    }
        //}  
        public ActionResult loaddata()
        {
            PaperCriteriaConfigDBHelper dbhandle = new PaperCriteriaConfigDBHelper();
            ModelState.Clear();

            var data = dbhandle.Details();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /PaperCriteriaConfig/Details/5
        public ActionResult Details(int ExamTypeid, string defaultCriteria)
        {
            PaperCriteriaConfigDBHelper dbhandle = new PaperCriteriaConfigDBHelper();
            var data = dbhandle.Details();
            return View();
        }

        [HttpPost]
        public JsonResult GetInstitutesByExamC(string ExamTypeID)
        {
            int _examTypeID;
            List<SelectListItem> InstituteNames = new List<SelectListItem>();
            InstituteDBHelper instDB=new InstituteDBHelper();
            if (!string.IsNullOrEmpty(ExamTypeID))
            {
                _examTypeID = Convert.ToInt32(ExamTypeID);
                List<InstituteViewModel> ListInst = new List<InstituteViewModel>();
                List<SelectListItem> li = instDB.GetInstitutesByExamID(_examTypeID).ToList();
                ListInst.Insert(0, new InstituteViewModel { InstituteID = 0, InstituteName = "Select Institute" });
            }
            return Json(new SelectList("InstituteID", "InstituteName"));
        }



        //
        // GET: /PaperCriteriaConfig/Create
        public ActionResult Create()
        {
            PaperCriteriaConfigDBHelper dbhandle = new PaperCriteriaConfigDBHelper();
            return View(dbhandle.GetQPaperExamType());        
        }

        //
        // POST: /PaperCriteriaConfig/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /PaperCriteriaConfig/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /PaperCriteriaConfig/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /PaperCriteriaConfig/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /PaperCriteriaConfig/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
