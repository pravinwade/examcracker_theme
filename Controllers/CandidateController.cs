using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Demo.Models.DBHelper;
using Demo.Models;
using System.IO;


namespace Demo.Controllers
{
    public class CandidateController : Controller
    {
        //
        // GET: /Candidate/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult loaddata()
        {
            CandidateDBHelper dbhandle = new CandidateDBHelper();
            ModelState.Clear();

            var data = dbhandle.Details();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            CandidateDBHelper objDB = new CandidateDBHelper();
            CandiateViewModel objCand = new CandiateViewModel();
            StateDBHelper sdh = new StateDBHelper();
            ClassDBHelper cdh = new ClassDBHelper();
            InstituteDBHelper idbh = new InstituteDBHelper();
            ExamTypeDBHelper etdb = new ExamTypeDBHelper();

            objCand.States = sdh.GetStates();
            objCand.Institutes = idbh.GetInstitutes();
            objCand.ExamTypes = etdb.GetExamTypes();
            objCand.Class = cdh.GetClass();

            return View(objCand);
        }

        [HttpPost]
        public ActionResult Create(CandiateViewModel objCandidate)
        {
            try
            {

                objCandidate.DateOfBirth = Convert.ToDateTime(objCandidate.DateOfBirth);
                if (ModelState.IsValid) //checking model is valid or not  
                {
                    CandidateDBHelper objDB = new CandidateDBHelper();

                    if (objCandidate.CandImage != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(objCandidate.CandImage.FileName);
                        string extension = Path.GetExtension(objCandidate.CandImage.FileName);
                        filename = objCandidate.FName + "_" + objCandidate.LName + filename + extension;
                        objCandidate.Photo = "~/Image/Candidate/" + filename;
                        filename = Path.Combine(Server.MapPath("~/Image/Candidate/"), filename);
                        objCandidate.CandImage.SaveAs(filename);
                    }
                    else
                    {
                        objCandidate.Photo = "~/Image/no_image.jpg";
                    }

                    string result = objDB.Create(objCandidate);

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
            CandiateViewModel objCandidate = new CandiateViewModel();
            CandidateDBHelper objDB = new CandidateDBHelper(); //calling class DBdata  
            objCandidate.ShowallCandidate = objDB.Details();
            return View(objCandidate);
        }



        public ActionResult Edit(int id)
        {
            CandidateDBHelper objDB = new CandidateDBHelper();
            CandiateViewModel cmodel = objDB.Details().Find(cmodel1 => cmodel1.CandidateID == id);
            var cimg = cmodel.Photo;
            TempData["Cimg"] = cimg.ToString();
            return View(cmodel);
        }


        [HttpPost]
        public ActionResult Edit(int id, CandiateViewModel cmodel)
        {
            string imageURL = string.Empty;
            try
            {
                CandidateDBHelper objDB = new CandidateDBHelper();
                imageURL = TempData["Cimg"].ToString(); 
                if (cmodel.CandImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(cmodel.CandImage.FileName);
                    string extension = Path.GetExtension(cmodel.CandImage.FileName);
                    filename = cmodel.FName + "_" + cmodel.LName + filename + extension;
                    cmodel.Photo = "~/Image/Candidate/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Image/Candidate/"), filename);
                    cmodel.CandImage.SaveAs(filename);
                }
                else if (imageURL != string.Empty)
                {
                    cmodel.Photo = imageURL;
                }

                objDB.Update(cmodel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // 4. ************* DELETE STUDENT DETAILS ******************
        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                CandidateDBHelper objDB = new CandidateDBHelper();
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
