using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BE.Schedule;
using FeetClinic_DAL.Abstarct;

namespace BLL.Managers
{
    public class DayAgendasManager : AbstractManager<DayAgenda>
    {
        public DayAgendasManager()
        {
            Repository = Facade.DayAgenda;

        }

        protected override IRepository<DayAgenda> GetRepository()
        {
            return Repository;
        }
    }
}
