using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.BE.Schedule
{
    public class Holiday
    {
        public int Id { get; set; }
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
    }
}
