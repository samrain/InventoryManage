using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Data.Common;

namespace InventoryManager
{
    public class DbManagerForSqlite
    {
        SQLiteConnection con ;
        SQLiteCommand command;
        
        public DbManagerForSqlite()
        {
            con = new SQLiteConnection("Data Source=Inventory.rc");
            con.Open();
            command = con.CreateCommand();            
        }

        //�ر����ݿ⣬�ͷ�����
        public void Close()
        {
            con.Close();
            con.Dispose();
        }

        //���ݴ���ı�������ȡ�����ݱ��¼
        public DataTable ReadTable(string tableName)
        {
            command.CommandText = "SELECT * FROM " + tableName;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable(tableName);
            da.Fill(dt);
            return dt;
        }

        //���ݴ���ı�������ȡ�����ݱ��¼
        public DataTable ReadTableOrderByXXId(string tableName,string xxid)
        {
            command.CommandText = "SELECT * FROM " + tableName + " order by " + xxid;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable(tableName);
            da.Fill(dt);
            return dt;
        }

        //���ݴ���ı�����Ϊ�˲����׼��һ��DataTable
        private DataTable CreateDataTable(string tableName)
        {
            command.CommandText = "SELECT * FROM " + tableName + " limit 1";
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable(tableName);
            da.Fill(dt);
            return dt;
        }

        //ͨ���޸����ݺ���
        private string UpdateTable(DataTable srcTable, string tableName)
        {            
            try
            {                
                command.CommandText = "SELECT * FROM " + tableName;
                SQLiteDataAdapter oda = new SQLiteDataAdapter(command);
                SQLiteCommandBuilder ocb = new SQLiteCommandBuilder(oda);
                oda.InsertCommand = ocb.GetInsertCommand();
                oda.DeleteCommand = ocb.GetDeleteCommand();
                oda.UpdateCommand = ocb.GetUpdateCommand();
                oda.Update(srcTable);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "success";
        }

        //����Customer��id���������������Բ�����ֵ
        public string InsertCustomer(Customer customer)
        {                       
            DataTable dt = new DataTable();
            dt = CreateDataTable("Customer");
            DataRow datarow;
            datarow = dt.NewRow();
            datarow[0] = customer.Id;
            datarow[1] = customer.Name;
            datarow[2] = customer.Address;
            datarow[3] = customer.Phone;
            datarow[4] = customer.Fax;
            datarow[5] = customer.Zipcode;
            datarow[6] = customer.Email;
            datarow[7] = customer.Remark;
            dt.Rows.Add(datarow);
            return UpdateTable(dt, "Customer");           
        }
        //����Customer��
        public string UpdateCustomer(Customer customer)
        {            
            DataTable dt = new DataTable();
            dt = SelectCustomerToDataTable(customer);
            if (dt.Rows.Count == 1)
            {
                dt.Rows[0][0] = customer.Id;
                dt.Rows[0][1] = customer.Name;
                dt.Rows[0][2] = customer.Address;
                dt.Rows[0][3] = customer.Phone;
                dt.Rows[0][4] = customer.Fax;
                dt.Rows[0][5] = customer.Zipcode;
                dt.Rows[0][6] = customer.Email;
                dt.Rows[0][7] = customer.Remark;                    
            }                
            return UpdateTable(dt, "Customer");            
        }
        //�õ����е�Customer���¼
        public DataTable SelectCustomerAllToDataTable()
        {
            return this.ReadTableOrderByXXId("Customer", "Name");
        }

        
        //�õ����е�Customer���¼
        public List<Customer> SelectCustomerAllToList()
        {
            List<Customer> listcustomer = new List<Customer>();
            DataTable dt = new DataTable();
            dt = this.ReadTableOrderByXXId("Customer", "Name");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Customer customer = new Customer();
                customer.Id = Convert.ToInt16(dt.Rows[i][0].ToString());
                customer.Name = dt.Rows[i][1].ToString();
                customer.Address = dt.Rows[i][2].ToString();
                customer.Phone = dt.Rows[i][3].ToString();
                customer.Fax = dt.Rows[i][4].ToString();
                customer.Zipcode = dt.Rows[i][5].ToString();
                customer.Email = dt.Rows[i][6].ToString();
                customer.Remark = dt.Rows[i][7].ToString();
                listcustomer.Add(customer);
            }
            return listcustomer;
        }

