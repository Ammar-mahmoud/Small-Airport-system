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
    public partial class book_flight : Form
    {
        private string email;
        private string type;
        public book_flight(string e, string t)
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

            SqlCommand myCommand = new SqlCommand("SELECT id, date, src, dest, b_price, e_price, seats_b, seats_e,  aircraft_id FROM flight", con);
            SqlDataAdapter adp = new SqlDataAdapter(myCommand);

            adp.Fill(dtb);
            con.Close();
            con.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dtb.Clear();
            string source = textBox4.Text;
            string destination = textBox1.Text;
            DateTime date = dateTimePicker1.Value.Date;
            int requiredSeats = (int)numericUpDown1.Value; 
            string flightClass = listBox1.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(flightClass))
            {
                flightClass = "Economy"; 
            }

            viewdata();

            List<string> availableFlights = new List<string>();
            List<string> allFlights = new List<string>();

            if (dtb.Rows.Count > 0)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    int availableSeats = 0;
                    string className = "";

                    if (flightClass == "Business")
                    {
                        availableSeats = Convert.ToInt32(row["seats_b"]);
                        className = "Business Class";
                    }
                    else if (flightClass == "Economy")
                    {
                        availableSeats = Convert.ToInt32(row["seats_e"]);
                        className = "Economy Class";
                    }
                    string flightInfo = $"Flight ID: {row["id"]}, Date: {row["date"]}, Source: {row["src"]}, Destination: {row["dest"]}";
                    allFlights.Add(flightInfo);
                    if (availableSeats >= requiredSeats && row["src"].ToString() == source && row["dest"].ToString() == destination && Convert.ToDateTime(row["date"]).Date == date)
                    {
                        availableFlights.Add(flightInfo);
                    }
                }
            }

            if (availableFlights.Count > 0)
            {
                // Display the available flights
                MessageBox.Show("Available Flights:\n\n" + string.Join("\n", availableFlights));
            }
            else
            {
                MessageBox.Show("No available flights matching the criteria.\nBut we have flights can like you\n\n"+ string.Join("\n", allFlights));
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int flightId = (int)numericUpDown4.Value;
            string flightClass = listBox1.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(flightClass) || flightClass == "Economy")
            {
                flightClass = "e";
            }
            else
            {
                flightClass = "b";
            }

            try
            {
                int noOfSeats = (int)numericUpDown1.Value;
                decimal totalPrice = 0;

                using (SqlConnection connection = new SqlConnection(@"Data Source=LAP-OF-THRONES\SQLSERVER;Initial Catalog=Airport_System; Integrated Security=True "))
                {
                    connection.Open();

                    SqlCommand checkFlightCommand = new SqlCommand("SELECT * FROM flight WHERE id = @FlightId", connection);
                    checkFlightCommand.Parameters.AddWithValue("@FlightId", flightId);

                    SqlDataReader reader = checkFlightCommand.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        reader.Close();
                        MessageBox.Show("Flight not found.");
                        return;
                    }

                    reader.Close();

                    SqlCommand checkSeatsCommand = new SqlCommand("SELECT seats_" + flightClass[0] + " FROM flight WHERE id = @FlightId", connection);
                    checkSeatsCommand.Parameters.AddWithValue("@FlightId", flightId);

                    int availableSeats = Convert.ToInt32(checkSeatsCommand.ExecuteScalar());

                    if (availableSeats < noOfSeats)
                    {
                        MessageBox.Show("Insufficient seats in the selected class.");
                        return;
                    }

                    SqlCommand getPriceCommand = new SqlCommand("SELECT " + flightClass + "_price FROM flight WHERE id = @FlightId", connection);
                    getPriceCommand.Parameters.AddWithValue("@FlightId", flightId);

                    decimal pricePerSeat = Convert.ToDecimal(getPriceCommand.ExecuteScalar());
                    totalPrice = pricePerSeat * noOfSeats;

                    SqlCommand insertBookingCommand = new SqlCommand("INSERT INTO booking (flight_id, U_email, class, noSeats) VALUES (@FlightId, @Email, @FlightClass, @NoOfSeats)", connection);
                    insertBookingCommand.Parameters.AddWithValue("@FlightId", flightId);
                    insertBookingCommand.Parameters.AddWithValue("@FlightClass", flightClass);
                    insertBookingCommand.Parameters.AddWithValue("@NoOfSeats", noOfSeats);
                    insertBookingCommand.Parameters.AddWithValue("@Email", email);

                    int rowsAffected = insertBookingCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        SqlCommand updateSeatsCommand = new SqlCommand("UPDATE flight SET seats_" + flightClass + " = seats_" + flightClass + " - @NoOfSeats WHERE id = @FlightId", connection);
                        updateSeatsCommand.Parameters.AddWithValue("@NoOfSeats", noOfSeats);
                        updateSeatsCommand.Parameters.AddWithValue("@FlightId", flightId);

                        updateSeatsCommand.ExecuteNonQuery();

                        MessageBox.Show("Booking added successfully! Total Price: " + totalPrice.ToString("C2"));

                        this.Hide();
                        customer_list f3 = new customer_list(email, type);
                        f3.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add booking.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            customer_list f3 = new customer_list(email, type);
            f3.ShowDialog();
            this.Close();
        }
    }
}
