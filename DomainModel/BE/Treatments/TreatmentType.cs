using System.Collections.Generic;
using DomainModel.Interfaces;

namespace DomainModel.BE.Treatments
{
    public class TreatmentType : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public virtual List<Treatment> Treatments { get; set; }
    }
}