        //��ѯĳ��Customer����ϸ��Ϣ
        public DataTable SelectCustomerToDataTable(Customer customer)
        {
            command.CommandText = "SELECT * FROM Customer where Id=" +customer.Id;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("Customer");
            da.Fill(dt);
            return dt;
        }

        //��ѯĳ��Customer����ϸ��Ϣ
        public Customer SelectCustomerToClass(Customer customer)
        {
            command.CommandText = "SELECT * FROM Customer where Id=" + customer.Id;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("Customer");
            da.Fill(dt);

            Customer returnvalue = new Customer();
            if (dt.Rows.Count > 0)
            {
                returnvalue.Id = Convert.ToInt16(dt.Rows[0][0].ToString());
                returnvalue.Name = dt.Rows[0][1].ToString();
                returnvalue.Address = dt.Rows[0][2].ToString();
                returnvalue.Phone = dt.Rows[0][3].ToString();
                returnvalue.Fax = dt.Rows[0][4].ToString();
                returnvalue.Zipcode = dt.Rows[0][5].ToString();
                returnvalue.Email = dt.Rows[0][6].ToString();
                returnvalue.Remark = dt.Rows[0][7].ToString();
            }            
            return returnvalue;
        }

        //У���Ƿ���ͬ����Customer�� true-ͨ�����ԣ�û�����Ƶ�,false-û��ͨ�����ԣ������Ƶ�
        public bool CheckCustomer(Customer customer)
        {            
            command.CommandText = "SELECT * FROM Customer where Name = '" + customer.Name + "'";
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("Customer");
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //�����û�
        public string InsertUser(User user)
        {
            DataTable dt = new DataTable();
            dt = CreateDataTable("User");
            DataRow datarow;
            datarow = dt.NewRow();
            datarow[0] = user.Id;
            datarow[1] = user.UserId.ToLower();
            datarow[2] = user.UserName;
            datarow[3] = user.UserLevel;
            datarow[4] = user.UserInfo;
            datarow[5] = user.Remark;
            datarow[6] = user.Password;
            dt.Rows.Add(datarow);
            return UpdateTable(dt, "User");
        }
        //�޸��û�
        public string UpdateUser(User user)
        {
            DataTable dt = new DataTable();
            dt = SelectUser(user);
            if (dt.Rows.Count == 1)
            {
                dt.Rows[0][0] = user.Id;
                dt.Rows[0][1] = user.UserId;
                dt.Rows[0][2] = user.UserName;
                dt.Rows[0][3] = user.UserLevel;
                dt.Rows[0][4] = user.UserInfo;
                dt.Rows[0][5] = user.Remark;
                dt.Rows[0][6] = user.Password;
            }
            return UpdateTable(dt, "User");
        }
        //��ѯ�������û�
        public DataTable SelectUserAll()
        {
            return ReadTableOrderByXXId("User","UserName");
        }
        //��ѯ��ĳ���û���Ϣ
        public DataTable SelectUser(User user)
        {
            command.CommandText = "SELECT * FROM User where Id=" + user.Id;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("User");
            da.Fill(dt);
            return dt;
        }

        //��ѯ�������û�
        public List<User> SelectUserAllToList()
        {
            List<User> listuser = new List<User>();
            DataTable dt = new DataTable();
            dt = ReadTableOrderByXXId("User", "UserName");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                User user = new User();
                user.Id = Convert.ToInt16(dt.Rows[i][0].ToString());
                user.UserId = dt.Rows[i][1].ToString();
                user.UserName = dt.Rows[i][2].ToString();
                user.UserLevel = dt.Rows[i][3].ToString();
                user.UserInfo = dt.Rows[i][4].ToString();
                user.Remark = dt.Rows[i][5].ToString();
                user.Password = dt.Rows[i][6].ToString();
                listuser.Add(user);
            }
            return listuser;
        }
        //��ѯ��ĳ���û���Ϣ
        public User SelectUserToClass(User user)
        {
            command.CommandText = "SELECT * FROM User where Id = " + user.Id;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("User");
            da.Fill(dt);

            User returnvalue = new User();
            if(dt.Rows.Count > 0)
            {
                returnvalue.Id = Convert.ToInt16(dt.Rows[0][0].ToString());
                returnvalue.UserId = dt.Rows[0][1].ToString();
                returnvalue.UserName = dt.Rows[0][2].ToString();
                returnvalue.UserLevel = dt.Rows[0][3].ToString();
                returnvalue.UserInfo = dt.Rows[0][4].ToString();
                returnvalue.Remark = dt.Rows[0][5].ToString();
                returnvalue.Password = dt.Rows[0][6].ToString();
            }
            
            return returnvalue;
        }

