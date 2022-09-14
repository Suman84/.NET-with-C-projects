using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class updatePersonRequest
    {
        public String Name { get; set; } = string.Empty; 
        public int Age { get; set; }
        public long Phonenumber { get; set; }
        public String Email { get; set; } = String.Empty;
    }
}
