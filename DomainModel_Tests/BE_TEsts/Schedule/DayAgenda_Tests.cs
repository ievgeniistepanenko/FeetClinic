using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BE;
using DomainModel.BE.Schedule;
using DomainModel.Interfaces;
using Moq;
using NUnit.Framework;

namespace DomainModel_Tests.BE_TEsts.Schedule
{
    [TestFixture]
    class DayAgenda_Tests
    {
        #region constructor tests
   
        [Test]
        public void Can_Create_Right_Antal_TimeSlots()
        {
            IDayWorkingHours wh = new DayWorkingHours(new Time(8,0), new Time(15, 25),
                new Time(11,30), new TimeSpan(0,0,30,0));
            DayAgenda agenda = new DayAgenda(new DateTime(2000,1,1), wh);

            int amounOfTimeSlots = agenda.TimeSlots.Count;

            Assert.AreEqual(30,amounOfTimeSlots);
            
        }

        [Test]
        public void Can_Create_TimeSlots_With_Unavailable_Slots_For_Lunch()
        {
            DayWorkingHours wh = new DayWorkingHours(new Time(8, 0), new Time(15, 25),
                new Time(11, 30), new TimeSpan(0, 0, 30, 0));
            DayAgenda agenda = new DayAgenda(new DateTime(2000, 1, 1), wh);
            
            Assert.IsTrue(agenda.TimeSlots.Find(ts => ts.Number == 14).IsAvailable);
            Assert.IsFalse(agenda.TimeSlots.Find(ts => ts.Number == 15).IsAvailable);
            Assert.IsFalse(agenda.TimeSlots.Find(ts => ts.Number == 16).IsAvailable);
            Assert.IsTrue(agenda.TimeSlots.Find(ts => ts.Number == 17).IsAvailable);
        }

