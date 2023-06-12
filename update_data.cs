using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FCAI_Airport
{
    public partial class update_data : Form
    {
        private string email;
        private string type;
        public update_data(string e, string t)
        {
            email = e;
            type = t;
            InitializeComponent();
        }

        private void update_data_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(type == "A")
            {
                this.Hide();
                manger_list f3 = new manger_list(email, type);
                f3.ShowDialog();
                this.Close();
            }
            else
            {
                this.Hide();
                customer_list f3 = new customer_list(email, type);
                f3.ShowDialog();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            update_password f3 = new update_password(email, type);
            f3.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            update_phone f3 = new update_phone(email, type);
            f3.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            update_name f3 = new update_name(email, type);
            f3.ShowDialog();
            this.Close();
        }
    }
}