        //��ѯ��ĳ���û���Ϣ
        public User SelectUserByUserId(String userid)
        {
            command.CommandText = "SELECT * FROM User where UserId = '" + userid + "'";
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("User");
            da.Fill(dt);

            User returnvalue = new User();
            if (dt.Rows.Count > 0)
            {
                returnvalue.Id = Convert.ToInt16(dt.Rows[0][0].ToString());
                returnvalue.UserId = dt.Rows[0][1].ToString();
                returnvalue.UserName = dt.Rows[0][2].ToString();
                returnvalue.UserLevel = dt.Rows[0][3].ToString();
                returnvalue.UserInfo = dt.Rows[0][4].ToString();
                returnvalue.Remark = dt.Rows[0][5].ToString();
                returnvalue.Password = dt.Rows[0][6].ToString();
            }
            return returnvalue;
        }

        //У���Ƿ���ͬUserid���û� true-ͨ�����ԣ�û�����Ƶ�,false-û��ͨ�����ԣ������Ƶ�
        public bool CheckUser(User user)
        {
            command.CommandText = "SELECT * FROM User where UserId = '" + user.UserId + "'";
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("User");
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //������Ʒ
        public string InsertProduct(Product product)
        {
            DataTable dt = new DataTable();
            dt = CreateDataTable("Product");
            DataRow datarow;
            datarow = dt.NewRow();
            datarow[0] = product.Id;
            datarow[1] = product.ProductSn;
            datarow[2] = product.ProductName;
            datarow[3] = product.Remark;
            datarow[4] = product.UnitPerBox;
            dt.Rows.Add(datarow);
            return UpdateTable(dt, "Product");
        }

        //������Ʒ�����ڿ�����Ҳ����һ��
        public string InsertProductFull(Product product)
        {
            string returnflag = InsertProduct(product);
            if (returnflag == "success")
            {
                return InsertStocks();
            }
            else
            {
                return returnflag;
            }
            
        }

        //���²�Ʒ
        public string UpdateProduct(Product product)
        {
            DataTable dt = new DataTable();
            dt = SelectProduct(product);
            if (dt.Rows.Count == 1)
            {
                dt.Rows[0][0] = product.Id;
                dt.Rows[0][1] = product.ProductSn;
                dt.Rows[0][2] = product.ProductName;
                dt.Rows[0][3] = product.Remark;
                dt.Rows[0][4] = product.UnitPerBox;
            }
            return UpdateTable(dt, "Product");
        }
        //��ѯ�����в�Ʒ
        public DataTable SelectProductAll()
        {
            return ReadTableOrderByXXId("Product", "ProductSn");
        }
        //��ѯĳ����Ʒ��Ϣ
        public DataTable SelectProduct(Product product)
        {
            command.CommandText = "SELECT * FROM Product where Id = '" + product.Id + "'";
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("Product");
            da.Fill(dt);
            return dt;
        }

        //��ѯ�����в�Ʒ
        public List<Product> SelectProductAllToList()
        {
            List<Product> listproduct = new List<Product>();
            DataTable dt = new DataTable();
            dt = ReadTableOrderByXXId("Product", "ProductSn");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Product returnvalue = new Product();
                returnvalue.Id = Convert.ToInt16(dt.Rows[i][0].ToString());
                returnvalue.ProductSn = dt.Rows[i][1].ToString();
                returnvalue.ProductName = dt.Rows[i][2].ToString();
                returnvalue.Remark = dt.Rows[i][3].ToString();
                returnvalue.UnitPerBox = Convert.ToInt16(dt.Rows[i][4].ToString());
                listproduct.Add(returnvalue);
            }
            return listproduct;
        }
        //��ѯĳ����Ʒ��Ϣ
        public Product SelectProductToClass(Product product)
        {
            command.CommandText = "SELECT * FROM Product where Id = '" + product.Id + "'";
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("Product");
            da.Fill(dt);

            Product returnvalue = new Product();
            if (dt.Rows.Count > 0)
            {
                returnvalue.Id = Convert.ToInt16(dt.Rows[0][0].ToString());
                returnvalue.ProductSn = dt.Rows[0][1].ToString();
                returnvalue.ProductName = dt.Rows[0][2].ToString();
                returnvalue.Remark = dt.Rows[0][3].ToString();
                returnvalue.UnitPerBox = Convert.ToInt16(dt.Rows[0][4].ToString());
            }
            return returnvalue;
        }

