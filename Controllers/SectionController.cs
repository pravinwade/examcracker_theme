using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Demo.Models;
using Demo.Models.DBHelper;

namespace Demo.Controllers
{
    public class SectionController : Controller
    {
        //
        // GET: 
        //public ActionResult Index()
        //{
        //    SectionDBHelper dbhandle = new SectionDBHelper();
        //    ModelState.Clear();
        //    List<SelectListItem> list = dbhandle.GetSubjects();
        //    ViewData["Subjects"] = list;
        //    return View(dbhandle.Details());
        //}

        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                ViewBag.Error = "Error";
                RedirectToAction("Index", "Login");
            }
            else
                ViewBag.Error = "";
            return View();
        }
        public ActionResult loaddata()
        {
            SectionDBHelper dbhandle = new SectionDBHelper();
            SubjectDBHelper subDbh = new SubjectDBHelper();
            ModelState.Clear();
            List<SelectListItem> list = subDbh.GetSubjects();
            ViewData["Subjects"] = list;
            var data = dbhandle.Details();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
        // GET: Student/Create
        public ActionResult Create()
        {
            SubjectDBHelper objsDB = new SubjectDBHelper();
            SectionViewModel objSec = new SectionViewModel();
            
            objSec.Subjects = objsDB.GetSubjects();

            return View(objSec);

        }

        [HttpPost]
        public ActionResult Create(SectionViewModel objSection)
        {
            try
            {

                if (ModelState.IsValid) //checking model is valid or not  
                {
                    SectionDBHelper objDB = new SectionDBHelper();

                    string result = objDB.Create(objSection);
                    TempData["result1"] = result;
                    ModelState.Clear(); //clearing model  


                    return RedirectToAction("Index");
                }

                else
                {
                    ModelState.AddModelError("", "Error in saving data");
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Details()
        {
            SectionViewModel objSection = new SectionViewModel();
            SectionDBHelper objDB = new SectionDBHelper(); //calling class DBdata  
            objSection.ShowallSections = objDB.Details();
            return View(objSection);
        }



        public ActionResult Edit(int id)
        {
            SectionDBHelper objDB = new SectionDBHelper();
            return View(objDB.Details().Find(cmodel => cmodel.SectionID == id));
        }


        [HttpPost]
        public ActionResult Edit(int id, SectionViewModel cmodel)
        {
            try
            {
                SectionDBHelper objDB = new SectionDBHelper();
                objDB.Update(cmodel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            try
            {
                SectionDBHelper objDB = new SectionDBHelper();
                if (objDB.Delete(id))
                {
                    ViewBag.AlertMsg = "Student Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
