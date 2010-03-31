using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManager
{
    public class Product
    {
        public int Id;
        public string ProductSn;
        public string ProductName;
        public string Remark;
        public int UnitPerBox;
        public override string ToString()
        {
            string returnvalue;
            returnvalue = "Id:" + Id.ToString() + "\r\n";
            returnvalue += "ProductSn:" + ProductSn + "\r\n";
            returnvalue += "ProductName:" + ProductName + "\r\n";
            returnvalue += "Remark:" + Remark + "\r\n";
            returnvalue += "UnitPerBox:" + UnitPerBox.ToString();
            return returnvalue;
        }
    }
}
