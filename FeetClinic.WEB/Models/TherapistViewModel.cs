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
    public class TherapistViewModel
    {
        // Display Attribute will appear in the Html.LabelFor
        [Display(Name = "Treatment")]
        public int SelectedTreatmentId { get; set; }
        public IEnumerable<SelectListItem> treatments { get; set; }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public Holiday Holidays { get; set; }
        public Treatment Treatments { get; set; }
        public DayWorkingHours WorkingHourses { get; set; }
    }

}