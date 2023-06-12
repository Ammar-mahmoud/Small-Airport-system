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
    public partial class sing_up : Form
    {
        public sing_up()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            check_sign_up f3 = new check_sign_up();
            f3.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox4.Text, email = textBox1.Text, password = textBox2.Text;
            string nationality = textBox3.Text, phone = textBox5.Text;
            using (SqlConnection connection = new SqlConnection(@"Data Source=LAP-OF-THRONES\SQLSERVER;Initial Catalog=Airport_System; Integrated Security=True "))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO [User] (name, email, password, phone , type) VALUES (@Name, @email, @password, @phone,'C')", connection);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@phone", phone);

                    SqlCommand command2 = new SqlCommand("INSERT INTO Customer (U_email, Nationality) VALUES (@email, @nationality)", connection);
                    command2.Parameters.AddWithValue("@email", email);
                    command2.Parameters.AddWithValue("@nationality", nationality);

                    int rowsAffected = command.ExecuteNonQuery();
                    int rowsAffected2 = command2.ExecuteNonQuery();
                    if (rowsAffected > 0 && rowsAffected2 > 0)
                    {
                        MessageBox.Show("added successfully!");

                        this.Hide();
                        customer_list f3 = new customer_list(email, "C");
                        f3.ShowDialog();
                        this.Close();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }

            }
        } 

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
