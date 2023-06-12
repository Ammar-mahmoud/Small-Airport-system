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
    public partial class update_password : Form
    {
        private string email;
        private string type;
        public update_password(string e, string t)
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

        private void button1_Click(object sender, EventArgs e)
        {
            string newPassword = textBox3.Text; // Assuming the new password is entered in a TextBox

            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=LAP-OF-THRONES\SQLSERVER;Initial Catalog=Airport_System; Integrated Security=True "))
                {
                    connection.Open();

                    SqlCommand updatePasswordCommand = new SqlCommand("UPDATE [user] SET pass = @NewPassword WHERE U_email = @UserEmail", connection);
                    updatePasswordCommand.Parameters.AddWithValue("@NewPassword", newPassword);
                    updatePasswordCommand.Parameters.AddWithValue("@UserEmail", email); // Assuming email is accessible in this scope

                    int rowsAffected = updatePasswordCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Password updated successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Failed to update password.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

    }
}
