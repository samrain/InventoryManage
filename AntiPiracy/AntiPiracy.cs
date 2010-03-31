using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Security.Cryptography;

namespace InventoryManager
{
    public class AntiPiracy
    {
        //得到新的序列号
        public AntiPiracy()
        {            
        }

        //得到新的序列号
        public string  GenerateSerialNo(string licence)
        {
            return SHA1(licence);            
        }

        //Base64加密
        public string GetSerialNo()
        {
            byte[] bytes = Encoding.Default.GetBytes(GetComputerNo());
            return Convert.ToBase64String(bytes);
        }

        //Base64解密
        public string DecodeSerialNo(string serialno)
        {
            byte[] bytes = Convert.FromBase64String(serialno);
            return Encoding.Default.GetString(bytes); ;
        }

        //得到当前机器的编号
        public string GetComputerNo()
        {
            return "CPUID:" + getCpuId() + "|HDid:" + getHardDiskId() + "|mac:" + getMac() + "|MBId:" + getMBId();
             
        }

        //获取CPU编号
        private string getCpuId()
        {
            string strID = null;
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();             
            foreach( ManagementObject mo in moc )
            {
                if (mo["ProcessorId"] != null)
                {
                    strID = mo["ProcessorId"].ToString();
                    break; 
                }
                
            }
            return strID;
        }

        //获取硬盘序列号
        private string getHardDiskId()
        {
            string HDid = null;
            ManagementClass mcHD = new ManagementClass("win32_logicaldisk");
            ManagementObjectCollection mocHD = mcHD.GetInstances();
            foreach (ManagementObject m in mocHD)
            {
                if (m["DeviceID"].ToString() == "C:")
                {
                    HDid = m["VolumeSerialNumber"].ToString();
                    break;
                }
            }
            return HDid;
        }
        //获取网卡MAC
        private string getMac()
        {
            string mac = null;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach(ManagementObject mo in moc)
            {
                if((bool)mo["IPEnabled"] == true)
                {
                    mac = mo["MacAddress"].ToString();
                    break;
                }
            } 
            return mac;
        }
        //获取主板序列号
        private string getMBId()
        {
            string MBId = null;
            ManagementClass mc = new ManagementClass("Win32_BaseBoard");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach(ManagementObject mo in moc)
            {
                MBId = mo["SerialNumber"].ToString();
                break;
            } 
            return MBId;
        }

        //SHA算法
        private string SHA1(string strPlain)
        {
            SHA1Managed sha1 = new SHA1Managed();
            string strHash = string.Empty;
            byte[] btHash = sha1.ComputeHash(UnicodeEncoding.Unicode.GetBytes(strPlain));
            for (int i = 0; i < btHash.Length; i++)
            {
                strHash = strHash + Convert.ToString(btHash[i], 16);
            }
            return strHash;
        }
    }
}