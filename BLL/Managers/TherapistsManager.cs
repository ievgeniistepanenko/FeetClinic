using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.BE;
using BE.BE.Treatments;
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

        public override Therapist Create(Therapist entity)
        {
            if (entity.Treatments != null && entity.Treatments.Any())
            {
                foreach (Treatment treatment in entity.Treatments)
                {
                    Facade.Treatments.AttachIfDetached(treatment);
                }
            }
            return base.Create(entity);
        }

    }
}
