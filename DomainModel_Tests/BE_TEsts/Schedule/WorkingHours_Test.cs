using System;
using DomainModel.BE.Schedule;
using NUnit.Core;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace DomainModel_Tests.BE_TEsts.Schedule
{
    [TestFixture]
    class WorkingHours_Test
    {
        [SetUp]
        public void SetUp() { }
        [TearDown]
        public void TearDown() { }
        [Test]
        public void Can_Not_Create_Invalid_WorkingHours()
        {
            Assert.Throws(typeof(ArgumentException), Create_Working_Hours_StartGreaterThanEnd);
            Assert.Throws(typeof(ArgumentException), Create_WorkingHours_StartLunchSameSomEndTime);
            Assert.Throws(typeof(ArgumentException), Create_WorkingHours_With_LongLunch);
            Assert.Throws(typeof(ArgumentException), Create_WorkingHours_With_Invalid_MultiplicityStartTime);
            Assert.Throws(typeof(ArgumentException), Create_WorkingHours_With_Invalid_MultiplicityEndtTime);
            Assert.Throws(typeof(ArgumentException), Create_WorkingHours_With_Invalid_MultiplicityLunchDuration);
            Assert.Throws(typeof(ArgumentException), Create_WorkingHours_With_Invalid_MultiplicityStartLunchTime);
        }
        private void Create_Working_Hours_StartGreaterThanEnd()
        {
            DateTime startTime = new DateTime(2000, 1, 1, 7, 0, 0);         //7:00  START
            DateTime endTime = new DateTime(2000, 1, 1, 6, 30, 0);          //6:30  END 
            DateTime startLunchTime = new DateTime(2000, 1, 1, 12, 0, 0);   //12:00 
            TimeSpan lunchDuration = new TimeSpan(0, 30, 0);                //30 min
            WorkingHours wh = new WorkingHours(startTime, endTime, startLunchTime, lunchDuration);
        }
        private void Create_WorkingHours_StartLunchSameSomEndTime()
        {
            DateTime starTime = new DateTime(2000, 1, 1, 7, 0, 0);          //7:00
            DateTime endTime = new DateTime(2000, 1, 1, 15, 30, 0);         //15:30
            DateTime starLunchTime = new DateTime(2000, 1, 1, 15, 30, 0);   //15:30 START LUNCH
            TimeSpan lunchDuration = new TimeSpan(0, 30, 0);                //30 min
            WorkingHours wh = new WorkingHours(starTime, endTime, starLunchTime, lunchDuration);
        }
        private void Create_WorkingHours_With_LongLunch()
        {
            DateTime starTime = new DateTime(2000, 1, 1, 7, 0, 0);          //7:00
            DateTime endTime = new DateTime(2000, 1, 1, 15, 30, 0);         //15:30
            DateTime starLunchTime = new DateTime(2000, 1, 1, 14, 00, 0);   //14:00 START LUNCH
            TimeSpan lunchDuration = new TimeSpan(0, 90, 0);                //90 min
            WorkingHours wh = new WorkingHours(starTime, endTime, starLunchTime, lunchDuration);
        }

        private void Create_WorkingHours_With_Invalid_MultiplicityStartTime()
        {
            DateTime starTime = new DateTime(2000, 1, 1, 7, 2, 0);          //7:02
            DateTime endTime = new DateTime(2000, 1, 1, 15, 30, 0);         //15:30
            DateTime starLunchTime = new DateTime(2000, 1, 1, 12, 00, 0);   //12:00 START LUNCH
            TimeSpan lunchDuration = new TimeSpan(0, 30, 0);                //30 min
            WorkingHours wh = new WorkingHours(starTime, endTime, starLunchTime, lunchDuration);
        }
        private void Create_WorkingHours_With_Invalid_MultiplicityEndtTime()
        {
            DateTime starTime = new DateTime(2000, 1, 1, 7, 0, 0);          //7:00
            DateTime endTime = new DateTime(2000, 1, 1, 15, 36, 0);         //15:36
            DateTime starLunchTime = new DateTime(2000, 1, 1, 12, 00, 0);   //12:00 START LUNCH
            TimeSpan lunchDuration = new TimeSpan(0, 30, 0);                //30 min
            WorkingHours wh = new WorkingHours(starTime, endTime, starLunchTime, lunchDuration);
        }
        private void Create_WorkingHours_With_Invalid_MultiplicityStartLunchTime()
        {
            DateTime starTime = new DateTime(2000, 1, 1, 7, 0, 0);          //7:00
            DateTime endTime = new DateTime(2000, 1, 1, 15, 30, 0);         //15:30
            DateTime starLunchTime = new DateTime(2000, 1, 1, 12, 01, 0);   //12:01 START LUNCH
            TimeSpan lunchDuration = new TimeSpan(0, 30, 0);                //30 min
            WorkingHours wh = new WorkingHours(starTime, endTime, starLunchTime, lunchDuration);
        }
        private void Create_WorkingHours_With_Invalid_MultiplicityLunchDuration()
        {
            DateTime starTime = new DateTime(2000, 1, 1, 7, 0, 0);          //7:00
            DateTime endTime = new DateTime(2000, 1, 1, 15, 30, 0);         //15:30
            DateTime starLunchTime = new DateTime(2000, 1, 1, 12, 00, 0);   //12:00 START LUNCH
            TimeSpan lunchDuration = new TimeSpan(0, 39, 0);                //39 min
            WorkingHours wh = new WorkingHours(starTime, endTime, starLunchTime, lunchDuration);
        }

    }
}
