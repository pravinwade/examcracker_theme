using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Demo.Models
{
    public class InstituteViewModel
    {
        [Key]
        [Display(Name = "Institute")]
        public int InstituteID { get; set; }

        [MaxLength(150)]
        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "Institute")]
        public string InstituteName { get; set; }

        [MaxLength(256)]
        [Display(Name = "Institute Details")]
        public string InstituteDescription { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "State")]
        public int StateId { get; set; }
        [Display(Name = "State Name")]
        public string StateDescription { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        [Display(Name = "Email ID")]
        public string EmailID { get; set; }
        public Nullable<bool> Active { get; set; }

        [Display(Name = "Logo")]
        public string Logo { get; set; }
        public List<SelectListItem> States { get; set; }

        [Display(Name = "Exam")]
        [Required(ErrorMessage = "This field is required !")]
        public List<SelectListItem> ExamTypes { get; set; }

    }
}