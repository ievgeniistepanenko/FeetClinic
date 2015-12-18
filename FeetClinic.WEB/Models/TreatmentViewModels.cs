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
        public int SelectedTypeId { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }

        [Display(Name = "Therapist")]
        public int[] SelectedTherapistId { get; set; }
        public IEnumerable<SelectListItem> Therap { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
    }

    public class TreatmentEditViewModel : TreatmentCreateViewModel
    {
        [Key]
        public int Id { get; set; }
        
    }

}