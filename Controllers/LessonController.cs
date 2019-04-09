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
    public class LessonController : Controller
    {
        //
        // GET: 
        //public ActionResult Index()
        //{
        //    ClassDBHelper dbClass = new ClassDBHelper();
        //    LessonDBHelper dbhandle = new LessonDBHelper();
        //    ModelState.Clear();
        //    List<SelectListItem> list1 = dbhandle.GetSubjects();
        //    List<SelectListItem> list2 = dbhandle.GetSections();
        //    List<SelectListItem> list3 = dbClass.GetClass();

        //    ViewData["Subjects"] = list1;
        //    ViewData["Sections"] = list2;
        //    ViewData["Class"] = list3;

        //    return View(dbhandle.Details());
        //}

        public ActionResult Index()
        {
           return View();
        }

        public ActionResult loaddata()
        {            
            //ClassDBHelper dbClass = new ClassDBHelper();
            //SubjectDBHelper sdbh = new SubjectDBHelper();
            //SectionDBHelper secdbh = new SectionDBHelper();
            LessonDBHelper dbhandle = new LessonDBHelper();

            ModelState.Clear();
            //List<SelectListItem> list1 = sdbh.GetSubjects();
            //List<SelectListItem> list2 = secdbh.GetSections();
            //List<SelectListItem> list3 = dbClass.GetClass();

            //ViewData["Subjects"] = list1;
            //ViewData["Sections"] = list2;
            //ViewData["Class"] = list3;

            var data = dbhandle.Details();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            LessonViewModel objLesson = new LessonViewModel();
            ClassDBHelper dbClass = new ClassDBHelper();
            LessonDBHelper objDB = new LessonDBHelper();            
            SubjectDBHelper sobj = new SubjectDBHelper();
            SectionDBHelper scobj = new SectionDBHelper();

            objLesson.Subjects = sobj.GetSubjects();
            objLesson.Sections = scobj.GetSections();
            objLesson.Class = dbClass.GetClass();

            return View(objLesson);
        }

        [HttpPost]
        public ActionResult Create(LessonViewModel objLesson)
        {
            try
            {
                if (ModelState.IsValid) //checking model is valid or not  
                {
                    LessonDBHelper objDB = new LessonDBHelper();

                    string result = objDB.Create(objLesson);
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
        public ActionResult Details(int id=0)
        {
            LessonDBHelper objDB = new LessonDBHelper(); 
            return View(objDB.GetLessonById(id));
        }

        public ActionResult Edit(int id)
        {
            LessonDBHelper objDB = new LessonDBHelper();
            return View(objDB.GetLessonById( id));
        }


        [HttpPost]
        public ActionResult Edit(int id, LessonViewModel lmodel)
        {
            try
            {
                LessonDBHelper objDB = new LessonDBHelper();
                objDB.Update(lmodel);
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
                LessonDBHelper objDB = new LessonDBHelper();
                if (objDB.Delete(id))
                {
                    ViewBag.AlertMsg = "Record Deleted Successfully";
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
