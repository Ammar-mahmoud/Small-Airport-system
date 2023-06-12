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
    public partial class add_flight : Form
    {
        private string email;
        private string type;
        public add_flight(string e, string t)
        {
            email = e;
            type = t;
            InitializeComponent();
        }

        private void add_flight_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            manger_list f3 = new manger_list(email, type);
            f3.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string source = textBox4.Text;
            string destination = textBox1.Text;
            DateTime date = dateTimePicker1.Value;
            decimal economyPrice = numericUpDown1.Value;
            decimal businessPrice = numericUpDown2.Value;
            int flightId = (int)numericUpDown3.Value;
            int aircraftId = (int)numericUpDown4.Value;

            // Create a SqlConnection object using the connection string
            using (SqlConnection connection = new SqlConnection(@"Data Source=LAP-OF-THRONES\SQLSERVER;Initial Catalog=Airport_System;Integrated Security=True"))
            {
                try
                {
                    connection.Open();

                    // Check if the aircraft exists
                    SqlCommand aircraftCommand = new SqlCommand("SELECT COUNT(*) FROM Aircraft WHERE id = @AircraftId", connection);
                    aircraftCommand.Parameters.AddWithValue("@AircraftId", aircraftId);

                    int aircraftCount = (int)aircraftCommand.ExecuteScalar();

                    if (aircraftCount == 0)
                    {
                        MessageBox.Show("Invalid aircraft ID. Please try again.");
                        return;
                    }

                    SqlCommand maxCapacityCommand = new SqlCommand("SELECT max_capacity_b, max_capacity_e FROM Aircraft WHERE id = @AircraftId", connection);
                    maxCapacityCommand.Parameters.AddWithValue("@AircraftId", aircraftId);

                    SqlDataReader reader = maxCapacityCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        int maxBusinessSeats = reader.GetInt32(0);
                        int maxEconomySeats = reader.GetInt32(1);

                        reader.Close();

                        SqlCommand flightCommand = new SqlCommand("INSERT INTO Flight (id, date, src, dest, seats_b, b_price, seats_e, e_price, u_email, aircraft_id) VALUES (@FlightId, @Date, @Source, @Destination, @BusinessSeats, @BusinessPrice, @EconomySeats, @EconomyPrice, @email, @AircraftId)", connection);
                        flightCommand.Parameters.AddWithValue("@FlightId", flightId);
                        flightCommand.Parameters.AddWithValue("@Date", date);
                        flightCommand.Parameters.AddWithValue("@Source", source);
                        flightCommand.Parameters.AddWithValue("@Destination", destination);
                        flightCommand.Parameters.AddWithValue("@BusinessSeats", maxBusinessSeats);
                        flightCommand.Parameters.AddWithValue("@BusinessPrice", businessPrice);
                        flightCommand.Parameters.AddWithValue("@EconomySeats", maxEconomySeats);
                        flightCommand.Parameters.AddWithValue("@EconomyPrice", economyPrice);
                        flightCommand.Parameters.AddWithValue("@AircraftId", aircraftId);
                        flightCommand.Parameters.AddWithValue("@email", email);

                        int rowsAffected = flightCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Flight added successfully!");
                            this.Hide();
                            manger_list f3 = new manger_list(email, type);
                            f3.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add flight. Please try again.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to retrieve aircraft information. Please try again.");
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
