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

namespace Gosti
{
    public partial class RedBooking : Form
    {
        string connectionString = "Data Source=LAPTOP-JK1JSQVJ\\MSSQLSERVER02;Initial Catalog=Rum;Integrated Security=True";
        private int _bookingID; // ID бронирования для редактирования

        public RedBooking(int bookingID)
        {
            InitializeComponent();
            _bookingID = bookingID;
            LoadBookingDetails();
            LoadRoomIDs();
        }

        // Метод для загрузки данных о бронировании
        private void LoadBookingDetails()
        {
            string query = "SELECT BookingNumber, ClientName, ContactPhone, CheckInDate, CheckOutDate, RoomType, Status, TotalPrice, Notes, RoomID " +
                           "FROM Bookings WHERE BookingID = @BookingID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookingID", _bookingID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    txtBookingNumber.Text = reader["BookingNumber"].ToString();
                    txtClientName.Text = reader["ClientName"].ToString();
                    txtContactPhone.Text = reader["ContactPhone"].ToString();
                    dtpCheckInDate.Value = Convert.ToDateTime(reader["CheckInDate"]);
                    dtpCheckOutDate.Value = Convert.ToDateTime(reader["CheckOutDate"]);
                    cmbRoomType.SelectedItem = reader["RoomType"].ToString();
                    cmbStatus.SelectedItem = reader["Status"].ToString();
                    txtTotalPrice.Text = reader["TotalPrice"].ToString();
                    txtNotes.Text = reader["Notes"].ToString();
                    cmbRoomID.SelectedValue = reader["RoomID"];  // Устанавливаем выбранную комнату по RoomID
                }
            }
        }

        private void LoadRoomIDs()
        {
            string query = "SELECT RoomID, RoomNumber FROM Rooms";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                try
                {
                    connection.Open();
                    dataAdapter.Fill(dataTable);
                    cmbRoomID.DataSource = dataTable;
                    cmbRoomID.DisplayMember = "RoomNumber";  // Отображаем номер комнаты
                    cmbRoomID.ValueMember = "RoomID";        // Привязываем ID комнаты
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки номеров: " + ex.Message);
                }
            }
        }

        // Метод для сохранения изменений
        private void btnSave_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Bookings SET BookingNumber = @BookingNumber, ClientName = @ClientName, ContactPhone = @ContactPhone, " +
                           "CheckInDate = @CheckInDate, CheckOutDate = @CheckOutDate, RoomType = @RoomType, Status = @Status, " +
                           "TotalPrice = @TotalPrice, Notes = @Notes, RoomID = @RoomID WHERE BookingID = @BookingID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookingNumber", txtBookingNumber.Text);
                command.Parameters.AddWithValue("@ClientName", txtClientName.Text);
                command.Parameters.AddWithValue("@ContactPhone", txtContactPhone.Text);
                command.Parameters.AddWithValue("@CheckInDate", dtpCheckInDate.Value);
                command.Parameters.AddWithValue("@CheckOutDate", dtpCheckOutDate.Value);
                command.Parameters.AddWithValue("@RoomType", cmbRoomType.SelectedItem);
                command.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem);
                command.Parameters.AddWithValue("@TotalPrice", Convert.ToDecimal(txtTotalPrice.Text));
                command.Parameters.AddWithValue("@Notes", txtNotes.Text);
                command.Parameters.AddWithValue("@RoomID", cmbRoomID.SelectedValue);  // Сохраняем выбранный RoomID
                command.Parameters.AddWithValue("@BookingID", _bookingID);

                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Бронирование обновлено!");
                this.Close();  // Закрываем форму после сохранения
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}