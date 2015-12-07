using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BE;
using DomainModel.BE.Treatments;
using FeetClinic_DAL.Abstarct;

namespace BLL.Managers
{
    public class TreatmentsManager : AbstractManager<Treatment>
    {
        public TreatmentsManager()
        {
            Repository = Facade.Treatments;
        }

        protected override IRepository<Treatment> GetRepository()
        {
            return Repository;
        }
    }
}
