using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeetClinic.WEB.Models
{
    public class TreatmentCreateViewModel
    {
        [Display(Name = "Treatment Type")]
        [Required(ErrorMessage = "Choose type for treatment")]
        public int SelectedTypeId { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }

        [Display(Name = "Therapist")]
        [Required(ErrorMessage = "Choose therapists")]
        public int[] SelectedTherapistId { get; set; }
        public IEnumerable<SelectListItem> Therap { get; set; }

        [Required(ErrorMessage = "Enter the name for treatment")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Enter the price for treatment")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Enter duration i format tt:mm:ss")]
        public TimeSpan Duration { get; set; }
    }

    public class TreatmentEditViewModel : TreatmentCreateViewModel
    {
        [Key]
        public int Id { get; set; }
        
    }

}