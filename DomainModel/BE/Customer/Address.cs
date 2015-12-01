namespace DomainModel.BE
{
    public class Address

    {
        public int Id;
        public string StreetName { get; set; }

        public string StreetNumber { get; set; }

        public string City { get; set; }

        public int ZipCode { get; set; }
    }
}