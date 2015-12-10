using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BE.BE.Customer;
using BE.BE.Identity;

namespace FeetClinic.WEB.Models
{
        public class LoginViewModel
        {
            [Required]
            [Display(Name="Email or tlf nr")]
            public string LogIn { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }


        public class RegisterViewModel
        {
            [Required]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Phone")]
            [Range(10000000,99999999)]
            public int Phone { get; set; }
            [Required]
            [Display(Name = "First Name")]
            [DataType(DataType.Text)]
            public string FirstName { get; set; }
            [Required]
            [Display(Name = "Last Name")]
            [DataType(DataType.Text)]
            public string LastName { get; set; }
            [Required]
            [Display(Name = "Street")]
            public string StreetName { get; set; }
            [Required]
            [Display(Name = "House number ")]
            public string StreetNumber { get; set; }
            [Required]
            [Display(Name = "City ")]
            public string City { get; set; }
            [Required]
            [Display(Name = "Postal Code")]
            [Range(1000, 9999)]
            public int ZipCode { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public RegisterViewModel() { }
            public RegisterViewModel(User user, CustomerProfile profile)
            {
                Email = user.Email;
                Phone = user.Phone;
                Password = user.Password;
                FirstName = profile.FirstName;
                LastName = profile.LastName;
                StreetName = profile.Address.StreetName;
                StreetNumber = profile.Address.StreetNumber;
                City = profile.Address.City;
                ZipCode = profile.Address.ZipCode;
            }
        }
}