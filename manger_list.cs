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
    public partial class manger_list : Form
    {
        private string email;
        private string type;
        public manger_list(string e, string t)
        {
            email = e;
            type = t;
            InitializeComponent();
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
            update_data f3 = new update_data(email,type);
            f3.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            add_aircraft f3 = new add_aircraft(email, type);
            f3.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            change_state_aircraft f3 = new change_state_aircraft(email, type);
            f3.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            add_flight f3 = new add_flight(email, type);
            f3.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            update_flight f3 = new update_flight(email, type);
            f3.ShowDialog();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            stat f3 = new stat(email, type);
            f3.ShowDialog();
            this.Close();
        }
    }
}
