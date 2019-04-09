using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Models
{
    public class TopicViewModel
    {
        [Key]
        public int TopicID { get; set; }

        [Display(Name = "Topic")]
        [MaxLength(80)]
        [Required(ErrorMessage = "This field is required !")]
        public string TopicName { get; set; }

        //[Required(ErrorMessage = "This field is required !")]
        [Display(Name = "Description")]
        public string TopicDescription { get; set; }

        [Display(Name = "Lesson")]
        [Required(ErrorMessage = "This field is required !")]
        public int LessonID { get; set; }

        [Display(Name = "Section")]
        public Nullable<int> SectionID { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "This field is required !")]
        public int SubjectID { get; set; }

         [Display(Name = "Lesson")]
        public string LessonName { get; set; }
         [Display(Name = "Subject")]
        public string SubjectName { get; set; }
         [Display(Name = "Section")]
        public string SectionName { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> Active { get; set; }

        public List<TopicViewModel> ShowallTopics { get; set; }

        public List<SelectListItem> Topics { get; set; }

        public List<SelectListItem> Sections { get; set; }

        public List<SelectListItem> Lessons { get; set; }

        public List<SelectListItem> Subjects { get; set; }

    }
}