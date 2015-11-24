using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BE.Schedule;
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
            TimeSlot timeslot = new TimeSlot(0,new DateTime(2015,11,24,8,0,0),new TimeSpan(0,0,5,0),true);
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
            TimeSlot timeslot = new TimeSlot(0,new DateTime(2000,1,1,0,0,0),new TimeSpan(0,0,15,0),true );
            WorkingHours workingHours = new WorkingHours(new DateTime(2000, 1, 1, 8, 0, 0), new DateTime(2000, 1, 1, 15, 30, 0),
                new DateTime(2000, 1, 1, 11, 30, 0), new TimeSpan(0, 0, 30, 0));
            //Mock<WorkingHours> mock = new Mock<WorkingHours>();
            //mock.Setup(wh => wh.StartTime).Returns(new DateTime(2000, 1, 1, 8, 0, 0));
            //mock.Setup(wh => wh.EndTime).Returns(new DateTime(2000, 1, 1, 15, 30, 0));

            amount = timeslot.GetSlotsAmount(workingHours);

            Assert.AreEqual(30,amount);

            workingHours = new WorkingHours(new DateTime(2000, 1, 1, 8, 0, 0), new DateTime(2000, 1, 1, 15, 35, 0),
               new DateTime(2000, 1, 1, 11, 30, 0), new TimeSpan(0, 0, 30, 0));

            amount = timeslot.GetSlotsAmount(workingHours);

            Assert.AreEqual(31,amount);

            workingHours = new WorkingHours(new DateTime(2000, 1, 1, 8, 0, 0), new DateTime(2000, 1, 1, 15, 20, 0),
             new DateTime(2000, 1, 1, 11, 30, 0), new TimeSpan(0, 0, 30, 0));

            amount = timeslot.GetSlotsAmount(workingHours);

            Assert.AreEqual(30, amount);

        }

    }
}
