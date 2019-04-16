using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Demo.Models
{
    public class CandiateViewModel
    {
        [Key]
        public int CandidateID { get; set; }

        [Display(Name = "Institue")]
        [Required(ErrorMessage = "This field is required !")]
        public int InstituteID { get; set; }

        [Display(Name = "Institue")]
        public string Institute { get; set; }

        
        [RegularExpression(@"^[a-zA-Z]+$",ErrorMessage="Please enter only characters !")]
        [Required(ErrorMessage = "This field is required !")]
        [StringLength(50, ErrorMessage = "First Name should be less than or equal to 50 characters.")]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter only characters !")]
        [StringLength(50, ErrorMessage = "Middle Name should be less than or equal to 50 characters.")]
        [Display(Name = "Middle Name")]
        public string MName { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter only characters !")]
        [StringLength(50, ErrorMessage = "Last Name should be less than or equal to 50 characters.")]
        [Display(Name = "Last Name")]
        public string LName { get; set; }  


        

        public string Address { get; set; }


        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "State")]
        public int StateID { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter only characters !")]
        public string City { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        [StringLength(12,MinimumLength=12,ErrorMessage = "Adhar Card Number should be equal to 12 characters !")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Adhar Card Number should be numeric !")]
        [Display(Name = "Adhar Card Number")]
        public string AdharCardNo { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "This field is required !")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        //[EntranceExamDemo.Models.CustomValidation.ValidBirthDate(ErrorMessage = "Birth Date can not be greater than current date")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }


        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "Mobile")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Mobile Number should be equal to 10 characters !")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile Number should be numeric !")]
        public string Mobile { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "This field is required !")]
        public Nullable<char> Gender { get; set; }

        //[DataType(DataType.Upload)]
        [Display(Name = "Profile Photo")]
        public string Photo { get; set; }

        [Display(Name = "Email ID")]
        // [Required(ErrorMessage = "Enter Your EmailID")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address !")]
        public string EmailID { get; set; }

        [Display(Name = "College")]
        public string CollegeName { get; set; }

        

        [Display(Name = "Class")]
        [Required(ErrorMessage = "This field is required !")]
        public Nullable<int> ClassID { get; set; }

        public bool Active { get; set; }
        public List<CandiateViewModel> ShowallCandidate { get; set; }

        [Display(Name = "Exam")]
        public List<SelectListItem> ExamTypes { get; set; }

        public List<SelectListItem> States { get; set; }

        public List<SelectListItem> Institutes { get; set; }

        public List<SelectListItem> Class { get; set; }

        public HttpPostedFileBase CandImage { get; set; }
    }
}