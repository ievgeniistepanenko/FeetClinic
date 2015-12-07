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
   public class TreatmentTypesManager : AbstractManager<TreatmentType>
    {
       public TreatmentTypesManager()
       {
           Repository = Facade.TreamentTypes;
       }

       protected override IRepository<TreatmentType> GetRepository()
       {
           return Repository;
       }
    }
}
