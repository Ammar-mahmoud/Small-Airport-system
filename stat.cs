using FCAI_Airport.bin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FCAI_Airport
{
    public partial class stat : Form
    {
        private string email;
        private string type;
        public stat(string e, string t)
        {
            email = e;
            type = t;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=LAP-OF-THRONES\SQLSERVER;Initial Catalog=Airport_System; Integrated Security=True "))
                {
                    connection.Open();

                    SqlCommand countFlightsCommand = new SqlCommand("SELECT COUNT(*) FROM flight", connection);
                    int numberOfFlights = (int)countFlightsCommand.ExecuteScalar();

                    MessageBox.Show($"Number of flights: {numberOfFlights}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=LAP-OF-THRONES\SQLSERVER;Initial Catalog=Airport_System; Integrated Security=True "))
                {
                    connection.Open();

                    SqlCommand sumSeatsCommand = new SqlCommand("SELECT SUM(noSeats) FROM booking", connection);
                    int totalSeats = Convert.ToInt32(sumSeatsCommand.ExecuteScalar());

                    MessageBox.Show($"Total Tickets booked: {totalSeats}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            manger_list f3 = new manger_list(email, type);
            f3.ShowDialog();
            this.Close();
        }
    }
}
