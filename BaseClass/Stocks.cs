using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManager
{
    public class Stocks
    {
        public int ProductId;
        public int WarehouseId;
        public int Remain;
        public DateTime Lasttime;
        public string ProductSn;
        public string ProductName;

        public override string ToString()
        {
            string returnvalue;
            returnvalue = "ProductId:" + ProductId.ToString() + "\r\n";
            returnvalue += "WarehouseId:" + WarehouseId.ToString() + "\r\n";
            returnvalue += "Remain:" + Remain.ToString() + "\r\n";
            returnvalue += "Lasttime:" + Lasttime.ToString() + "\r\n";
            returnvalue += "ProductSn:" + ProductSn.ToString() + "\r\n";
            returnvalue += "ProductName:" + ProductName.ToString();
            return returnvalue;
        }

    }
}
