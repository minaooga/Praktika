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
    public partial class ClientForm : Form
    {
        private string _fullName;
        string connectionString = "Data Source=LAPTOP-JK1JSQVJ\\MSSQLSERVER02;Initial Catalog=Rum;Integrated Security=True";
        public ClientForm(string fullName)
        {

            InitializeComponent();
            _fullName = fullName;
            lblWelcome.Text = $"Добро пожаловать, {_fullName}! Отель Mr.BeastiX рад вас видеть!";
            LoadBookings(); // Загружаем бронирования при инициализации
        }

        private void LoadBookings()
        {
            // Запрос для получения бронирований текущего клиента
            string query = "SELECT BookingID, BookingNumber, CheckInDate, CheckOutDate, RoomType, TotalPrice, Status " +
                           "FROM Bookings WHERE ClientName = @ClientName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@ClientName", _fullName);

                DataTable dataTable = new DataTable();
                try
                {
                    connection.Open();
                    dataAdapter.Fill(dataTable);
                    dgvBookings.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки данных: " + ex.Message);
                }
            }
        }

        private void btnSaveBooking_Click(object sender, EventArgs e)
        {
            // Пример сохранения новых или измененных данных бронирования
            string bookingNumber = txtBookingNumber.Text;
            DateTime checkInDate = dtpCheckInDate.Value;
            DateTime checkOutDate = dtpCheckOutDate.Value;
            string roomType = cmbRoomType.SelectedItem.ToString();
            string notes = txtNotes.Text;
            string contactPhone = txtContactPhone.Text;

            string query = "INSERT INTO Bookings (BookingNumber, CreatedAt, CheckInDate, CheckOutDate, RoomType, ClientName, Notes, ContactPhone) " +
                       "VALUES (@BookingNumber, @CreatedAt, @CheckInDate, @CheckOutDate, @RoomType, @ClientName, @Notes, @ContactPhone)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookingNumber", bookingNumber);
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                command.Parameters.AddWithValue("@CheckInDate", checkInDate);
                command.Parameters.AddWithValue("@CheckOutDate", checkOutDate);
                command.Parameters.AddWithValue("@RoomType", roomType);
                command.Parameters.AddWithValue("@ClientName", _fullName);
                command.Parameters.AddWithValue("@Notes", notes);
                command.Parameters.AddWithValue("@ContactPhone", contactPhone); 


                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Бронирование сохранено!");
                    LoadBookings(); // Перезагружаем список бронирований
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка сохранения данных: " + ex.Message);
                }
            }
        }
    }
}
