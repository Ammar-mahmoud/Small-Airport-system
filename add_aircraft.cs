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
    public partial class add_aircraft : Form
    {
        private string email;
        private string type;
        public add_aircraft(string e , string t)
        {
            email = e;
            type = t;
            InitializeComponent();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void add_aircraft_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = (int)numericUpDown3.Value;
            string aircraftName = textBox4.Text;
            int maxCapacityEconomy = (int)numericUpDown1.Value;
            int maxCapacityBusiness = (int)numericUpDown2.Value;
            string aircraftModel = textBox1.Text;

            // Create a SqlConnection object using the connection string
            using (SqlConnection connection = new SqlConnection(@"Data Source=LAP-OF-THRONES\SQLSERVER;Initial Catalog=Airport_System; Integrated Security=True "))
            {
                try
                {
                    connection.Open();

                    // Create a SqlCommand object
                    SqlCommand command = new SqlCommand("INSERT INTO Aircraft (id, name, max_capacity_e, max_capacity_b, model, by_u_email) VALUES (@ID, @Name, @MaxCapacityEconomy, @MaxCapacityBusiness, @Model, @Email)", connection);
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Name", aircraftName);
                    command.Parameters.AddWithValue("@MaxCapacityEconomy", maxCapacityEconomy);
                    command.Parameters.AddWithValue("@MaxCapacityBusiness", maxCapacityBusiness);
                    command.Parameters.AddWithValue("@Model", aircraftModel);
                    command.Parameters.AddWithValue("@Email", this.email);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Aircraft added successfully!");
                        this.Hide();
                        manger_list f3 = new manger_list(email, type);
                        f3.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add aircraft.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            manger_list f3 = new manger_list(email, type);
            f3.ShowDialog();
            this.Close();
        }
    }
}
