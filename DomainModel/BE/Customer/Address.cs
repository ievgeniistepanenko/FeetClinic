using DomainModel.BLL.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace DomainModel.BE
{
    public class Address : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(4),MaxLength(30)]
        
        public string StreetName { get; set; }

        [Required]
        [MinLength(1),MaxLength(10)]
        public string StreetNumber { get; set; }

        [Required]
        [MaxLength(30)]
       
        public string City { get; set; }

        [Required]
        [Range(1000,9999)]
        [DataType(DataType.PostalCode)]
        public int ZipCode { get; set; }
    }
}