        [Test]
        public void Can_Create_With_Booking_And_Update_TimeSots()
        {
            DayWorkingHours wh = new DayWorkingHours(new Time(8, 0), new Time(15, 25),
               new Time(11, 30), new TimeSpan(0, 0, 30, 0));
            List<Booking> bookings = new List<Booking>
            {
                new Booking() {Treatments = new List<Treatment>()
                {                    new Treatment { Duration = new TimeSpan(0, 0, 50, 0)},
                                     new Treatment {Duration = new TimeSpan(0, 1, 15, 0)}
                },              DateTime = new DateTime(2000,1,1,9,0,0) },
                new Booking() {Treatments = new List<Treatment>
                {                       new Treatment {Duration = new TimeSpan(0, 0, 45, 0)},
                                        new Treatment { Duration = new TimeSpan(0, 1, 0, 0) }},
                                DateTime = new DateTime(2000, 1, 1, 12, 30, 0)}
            };

            DayAgenda agenda = new DayAgenda(new DateTime(2000, 1, 1), wh,bookings);

            Assert.IsTrue(agenda.TimeSlots.Find(ts => ts.Number == 4).IsAvailable);
            Assert.IsFalse(agenda.TimeSlots.Find(ts=>ts.Number == 5).IsAvailable);
            Assert.IsFalse(agenda.TimeSlots.Find(ts => ts.Number == 9).IsAvailable);
            Assert.IsFalse(agenda.TimeSlots.Find(ts => ts.Number == 13).IsAvailable);
            Assert.IsTrue(agenda.TimeSlots.Find(ts => ts.Number == 14).IsAvailable);
            Assert.IsTrue(agenda.TimeSlots.Find(ts => ts.Number == 18).IsAvailable);
            Assert.IsFalse(agenda.TimeSlots.Find(ts => ts.Number == 19).IsAvailable);
            Assert.IsFalse(agenda.TimeSlots.Find(ts => ts.Number == 25).IsAvailable);
            Assert.IsTrue(agenda.TimeSlots.Find(ts => ts.Number == 26).IsAvailable);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Can_Not_Create_Agenda_With_Wrong_Booking_Duration()
        {
            DayWorkingHours wh = new DayWorkingHours(new Time(8, 0), new Time(15, 25),
               new Time(11, 30), new TimeSpan(0, 0, 30, 0));
            List<Booking> bookings = new List<Booking>
            {
                new Booking() {Treatments = new List<Treatment>()
                {                    new Treatment { Duration = new TimeSpan(0, 0, 60, 0)},
                                     new Treatment {Duration = new TimeSpan(0, 1, 35, 0)} //duration 2t 35min
                },              DateTime = new DateTime(2000,1,1,9,0,0) }
            };

            DayAgenda agenda = new DayAgenda(new DateTime(2000, 1, 1), wh, bookings);
        }
#endregion

        [Test]
        public void Can_Add_Booking()
        {
            DayWorkingHours wh = new DayWorkingHours(new Time(8, 0), new Time(15, 25),
               new Time(11, 30), new TimeSpan(0, 0, 30, 0));
            List<Booking> bookings = new List<Booking>
            {
                new Booking() {  Id = 1,
                                Treatments = new List<Treatment>()
                {                    new Treatment { Duration = new TimeSpan(0, 0, 50, 0)},
                                     new Treatment {Duration = new TimeSpan(0, 1, 15, 0)}
                },              DateTime = new DateTime(2000,1,1,9,0,0) },
                new Booking() {Id =2,
                                Treatments = new List<Treatment>
                {                       new Treatment {Duration = new TimeSpan(0, 0, 45, 0)},
                                        new Treatment { Duration = new TimeSpan(0, 1, 0, 0) }},
                                DateTime = new DateTime(2000, 1, 1, 12, 30, 0)}
            };

            DayAgenda agenda = new DayAgenda(new DateTime(2000, 1, 1), wh, bookings);
            Booking booking = new Booking() {Id = 3,
                Treatments = new List<Treatment>()
            {
                new Treatment() {Duration = new TimeSpan(0,0,45,0,0)}
            } , DateTime = new DateTime(2000,1,1,14,15,0)};
            agenda.AddBooking(booking);

            Assert.AreEqual(3,agenda.Bookings.Count);
            Assert.IsFalse(agenda.TimeSlots.Find(ts=>ts.Number == 26).IsAvailable);
            Assert.IsFalse(agenda.TimeSlots.Find(ts => ts.Number == 28).IsAvailable);
            Assert.IsTrue(agenda.TimeSlots.Find(ts => ts.Number == 29).IsAvailable);
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Can_Not_Add_Boking_If_No_Place()
        {
            DayWorkingHours wh = new DayWorkingHours(new Time(8, 0), new Time(15, 25),
               new Time(11, 30), new TimeSpan(0, 0, 30, 0));
            List<Booking> bookings = new List<Booking>
            {
                new Booking() {  Id = 1,
                                Treatments = new List<Treatment>()
                {                    new Treatment { Duration = new TimeSpan(0, 0, 50, 0)},
                                     new Treatment {Duration = new TimeSpan(0, 1, 15, 0)}
                },              DateTime = new DateTime(2000,1,1,9,0,0) },
                new Booking() {Id =2,
                                Treatments = new List<Treatment>
                {                       new Treatment {Duration = new TimeSpan(0, 0, 45, 0)},
                                        new Treatment { Duration = new TimeSpan(0, 1, 0, 0) }},
                                DateTime = new DateTime(2000, 1, 1, 12, 30, 0)}
            };

            DayAgenda agenda = new DayAgenda(new DateTime(2000, 1, 1), wh, bookings);
            Booking booking = new Booking()
            {
                Id = 3,
                Treatments = new List<Treatment>()
            {
                new Treatment() {Duration = new TimeSpan(0,1,5,0,0)}
            },
                DateTime = new DateTime(2000, 1, 1, 8, 0, 0)
            };
            agenda.AddBooking(booking);
        }
        [Test]
        public void Is_Place_For_Booking()
        {
            DayWorkingHours wh = new DayWorkingHours(new Time(8, 0), new Time(15, 25),
               new Time(11, 30), new TimeSpan(0, 0, 30, 0));
            List<Booking> bookings = new List<Booking>
            {
                new Booking() {  Id = 1,
                                Treatments = new List<Treatment>()
                {                    new Treatment { Duration = new TimeSpan(0, 0, 50, 0)},
                                     new Treatment {Duration = new TimeSpan(0, 1, 15, 0)}
                },              DateTime = new DateTime(2000,1,1,9,0,0) },
                new Booking() {Id =2,
                                Treatments = new List<Treatment>
                {                       new Treatment {Duration = new TimeSpan(0, 0, 45, 0)},
                                        new Treatment { Duration = new TimeSpan(0, 1, 0, 0) }},
                                DateTime = new DateTime(2000, 1, 1, 12, 30, 0)}
            };

            DayAgenda agenda = new DayAgenda(new DateTime(2000, 1, 1), wh, bookings);
            Booking booking = new Booking()
            {
                Id = 3,
                Treatments = new List<Treatment>()
            {
                new Treatment() {Duration = new TimeSpan(0,1,15,0,0)} //1time 15min
            }};

            Assert.IsTrue(agenda.IsAvailableForBooking(booking));
            booking.Treatments = new List<Treatment> { new Treatment() { Duration = new TimeSpan(0, 1, 16, 0, 0) } };
            Assert.IsFalse(agenda.IsAvailableForBooking(booking));
        }
        [Test]
        public void Can_Remove_Booking()
        {
            DayWorkingHours wh = new DayWorkingHours(new Time(8, 0), new Time(15, 25),
                 new Time(11, 30), new TimeSpan(0, 0, 30, 0));
            DateTime now = System.DateTime.Now;
            DateTime twoDaysFromNow = now.AddDays(2);
            List<Booking> bookings = new List<Booking>
            {
                new Booking() {  Id = 1,
                                Treatments = new List<Treatment>()
                {                    new Treatment { Duration = new TimeSpan(0, 0, 50, 0)},
                                     new Treatment {Duration = new TimeSpan(0, 1, 15, 0)}
                },              DateTime = new DateTime(twoDaysFromNow.Year,twoDaysFromNow.Month,twoDaysFromNow.Day,9,0,0) },
                new Booking() {Id =2,
                                Treatments = new List<Treatment>
                {                       new Treatment {Duration = new TimeSpan(0, 0, 45, 0)},
                                        new Treatment { Duration = new TimeSpan(0, 1, 0, 0) }},
                                DateTime = new DateTime(twoDaysFromNow.Year,twoDaysFromNow.Month,twoDaysFromNow.Day, 12, 30, 0)}
            };

            DayAgenda agenda = new DayAgenda(new DateTime(now.Year, now.Month, now.Day).AddDays(2), wh, bookings);
            agenda.RemoveBooking(bookings[0]);

            Assert.AreEqual(1,agenda.Bookings.Count);
            Assert.IsTrue(agenda.TimeSlots.Find(ts=>ts.Number==5).IsAvailable);
            Assert.IsTrue(agenda.TimeSlots.Find(ts => ts.Number == 13).IsAvailable);
            Assert.IsTrue(agenda.TimeSlots.Find(ts => ts.Number == 14).IsAvailable);

        }
        [Test]
        public void Can_Remove_Booking_With_Given_Id()
        {
            DayWorkingHours wh = new DayWorkingHours(new Time(8, 0), new Time(15, 25),
                 new Time(11, 30), new TimeSpan(0, 0, 30, 0));
            DateTime now = System.DateTime.Now;
            DateTime twoDaysFromNow = now.AddDays(2);
            List<Booking> bookings = new List<Booking>
            {
                new Booking() {  Id = 1,
                                Treatments = new List<Treatment>()
                {                    new Treatment { Duration = new TimeSpan(0, 0, 50, 0)},
                                     new Treatment {Duration = new TimeSpan(0, 1, 15, 0)}
                },              DateTime = new DateTime(twoDaysFromNow.Year,twoDaysFromNow.Month,twoDaysFromNow.Day,9,0,0) },
                new Booking() {Id =2,
                                Treatments = new List<Treatment>
                {                       new Treatment {Duration = new TimeSpan(0, 0, 45, 0)},
                                        new Treatment { Duration = new TimeSpan(0, 1, 0, 0) }},
                                DateTime = new DateTime(twoDaysFromNow.Year,twoDaysFromNow.Month,twoDaysFromNow.Day, 12, 30, 0)}
            };

            DayAgenda agenda = new DayAgenda(new DateTime(now.Year, now.Month, now.Day).AddDays(2), wh, bookings);
            agenda.RemoveBooking(1);

            Assert.AreEqual(1, agenda.Bookings.Count);
            Assert.IsTrue(agenda.TimeSlots.Find(ts => ts.Number == 5).IsAvailable);
            Assert.IsTrue(agenda.TimeSlots.Find(ts => ts.Number == 13).IsAvailable);
            Assert.IsTrue(agenda.TimeSlots.Find(ts => ts.Number == 14).IsAvailable);

        }

        [Test]
        [ExpectedException(typeof(OperationCanceledException))]
        public void Can_Not_Remove_Booking_24hours_Before()
        {
            DayWorkingHours wh = new DayWorkingHours(new Time(8, 0), new Time(15, 25),
               new Time(11, 30), new TimeSpan(0, 0, 30, 0));
            DateTime now = DateTime.Now;
            DateTime oneDayFromNow = now.AddDays(1);
            List<Booking> bookings = new List<Booking>
            {
                new Booking() {  Id = 1,
                                Treatments = new List<Treatment>()
                {                    new Treatment { Duration = new TimeSpan(0, 0, 50, 0)},
                                     new Treatment {Duration = new TimeSpan(0, 1, 15, 0)}
                },              DateTime = new DateTime(oneDayFromNow.Year,oneDayFromNow.Month,oneDayFromNow.Day,9,0,0) },
                new Booking() {Id =2,
                                Treatments = new List<Treatment>
                {                       new Treatment {Duration = new TimeSpan(0, 0, 45, 0)},
                                        new Treatment { Duration = new TimeSpan(0, 1, 0, 0) }},
                                DateTime = new DateTime(2000, 1, 1, 12, 30, 0)}
            };

            DayAgenda agenda = new DayAgenda(new DateTime(now.Year, now.Month, now.Day), wh, bookings);
            agenda.RemoveBooking(bookings[0]);
        }

    }
}
