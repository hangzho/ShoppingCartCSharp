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
using System.Collections;

namespace A5_Client_ZhouHang
{
    public partial class Form2 : Form
    {
        IService1 serv;
        Queue<string> mesg;
        string cid;
        ArrayList productIds =new ArrayList();
        ArrayList quantities = new ArrayList();

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(IService1 serv, string cid)
        {
            InitializeComponent();
            this.serv = serv;
            this.cid = cid;
            string[] prd = serv.readTable("Product");
            listBox1.Items.Clear();
            foreach (string p in prd)
                listBox1.Items.Add(p);

            mesg= new Queue<string>();
            mesg.Enqueue(listBox1.Items[0].ToString() + "\t Quantity");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (listBox1.SelectedIndex > 0)
            {

                int pid = listBox1.SelectedIndex;
                string pidstr = pid.ToString();
                pidstr = pidstr + pidstr + pidstr + pidstr;
                mesg.Enqueue(listBox1.SelectedItem.ToString()+"\t"+textBox1.Text);
                productIds.Add(pidstr);
                quantities.Add(Int32.Parse(textBox1.Text));
                
                while (mesg.Count > 0)
                {
                    listBox2.Items.Add(mesg.Dequeue());
                }
                
            }
            else
            {
                MessageBox.Show("Please select a product.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (productIds.Count > 1)
            {
                string[] productIdstr = productIds.ToArray().Cast<string>().ToArray();
                int[] quantitiesint = quantities.ToArray().Cast<int>().ToArray();
                serv.placeOrder(cid, productIdstr, quantitiesint);
                MessageBox.Show("Congratulations!");
            }
            else 
            {
                MessageBox.Show("Please select a product.");
            }
            
        }


    }
}
