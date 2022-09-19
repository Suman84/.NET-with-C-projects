using DomainLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomersProductsWebApp.Models
{
	public class UpdateCustomerViewModel
	{
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PaymentType { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public long PhoneNumber { get; set; } = 0;
        public int PID { get; set; }
        //public ICollection<Product> Products { get; set; }
        [ForeignKey("PID")]
        public Product product { get; set; }
    }
}
