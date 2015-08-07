using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using A5_Client_ZhouHang.ServiceReference1;

namespace A5_Client_ZhouHang
{
    public partial class Form1 : Form
    {
        IService1 serv;
        public Form1()
        {
            InitializeComponent();
            serv = new Service1Client();
            string[] cus = serv.readTable("Customer");
            listBox1.Items.Clear();
            foreach (string c in cus)
                listBox1.Items.Add(c);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 0)
            {
                Form2 form2 = new Form2(serv,listBox1.SelectedIndex.ToString());
                form2.Show();
            }
            else
            {
                MessageBox.Show("Please select a customer.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int cid = listBox1.SelectedIndex;
            string[] pur = serv.listPurchases(cid.ToString());
            listBox2.Items.Clear();
            foreach (string p in pur)
                listBox2.Items.Add(p);
        }
    }
}
