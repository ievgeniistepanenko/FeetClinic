using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BE.Interfaces;

namespace BE.BE.Customer
{
    public class CustomerProfile : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        [MinLength(2),MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2), MaxLength(50)]
        public string LastName { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<Booking> Bookings { get; set; }
        }

}