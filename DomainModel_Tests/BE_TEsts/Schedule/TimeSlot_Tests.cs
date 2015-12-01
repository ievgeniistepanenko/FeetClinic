using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BE.Schedule;
using DomainModel.Interfaces;
using Moq;
using NUnit.Core;
using NUnit.Framework;

namespace DomainModel_Tests.BE_TEsts.Schedule
{
    [TestFixture]
    class TimeSlot_Tests
    {
        [Test]
        public void Can_calculate_slotsAmount_from_timespan()
        {
            int amount = 0;
            TimeSlot timeslot = new TimeSlot(0,new Time(8,0), new TimeSpan(0,0,5,0),true);
            TimeSpan timespan = new TimeSpan(0,0,45,0);

            amount = timeslot.GetSlotsAmount(timespan);
            Assert.AreEqual(9,amount);

            timespan = new TimeSpan(0,0,44,0);
            amount = timeslot.GetSlotsAmount(timespan);
            Assert.AreEqual(9, amount);

            timespan = new TimeSpan(0, 0, 46, 0);
            amount = timeslot.GetSlotsAmount(timespan);
            Assert.AreEqual(10, amount);

        }
        [Test]
        public void Can_calculate_slotsAmount_from_workingHours()
        {
            int amount = 0;
            TimeSlot timeslot = new TimeSlot(0,new Time(0,0), new TimeSpan(0,0,15,0),true );

            IDayWorkingHours dayWorkingHours = new DayWorkingHours(new Time(8, 0), new Time(15, 30),
                new Time(11, 30), new TimeSpan(0, 0, 30, 0));
            amount = timeslot.GetSlotsAmount(dayWorkingHours);
            Assert.AreEqual(30,amount);

            dayWorkingHours = new DayWorkingHours(new Time(8, 0), new Time(15, 35),
               new Time(11, 30), new TimeSpan(0, 0, 30, 0));
            amount = timeslot.GetSlotsAmount(dayWorkingHours);
            Assert.AreEqual(31,amount);

            dayWorkingHours = new DayWorkingHours(new Time(8, 0), new Time(15, 20),
             new Time( 11, 30), new TimeSpan(0, 0, 30, 0));
            amount = timeslot.GetSlotsAmount(dayWorkingHours);
            Assert.AreEqual(30, amount);

            dayWorkingHours = new DayWorkingHours(new Time(8,0),new Time(15,30),new Time(12,0),new TimeSpan(0,0,0) );
            amount = timeslot.GetSlotsAmount(dayWorkingHours);
            Assert.AreEqual(30, amount);

        }

    }
}
