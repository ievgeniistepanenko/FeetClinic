using DomainModel.BLL.Interfaces;

namespace DomainModel.BE
{
    public class Address : IEntity
    {
        public int Id { get; set; }
        public string StreetName { get; set; }

        public string StreetNumber { get; set; }

        public string City { get; set; }

        public int ZipCode { get; set; }
        public int GetId()
        {
            return Id;
        }
    }
}