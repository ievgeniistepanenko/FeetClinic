using System.Collections.Generic;

namespace DomainModel.BE
{
    public class TreatmentType
    {
        public int Id { get; set; }

        public string Name { get; set; }
        private List<Treatment> treatments;

        public TreatmentType()
        {
            treatments = new List<Treatment>();
        }
    }
}