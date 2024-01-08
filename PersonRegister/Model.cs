using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegister
{
    public class Contact
    {
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        public Contact(string? fullName, string? address, string? phoneNumber)
        {
            FullName = fullName;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        public Contact() { }

        public override string ToString()
        {
            return FullName + " | " + Address + " | " + PhoneNumber;
        }
    }

}
