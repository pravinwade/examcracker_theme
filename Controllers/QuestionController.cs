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
    public class QuestionController : Controller
    {
        //private DB_A46486_examEntities db = new DB_A46486_examEntities();

        // GET: /Question/
        //public ActionResult Index()
        //{
        //    var questions = db.Questions.Include(q => q.QuestionDiffLevel).Include(q => q.QuestionType).Include(q => q.Subject).Include(q => q.Topic).Include(q => q.Lesson).Include(q => q.Section);
        //    return View(questions.ToList());
        //}
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public ActionResult loaddata()
        //{
        //    var data = db.GetQuestion(null).Select(u => new QuestionViewModel
        //    {
        //        _subjectName = u.SubjectName,
        //        _section = u.SectionName,
        //        _lession = u.LessonName,
        //        _topic = u.TopicName,
        //        QuestionText= u.QuestionText,
        //        MarksForQuestion = u.MarksForQuestion,
        //        _questiondifflevel = u.QuestionDiffLevel
        //    });
        //    return Json(new { data = data }, JsonRequestBehavior.AllowGet);
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
            QuestionDBHelper dbhandle = new QuestionDBHelper();
            ModelState.Clear();

            var data = dbhandle.Details();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        // GET: /Question/Details/5
        //public ActionResult Details(int? id)
        //{
        //    //if (id == null)
        //    //{
        //    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    //}
        //    Question question = new Question(); //= db.Questions.Find(id);
        //    //if (question == null)
        //    //{
        //    //    return HttpNotFound();
        //    //}
        //    return View(question);
        //}

        // GET: /Question/Create
        public ActionResult Create()
        {
            QuestionViewModel objQues = new QuestionViewModel();

            
            
            SubjectDBHelper subObj = new SubjectDBHelper();
            SectionDBHelper scobj = new SectionDBHelper();
            LessonDBHelper lobj = new LessonDBHelper();
            TopicDBHelper topObj = new TopicDBHelper();
            QuestionTypeDBHelper qtObj = new QuestionTypeDBHelper();
            QuesDiffLevelDBHelper qdlh = new QuesDiffLevelDBHelper();
            QuesSourceDBHelper qsd = new QuesSourceDBHelper();
            ExamTypeDBHelper etdh = new ExamTypeDBHelper();

            objQues.Subjects = subObj.GetSubjects();
            objQues.Sections = scobj.GetSections();
            objQues.Lessons = lobj.GetLessons();
            objQues.Topics = topObj.GetTopics();
            objQues.QuestionTypes = qtObj.GetQuestionTypes();
            objQues.QuestionDiffLevels = qdlh.GetQuesDiffLevels();
            objQues.QuestionSources = qsd.GetQuestionSources();
            objQues.ExamTypes = etdh.GetExamTypes();

            return View(objQues);
        }

        // POST: /Question/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(QuestionViewModel qvm)
        {
            try
            {
                //var errors = ModelState.Values.SelectMany(v => v.Errors);
                //if (ModelState.IsValid) //checking model is valid or not  
                //{
                    QuestionDBHelper objQB = new QuestionDBHelper();

                    AnswerViewModel ans1 = new AnswerViewModel();
                    ans1.AnswerID = null;
                    ans1.QuestionID = null;
                    ans1.AnswerText = qvm.Answer1.AnswerText;
                    ans1.AnswerImage = qvm.Answer1.AnswerImage;
                    ans1.SeqOfAnswer = 1;
                    ans1.CorrectAnswer = qvm.Answer1.CorrectAnswer;

                    AnswerViewModel ans2 = new AnswerViewModel();
                    ans2.AnswerText = qvm.Answer2.AnswerText;
                    ans2.AnswerImage = qvm.Answer2.AnswerImage;
                    ans2.SeqOfAnswer = 2;
                    ans2.CorrectAnswer = qvm.Answer2.CorrectAnswer;

                    AnswerViewModel ans3 = new AnswerViewModel();
                    ans3.AnswerText = qvm.Answer3.AnswerText;
                    ans3.AnswerImage = qvm.Answer3.AnswerImage;
                    ans3.SeqOfAnswer = 3;
                    ans3.CorrectAnswer = qvm.Answer3.CorrectAnswer;

                    AnswerViewModel ans4 = new AnswerViewModel();
                    ans4.AnswerText = qvm.Answer4.AnswerText;
                    ans4.AnswerImage = qvm.Answer4.AnswerImage;
                    ans4.SeqOfAnswer = 4;
                    ans4.CorrectAnswer = qvm.Answer4.CorrectAnswer;

                    List<AnswerViewModel> ansList = new List<AnswerViewModel>();

                    ansList.Add(ans1);
                    ansList.Add(ans2);
                    ansList.Add(ans3);
                    ansList.Add(ans4);

                    qvm.Answers = ansList;

                    string result = objQB.Create(qvm);
                    TempData["result1"] = result;
                    ModelState.Clear(); //clearing model  

                    return RedirectToAction("Index");
                //}
                //else
                //{
                //    ModelState.AddModelError("", "Error in saving data");
                //    return View();
                //}
            }
            catch
            {
                return View();
            }
        }

        // GET: /Question/Edit/5
        public ActionResult Edit(int id)
        {
            QuestionDBHelper objDB = new QuestionDBHelper();
            return View(objDB.GetQuestionAnswerByID(id));
        }

        // POST: /Question/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, QuestionViewModel quesVm)
        {
            try
            {
                QuestionDBHelper objQB = new QuestionDBHelper();

                AnswerViewModel ans1 = new AnswerViewModel();
                ans1.AnswerID = null;
                ans1.QuestionID = quesVm.QuestionID;
                ans1.AnswerText = quesVm.Answer1.AnswerText;
                ans1.AnswerImage = quesVm.Answer1.AnswerImage;
                ans1.SeqOfAnswer = 1;
                ans1.CorrectAnswer = quesVm.Answer1.CorrectAnswer;

                AnswerViewModel ans2 = new AnswerViewModel();
                ans2.QuestionID = quesVm.QuestionID;
                ans2.AnswerText = quesVm.Answer2.AnswerText;
                ans2.AnswerImage = quesVm.Answer2.AnswerImage;
                ans2.SeqOfAnswer = 2;
                ans2.CorrectAnswer = quesVm.Answer2.CorrectAnswer;

                AnswerViewModel ans3 = new AnswerViewModel();
                ans3.QuestionID = quesVm.QuestionID;
                ans3.AnswerText = quesVm.Answer3.AnswerText;
                ans3.AnswerImage = quesVm.Answer3.AnswerImage;
                ans3.SeqOfAnswer = 3;
                ans3.CorrectAnswer = quesVm.Answer3.CorrectAnswer;

                AnswerViewModel ans4 = new AnswerViewModel();
                ans4.QuestionID = quesVm.QuestionID;
                ans4.AnswerText = quesVm.Answer4.AnswerText;
                ans4.AnswerImage = quesVm.Answer4.AnswerImage;
                ans4.SeqOfAnswer = 4;
                ans4.CorrectAnswer = quesVm.Answer4.CorrectAnswer;

                List<AnswerViewModel> ansList = new List<AnswerViewModel>();

                ansList.Add(ans1);
                ansList.Add(ans2);
                ansList.Add(ans3);
                ansList.Add(ans4);

                quesVm.Answers = ansList;

                bool result = objQB.Update(quesVm);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: /Question/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                QuestionDBHelper objQdb = new QuestionDBHelper();
                if (objQdb.Delete(id))
                {
                    ViewBag.AlertMsg = "Question Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Details(int id = 0)
        {
            QuestionDBHelper objDB = new QuestionDBHelper();

            return View(objDB.GetQuestionAnswerByID(id));
                        
        }
    }
}