        //У���Ƿ��в�Ʒ����һ���ģ� true-ͨ�����ԣ�û�����Ƶ�,false-û��ͨ�����ԣ������Ƶ�
        public bool CheckProduct(Product product)
        {
            command.CommandText = "SELECT * FROM Product where ProductSn = '" + product.ProductSn + "'";
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("Product");
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //�������/�����¼�����Ҹ��Ŀ���
        public string InsertInventoryAndUpdateStocks(List<Inventory> listInventory)
        {
            SQLiteTransaction trans;
            trans = con.BeginTransaction();
            try
            {   
                if (this.InsertInventory(listInventory) == "success")
                {
                    if (this.UpdateStock(listInventory) == "success")
                    {
                        trans.Commit();
                        return "success";
                    }
                    else
                    {
                        trans.Rollback();
                        return "update stocks fail";
                    }
                }
                else
                {
                    trans.Rollback();
                    return "insert into Inventory fail";
                }
            }
            catch
            {                
                trans.Rollback();
                throw;
            }
            
        } 


        //�������/�����¼����I����ʾ��⣬��O����ʾ����
        public string InsertInventory(List<Inventory> listInventory)
        {
            DataTable dt = new DataTable();
            dt = CreateDataTable("Inventory");
            DataRow datarow;
            int i = 0;
            while (i < listInventory.Count)
            {
                datarow = dt.NewRow();
                datarow[0] = listInventory[i].Id;
                datarow[1] = listInventory[i].InventoryId;
                datarow[2] = listInventory[i].ProductId;
                datarow[3] = listInventory[i].CustomerId;
                datarow[4] = listInventory[i].Boxs;
                datarow[5] = listInventory[i].Counts;
                datarow[6] = listInventory[i].Price;
                datarow[7] = listInventory[i].InventoryType;
                datarow[8] = listInventory[i].Day;
                datarow[9] = listInventory[i].UserId;
                datarow[10] = listInventory[i].ProductSn;
                datarow[11] = listInventory[i].ProductName;
                datarow[12] = listInventory[i].CustomerName;
                dt.Rows.Add(datarow);
                i++;
            }
            return UpdateTable(dt, "Inventory");
        }
        //�������/�����¼
        public string UpdateInventory(List<Inventory> listInventory)
        {
            DataTable dt = new DataTable();
            dt = SelectInventory(listInventory);
            for (int i = 0; i < listInventory.Count;i++ )
            {
                dt.Rows[i][0] = listInventory[i].Id;
                dt.Rows[i][1] = listInventory[i].InventoryId;
                dt.Rows[i][2] = listInventory[i].ProductId;
                dt.Rows[i][3] = listInventory[i].CustomerId;
                dt.Rows[i][4] = listInventory[i].Boxs;
                dt.Rows[i][5] = listInventory[i].Counts;
                dt.Rows[i][6] = listInventory[i].Price;
                dt.Rows[i][7] = listInventory[i].InventoryType;
                dt.Rows[i][8] = listInventory[i].Day;
                dt.Rows[i][9] = listInventory[i].UserId;
                dt.Rows[i][10] = listInventory[i].ProductSn;
                dt.Rows[i][11] = listInventory[i].ProductName;
                dt.Rows[i][12] = listInventory[i].CustomerName;

            }
            return UpdateTable(dt, "Inventory");
        }
        //��ѯ���/�����¼
        public DataTable SelectInventory(List<Inventory> listInventory)
        {
            string querysql = "";
            int i = 0;
            while (i < listInventory.Count)
            {
                querysql = querysql + ",'" + listInventory[i].Id + "'";
                i++;
            }
            querysql = querysql.Substring(1);
            command.CommandText = "SELECT * FROM Inventory where Id in (" + querysql + ")";
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("Inventory");
            da.Fill(dt);
            return dt;
        }

        //��ѯ���/�����¼
        public List<Inventory> SelectInventoryForList(List<Inventory> listInventory)
        {
            string querysql = "";
            int j = 0;
            while (j < listInventory.Count)
            {
                querysql = querysql + ",'" + listInventory[j].Id + "'";
                j++;
            }
            querysql = querysql.Substring(1);
            command.CommandText = "SELECT * FROM Inventory where Id in (" + querysql + ")";
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("Inventory");
            da.Fill(dt);

            List<Inventory> listtemp = new List<Inventory>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Inventory temp = new Inventory();
                temp.Id = Convert.ToInt16(dt.Rows[i][0].ToString());
                temp.InventoryId = dt.Rows[i][1].ToString();
                temp.ProductId = Convert.ToInt16(dt.Rows[i][2].ToString());
                temp.CustomerId = Convert.ToInt16(dt.Rows[i][3].ToString());
                temp.Boxs = Convert.ToInt16(dt.Rows[i][4].ToString());
                temp.Counts = Convert.ToInt16(dt.Rows[i][5].ToString());
                temp.Price = Convert.ToDouble(dt.Rows[i][6].ToString());
                temp.InventoryType = dt.Rows[i][7].ToString();
                temp.Day = Convert.ToDateTime(dt.Rows[i][8].ToString());
                temp.UserId = dt.Rows[i][9].ToString();
                temp.ProductSn = dt.Rows[i][10].ToString();
                temp.ProductName = dt.Rows[i][11].ToString();
                temp.CustomerName = dt.Rows[i][12].ToString();
                listtemp.Add(temp);
            }
            return listtemp;
        }

