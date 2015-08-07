using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoLib_ZhouHang
{
    public class crud
    {
        public SqlConnection cnn;

        public crud(string dbn, string uid, string pass)
        {
            cnn = new SqlConnection();
            cnn.ConnectionString = String.Format("Data Source=(local);Initial Catalog={0};Persist Security Info=True;User ID={1};Password={2};", dbn, uid, pass);
            cnn.Open();
        }

        ~crud()
        {
            try
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();

            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        public List<string> readTable(string tableName)
        {
            List<string> ret = new List<string>();
            try
            {
                string query = "SELECT * FROM " + tableName;
                SqlCommand cmd = new SqlCommand(query, cnn);
                SqlDataReader reader = cmd.ExecuteReader();

                string header = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    header += reader.GetName(i) + "\t";
                }
                ret.Add(header);

                while (reader.Read())
                {
                    string s = "";
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        s += reader[i].ToString() + "\t";
                    }
                    ret.Add(s);
                }
                reader.Close();
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
            return ret;
        }

        public List<string> listPurchases(string cid) 
        {
            List<string> ret = new List<string>();
            try
            {
                string query = "SELECT Orders.Oid, Product.Pid, Price, Name, Quantity FROM Orders, Product, LineItem WHERE Orders.Oid = LineItem.Oid and LineItem.Pid = Product.Pid and Orders.Cid = " + cid;
                SqlCommand cmd = new SqlCommand(query, cnn);
                SqlDataReader reader = cmd.ExecuteReader();

                string header = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    header += reader.GetName(i) + "\t";
                }
                ret.Add(header);

                while (reader.Read())
                {
                    string s = "";
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        s += reader[i].ToString() + "\t";
                    }
                    ret.Add(s);
                }
                reader.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return ret;
        }

        public void placeOrder(string customerId, string[] productIds, int[] quantities)
        {
            try
            {
                string query = "SELECT MAX(oid) FROM Orders";
                SqlCommand cmd1 = new SqlCommand(query,cnn);
                SqlDataReader reader = cmd1.ExecuteReader();
                int oid;
                if(reader.Read())
                {
                    oid = (int)reader[0] +1;
                }else
                {
                    oid = 1;
                }
                reader.Close();

                DateTime now = DateTime.Now;
                string dateStr = now.ToString("yyyy-MM-dd");

                string insertordSQL = String.Format("Insert Into Orders (Oid, Cid, OrderDate) Values ({0}, {1}, '{2}')", oid, customerId, dateStr);
                SqlCommand insertordcmd = new SqlCommand(insertordSQL, cnn);
                insertordcmd.ExecuteNonQuery();


                string insertITSQL ;
                SqlCommand insertITcmd;
                for (int i = 0; i < productIds.Length; i++)
                {
                    insertITSQL = String.Format("Insert Into LineItem (Oid, Pid, Quantity) Values ({0},{1},{2})", oid, productIds[i], quantities[i]);
                    insertITcmd = new SqlCommand(insertITSQL, cnn);
                    insertITcmd.ExecuteNonQuery();
                }

                //string insertITSQL = "Insert Into LineItem (Oid, Pid, Quantity) Values (@oid1,@pid,@quantity)";
                //SqlCommand insertITcmd = new SqlCommand(insertITSQL,cnn);
                //SqlParameter poid = new SqlParameter();
                //poid.ParameterName = "@oid";
                //SqlParameter ppid = new SqlParameter();
                //ppid.ParameterName = "@pid";
                //SqlParameter pquantity = new SqlParameter();
                //pquantity.ParameterName = "@quantity";
                //for (int i = 0; i < productIds.Length; i++) 
                //{

                //    ppid.Value = productIds[i];
                //    poid.Value = oid;
                //    pquantity.Value = quantities[i];
                //    insertITcmd.ExecuteNonQuery();
                //}

//                for (int i = 0; i < productIds.Length; i++)
//                {
                    
//                    insertITcmd.Parameters.AddWithValue("@pid", productIds[i]);
//insertITcmd.Parameters.AddWithValue("@oid1", oid);
//                    insertITcmd.Parameters.AddWithValue("@quantity", quantities[i]);
//                    insertITcmd.ExecuteNonQuery();
//                }


            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
