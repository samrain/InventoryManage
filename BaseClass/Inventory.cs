using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManager
{
    public class Inventory
    {
        public int Id;
        public string InventoryId;
        public int ProductId;        
        public int CustomerId;
        public int Boxs;
        public int Counts;
        public double Price;
        public string InventoryType;
        public DateTime Day;
        public string UserId;
        public string ProductSn;
        public string ProductName;
        public string CustomerName;

        public override string ToString()
        {
            string returnvalue;
            returnvalue = "Id:" + Id.ToString() + "\r\n";
            returnvalue += "InventoryId:" + InventoryId + "\r\n";
            returnvalue += "ProductId:" + ProductId.ToString() + "\r\n";
            returnvalue += "CustomerId:" + CustomerId.ToString() + "\r\n";
            returnvalue += "Boxs:" + Boxs.ToString() + "\r\n";
            returnvalue += "Counts:" + Counts.ToString() + "\r\n";
            returnvalue += "Price:" + Price.ToString() + "\r\n";
            returnvalue += "InventoryType:" + InventoryType + "\r\n";
            returnvalue += "Day:" + Day.ToString() + "\r\n";
            returnvalue += "UserId:" + UserId + "\r\n";
            returnvalue += "ProductSn:" + ProductSn + "\r\n";
            returnvalue += "ProductName:" + ProductName + "\r\n";
            returnvalue += "CustomerName:" + CustomerName;
            return returnvalue;
        }
    }
}
