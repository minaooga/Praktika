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
    public partial class AdmForm : Form
    {
        string connectionString = "Data Source=LAPTOP-JK1JSQVJ\\MSSQLSERVER02;Initial Catalog=Rum;Integrated Security=True";

        public AdmForm()
        {
            InitializeComponent();
            LoadBookings(); // Загружаем данные о бронированиях
            LoadRooms(); // Загружаем данные о номерах
            LoadStatusOptions();
        }

        // Метод для загрузки данных о бронированиях
        private void LoadBookings()
        {
            string query = "SELECT BookingID, BookingNumber, CreatedAt, CheckInDate, CheckOutDate, RoomType, ClientName, ContactPhone, Status, TotalPrice, Notes, RoomID FROM Bookings";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    dataAdapter.Fill(dataTable);
                    dgvBookings.DataSource = dataTable;  // Убедитесь, что таблица dgvBookings обновляется
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки данных о бронированиях: " + ex.Message);
                }
            }
        }


        // Метод для загрузки данных о номерах
        private void LoadRooms()
        {
            string query = "SELECT RoomID, RoomNumber, RoomType, PricePerNight, Status, Description FROM Rooms";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                try
                {
                    connection.Open();
                    dataAdapter.Fill(dataTable);
                    dgvRooms.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки данных о номерах: " + ex.Message);
                }
            }
        }

        // Метод для удаления бронирования
        private void btnDeleteBooking_Click(object sender, EventArgs e)
        {
            if (dgvBookings.SelectedRows.Count > 0)
            {
                int bookingID = Convert.ToInt32(dgvBookings.SelectedRows[0].Cells[0].Value);
                string query = "DELETE FROM Bookings WHERE BookingID = @BookingID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@BookingID", bookingID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Бронирование удалено!");
                        LoadBookings(); // Перезагружаем таблицу бронирований
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка удаления бронирования: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите бронирование для удаления.");
            }
        }

        // Метод для редактирования бронирования
        private void btnEditBooking_Click(object sender, EventArgs e)
        {
            if (dgvBookings.SelectedRows.Count > 0)
            {
                int bookingID = Convert.ToInt32(dgvBookings.SelectedRows[0].Cells[0].Value);
                // Открытие формы редактирования бронирования
                RedBooking redBookingForm = new RedBooking(bookingID);
                redBookingForm.ShowDialog(); // Показываем форму как модальное окно
                LoadBookings(); // Перезагружаем таблицу после редактирования
            }
            else
            {
                MessageBox.Show("Выберите бронирование для редактирования.");
            }
        }
        // Метод для загрузки доступных статусов в ComboBox
        private void LoadStatusOptions()
        {
            comboBoxStatus.Items.Clear();
            comboBoxStatus.Items.Add("Доступен");
            comboBoxStatus.Items.Add("Занят");
            comboBoxStatus.Items.Add("На уборке");
            comboBoxStatus.Items.Add("В ремонте");
            comboBoxStatus.SelectedIndex = 0; // Устанавливаем первый элемент как выбранный
        }

        // Метод для изменения статуса выбранного номера
        private void btnChangeStatus_Click(object sender, EventArgs e)
        {
            if (dgvRooms.SelectedRows.Count > 0)
            {
                // Получаем выбранный RoomID
                int roomID = Convert.ToInt32(dgvRooms.SelectedRows[0].Cells["RoomID"].Value);
                string newStatus = comboBoxStatus.SelectedItem.ToString(); // Получаем выбранный статус

                // Обновляем статус в базе данных
                string query = "UPDATE Rooms SET Status = @Status WHERE RoomID = @RoomID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Status", newStatus);
                    command.Parameters.AddWithValue("@RoomID", roomID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Статус номера изменен!");
                        LoadRooms(); // Перезагружаем таблицу с номерами
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка обновления статуса: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите номер для изменения статуса.");
            }
        }
    }
}
