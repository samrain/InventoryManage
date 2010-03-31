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

        //关闭数据库，释放连接
        public void Close()
        {
            con.Close();
            con.Dispose();
        }

        //根据传入的表名，读取该数据表记录
        public DataTable ReadTable(string tableName)
        {
            command.CommandText = "SELECT * FROM " + tableName;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable(tableName);
            da.Fill(dt);
            return dt;
        }

        //根据传入的表名，读取该数据表记录
        public DataTable ReadTableOrderByXXId(string tableName,string xxid)
        {
            command.CommandText = "SELECT * FROM " + tableName + " order by " + xxid;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable(tableName);
            da.Fill(dt);
            return dt;
        }

        //根据传入的表名，为了插入表准备一个DataTable
        private DataTable CreateDataTable(string tableName)
        {
            command.CommandText = "SELECT * FROM " + tableName + " limit 1";
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable(tableName);
            da.Fill(dt);
            return dt;
        }

        //通用修改数据函数
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

        //新增Customer表，id由于是自增长可以不输入值
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
        //更新Customer表
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
        //得到所有的Customer表记录
        public DataTable SelectCustomerAllToDataTable()
        {
            return this.ReadTableOrderByXXId("Customer", "Name");
        }

        
        //得到所有的Customer表记录
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

        //查询某个Customer的详细信息
        public DataTable SelectCustomerToDataTable(Customer customer)
        {
            command.CommandText = "SELECT * FROM Customer where Id=" +customer.Id;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("Customer");
            da.Fill(dt);
            return dt;
        }

        //查询某个Customer的详细信息
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

        //校验是否有同名的Customer， true-通过测试，没有类似的,false-没有通过测试，有类似的
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

        //新增用户
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
        //修改用户
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
        //查询出所有用户
        public DataTable SelectUserAll()
        {
            return ReadTableOrderByXXId("User","UserName");
        }
        //查询出某个用户信息
        public DataTable SelectUser(User user)
        {
            command.CommandText = "SELECT * FROM User where Id=" + user.Id;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("User");
            da.Fill(dt);
            return dt;
        }

        //查询出所有用户
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
        //查询出某个用户信息
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

        //查询出某个用户信息
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

        //校验是否有同Userid的用户 true-通过测试，没有类似的,false-没有通过测试，有类似的
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

        //新增产品
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

        //新增产品并且在库存表中也新增一个
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

        //更新产品
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
        //查询出所有产品
        public DataTable SelectProductAll()
        {
            return ReadTableOrderByXXId("Product", "ProductSn");
        }
        //查询某个产品信息
        public DataTable SelectProduct(Product product)
        {
            command.CommandText = "SELECT * FROM Product where Id = '" + product.Id + "'";
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            DataTable dt = new DataTable("Product");
            da.Fill(dt);
            return dt;
        }

        //查询出所有产品
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
        //查询某个产品信息
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

        //校验是否有产品代号一样的， true-通过测试，没有类似的,false-没有通过测试，有类似的
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

        //新增入库/出库记录，并且更改库存表
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


        //新增入库/出库记录，“I”表示入库，“O”表示出库
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
        //更新入库/出库记录
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
        //查询入库/出库记录
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

        //查询入库/出库记录
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

        //根据入库/出库和时间范围来查询入库/出库记录
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
        //根据入库/出库和时间范围来查询入库/出库记录
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

        //根据productid、Customerid、入库/出库、和时间范围来查询入库/出库记录
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

        //统计一段时间内的出库记录
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

        //更新库存
        public string UpdateStock(List<Inventory> listInventory)
        {            
            //先把所有的Stocks都列出来     
            int productid;
            int remain;
            DataTable dtStocks = new DataTable();
            dtStocks = SelectStocksAllForDataTable();
            int stocksnum = dtStocks.Rows.Count;

            //对Stocks进行遍历，每取出一个就和listInventory里面进行比较，如果是同一个ProductId，
            //那么根据InventoryType进行加减操作
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
        //查询出目前所有库存
        public DataTable SelectStocksAllForDataTable()
        {
            return ReadTableOrderByXXId("Stocks", "ProductSn");
        }

        //查询出目前所有库存
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

        //新增库存
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

        //新增入帐记录
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
        //更新入帐记录
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
        //查询入帐记录
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

        //根据Customerid和时间范围来查询入帐记录
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

        //根据Customerid和时间范围来查询入帐记录
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

        //统计实收帐款
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

        //统计应收帐款
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
