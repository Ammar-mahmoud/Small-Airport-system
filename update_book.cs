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
    public partial class update_book : Form
    {
        private string email;
        private string type;
        public update_book(string e, string t)
        {
            email = e;
            type = t;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            customer_list f3 = new customer_list(email, type);
            f3.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=LAP-OF-THRONES\SQLSERVER;Initial Catalog=Airport_System; Integrated Security=True "))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT f.id, f.src, f.dest, f.date, b.class, b.noSeats " +
                                                        "FROM flight f " +
                                                        "INNER JOIN booking b ON f.id = b.flight_id " +
                                                        "WHERE b.U_email = @Email", connection);
                    command.Parameters.AddWithValue("@Email", email);

                    SqlDataReader reader = command.ExecuteReader();

                    StringBuilder flightsInfo = new StringBuilder();

                    while (reader.Read())
                    {
                        int flightId = reader.GetInt32(0);
                        string source = reader.GetString(1);
                        string destination = reader.GetString(2);
                        DateTime date = reader.GetDateTime(3);
                        string flightClass = reader.GetString(4);
                        int noOfSeats = reader.GetInt32(5);

                        flightsInfo.AppendLine($"Flight ID: {flightId}, Source: {source}, Destination: {destination}, Date: {date}, Class: {flightClass}, No. of Seats: {noOfSeats}");
                    }

                    reader.Close();

                    if (flightsInfo.Length > 0)
                    {
                        MessageBox.Show("Flights Booked:\n" + flightsInfo.ToString());
                    }
                    else
                    {
                        MessageBox.Show("No flights booked.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int flightId = (int)numericUpDown4.Value;
                string userEmail = email;

                using (SqlConnection connection = new SqlConnection(@"Data Source=LAP-OF-THRONES\SQLSERVER;Initial Catalog=Airport_System; Integrated Security=True "))
                {
                    connection.Open();

                    SqlCommand checkBookingCommand = new SqlCommand("SELECT class, noSeats FROM booking WHERE flight_id = @FlightId AND U_email = @UserEmail", connection);
                    checkBookingCommand.Parameters.AddWithValue("@FlightId", flightId);
                    checkBookingCommand.Parameters.AddWithValue("@UserEmail", userEmail);

                    SqlDataReader reader = checkBookingCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        string flightClass = reader.GetString(0);
                        int noOfSeats = reader.GetInt32(1);

                        reader.Close();

                        SqlCommand deleteBookingCommand = new SqlCommand("DELETE FROM booking WHERE flight_id = @FlightId AND U_email = @UserEmail", connection);
                        deleteBookingCommand.Parameters.AddWithValue("@FlightId", flightId);
                        deleteBookingCommand.Parameters.AddWithValue("@UserEmail", userEmail);

                        int rowsAffected = deleteBookingCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            SqlCommand updateSeatsCommand = new SqlCommand("UPDATE flight SET seats_" + flightClass + " = seats_" + flightClass + " + @NoOfSeats WHERE id = @FlightId", connection);
                            updateSeatsCommand.Parameters.AddWithValue("@NoOfSeats", noOfSeats);
                            updateSeatsCommand.Parameters.AddWithValue("@FlightId", flightId);

                            updateSeatsCommand.ExecuteNonQuery();

                            MessageBox.Show("Booking canceled successfully!");
                            this.Hide();
                            customer_list f3 = new customer_list(email, type);
                            f3.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to cancel booking.");
                        }
                    }
                    else
                    {
                        reader.Close();
                        MessageBox.Show("Booking not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int flightId = (int)numericUpDown4.Value;
                string userEmail = email;
                string newFlightClass = listBox1.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(newFlightClass) || newFlightClass == "Economy")
                {
                    newFlightClass = "e";
                }
                else
                {
                    newFlightClass = "b";
                }

                using (SqlConnection connection = new SqlConnection(@"Data Source=LAP-OF-THRONES\SQLSERVER;Initial Catalog=Airport_System; Integrated Security=True "))
                {
                    connection.Open();

                    SqlCommand checkBookingCommand = new SqlCommand("SELECT class, noSeats FROM booking WHERE flight_id = @FlightId AND U_email = @UserEmail", connection);
                    checkBookingCommand.Parameters.AddWithValue("@FlightId", flightId);
                    checkBookingCommand.Parameters.AddWithValue("@UserEmail", userEmail);

                    SqlDataReader reader = checkBookingCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        string currentFlightClass = reader.GetString(0);
                        int noOfSeats = reader.GetInt32(1);

                        reader.Close();

                        if (currentFlightClass == newFlightClass)
                        {
                            MessageBox.Show("The booking is already in the selected class.");
                            return;
                        }

                        SqlCommand flightInfoCommand = new SqlCommand("SELECT seats_" + newFlightClass + ", e_price, b_price FROM flight WHERE id = @FlightId", connection);
                        flightInfoCommand.Parameters.AddWithValue("@FlightId", flightId);
                        SqlDataReader flightInfoReader = flightInfoCommand.ExecuteReader();

                        if (flightInfoReader.Read())
                        {
                            int availableSeats = flightInfoReader.GetInt32(0);
                            decimal economyPrice = flightInfoReader.GetDecimal(1);
                            decimal businessPrice = flightInfoReader.GetDecimal(2);

                            flightInfoReader.Close();

                            if (availableSeats < noOfSeats)
                            {
                                MessageBox.Show("Insufficient seats in the selected class.");
                                return;
                            }

                            SqlCommand updateBookingCommand = new SqlCommand("UPDATE booking SET class = @NewFlightClass WHERE flight_id = @FlightId AND U_email = @UserEmail", connection);
                            updateBookingCommand.Parameters.AddWithValue("@NewFlightClass", newFlightClass);
                            updateBookingCommand.Parameters.AddWithValue("@FlightId", flightId);
                            updateBookingCommand.Parameters.AddWithValue("@UserEmail", userEmail);

                            int rowsAffected = updateBookingCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                int priceDifference = CalculatePriceDifference(currentFlightClass, newFlightClass, noOfSeats, economyPrice, businessPrice);

                                // Increase seats in the class the customer is leaving
                                SqlCommand increaseSeatsCommand = new SqlCommand("UPDATE flight SET seats_" + currentFlightClass + " = seats_" + currentFlightClass + " + @NoOfSeats WHERE id = @FlightId", connection);
                                increaseSeatsCommand.Parameters.AddWithValue("@NoOfSeats", noOfSeats);
                                increaseSeatsCommand.Parameters.AddWithValue("@FlightId", flightId);
                                increaseSeatsCommand.ExecuteNonQuery();

                                // Decrease seats in the new class
                                SqlCommand decreaseSeatsCommand = new SqlCommand("UPDATE flight SET seats_" + newFlightClass + " = seats_" + newFlightClass + " - @NoOfSeats WHERE id = @FlightId", connection);
                                decreaseSeatsCommand.Parameters.AddWithValue("@NoOfSeats", noOfSeats);
                                decreaseSeatsCommand.Parameters.AddWithValue("@FlightId", flightId);
                                decreaseSeatsCommand.ExecuteNonQuery();

                                MessageBox.Show(priceDifference > 0 ? $"Please pay ${priceDifference}." : $"You will be refunded ${-priceDifference}.");

                                this.Hide();
                                customer_list f3 = new customer_list(email, type);
                                f3.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Failed to change booking class.");
                            }
                        }
                        else
                        {
                            flightInfoReader.Close();
                            MessageBox.Show("Flight information not found.");
                        }
                    }
                    else
                    {
                        reader.Close();
                        MessageBox.Show("Booking not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private int CalculatePriceDifference(string currentClass, string newClass, int noOfSeats, decimal economyPrice, decimal businessPrice)
        {
            decimal currentPrice = currentClass == "e" ? economyPrice : businessPrice;
            decimal newPrice = newClass == "e" ? economyPrice : businessPrice;

            return (int)((newPrice - currentPrice) * noOfSeats);
        }



    }
}
