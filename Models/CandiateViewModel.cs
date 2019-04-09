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
        public int InstituteID { get; set; }

        [Display(Name = "Institue")]
        public string Institute { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        //[StringLength(10, ErrorMessage = "First Name should be less than or equal to 10 characters.")]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        //[StringLength(15, ErrorMessage = "Middle Name should be less than or equal to 15 characters.")]
        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "Middle Name")]
        public string MName { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        //[StringLength(10, ErrorMessage = "Last Name should be less than or equal to 10 characters.")]
        [Display(Name = "Last Name")]
        public string LName { get; set; }


        [Required(ErrorMessage = "This field is required !")]
        //[StringLength(100, ErrorMessage = "Address should be less than or equal to 100 characters.")]
        public string Address { get; set; }


        //[Required(ErrorMessage = "Enter Your State")]
        //[StringLength(10, ErrorMessage = "State should be less than or equal to ten characters.")]
           [Display(Name = "State")]
        public int StateID { get; set; }

        //[Required(ErrorMessage = "Enter Your City")]
        //[StringLength(10, ErrorMessage = "City should be less than or equal to ten characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "This field is required !")]
        //[StringLength(12, ErrorMessage = "Adhar Card Number should be equal to 12 characters.")]
        [Display(Name = "Adhar Card Number")]
        public string AdharCardNo { get; set; }

        //[DataType(DataType.Date)]
        //[Required(ErrorMessage = "Enter Your DOB.")]
       // [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        //[EntranceExamDemo.Models.CustomValidation.ValidBirthDate(ErrorMessage = "Birth Date can not be greater than current date")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }


        [Required(ErrorMessage = "This field is required !")]
        [Display(Name = "Mobile")]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string Mobile { get; set; }

        [Display(Name = "Gender")]
        public Nullable<char> Gender { get; set; }

        //[DataType(DataType.Upload)]
        [Display(Name = "Profile Photo")]
        public string Photo { get; set; }

        [Display(Name = "Email ID")]
        // [Required(ErrorMessage = "Enter Your EmailID")]
        //[RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string EmailID { get; set; }

        [Display(Name = "College Name")]
        public string CollegeName { get; set; }

        [Display(Name = "Class")]

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