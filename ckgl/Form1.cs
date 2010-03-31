using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;

namespace InventoryManager
{
    public partial class Form1 : Form
    {
        DbManagerForSqlite dba;
        public Form1()
        {
            InitializeComponent();
            dba = new DbManagerForSqlite();
        }
        //刷新数据源
        private void RefreshTable()
        {
            string tbname = textBox1.Text;
            this.dataGridView1.DataSource = dba.ReadTable(tbname);
        }
        
        //浏览
        private void button1_Click(object sender, EventArgs e)
        {
            RefreshTable();
        }
        
        
        //InsertCustomer
        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Customer";
            Customer customer = new Customer();
            customer.Id = 2;
            customer.Name = "hytest";
            customer.Address = "shanghai";
            customer.Email = "a@b.c";
            customer.Fax = "110119120";
            customer.Zipcode = "888888";
            customer.Phone = "1381383838438";
            customer.Remark = "test";
            MessageBox.Show(dba.InsertCustomer(customer));
        }

        //SelectCustomer
        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Customer";
            Customer customer = new Customer();
            customer.Id = 2;
            customer.Name = "hytest";
            customer.Address = "shanghai";
            customer.Email = "a@b.c";
            customer.Fax = "110119120";
            customer.Zipcode = "888888";
            customer.Phone = "1381383838438";
            customer.Remark = "test";
            //MessageBox.Show(customer.ToString());
            if (dba.SelectCustomerToDataTable(customer).Rows.Count > 0)
            {
                MessageBox.Show("Success");
            }
            else
            {
                MessageBox.Show("No rows");
            }
            MessageBox.Show(dba.SelectCustomerToClass(customer).ToString());
            List<Customer> listc = new List<Customer>();
            listc = dba.SelectCustomerAllToList();
            for(int i=0;i<listc.Count;i++)
            {
                MessageBox.Show(listc[i].ToString());
            }
            
            
        }

        //UpdateCustomer
        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Customer";
            Customer customer = new Customer();
            customer.Id = 2;
            customer.Name = "hytest2";
            customer.Address = "shanghai";
            customer.Email = "a@b.c";
            customer.Fax = "110119120";
            customer.Zipcode = "888888";
            customer.Phone = "1381383838438";
            customer.Remark = "test";
            MessageBox.Show(dba.UpdateCustomer(customer));
        }

        //CheckCustomer
        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Customer";
            Customer customer = new Customer();
            customer.Name = "hytest";
            customer.Address = "shanghai";
            customer.Email = "a@b.c";
            customer.Fax = "110119120";
            customer.Zipcode = "888888";
            customer.Phone = "1381383838438";
            customer.Remark = "test";
            MessageBox.Show(dba.CheckCustomer(customer).ToString());
        }
        //InsertUser
        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text = "User";
            User user = new User();
            user.Id = 2;
            user.UserId = "usertest"+System.DateTime.Now.ToString();
            user.UserName = "草泥马";
            user.UserInfo = "和谐神兽";
            user.UserLevel = "一般用户";
            user.Remark = "没什么可写的";
            MessageBox.Show(dba.InsertUser(user));
        }
        //UpdateUser
        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = "User";
            User user = new User();
            user.Id = 2;
            user.UserId = "usertest" + System.DateTime.Now.ToString();
            user.UserName = "草泥马";
            user.UserInfo = "和谐神兽";
            user.UserLevel = "一般用户";
            user.Remark = "没什么可写的";
            user.Password = "72726262";
            MessageBox.Show(dba.UpdateUser(user));
        }
        //SelectUser
        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = "User";
            User user = new User();
            user.Id = 2;
            user.UserId = "usertest" + System.DateTime.Now.ToString();
            user.UserName = "草泥马";
            user.UserInfo = "和谐神兽";
            user.UserLevel = "一般用户";
            user.Remark = "没什么可写的";
            //MessageBox.Show(user.ToString());
            MessageBox.Show(dba.SelectUser(user).Rows[0][0].ToString());
            MessageBox.Show(dba.SelectUserToClass(user).ToString());
            List<User> listuser = new List<User>();
            listuser = dba.SelectUserAllToList();
            for (int i = 0; i < listuser.Count; i++)
            {
                MessageBox.Show(listuser[i].ToString());
            }
            
        }
        //CheckUser
        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = "User";
            User user = new User();
            user.Id = 2;
            user.UserId = "usertest" + System.DateTime.Now.ToString();
            user.UserName = "草泥马";
            user.UserInfo = "和谐神兽";
            user.UserLevel = "一般用户";
            user.Remark = "没什么可写的";
            MessageBox.Show(dba.CheckUser(user).ToString());
        }
        //InsertProduct
        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Product";
            Product product = new Product();
            product.Id = 2;
            product.ProductSn = "Product test" + System.DateTime.Now.ToString();
            product.ProductName = "草泥马";
            product.Remark = "没什么可写的";
            MessageBox.Show(dba.InsertProductFull(product));
        }
        
        //SelectProduct
        private void button13_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "Product";
            Product product = new Product();
            product.Id = 2;
            product.ProductSn = "Product test" + System.DateTime.Now.ToString();
            product.ProductName = "草泥马";
            product.Remark = "没什么可写的";
            //MessageBox.Show(product.ToString());
            MessageBox.Show(dba.SelectProduct(product).Rows[0][0].ToString());
            MessageBox.Show(dba.SelectProductToClass(product).ToString());
            List<Product> listProduct = new List<Product>();
            listProduct = dba.SelectProductAllToList();
            for (int i = 0; i < listProduct.Count; i++)
            {
                MessageBox.Show(listProduct[i].ToString());
            }
        }

        //UpdateProduct
        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Product";
            Product product = new Product();
            product.Id = 2;
            product.ProductSn = "Product test" + System.DateTime.Now.ToString();
            product.ProductName = "草泥马";
            product.Remark = "没什么可写的";
            MessageBox.Show(dba.UpdateProduct(product));
        }
        //CheckProduct
        private void button15_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "Product";
            Product product = new Product();
            product.Id = 2;
            product.ProductSn = "Product test" + System.DateTime.Now.ToString();
            product.ProductName = "草泥马";
            product.Remark = "没什么可写的";
            MessageBox.Show(dba.CheckProduct(product).ToString());
        }

        //InsertInventory
        private void button19_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Inventory";
            List<Inventory> listinventory = new List<Inventory>();
            int i = 1;
            while (i < 3)
            {            
                Inventory inventory = new Inventory();
                inventory.InventoryId = "1";
                inventory.ProductId = i;
                inventory.CustomerId = i;
                inventory.Counts = 1000 * i + i * 100 + i * 10 + i;
                inventory.Price = i * 0.13 + 200;
                inventory.InventoryType = "O";
                inventory.Day = System.DateTime.Now;
                listinventory.Add(inventory);
                i++;
            }
            MessageBox.Show(dba.InsertInventory(listinventory));
        }
        //SelectInventory
        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Inventory";
            List<Inventory> listinventory = new List<Inventory>();
            int i = 1;
            while (i < 20)
            {
                Inventory inventory = new Inventory();
                inventory.Id = i;
                inventory.InventoryId = "1";
                inventory.ProductId = i;
                inventory.CustomerId = i;
                inventory.Counts = 1000 * i + i * 100 + i * 10 + i;
                inventory.Price = i * 0.13 + 200;
                inventory.InventoryType = "O";
                inventory.Day = System.DateTime.Now;
                //MessageBox.Show(inventory.ToString());
                listinventory.Add(inventory);
                i++;
            }
            MessageBox.Show(dba.SelectInventory(listinventory).Rows.Count.ToString());

            List<Inventory> temp = new List<Inventory>();
            temp = dba.SelectInventoryForList(listinventory);
            for (int j = 0; j < temp.Count; j++)
            {
                MessageBox.Show(temp[j].ToString());
            }
            
        }
        //UpdateInventory
        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Inventory";
            List<Inventory> listinventory = new List<Inventory>();
            int i = 1;
            while (i < 20)
            {
                Inventory inventory = new Inventory();
                inventory.Id = i;
                inventory.InventoryId = "1";
                inventory.ProductId = i;
                inventory.CustomerId = i;
                inventory.Counts = 11000 * i + i * 100 + i * 10 + i;
                inventory.Price = i * 0.13 + 200;
                inventory.InventoryType = "O";
                inventory.Day = System.DateTime.Now;
                listinventory.Add(inventory);
                i++;
            }
            MessageBox.Show(dba.UpdateInventory(listinventory));
        }
        //CheckInventory
        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Inventory";
            //string type = "O";
            //MessageBox.Show(dba.SelectInventoryByTypeAndTime(type, DateTime.Now.AddMinutes(-5), DateTime.Now.AddMinutes(5)).Rows.Count.ToString());

            //List<Inventory> listInventory = new List<Inventory>();
            //listInventory = dba.SelectInventoryByTypeAndTimeToList(type, DateTime.Now.AddMinutes(-5), DateTime.Now.AddMinutes(5));
            //for (int i = 0; i < listInventory.Count; i++)
            //{
            //    MessageBox.Show(listInventory[i].ToString());
            //}

            //List<Inventory> listInventory = new List<Inventory>();
            //Inventory condition = new Inventory();
            //condition.InventoryType = "O";
            //condition.CustomerId = 1;
            //condition.ProductId = 1;
            //listInventory = dba.SelectInventoryByInventoryAndTimeToList(condition, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(5));
            //for (int i = 0; i < listInventory.Count; i++)
            //{
            //    MessageBox.Show(listInventory[i].ToString());
            //}

            List<Inventory> listInventory = new List<Inventory>();
            listInventory = dba.CountInventoryByTime(DateTime.Now.AddDays(-5), DateTime.Now.AddDays(5));
            for (int i = 0; i < listInventory.Count; i++)
            {
                MessageBox.Show(listInventory[i].ToString());
            }
        }

        //SelectStockAll
        private void button22_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Stocks";
            MessageBox.Show(dba.SelectStocksAllForDataTable().Rows.Count.ToString());

            List<Stocks> temp = new List<Stocks>();
            temp = dba.SelectStocksAllForList();
            for (int j = 0; j < temp.Count; j++)
            {
                MessageBox.Show(temp[j].ToString());
            }
        }

        //UpdateStock
        private void button23_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Stocks";
            List<Inventory> listinventory = new List<Inventory>();
            int i = 1;
            while (i < 3)
            {
                Inventory inventory = new Inventory();
                inventory.InventoryId = "1";
                inventory.ProductId = i;
                inventory.CustomerId = i;
                inventory.Counts = 1000 * i + i * 100 + i * 10 + i;
                inventory.Price = i * 0.13 + 200;
                inventory.InventoryType = "O";
                inventory.Day = System.DateTime.Now;
                listinventory.Add(inventory);
                i++;
            }
            dba.InsertInventory(listinventory);
            MessageBox.Show(dba.UpdateStock(listinventory));
        }
        private void button3_Click(object sender, EventArgs e)
        {
            AntiPiracy temp = new AntiPiracy();
            MessageBox.Show(temp.DecodeSerialNo(temp.GetSerialNo()) + "|" + temp.GetComputerNo());
            //MessageBox.Show(temp.GenerateSerialNo(temp.GetSerialNo()));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show( dba.SelectUserByUserId("Admin").ToString());
        }

        private void button20_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Stocks";
            List<Inventory> listinventory = new List<Inventory>();
            int i = 1;
            while (i < 3)
            {
                Inventory inventory = new Inventory();
                inventory.InventoryId = "1";
                inventory.ProductId = i;
                inventory.CustomerId = i;
                inventory.Counts = 1000 * i + i * 100 + i * 10 + i;
                inventory.Price = i * 0.13 + 200;
                inventory.InventoryType = "O";
                inventory.Day = System.DateTime.Now;
                listinventory.Add(inventory);
                i++;
            }
            
            MessageBox.Show(dba.InsertInventoryAndUpdateStocks(listinventory));
        }

        private void button21_Click(object sender, EventArgs e)
        {
            string selectSql = "select * From User Where UserId = 'Admin';";
            using (SQLiteConnection connNew = new SQLiteConnection("Data Source=Inventory.rc"))
            {
                using (SQLiteTransaction trans = connNew.BeginTransaction())
                using (SQLiteCommand updateCmd = new SQLiteCommand(selectSql, connNew, trans))
                {
                        updateCmd.ExecuteNonQuery();
                        trans.Commit();
                }
            }            
        }
        //insert account
        private void button24_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Account";
            List<Account> listiaccount = new List<Account>();
            int i = 1;
            while (i < 20)
            {
                Account account = new Account();
                account.Id = i;
                account.CustomerId = i;
                account.Day = System.DateTime.Now;
                account.InMoney = 10000;
                listiaccount.Add(account);
                i++;
            }
            MessageBox.Show(dba.InsertAccount(listiaccount));
        }
        //update account
        private void button25_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Account";
            List<Account> listiaccount = new List<Account>();
            int i = 1;
            while (i < 20)
            {
                Account account = new Account();
                account.Id = i;
                account.CustomerId = i;
                account.Day = System.DateTime.Now;
                account.InMoney = 20000;
                listiaccount.Add(account);
                i++;
            }
            MessageBox.Show(dba.UpdateAccount(listiaccount));
        }

        //select account
        private void button26_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Account";
            List<Account> listiaccount = new List<Account>();
            int i = 1;
            while (i < 20)
            {
                Account account = new Account();
                account.Id = i;
                account.CustomerId = i;
                account.Day = System.DateTime.Now;
                account.InMoney = 20000;
                listiaccount.Add(account);
                i++;
            }
            dba.SelectAccount(listiaccount);
        }

        //sumrealmoney
        private void button27_Click(object sender, EventArgs e)
        {
            dba.SumRealInMoney();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            dba.SumRequestInMoney();
        }


    }
}