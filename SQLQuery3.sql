-- Create User table
CREATE TABLE [User] (
  email VARCHAR(255) PRIMARY KEY,
  name VARCHAR(255),
  password VARCHAR(255),
  type CHAR(1) CHECK (type IN ('A', 'C'))
);

-- Create Aircraft table
CREATE TABLE Aircraft (
  id INT PRIMARY KEY,
  name VARCHAR(255),
  max_capacity_e INT,
  max_capacity_b INT,
  model VARCHAR(255),
  by_u_email VARCHAR(255),
  FOREIGN KEY (by_u_email) REFERENCES [User] (email)
);

-- Create flight table
CREATE TABLE flight (
  id INT PRIMARY KEY,
  date DATE,
  src VARCHAR(255),
  dest VARCHAR(255),
  seats_b INT,
  b_price DECIMAL(10, 2),
  seats_e INT,
  e_price DECIMAL(10, 2),
  u_email VARCHAR(255),
  aircraft_id INT,
  FOREIGN KEY (u_email) REFERENCES [User] (email),
  FOREIGN KEY (aircraft_id) REFERENCES Aircraft (id)
);

-- Create Admin table
CREATE TABLE Admin (
  U_email VARCHAR(255) PRIMARY KEY,
  managerDegree VARCHAR(255),
  FOREIGN KEY (U_email) REFERENCES [User] (email)
);

-- Create Customer table
CREATE TABLE Customer (
  U_email VARCHAR(255) PRIMARY KEY,
  phone VARCHAR(255),
  Nationality VARCHAR(255),
  FOREIGN KEY (U_email) REFERENCES [User] (email)
);

-- Create bookedFlights table
CREATE TABLE bookedFlights (
  U_email VARCHAR(255),
  flight_id INT,
  PRIMARY KEY (U_email, flight_id),
  FOREIGN KEY (U_email) REFERENCES [User] (email),
  FOREIGN KEY (flight_id) REFERENCES flight (id)
);

-- Create booking table
CREATE TABLE booking (
  flight_id INT,
  U_email VARCHAR(255),
  class CHAR(1) CHECK (class IN ('e', 'b')),
  noSeats INT,
  PRIMARY KEY (flight_id, U_email),
  FOREIGN KEY (flight_id) REFERENCES flight (id),
  FOREIGN KEY (U_email) REFERENCES [User] (email)
);
