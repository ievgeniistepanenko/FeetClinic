using System.Collections.Generic;
using DomainModel.BLL.Interfaces;

namespace DomainModel.BE.Treatment
{
    public class TreatmentType : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public virtual List<Treatment> Treatments { get; set; }
    }
}