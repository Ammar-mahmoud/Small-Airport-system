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
    public partial class update_name : Form
    {
        private string email;
        private string type;
        public update_name(string e, string t)
        {
            email = e;
            type = t;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            update_data f3 = new update_data(email,type);
            f3.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newUserName = textBox3.Text; // Assuming the new name is entered in a TextBox

            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=LAP-OF-THRONES\SQLSERVER;Initial Catalog=Airport_System; Integrated Security=True "))
                {
                    connection.Open();

                    SqlCommand updateNameCommand = new SqlCommand("UPDATE [user] SET name = @NewName WHERE U_email = @UserEmail", connection);
                    updateNameCommand.Parameters.AddWithValue("@NewName", newUserName);
                    updateNameCommand.Parameters.AddWithValue("@UserEmail", email); // Assuming email is accessible in this scope

                    int rowsAffected = updateNameCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Name updated successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Failed to update name.");
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
