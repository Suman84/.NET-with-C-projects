using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Person
    {
        public Guid ID { get; set; }
        public String Name { get; set; } = String.Empty;
        public int Age  { get; set; }
        public long Phonenumber { get; set; }
        public String Email { get; set; } = String.Empty;
    }
}
