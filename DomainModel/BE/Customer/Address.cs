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
        [DataType(DataType.Text)]
        public string StreetName { get; set; }

        [Required]
        [MinLength(1),MaxLength(10)]
        public string StreetNumber { get; set; }

        [Required]
        [MaxLength(30)]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [Required]
        [MinLength(4),MaxLength(4)]
        [DataType(DataType.PostalCode)]
        public int ZipCode { get; set; }
    }
}