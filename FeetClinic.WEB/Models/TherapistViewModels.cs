using BE.BE;
using BE.BE.Schedule;
using BE.BE.Treatments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeetClinic.WEB.Models
{
    public class TherapistViewModel: CreateTherapistViewModel
    {
        public int Id { get; set; }
    }

    public class CreateTherapistViewModel
    {
        [Display(Name = "Treatments")]
        public int[] SelectedTreatmentId { get; set; }
        public IEnumerable<SelectListItem> TreatmentsSelectListItems { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public List<Treatment> Treatments { get; set; }
        public List<DayWorkingHours> WorkingHourses { get; set; }

    }

    

}