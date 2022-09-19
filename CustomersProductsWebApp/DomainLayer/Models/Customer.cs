using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Customer
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
