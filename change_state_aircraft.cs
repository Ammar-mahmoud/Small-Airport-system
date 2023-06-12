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
    public partial class change_state_aircraft : Form
    {
        private string email, type;
        public change_state_aircraft(string e, string t)
        {
            email = e;
            type = t;
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            manger_list f3 = new manger_list(email, type);
            f3.ShowDialog();
            this.Close();
        }
        DataTable dtb = new DataTable();
        void viewdata()
        {
            SqlConnection con = new SqlConnection(@"Data Source=LAP-OF-THRONES\SQLSERVER;Initial Catalog=Airport_System; Integrated Security=True ");
            con.Open();

            SqlCommand myCommand = new SqlCommand("SELECT id, name, model, state FROM Aircraft", con);
            SqlDataAdapter adp = new SqlDataAdapter(myCommand);

            adp.Fill(dtb);
            con.Close();
            con.Dispose();
        }
        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dtb.Clear();
            SqlConnection con = new SqlConnection(@"Data Source=LAP-OF-THRONES\SQLSERVER;Initial Catalog=Airport_System; Integrated Security=True ");
            con.Open();
            CrystalReport1 crp = new CrystalReport1();
            viewdata();
            crp.Database.Tables["Aircraft"].SetDataSource(dtb);
            crystalReportViewer1.ReportSource = crp;
            con.Close();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int aircraftId = (int)numericUpDown3.Value;
            string state = checkBox1.Checked ? "active" : "not active";

            // Create a SqlConnection object using the connection string
            using (SqlConnection connection = new SqlConnection(@"Data Source=LAP-OF-THRONES\SQLSERVER;Initial Catalog=Airport_System;Integrated Security=True"))
            {
                try
                {
                    connection.Open();

                    // Create a SqlCommand object
                    SqlCommand command = new SqlCommand("UPDATE Aircraft SET state = @State WHERE id = @AircraftId", connection);
                    command.Parameters.AddWithValue("@State", state);
                    command.Parameters.AddWithValue("@AircraftId", aircraftId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("State updated successfully!");
                        this.Hide();
                        manger_list f3 = new manger_list(email, type);
                        f3.ShowDialog();
                        this.Close();
                        // Perform any additional actions or display messages if needed
                    }
                    else
                    {
                        MessageBox.Show("Failed to update state.");
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
