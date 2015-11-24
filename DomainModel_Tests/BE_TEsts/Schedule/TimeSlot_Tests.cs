using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BE.Schedule;
using NUnit.Core;
using NUnit.Framework;

namespace DomainModel_Tests.BE_TEsts.Schedule
{
    [TestFixture]
    class TimeSlot_Tests
    {
        [Test]
        public void Can_calculate_slotsAmount()
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

    }
}
