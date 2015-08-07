using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.ServiceReference1;
using AdoLib_ZhouHang;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime now = DateTime.Now;
            Console.WriteLine(now.ToString("yyyy-MM-dd"));
            Service1Client client = new Service1Client();
            string[] productIds1 = new string[2]{"1111","2222"};
            int[] quantities1 = new int[2]{119,911};

            crud db = new crud("OrdersDB", "ism6236", "ism6236bo");
            db.placeOrder("2",productIds1,quantities1);
            //client.placeOrder("2",productIds1,quantities1);
            // Use the 'client' variable to call operations on the service.
            //string[] tt = client.listPurchases("1");
            //string[] tt = client.readTable("Orders");
            //foreach (string t in tt)
            //{
            //Console.WriteLine(t);
            //}

            // Always close the client.
            client.Close();
            Console.Read();
        }
    }
}
