using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManager
{
    public class Customer
    {
        public int Id;
        public string Name;
        public string Address;
        public string Phone;
        public string Fax;
        public string Zipcode;
        public string Email;
        public string Remark;
        public override string ToString()
        { 
            string returnvalue;
            returnvalue = "Id:" + Id.ToString() + "\r\n";
            returnvalue += "Name:" + Name + "\r\n";
            returnvalue += "Address:" + Address + "\r\n";
            returnvalue += "Phone:" + Phone + "\r\n";
            returnvalue += "Fax:" + Fax + "\r\n";
            returnvalue += "Zipcode:" + Zipcode + "\r\n";
            returnvalue += "Email:" + Email + "\r\n";
            returnvalue += "Remark:" + Remark;
            return returnvalue;
        }
    }
}
