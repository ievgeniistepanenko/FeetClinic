using System;
using BE.Interfaces;

namespace BE.BE.Schedule
{
    public class Holiday : IEntity
    {
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
    }
}
