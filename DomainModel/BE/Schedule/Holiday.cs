using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Interfaces;

namespace BE.BE.Schedule
{
    public class Holiday : IEntity
    {
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
    }
}
