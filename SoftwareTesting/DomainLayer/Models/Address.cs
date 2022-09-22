namespace DomainLayer.Models
{
    public class Address : BaseEntity
    {
        public string Country { get; set; }
        public string City { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
