using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BLL.Interfaces;

namespace DomainModel.BE
{
    public class Treatment : IEntity
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public TimeSpan Duration { get; set; }

        public TreatmentType TreatmentType { get; set; }
        public List<Therapist> Therapists { get; set; }

        public int GetId()
        {
            return Id;
        }
    }
}
