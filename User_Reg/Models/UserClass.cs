using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace User_Reg.Models
{
    public class UserClass
    {
        // Declaring the Variables and Validation of the data  (DataAnnotations)
        public String UserId { get; set; } 

        [Required(ErrorMessage = "Enter FirstName")]
        [Display(Name = "First Name")]
        [StringLength(maximumLength: 100, MinimumLength = 3, ErrorMessage = "Minimum length must be 3")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "Enter LastName")]
        [Display(Name = "Last Name")]
        [StringLength(maximumLength: 100
            , MinimumLength = 3, ErrorMessage = "Minimum length must be 3")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Enter DOB")]
        [Display(Name = "DOB")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DOB { get; set; }


        [Required(ErrorMessage = "Enter Email-Address")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
      
        [Display(Name = "Email-Address")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Enter Gender")]
        [Display(Name = "Gender")]
        public String Gender { get; set; }

        [Required(ErrorMessage = "Enter Phone Number")]
        [Display(Name = "Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Mobile number")]
        [StringLength(maximumLength: 10, MinimumLength = 10, ErrorMessage = "length must be 10")]
        public String PhoneNo
        {
            get; set;
        }



        [Required(ErrorMessage = "Enter city")]
        [Display(Name = "CityName")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Upload Profile-Image")]
        [Display(Name = "ProfileImgUrl")]
        public String ProfileImgUrl { get; set; }

        public List<CityMaster> CityRecords { get; set; }
       
    }

    public class CityMaster
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
}

