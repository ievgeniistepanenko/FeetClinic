using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
>>>>>>> c3d4cfab2133fec978445d4bffeb97cf0e0bb162
using BE.Interfaces;

namespace BE.BE.Schedule
{
    public class Holiday : IEntity
    {
<<<<<<< HEAD
        private DateTime date1;
        private DateTime date2;
        private int DayOfYear;
        private int yearsSchedulerId;

        public Holiday(DateTime date1, DateTime date2)
        {
            this.date1 = date1;
            this.date2 = date2;
        }

        public int Id { get; set; }
=======
        public int Id { get; set; }
        public int TherapistId { get; set; }
        private DateTime _startDate;
        public DateTime StartDate
        { get { return _startDate; }
          set { _startDate = value.Date; }
        }
        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value.Date; }
        }

        public Holiday(DateTime startDate, DateTime endDate)
        {
            _startDate = startDate;
            _endDate = endDate;
        }
>>>>>>> c3d4cfab2133fec978445d4bffeb97cf0e0bb162
    }
}
