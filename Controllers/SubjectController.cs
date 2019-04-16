using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Demo.Models;
using Demo.Models.DBHelper;

namespace Demo.Controllers
{
    public class SubjectController : Controller
    {

        //public ActionResult Index()
        //{
        //    SubjectDBHelper dbhandle = new SubjectDBHelper();
        //    ModelState.Clear();
        //    List<SelectListItem> list = dbhandle.GetClass();
        //    ViewData["Class"] = list;
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
            SubjectDBHelper dbhandle = new SubjectDBHelper();
            ClassDBHelper cdb = new ClassDBHelper();
            ModelState.Clear();
            List<SelectListItem> list = cdb.GetClass();
            ViewData["Class"] = list;
            var data = dbhandle.Details();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        // GET: 
        public ActionResult Create()
        {
            SubjectViewModel objSub = new SubjectViewModel();
            ClassDBHelper cdb = new ClassDBHelper();

            objSub.Class = cdb.GetClass();
            return View(objSub);
        }

        [HttpPost]
        public ActionResult Create(SubjectViewModel objSubject)
        {
            try
            {
                if (ModelState.IsValid) //checking model is valid or not  
                {
                    SubjectDBHelper objDB = new SubjectDBHelper();

                    string result = objDB.Create(objSubject);
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
            SubjectViewModel objSubject = new SubjectViewModel();
            SubjectDBHelper objDB = new SubjectDBHelper(); //calling class DBdata  
            objSubject.ShowallSubjects = objDB.Details();
            return View(objSubject);
        }



        public ActionResult Edit(int id)
        {
            SubjectDBHelper objDB = new SubjectDBHelper();
            return View(objDB.Details().Find(cmodel => cmodel.SubjectID == id));
        }


        [HttpPost]
        public ActionResult Edit(int id, SubjectViewModel cmodel)
        {
            try
            {
                SubjectDBHelper objDB = new SubjectDBHelper();
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
                SubjectDBHelper objDB = new SubjectDBHelper();
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
