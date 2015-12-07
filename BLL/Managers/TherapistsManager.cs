using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.BE;
using DomainModel.BE;
using FeetClinic_DAL.Abstarct;

namespace BLL.Managers
{
    public class TherapistsManager : AbstractManager<Therapist>
    {
        public TherapistsManager()
        {
            Repository = Facade.Therapist;
        }

        protected override IRepository<Therapist> GetRepository()
        {
            return Repository;
        }
    }
}
