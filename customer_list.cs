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
    public partial class customer_list : Form
    {
        private string email;
        private string type;
        public customer_list(string e, string t)
        {
            email = e;
            type = t;
            InitializeComponent();
        }

        private void customer_list_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            sign_in f3 = new sign_in();
            f3.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            update_data f3 = new update_data(email, type);
            f3.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            book_flight f3 = new book_flight(email, type);
            f3.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            update_book f3 = new update_book(email, type);
            f3.ShowDialog();
            this.Close();
        }
    }
}
