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
    public partial class sign_up_manger : Form
    {
        public sign_up_manger()
        {
            InitializeComponent();
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
            string name = textBoxName.Text;
            string email = textBoxEmail.Text;
            string phone = textBoxPhone.Text;
            string password = textBoxPassword.Text;
            string managerDegree = textBoxManagerDegree.Text;
            string systemPassword = textBoxSystemPassword.Text;

            string system = "system123";
            if(systemPassword != system)
            {
                MessageBox.Show("Failed to sign up manager. System Password is not valid.");
                return;
            }
            // Create a SqlConnection object using the connection string
            using (SqlConnection connection = new SqlConnection(@"Data Source=LAP-OF-THRONES\SQLSERVER;Initial Catalog=Airport_System; Integrated Security=True "))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO [User] (name, email, password, phone , type) VALUES (@Name, @email, @password, @phone,'A')", connection);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@phone", phone);

                    SqlCommand command2 = new SqlCommand("INSERT INTO Admin (U_email, managerDegree) VALUES (@email, @managdeg)", connection);
                    command2.Parameters.AddWithValue("@email", email);
                    command2.Parameters.AddWithValue("@managdeg", managerDegree);

                    int rowsAffected = command.ExecuteNonQuery();
                    int rowsAffected2 = command2.ExecuteNonQuery();
                    if (rowsAffected > 0 && rowsAffected2 > 0)
                    {
                        MessageBox.Show("added successfully!");
                        this.Hide();
                        manger_list f3 = new manger_list(email, "A");
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
    }
}
