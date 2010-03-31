using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManager
{
    public class User
    {
        public int Id;
        public string UserId;
        public string UserName;
        public string UserLevel;
        public string UserInfo;
        public string Remark;
        public string Password;
        public override string ToString()
        {
            string returnvalue;
            returnvalue = "Id:" + Id.ToString() + "\r\n";
            returnvalue += "UserId:" + UserId + "\r\n";
            returnvalue += "UserName:" + UserName + "\r\n";
            returnvalue += "UserLevel:" + UserLevel + "\r\n";
            returnvalue += "UserInfo:" + UserInfo + "\r\n";
            returnvalue += "Remark:" + Remark + "\r\n";
            returnvalue += "Password:" + Password + "\r\n";
            return returnvalue;
        }
    }
}
