using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.BE
{
   public class Booking
    {
       public int Id { get; set; }

       public DateTime BookingDate { get; set; }

       public DateTime DateTime { get; set; }

       public CustomerProfile CustomerProfile { get; set; }

       public List<Treatment> Treatments { get; set; }

       public Therapist Therapist { get; set; }


    }
}
