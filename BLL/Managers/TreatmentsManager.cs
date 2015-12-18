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

        public override Treatment Create(Treatment entity)
        {
            if (entity.Therapists !=null && entity.Therapists.Any())
            {
                foreach (Therapist therapist in entity.Therapists)
                {
                    Facade.Therapist.AttachIfDetached(therapist);
                }
            }
            if (entity.TreatmentType != null)
            {
                Facade.TreamentTypes.AttachIfDetached(entity.TreatmentType);
            }
            return base.Create(entity);
        }
    }
}
