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
    public partial class update_flight : Form
    {
        private string email, type;
        public update_flight(string e, string t)
        {
            email = e;
            type = t;
            InitializeComponent();
        }
        DataTable dtb = new DataTable();
        void viewdata()
        {
            SqlConnection con = new SqlConnection(@"Data Source=LAP-OF-THRONES\SQLSERVER;Initial Catalog=Airport_System; Integrated Security=True ");
            con.Open();

            SqlCommand myCommand = new SqlCommand("SELECT id, date, src, dest, b_price, e_price, aircraft_id FROM flight", con);
            SqlDataAdapter adp = new SqlDataAdapter(myCommand);

            adp.Fill(dtb);
            con.Close();
            con.Dispose();
        }
        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime date = dateTimePicker1.Value;
            decimal economyPrice = numericUpDown5.Value;
            decimal businessPrice = numericUpDown2.Value;
            int flightId = (int)numericUpDown1.Value;
            int aircraftId = (int)numericUpDown4.Value;

            using (SqlConnection connection = new SqlConnection(@"Data Source=LAP-OF-THRONES\SQLSERVER;Initial Catalog=Airport_System;Integrated Security=True"))
            {
                try
                {
                    connection.Open();

                    // Check if the flight exists
                    SqlCommand checkFlightCommand = new SqlCommand("SELECT COUNT(*) FROM Flight WHERE id = @FlightId", connection);
                    checkFlightCommand.Parameters.AddWithValue("@FlightId", flightId);

                    int flightCount = (int)checkFlightCommand.ExecuteScalar();

                    if (flightCount == 0)
                    {
                        MessageBox.Show("Invalid flight ID. Please try again.");
                        return;
                    }

                    // Check if the aircraft exists
                    SqlCommand checkAircraftCommand = new SqlCommand("SELECT COUNT(*) FROM Aircraft WHERE id = @AircraftId", connection);
                    checkAircraftCommand.Parameters.AddWithValue("@AircraftId", aircraftId);

                    int aircraftCount = (int)checkAircraftCommand.ExecuteScalar();

                    if (aircraftCount == 0)
                    {
                        MessageBox.Show("Invalid aircraft ID. Please try again.");
                        return;
                    }

                    // Update the flight information
                    SqlCommand updateFlightCommand = new SqlCommand("UPDATE Flight SET aircraft_id = @AircraftId, b_price = @BusinessPrice, e_price = @EconomyPrice, date = @Date WHERE id = @FlightId", connection);
                    updateFlightCommand.Parameters.AddWithValue("@AircraftId", aircraftId);
                    updateFlightCommand.Parameters.AddWithValue("@BusinessPrice", businessPrice);
                    updateFlightCommand.Parameters.AddWithValue("@EconomyPrice", economyPrice);
                    updateFlightCommand.Parameters.AddWithValue("@Date", date);
                    updateFlightCommand.Parameters.AddWithValue("@FlightId", flightId);

                    int rowsAffected = updateFlightCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Flight updated successfully!");
                        // Perform any additional actions or display messages if needed
                    }
                    else
                    {
                        MessageBox.Show("Failed to update flight. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            dtb.Clear();
            SqlConnection con = new SqlConnection(@"Data Source=LAP-OF-THRONES\SQLSERVER;Initial Catalog=Airport_System; Integrated Security=True ");
            con.Open();
            CrystalReport2 crp = new CrystalReport2();
            viewdata();
            crp.Database.Tables["flight"].SetDataSource(dtb);
            crystalReportViewer1.ReportSource = crp;
            con.Close();
        }

        private void crystalReportViewer1_Load_1(object sender, EventArgs e)
        {
            
        }

        private void update_flight_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            manger_list f3 = new manger_list(email, type);
            f3.ShowDialog();
            this.Close();
        }
    }
}
