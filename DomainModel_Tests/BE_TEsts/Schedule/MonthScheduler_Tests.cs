using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.BE;
using BE.BE.Schedule;
using BE.Interfaces;
using Moq;
using NUnit.Core;
using NUnit.Framework;

namespace DomainModel_Tests.BE_TEsts.Schedule
{
    [TestFixture]
    class MonthScheduler_Tests
    {
        private List<DateTime> _workingDays;
        private List<DateTime> _holidayDays;
        private List<IDayAgenda> _agendas;
        private int year = 2000;
        private int month = 1;
        private int hour = 0;
        private int min = 0;
        private int sec = 0;

        [SetUp]
        public void SetUp()
        {
            _workingDays = new List<DateTime>
            {
                new DateTime(year,month,1,hour,min,sec),
                new DateTime(year,month,2,hour,min,sec),
                new DateTime(year,month,3,hour,min,sec),
                new DateTime(year,month,4,hour,min,sec),
                new DateTime(year,month,5,hour,min,sec)
            };
            _holidayDays = new List<DateTime>
            {
          
                new DateTime(year,month,6,hour,min,sec),
                new DateTime(year,month,7,hour,min,sec)
            };
            _agendas = new List<IDayAgenda>();


        }
        [TearDown]
        public void TearDown()
        {
            
        }
        #region Constructor test
        [Test]
        public void Can_Create_Month_Scheduler()
        {
            
            IMonthScheduler scheduler = new MonthScheduler(1,_workingDays,_holidayDays,_agendas);
            Assert.AreEqual(month,scheduler.Month);
            Assert.AreEqual(_workingDays.Count,scheduler.WorkingDays.Count);
            Assert.AreEqual(_holidayDays.Count,scheduler.HolidayDays.Count);
            Assert.AreEqual(0,scheduler.GetAgendas().Count);
        }

