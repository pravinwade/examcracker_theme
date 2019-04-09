﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Models
{
    public class PaperCriteriaConfigViewModel
    {
        [Display(Name = "Exam")]
        [Required(ErrorMessage = "This field is required !")]
        public int ExamTypeID { get; set; }

        [Display(Name = "Institute")]
        [Required(ErrorMessage = "This field is required !")]
        public int InstituteID { get; set; }

        public bool DefaultCriteria { get; set; }

        public int SubjectID { get; set; }
        public Nullable<int> SectionID { get; set; }
        public Nullable<int> LessonID { get; set; }
        public Nullable<int> TopicID { get; set; }

        //[Required(ErrorMessage = "This field is required !")]
        //public int QuesDiffLevelID { get; set; }
        //public Nullable<int> QuestionSourceId { get; set; }

        public List<SelectListItem> Institutes { get; set; }
        public List<SelectListItem> Subjects { get; set; }
        public List<SelectListItem> Sections { get; set; }
        public List<SelectListItem> Lessons { get; set; }
        public List<SelectListItem> Topics { get; set; }
        public List<SelectListItem> QuestionDiffLevels { get; set; }
        public List<SelectListItem> QuestionSources { get; set; }


        [Display(Name = "Exam")]
        public List<SelectListItem> ExamTypes { get; set; }
    }
}