using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BE;
using NUnit.Framework;

namespace DomainModel_Tests.BE_TEsts
{
    [TestFixture]
    class Booking_Tests
    {
        [Test]
        public void Can_get_duration_time()
        {
            Treatment treament1 = new Treatment();
            Treatment treament2 = new Treatment();

            treament1.Duration = new TimeSpan(0,1,0,0);
            treament2.Duration = new TimeSpan(0,0,45,0);

            Booking booking = new Booking();

            Assert.AreEqual(new TimeSpan(0,0,0,0),booking.GetDuration());

            booking.Treatments.Add(treament1);
            booking.Treatments.Add(treament2);
            
            TimeSpan timeSpan = new TimeSpan(0,1,45,0).Duration();

            Assert.AreEqual(timeSpan,booking.GetDuration());

            
        }
    }
}
