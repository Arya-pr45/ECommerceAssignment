namespace Assignment1.Models
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }

        public Address(string street, string city, string zipcode, string country)
        {
            Street = street;
            City = city;
            Zipcode = zipcode;
            Country = country;
        }
        public override string ToString()
        {
            return $"{Street}, {City}, {Zipcode}, {Country}";
        }
    }
}
