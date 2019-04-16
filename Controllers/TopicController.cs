
using System.Web.Mvc;
using Demo.Models;
using Demo.Models.DBHelper;

namespace Demo.Controllers
{
    public class TopicController : Controller
    {
        //private DB_A46486_examEntities db = new DB_A46486_examEntities();

        // GET: /Topic/
        //public ActionResult Index()
        //{
        //    TopicDBHelper dbhandle = new TopicDBHelper();
        //    ModelState.Clear();
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
            TopicDBHelper dbhandle = new TopicDBHelper();
            ModelState.Clear();
            var data = dbhandle.Details();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        // GET: /Topic/Create
        public ActionResult Create()
        {
            TopicViewModel objTopic = new TopicViewModel();
            SubjectDBHelper sobj = new SubjectDBHelper();
            SectionDBHelper scobj = new SectionDBHelper();
            LessonDBHelper lobj = new LessonDBHelper();

            objTopic.Subjects = sobj.GetSubjects();
            objTopic.Sections = scobj.GetSections();
            objTopic.Lessons = lobj.GetLessons();

            return View(objTopic);
        }

        // POST: /Topic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(TopicViewModel objTopic)
        {
            try
            {
                if (ModelState.IsValid) //checking model is valid or not  
                {
                    TopicDBHelper objDB = new TopicDBHelper();

                    string result = objDB.Create(objTopic);
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

        // GET: /Topic/Edit/5
        public ActionResult Edit(int? id)
        {
            TopicDBHelper objDB = new TopicDBHelper();
            return View(objDB.Details().Find(cmodel => cmodel.TopicID == id)); 
        }

        // POST: /Topic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(int id, TopicViewModel todel)
        {
            try
            {
                TopicDBHelper objDB = new TopicDBHelper();
                objDB.Update(todel);
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
                TopicDBHelper objDB = new TopicDBHelper();
                if (objDB.Delete(id))
                {
                    ViewBag.AlertMsg = "Topic Deleted Successfully";
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
