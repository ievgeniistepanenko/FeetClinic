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
    class WorkingHours_Tests
    {
        [Test]
        public void Can_Create_Working_Hours()
        {
            Mock<Dictionary<DateTime,IDayWorkingHours>> mockDate = 
                new Mock<Dictionary<DateTime, IDayWorkingHours>>();
            Mock<Dictionary<DayOfWeek,IDayWorkingHours>> mockDayOfWeek = 
                new Mock<Dictionary<DayOfWeek, IDayWorkingHours>>();

            //IWorkingHoursManager wh = new WorkingHoursManager(mockDate.Object,mockDayOfWeek.Object);
        }

    }
}
