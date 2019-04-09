using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Models
{
    public class LessonViewModel
    {
        [Key]
        public int LessonID { get; set; }

        [Display(Name = "Section")]
        public Nullable<int> SectionID { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "This field is required !")]
        public Nullable<int> SubjectID { get; set; }

       
        [Display(Name = "Lesson")]
        [Required(ErrorMessage = "This field is required !")]
        public string LessonName { get; set; }

        public string SubjectName { get; set; }

        public string SectionName { get; set; }


        [Required(ErrorMessage = "This field is required !")]
        public string Description { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> Active { get; set; }

        public List<LessonViewModel> ShowallLessons { get; set; }

        public List<SelectListItem> Subjects { get; set; }

        public List<SelectListItem> Sections { get; set; }

        public List<SelectListItem> Class { get; set; }

    }
}