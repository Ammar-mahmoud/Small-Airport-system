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
    public partial class update_phone : Form
    {
        private string email;
        private string type;
        public update_phone(string e, string t)
        {
            email = e;
            type = t;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            update_data f3 = new update_data(email, type);
            f3.ShowDialog();
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newPhoneNumber = textBox3.Text; // Assuming the new phone number is entered in a TextBox

            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=LAP-OF-THRONES\SQLSERVER;Initial Catalog=Airport_System; Integrated Security=True "))
                {
                    connection.Open();

                    SqlCommand updatePhoneCommand = new SqlCommand("UPDATE [user] SET phone = @NewPhone WHERE U_email = @UserEmail", connection);
                    updatePhoneCommand.Parameters.AddWithValue("@NewPhone", newPhoneNumber);
                    updatePhoneCommand.Parameters.AddWithValue("@UserEmail", email); // Assuming email is accessible in this scope

                    int rowsAffected = updatePhoneCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Phone number updated successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Failed to update phone number.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
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

        private void update_phone_Load(object sender, EventArgs e)
        {

        }
    }
}