        //�������/�����ʱ�䷶Χ����ѯ���/�����¼
        public DataTable SelectInventoryByTypeAndTime(string type,DateTime startdate,DateTime enddate)
        {
            string querysql = "";
            querysql += " where InventoryType = '" + type + "'";
            querysql += " and day >= '" + startdate.ToString("s") + "'";
            querysql += " and day <= '" + enddate.ToString("s") + "'";

            command.CommandText = "SELECT * FROM Inventory" + querysql;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("Inventory");
            da.Fill(dt);
            return dt;
        }
        //�������/�����ʱ�䷶Χ����ѯ���/�����¼
        public List<Inventory> SelectInventoryByTypeAndTimeToList(string type, DateTime startdate, DateTime enddate)
        {
            string querysql = "";
            querysql += " where InventoryType = '" + type + "'";
            querysql += " and day >= '" + startdate.ToString("s") + "'";
            querysql += " and day <= '" + enddate.ToString("s") + "'";

            command.CommandText = "SELECT * FROM Inventory" + querysql;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("Inventory");
            da.Fill(dt);

            List<Inventory> listInventory = new List<Inventory>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Inventory inventory = new Inventory();
                inventory.Id = Convert.ToInt16(dt.Rows[i][0].ToString());
                inventory.InventoryId = dt.Rows[i][1].ToString();
                inventory.ProductId = Convert.ToInt16(dt.Rows[i][2].ToString());
                inventory.CustomerId = Convert.ToInt16(dt.Rows[i][3].ToString());
                inventory.Boxs = Convert.ToInt16(dt.Rows[i][4].ToString());
                inventory.Counts = Convert.ToInt16(dt.Rows[i][5].ToString());
                inventory.Price = Convert.ToDouble(dt.Rows[i][6]);
                inventory.InventoryType = dt.Rows[i][7].ToString();
                inventory.Day = Convert.ToDateTime(dt.Rows[i][8].ToString());
                inventory.UserId = dt.Rows[i][9].ToString();
                inventory.ProductSn = dt.Rows[i][10].ToString();
                inventory.ProductName = dt.Rows[i][11].ToString();
                inventory.CustomerName = dt.Rows[i][12].ToString();
                listInventory.Add(inventory);
            }
            return listInventory;            
        }

