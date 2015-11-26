using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BE.Schedule;
using NUnit.Framework;

namespace DomainModel_Tests.BE_TEsts.Schedule
{
    [TestFixture]
    class Time_Tests
    {
        [Test]
        public void Can_Create_Time()
        {
            Time time = new Time(12,30);
            Assert.AreEqual(12,time.Hour);
            Assert.AreEqual(30,time.Minute);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Can_Not_Create_Time_With_Wrong_Hours()
        {
            Time time = new Time(24,0);
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Can_Not_Create_Time_With_Wrong_Minutes()
        {
            Time time = new Time(12,60);
        }

        [Test]
        public void Can_Add_Hours()
        {
            Time time = new Time(12,30);
            time = time.AddHours(1);
            Assert.AreEqual(13,time.Hour);
            Assert.AreEqual(30,time.Minute);
            time = time.AddHours(11);
            Assert.AreEqual(0,time.Hour);
        }

        [Test]
        public void Can_Add_Minutes()
        {
            Time time = new Time(22, 30);
            time = time.AddMinutes(105);
            Assert.AreEqual(0, time.Hour);
            Assert.AreEqual(15, time.Minute);
        }

        [Test]
        public void Can_Add_Time()
        {
            Time t1 = new Time(3,50);
            Time t2 = new Time(1,45);
            Time t3 = t1.Add(t2);
            Assert.AreEqual(5,t3.Hour);
            Assert.AreEqual(35,t3.Minute);
        }

        [Test]
        public void Can_Add_TimeSpan()
        {
            Time t1 = new Time(3, 50);
            TimeSpan timeSpan = new TimeSpan(1,2,15,0);  //1d 2t 15min 0sec
            Time t3 = t1.Add(timeSpan);
            Assert.AreEqual(6, t3.Hour);
            Assert.AreEqual(5, t3.Minute);
        }
        [Test]
        public void Can_Subtract_Time()
        {
            Time t1 = new Time(1,30);
            Time t2 = new Time(2,10);
            Time t3 = t1.Subtract(t2);

            Assert.AreEqual(23,t3.Hour);
            Assert.AreEqual(20,t3.Minute);
        }
        [Test]
        public void Can_Subtract_TimeSpan()
        {
            Time t1 = new Time(0,30);
            TimeSpan timeSpan = new TimeSpan(2,1,45,0);
            Time t3 = t1.Subtract(timeSpan);
            Assert.AreEqual(22, t3.Hour);
            Assert.AreEqual(45, t3.Minute);
        }
        [Test]
        public void Can_Compare_Time()
        {
            Time t1 = new Time(23,30);
            Time t2 = new Time(23,30);
            Time t3 = new Time(12,00);
            Assert.IsTrue(t1.Equals(t2));
            Assert.IsFalse(t1.Equals(t3));
            Assert.IsTrue(t1 > t3);
            Assert.IsTrue(t3 < t1);
            Assert.IsFalse(t1 > t2);
            Assert.IsTrue(t1 >= t2);
            Assert.IsTrue(t2 >= t1);
        }
    }
}
