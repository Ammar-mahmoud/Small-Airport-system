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
    public partial class sign_in : Form
    {
        public sign_in()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f3 = new Form1();
            f3.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string password = textBox2.Text;

            using (SqlConnection connection = new SqlConnection(@"Data Source=LAP-OF-THRONES\SQLSERVER;Initial Catalog=Airport_System;Integrated Security=True"))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT email, password, type FROM [User] WHERE email = @Email", connection);
                    command.Parameters.AddWithValue("@Email", email);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string storedEmail = reader.GetString(0);
                        string storedPassword = reader.GetString(1);
                        string userType = reader.GetString(2);

                        if (password == storedPassword)
                        {
                            if (userType == "A")
                            {
                                MessageBox.Show("Admin login successful!");
                                this.Hide();
                                manger_list f3 = new manger_list(storedEmail, userType);
                                f3.ShowDialog();
                                this.Close();
                            }
                            else if (userType == "C")
                            {
                                MessageBox.Show("Customer login successful!");
                                this.Hide();
                                customer_list f3 = new customer_list(storedEmail, userType);
                                f3.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Invalid user type.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid password.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Email not found.");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

    }
}