        [Test]
        [ExpectedException(typeof (ArgumentException))]
        public void Can_Not_Create_With_Null_Parameter()
        {
            new MonthScheduler(month, _workingDays, _holidayDays, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Can_not_create_month_scheduler_if_workingdays_equals_holidays()
        {
            _holidayDays.Add(new DateTime(year, month, 5, hour, min, sec));
            new MonthScheduler(month, _workingDays, _holidayDays, _agendas);

        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Can_not_create_month_scheduler_if_holidayDays_equals_agendas()
        {
            Mock<IDayAgenda> mock = new Mock<IDayAgenda>();
            mock.Setup(da => da.Date).Returns(new DateTime(year, month, 6, hour, min, sec));
            _agendas.Add(mock.Object);    

            new MonthScheduler(month, _workingDays, _holidayDays, _agendas);
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Can_not_create_month_scheduler_with_Invalid_Month_In_Holidays()
        {
            _holidayDays.Add(new DateTime(year, 2, 10, hour, min, sec));
            new MonthScheduler(month, _workingDays, _holidayDays, _agendas);
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Can_not_create_month_scheduler_with_Invalid_Month_In_Workings()
        {
            _workingDays.Add(new DateTime(year, 2, 6, hour, min, sec));
            new MonthScheduler(month, _workingDays, _holidayDays, _agendas);
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Can_not_create_month_scheduler_with_Invalid_Month_In_Agendas()
        {
            Mock<IDayAgenda> mock = new Mock<IDayAgenda>();
            mock.Setup(da => da.Date).Returns(new DateTime(year, 2, 6, hour, min, sec));
            _agendas.Add(mock.Object);

            new MonthScheduler(month, _workingDays, _holidayDays, _agendas);
        }
#endregion

        [Test]
        public void Can_Get_All_WorkingDays()
        {
            IMonthScheduler scheduler = new MonthScheduler(month,_workingDays,_holidayDays,_agendas);

            Assert.AreEqual(5,scheduler.WorkingDays.Count);
            Assert.IsTrue(scheduler.WorkingDays.Contains(new DateTime(year, month, 1, hour, min, sec)));
            
        }
        [Test]
        public void Can_Get_All_HolidayDays()
        {
            IMonthScheduler scheduler = new MonthScheduler(month, _workingDays, _holidayDays, _agendas);

            Assert.AreEqual(2, scheduler.HolidayDays.Count);
            Assert.IsTrue(scheduler.HolidayDays.Contains(new DateTime(year, month, 6, hour, min, sec)));

        }

        [Test]
        public void Can_Get_One_Agenda_With_Date()
        {
            Mock<IDayAgenda> mock1 = new Mock<IDayAgenda>();
            mock1.Setup(da => da.Date).Returns(new DateTime(year, month, 1, hour, min, sec));
            mock1.Setup(da => da.Bookings).Returns(new List<Booking>());
            Mock<IDayAgenda> mock2 = new Mock<IDayAgenda>();
            mock2.Setup(da => da.Date).Returns(new DateTime(year, month, 5, hour, min, sec));

            _agendas.Add(mock1.Object);
            _agendas.Add(mock2.Object);
            
            IMonthScheduler scheduler = new MonthScheduler(month,_workingDays,_holidayDays,_agendas);
            IDayAgenda agenda = scheduler.GetAgenda(new DateTime(year, month, 1, hour, min, sec));

            Assert.IsNotNull(agenda);
            Assert.AreEqual(0,agenda.Bookings.Count);
        }
        [Test]
        public void Can_Get_Null_Object_If_Date_Not_Exist()
        {
            Mock<IDayAgenda> mock1 = new Mock<IDayAgenda>();
            mock1.Setup(da => da.Date).Returns(new DateTime(year, month, 1, hour, min, sec));
            Mock<IDayAgenda> mock2 = new Mock<IDayAgenda>();
            mock2.Setup(da => da.Date).Returns(new DateTime(year, month, 5, hour, min, sec));

            _agendas.Add(mock1.Object);
            _agendas.Add(mock2.Object);

            IMonthScheduler scheduler = new MonthScheduler(month, _workingDays, _holidayDays, _agendas);
            IDayAgenda agenda = scheduler.GetAgenda(new DateTime(year, month, 2, hour, min, sec));

            Assert.IsNull(agenda);
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Can_Not_Get_Scheduler_If_Invalid_Month()
        {
            Mock<IDayAgenda> mock1 = new Mock<IDayAgenda>();
            mock1.Setup(da => da.Date).Returns(new DateTime(year, month, 1, hour, min, sec));
            Mock<IDayAgenda> mock2 = new Mock<IDayAgenda>();
            mock2.Setup(da => da.Date).Returns(new DateTime(year, month, 5, hour, min, sec));

            _agendas.Add(mock1.Object);
            _agendas.Add(mock2.Object);

            IMonthScheduler scheduler = new MonthScheduler(month, _workingDays, _holidayDays, _agendas);
            IDayAgenda agenda = scheduler.GetAgenda(new DateTime(year, month + 1, 2, hour, min, sec));

        }

        [Test]
        public void Can_Get_Agendas_In_Given_Period()
        {
            Mock<IDayAgenda> mock1 = new Mock<IDayAgenda>();
            mock1.Setup(da => da.Date).Returns(new DateTime(year, month, 2, hour, min, sec));
            Mock<IDayAgenda> mock2 = new Mock<IDayAgenda>();
            mock2.Setup(da => da.Date).Returns(new DateTime(year, month, 5, hour, min, sec));
            Mock<IDayAgenda> mock3 = new Mock<IDayAgenda>();
            mock3.Setup(da => da.Date).Returns(new DateTime(year, month, 8, hour, min, sec));

            _agendas.Add(mock1.Object);
            _agendas.Add(mock2.Object);
            _agendas.Add(mock3.Object);

            IMonthScheduler scheduler = new MonthScheduler(month, _workingDays, _holidayDays, _agendas);

            List<IDayAgenda> agendas = scheduler.GetAgendas(new DateTime(year, month, 2, hour, min, sec),
                new DateTime(year, month, 5, hour, min, sec));
            Assert.AreEqual(2,agendas.Count);

            agendas = scheduler.GetAgendas(new DateTime(year, month, 9, hour, min, sec),
                new DateTime(year, month, 12, hour, min, sec));
            Assert.AreEqual(0,agendas.Count);
        }
        [Test]
        public void Can_Get_Agendas_From_Given_Date()
        {
            Mock<IDayAgenda> mock1 = new Mock<IDayAgenda>();
            mock1.Setup(da => da.Date).Returns(new DateTime(year, month, 2, hour, min, sec));
            Mock<IDayAgenda> mock2 = new Mock<IDayAgenda>();
            mock2.Setup(da => da.Date).Returns(new DateTime(year, month, 5, hour, min, sec));
            Mock<IDayAgenda> mock3 = new Mock<IDayAgenda>();
            mock3.Setup(da => da.Date).Returns(new DateTime(year, month, 8, hour, min, sec));

            _agendas.Add(mock1.Object);
            _agendas.Add(mock2.Object);
            _agendas.Add(mock3.Object);

            IMonthScheduler scheduler = new MonthScheduler(month, _workingDays, _holidayDays, _agendas);

            List<IDayAgenda> agendas = scheduler.GetAgendas(new DateTime(year, month, 3, hour, min, sec));
            Assert.AreEqual(2,agendas.Count);

            agendas = scheduler.GetAgendas(new DateTime(year, month, 9, hour, min, sec));
            Assert.AreEqual(0,agendas.Count);
        }

        [Test]
        public void Can_Get_Agendas_For_Given_Dates()
        {
             
            Mock<IDayAgenda> mock1 = new Mock<IDayAgenda>();
            DateTime date1 = new DateTime(year, month, 2, hour, min, sec);
            mock1.Setup(da => da.Date).Returns(date1);
            Mock<IDayAgenda> mock2 = new Mock<IDayAgenda>();
            DateTime date2 = new DateTime(year, month, 5, hour, min, sec);
            mock2.Setup(da => da.Date).Returns(date2);
            Mock<IDayAgenda> mock3 = new Mock<IDayAgenda>();
            DateTime date3 = new DateTime(year, month, 8, hour, min, sec);
            mock3.Setup(da => da.Date).Returns(date3);

            _agendas.Add(mock1.Object);
            _agendas.Add(mock2.Object);
            _agendas.Add(mock3.Object);

            IMonthScheduler scheduler = new MonthScheduler(month, _workingDays, _holidayDays, _agendas);

            DateTime date4 = date3.AddDays(1);
            List<DateTime> dates = new List<DateTime>();
            dates.Add(date4);

            List<IDayAgenda> agendas = scheduler.GetAgendas(dates);
            Assert.AreEqual(0,agendas.Count);

            dates.Add(date1);
            dates.Add(date3);

            agendas = scheduler.GetAgendas(dates);
            Assert.AreEqual(2, agendas.Count);
        }

        [Test]
        public void Can_Get_All_Agendas()
        {
            Mock<IDayAgenda> mock1 = new Mock<IDayAgenda>();
            DateTime date1 = new DateTime(year, month, 2, hour, min, sec);
            mock1.Setup(da => da.Date).Returns(date1);
            Mock<IDayAgenda> mock2 = new Mock<IDayAgenda>();
            DateTime date2 = new DateTime(year, month, 5, hour, min, sec);
            mock2.Setup(da => da.Date).Returns(date2);
            Mock<IDayAgenda> mock3 = new Mock<IDayAgenda>();
            DateTime date3 = new DateTime(year, month, 8, hour, min, sec);
            mock3.Setup(da => da.Date).Returns(date3);

            _agendas.Add(mock1.Object);
            _agendas.Add(mock2.Object);
            _agendas.Add(mock3.Object);

            IMonthScheduler scheduler = new MonthScheduler(month, _workingDays, _holidayDays, _agendas);

            Assert.AreEqual(3,scheduler.GetAgendas().Count);
        }

    }
}
