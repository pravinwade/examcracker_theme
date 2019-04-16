using System;
using System.Collections.Generic;
using System.Data;
////using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Demo.Models;
using Demo.Models.DBHelper;

namespace Demo.Controllers
{
    public class InstituteController : Controller
    {
        //private DB_A46486_examEntities555 db = new DB_A46486_examEntities555();

        // GET: /Institute/
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
            InstituteDBHelper dbhandle = new InstituteDBHelper();
            ModelState.Clear();

            var data = dbhandle.Details();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
        // GET: /Institute/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    //Institute institute = db.Institutes.Find(id);
        //    //if (institute == null)
        //    //{
        //    //    return HttpNotFound();
        //    //}
        //    //return View(institute);

        //    var instituteVM = db.Institutes
        //        .Where(i => i.InstituteID == id)
        //        .Select(i => new InstituteViewModel
        //        {
        //             InstituteID = i.InstituteID,
        //             InstituteName = i.InstituteName,
        //             InstituteDescription = i.InstituteDescription,
        //             Address = i.Address,
        //             City = i.City,
        //             StateId = i.StateId,
        //             Phone = i.Phone,
        //             Mobile = i.Mobile,
        //             EmailID = i.EmailID,
        //             Active = i.Active
        //        }).SingleOrDefault();

        //    return View(instituteVM);
        //}

        // GET: /Institute/Create
        //public ActionResult Create()
        //{
        //    ViewBag.StateId = new SelectList(db.States, "StateID", "StateDescription");
        //    return View();
        //}

        // POST: /Institute/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        public ActionResult Create()
        {
            InstituteViewModel objIns = new InstituteViewModel();
            StateDBHelper objSt = new StateDBHelper();
            ExamTypeDBHelper etdb = new ExamTypeDBHelper();

            objIns.ExamTypes = etdb.GetExamTypes();
            objIns.States = objSt.GetStates();
            return View(objIns);
        }

        [HttpPost]
        public ActionResult Create(InstituteViewModel objInstitute)
        {
            try
            {
                if (ModelState.IsValid) //checking model is valid or not  
                {
                    InstituteDBHelper objDB = new InstituteDBHelper();

                    string result = objDB.Create(objInstitute);
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

        // GET: /Institute/Edit/5
        public ActionResult Edit(int id)
        {
            InstituteDBHelper objDB = new InstituteDBHelper();
            return View(objDB.GetInstituteById(id));            
        }

        // POST: /Institute/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(int id, InstituteViewModel cmodel)
        {
            try
            {
                InstituteDBHelper objDB = new InstituteDBHelper();
                objDB.Update(cmodel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: /Institute/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                InstituteDBHelper objDB = new InstituteDBHelper();
                if (objDB.Delete(id))
                {
                    ViewBag.AlertMsg = "Institute Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: /Institute/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Institute institute = db.Institutes.Find(id);
        //    db.Institutes.Remove(institute);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
