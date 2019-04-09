using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class LoginViewModel
    {
        [Key]
        public int Userid { get; set; }
        
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
          [Display(Name = "User name")]
        public string Email{ get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string CandidateName { get; set; }
        public int Roleid { get; set; }
        public string Rolename { get; set; }
    }
}