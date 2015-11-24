using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BE.Schedule;
using Moq;
using NUnit.Framework;

namespace DomainModel_Tests.BE_TEsts.Schedule
{
    [TestFixture]
    class DayAgenda_Tests
    {
        [Test]
        public void CanCreateDayAgenda()
        {
            Mock<WorkingHours> mock = new Mock<WorkingHours>();
            DateTime startTime = new DateTime(2015,11,23,7,0,0);
            mock.Setup(m => m.StartTime).Returns(startTime);
            mock.Setup(m => m.EndTime).Returns(startTime.AddHours(8));



        }
    }
}
