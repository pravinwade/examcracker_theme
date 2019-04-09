using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Models
{
    public class QuestionViewModel
    {
        [Display(Name = "Question No.")]
        public int QuestionID { get; set; }
        [Required(ErrorMessage = "This field is required !")]
        public int SubjectID { get; set; }
        public Nullable<int> SectionID { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        public int LessonID { get; set; }
        public Nullable<int> TopicID { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        public Nullable<int> QuestionTypeID { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        public int QuesDiffLevelID { get; set; }
        public Nullable<int> QuestionSourceId { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Question")]
        public string QuestionText { get; set; }

        [Display(Name = "Question Image")]
        public string QuestionImage { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        [Range(1, 100)]
        [DefaultValue(1)]
        [Display(Name = "Marks")]
        public Nullable<int> MarksForQuestion { get; set; }
        public Nullable<bool> IsMultiSelect { get; set; }
        public Nullable<bool> Active { get; set; }

        [Display(Name = "Subject")]
        public string subjectName{ get; set; }

        [Display(Name = "Section")]
        public string section { get; set; }

        [Display(Name = "Lesson")]
        public string lesson{ get; set; }
        
        [Display(Name = "Topic")]
        public string topic{ get; set; }

        [Display(Name = "Difficulty Level")]
        public string questiondifflevel{ get; set; }

        [Display(Name = "Type")]
        public string QuestionTypeName { get; set; }

        [Display(Name = "Source")]
        public string QuestionSourceName { get; set; }
        

        

        public List<SelectListItem> Subjects { get; set; }
        public List<SelectListItem> Sections { get; set; }
        public List<SelectListItem> Lessons { get; set; }
        public List<SelectListItem> Topics { get; set; }
        public List<SelectListItem> QuestionTypes { get; set; }
        public List<SelectListItem> QuestionDiffLevels { get; set; }
        public List<SelectListItem> QuestionSources { get; set; }
        

        [Display(Name = "Exam Type")]
        public List<SelectListItem> ExamTypes { get; set; }
         [Display(Name = "Exam Type")]
        public List<SelectListItem> ExamTypesSelected { get; set; }
        public AnswerViewModel Answer1 { get; set; }

        public AnswerViewModel Answer2 { get; set; }

        public AnswerViewModel Answer3 { get; set; }

        public AnswerViewModel Answer4 { get; set; }

        public List<AnswerViewModel> Answers { get; set; }        
    }

    public class AnswerViewModel
    {
        public Nullable<int> AnswerID { get; set; }
        public Nullable<int> QuestionID { get; set; }
        public Nullable<int> SeqOfAnswer { get; set; }
        public bool CorrectAnswer { get; set; }
        [Required(ErrorMessage = "This field is required !")]
        public string AnswerText { get; set; }
        public string AnswerImage { get; set; }
        public bool Active { get; set; }
    }
}