        //����productid��Customerid�����/���⡢��ʱ�䷶Χ����ѯ���/�����¼
        public List<Inventory> SelectInventoryByInventoryAndTimeToList(Inventory condition, DateTime startdate, DateTime enddate)
        {
            string querysql = "";
            querysql += " where InventoryType = '" + condition.InventoryType + "'";
            querysql += " and day >= '" + startdate.ToString("s") + "'";
            querysql += " and day <= '" + enddate.ToString("s") + "'";
            if (condition.ProductId != 0)
            {
                querysql += " and ProductId = " + condition.ProductId;
            }
            if (condition.CustomerId != 0)
            {
                querysql += " and CustomerId = " + condition.CustomerId;
            }

            command.CommandText = "SELECT * FROM Inventory" + querysql;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("Inventory");
            da.Fill(dt);

            List<Inventory> listInventory = new List<Inventory>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Inventory inventory = new Inventory();
                inventory.Id = Convert.ToInt16(dt.Rows[i][0].ToString());
                inventory.InventoryId = dt.Rows[i][1].ToString();
                inventory.ProductId = Convert.ToInt16(dt.Rows[i][2].ToString());
                inventory.CustomerId = Convert.ToInt16(dt.Rows[i][3].ToString());
                inventory.Boxs = Convert.ToInt16(dt.Rows[i][4].ToString());
                inventory.Counts = Convert.ToInt16(dt.Rows[i][5].ToString());
                inventory.Price = Convert.ToDouble(dt.Rows[i][6]);
                inventory.InventoryType = dt.Rows[i][7].ToString();
                inventory.Day = Convert.ToDateTime(dt.Rows[i][8].ToString());
                inventory.UserId = dt.Rows[i][9].ToString();
                inventory.ProductSn = dt.Rows[i][10].ToString();
                inventory.ProductName = dt.Rows[i][11].ToString();
                inventory.CustomerName = dt.Rows[i][12].ToString();
                listInventory.Add(inventory);
            }
            return listInventory;
        }

        //ͳ��һ��ʱ���ڵĳ����¼
        public List<Inventory> CountInventoryByTime(DateTime startdate, DateTime enddate)
        {
            string querysql = "";
            querysql += " where InventoryType = 'O'";
            querysql += " and day >= '" + startdate.ToString("s") + "'";
            querysql += " and day <= '" + enddate.ToString("s") + "'";
            querysql += " group by ProductSn,CustomerName";

            command.CommandText = "select sum(Counts),sum(Boxs),sum(Counts*Price),ProductSn,CustomerName from Inventory" + querysql;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<Inventory> listInventory = new List<Inventory>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Inventory inventory = new Inventory();
                inventory.Counts = Convert.ToInt16(dt.Rows[i][0].ToString());
                inventory.Boxs = Convert.ToInt16(dt.Rows[i][1].ToString());
                inventory.Price = Convert.ToDouble(dt.Rows[i][2]);
                inventory.ProductSn = dt.Rows[i][3].ToString();
                inventory.CustomerName = dt.Rows[i][4].ToString();
                listInventory.Add(inventory);
            }
            return listInventory;
        }

        //���¿��
        public string UpdateStock(List<Inventory> listInventory)
        {            
            //�Ȱ����е�Stocks���г���     
            int productid;
            int remain;
            DataTable dtStocks = new DataTable();
            dtStocks = SelectStocksAllForDataTable();
            int stocksnum = dtStocks.Rows.Count;

            //��Stocks���б�����ÿȡ��һ���ͺ�listInventory������бȽϣ������ͬһ��ProductId��
            //��ô����InventoryType���мӼ�����
            for (int i = 0; i < stocksnum; i++)
            {
                productid = Convert.ToInt16(dtStocks.Rows[i][0].ToString());
                remain = Convert.ToInt16(dtStocks.Rows[i][2].ToString());
                
                for (int j = 0; j < listInventory.Count; j++)
                {
                    if (listInventory[j].ProductId == productid)
                    {
                        if (listInventory[j].InventoryType == "I")
                        {
                            remain += listInventory[j].Counts;
                        }
                        else
                        {
                            remain -= listInventory[j].Counts;
                        }
                    }                    
                }
                dtStocks.Rows[i][2] = remain;
            }
            return UpdateTable(dtStocks, "Stocks");
        }
        //��ѯ��Ŀǰ���п��
        public DataTable SelectStocksAllForDataTable()
        {
            return ReadTableOrderByXXId("Stocks", "ProductSn");
        }

        //��ѯ��Ŀǰ���п��
        public List<Stocks> SelectStocksAllForList()
        {
            DataTable dt = new DataTable();
            dt = ReadTableOrderByXXId("Stocks", "ProductSn");
            List<Stocks> listStocks = new List<Stocks>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Stocks stock = new Stocks();
                stock.ProductId = Convert.ToInt16(dt.Rows[i][0].ToString());
                stock.WarehouseId = Convert.ToInt16(dt.Rows[i][1].ToString());
                stock.Remain = Convert.ToInt16(dt.Rows[i][2].ToString());
                stock.Lasttime = Convert.ToDateTime(dt.Rows[i][3].ToString());
                stock.ProductSn = dt.Rows[i][4].ToString();
                stock.ProductName = dt.Rows[i][5].ToString();
                listStocks.Add(stock);
            }
            return listStocks;
        }

        //�������
        public string InsertStocks()
        {
            command.CommandText = "SELECT max(Id) FROM Product";
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dtMaxId = new DataTable();
            da.Fill(dtMaxId);
            int productid;
            Product productmax = new Product();
            

            if(dtMaxId.Rows.Count>0)
            {
                productid = Convert.ToInt16(dtMaxId.Rows[0][0].ToString());
                productmax.Id = productid;
                productmax = SelectProductToClass(productmax);
            }
            else
            {
                productid = 1;
            }

            DataTable dt = new DataTable();
            dt = CreateDataTable("Stocks");
            DataRow datarow;            
            datarow = dt.NewRow();
            datarow[0] = productmax.Id;
            datarow[1] = 0;
            datarow[2] = 0;
            datarow[3] = DateTime.Now;
            datarow[4] = productmax.ProductSn;
            datarow[5] = productmax.ProductName;
            dt.Rows.Add(datarow);
            return UpdateTable(dt, "Stocks");
        }

        //�������ʼ�¼
        public string InsertAccount(List<Account> listAccount)
        {
            DataTable dt = new DataTable();
            dt = CreateDataTable("Account");
            DataRow datarow;
            int i = 0;
            while (i < listAccount.Count)
            {
                datarow = dt.NewRow();
                datarow[0] = listAccount[i].Id;
                datarow[1] = listAccount[i].CustomerId;
                datarow[2] = listAccount[i].Day;
                datarow[3] = listAccount[i].InMoney;
                dt.Rows.Add(datarow);
                i++;
            }
            return UpdateTable(dt, "Account");
        }
        //�������ʼ�¼
        public string UpdateAccount(List<Account> listAccount)
        {
            DataTable dt = new DataTable();
            dt = SelectAccount(listAccount);
            for (int i = 0; i < listAccount.Count; i++)
            {
                dt.Rows[i][0] = listAccount[i].Id;
                dt.Rows[i][1] = listAccount[i].CustomerId;
                dt.Rows[i][2] = listAccount[i].Day;
                dt.Rows[i][3] = listAccount[i].InMoney;
            }
            return UpdateTable(dt, "Account");
        }
        //��ѯ���ʼ�¼
        public DataTable SelectAccount(List<Account> listAccount)
        {
            string querysql = "";
            int i = 0;
            while (i < listAccount.Count)
            {
                querysql = querysql + ",'" + listAccount[i].Id + "'";
                i++;
            }
            querysql = querysql.Substring(1);
            command.CommandText = "SELECT * FROM Account where Id in (" + querysql + ")";
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("Account");
            da.Fill(dt);
            return dt;
        }

        //����Customerid��ʱ�䷶Χ����ѯ���ʼ�¼
        public DataTable SelectAccountByTime(int customerId,DateTime startdate, DateTime enddate)
        {
            string querysql = "";
            querysql += " where day >= '" + startdate.ToString("s") + "'";
            querysql += " and day <= '" + enddate.ToString("s") + "'";

            if (customerId != 0)
            {
                querysql += " and CustomerId = " + customerId;
            }

            command.CommandText = "SELECT * FROM Account" + querysql;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("Account");
            da.Fill(dt);
            return dt;
        }

        //����Customerid��ʱ�䷶Χ����ѯ���ʼ�¼
        public List<Account> SelectAccountByTimeToList(int customerId, DateTime startdate, DateTime enddate)
        {
            string querysql = "";
            querysql += " where day >= '" + startdate.ToString("s") + "'";
            querysql += " and day <= '" + enddate.ToString("s") + "'";
            if (customerId != 0)
            {
                querysql += " and CustomerId = " + customerId;
            }

            command.CommandText = "SELECT * FROM Account" + querysql;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("Account");
            da.Fill(dt);

            List<Account> listAccount = new List<Account>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Account account = new Account();
                account.Id = Convert.ToInt16(dt.Rows[i][0].ToString());
                account.CustomerId = Convert.ToInt16(dt.Rows[i][1].ToString());
                account.Day = Convert.ToDateTime(dt.Rows[i][2].ToString());
                account.InMoney = Convert.ToDecimal(dt.Rows[i][3]);
                listAccount.Add(account);
            }
            return listAccount;
        }

        //ͳ��ʵ���ʿ�
        public List<Account> SumRealInMoney()
        {
            command.CommandText = "select CustomerId,sum(InMoney) from Account group by CustomerId";
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<Account> listAccount = new List<Account>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Account account = new Account();
                account.CustomerId = Convert.ToInt16(dt.Rows[i][0].ToString());
                account.InMoney = Convert.ToDecimal(dt.Rows[i][1]);
                listAccount.Add(account);
            }
            return listAccount;
        }

        //ͳ��Ӧ���ʿ�
        public List<Account> SumRequestInMoney()
        {
            command.CommandText = "select CustomerId,sum(Price*Counts) from Inventory group by CustomerId";
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<Account> listAccount = new List<Account>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Account account = new Account();
                account.CustomerId = Convert.ToInt16(dt.Rows[i][0].ToString());
                account.InMoney = Convert.ToDecimal(dt.Rows[i][1]);
                listAccount.Add(account);
            }
            return listAccount;
        }
    }
}
