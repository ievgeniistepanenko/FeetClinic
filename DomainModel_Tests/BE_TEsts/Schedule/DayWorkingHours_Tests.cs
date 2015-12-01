using System;
using DomainModel.BE.Schedule;
using NUnit.Core;
using NUnit.Framework;

namespace DomainModel_Tests.BE_TEsts.Schedule
{
    [TestFixture]
    class DayWorkingHours_Tests
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
            Time startTime = new Time(7,0);         //7:00  START
            Time endTime = new Time(6,30);          //6:30  END 
            Time startLunchTime = new Time(12, 0);   //12:00 
            TimeSpan lunchDuration = new TimeSpan(0, 30, 0);                //30 min
            DayWorkingHours wh = new DayWorkingHours(startTime, endTime, startLunchTime, lunchDuration);
        }
        private void Create_WorkingHours_StartLunchSameSomEndTime()
        {
            Time starTime = new Time(7, 0);          //7:00
            Time endTime = new Time(15,30);         //15:30
            Time starLunchTime = new Time(15, 3);   //15:30 START LUNCH
            TimeSpan lunchDuration = new TimeSpan(0, 30, 0);                //30 min
            DayWorkingHours wh = new DayWorkingHours(starTime, endTime, starLunchTime, lunchDuration);
        }
        private void Create_WorkingHours_With_LongLunch()
        {
            Time starTime = new Time(7, 0);          //7:00
            Time endTime = new Time(15, 30);         //15:30
            Time starLunchTime = new Time(14, 00);   //14:00 START LUNCH
            TimeSpan lunchDuration = new TimeSpan(0, 90, 0);                //90 min
            DayWorkingHours wh = new DayWorkingHours(starTime, endTime, starLunchTime, lunchDuration);
        }

        private void Create_WorkingHours_With_Invalid_MultiplicityStartTime()
        {
            Time starTime = new Time(7, 2);          //7:02
            Time endTime = new Time(15, 30);         //15:30
            Time starLunchTime = new Time(12, 0);   //12:00 START LUNCH
            TimeSpan lunchDuration = new TimeSpan(0, 30, 0);                //30 min
            DayWorkingHours wh = new DayWorkingHours(starTime, endTime, starLunchTime, lunchDuration);
        }
        private void Create_WorkingHours_With_Invalid_MultiplicityEndtTime()
        {
            Time starTime = new Time( 7, 0);          //7:00
            Time endTime = new Time( 15, 36);         //15:36
            Time starLunchTime = new Time( 12, 0);   //12:00 START LUNCH
            TimeSpan lunchDuration = new TimeSpan(0, 30, 0);                //30 min
            DayWorkingHours wh = new DayWorkingHours(starTime, endTime, starLunchTime, lunchDuration);
        }
        private void Create_WorkingHours_With_Invalid_MultiplicityStartLunchTime()
        {
            Time starTime = new Time(7, 0);          //7:00
            Time endTime = new Time(15, 30);         //15:30
            Time starLunchTime = new Time(12, 1);   //12:01 START LUNCH
            TimeSpan lunchDuration = new TimeSpan(0, 30, 0);                //30 min
            DayWorkingHours wh = new DayWorkingHours(starTime, endTime, starLunchTime, lunchDuration);
        }
        private void Create_WorkingHours_With_Invalid_MultiplicityLunchDuration()
        {
            Time starTime = new Time(7, 0);          //7:00
            Time endTime = new Time(15, 30);         //15:30
            Time starLunchTime = new Time(12, 0);   //12:00 START LUNCH
            TimeSpan lunchDuration = new TimeSpan(0, 39, 0);                //39 min
            DayWorkingHours wh = new DayWorkingHours(starTime, endTime, starLunchTime, lunchDuration);
        }

    }
}
