using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.BLL.Interfaces;

namespace DomainModel.BE
{
    public class Treatment : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        public TreatmentType TreatmentType { get; set; }
     
    }
}
