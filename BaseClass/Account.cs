using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManager
{
    public class Account
    {
        public int Id;
        public int CustomerId;
        public DateTime Day;
        public decimal InMoney;
        public override string ToString()
        {
            string returnvalue;
            returnvalue = "Id:" + Id.ToString() + "\r\n";
            returnvalue += "CustomerId:" + CustomerId.ToString() + "\r\n";
            returnvalue += "Day:" + Day.ToString() + "\r\n";
            returnvalue += "InMoney:" + InMoney.ToString();
            return returnvalue;
        }
    }
}
