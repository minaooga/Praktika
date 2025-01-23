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
    public partial class AddServiceForm : Form
    {
        string connectionString = "Data Source=LAPTOP-JK1JSQVJ\\MSSQLSERVER02;Initial Catalog=Rum;Integrated Security=True";

        public AddServiceForm()
        {
            InitializeComponent();
            LoadRooms(); // Загружаем номера комнат и BookingID
            LoadServiceTypes(); // Загружаем типы услуг
        }

        // Метод для загрузки номеров комнат и BookingID в ComboBox
        private void LoadRooms()
        {
            string query = @"
                SELECT r.RoomID, r.RoomNumber, b.BookingID 
                FROM Rooms r
                LEFT JOIN Bookings b ON r.RoomID = b.RoomID
                WHERE r.Status = 'Занят'"; // Фильтруем только занятые комнаты

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                try
                {
                    connection.Open();
                    dataAdapter.Fill(dataTable);
                    comboBoxRoomID.DataSource = dataTable;
                    comboBoxRoomID.DisplayMember = "RoomNumber"; // Показываем номер комнаты
                    comboBoxRoomID.ValueMember = "RoomID"; // Привязываем RoomID
                    comboBoxBookingID.DataSource = dataTable;
                    comboBoxBookingID.DisplayMember = "BookingID"; // Показываем BookingID
                    comboBoxBookingID.ValueMember = "BookingID"; // Привязываем BookingID
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки данных о номерах: " + ex.Message);
                }
            }
        }

        // Метод для загрузки типов услуг в ComboBox
        private void LoadServiceTypes()
        {
            var serviceTypes = new[] { "Уборка", "Заказ еды", "Спа-процедуры", "Транспортные услуги" };
            comboBoxServiceType.Items.AddRange(serviceTypes);
        }

        // Обработчик для добавления услуги
        private void btnAddService_Click(object sender, EventArgs e)
        {
            if (comboBoxRoomID.SelectedIndex != -1 && comboBoxServiceType.SelectedIndex != -1 && comboBoxBookingID.SelectedIndex != -1)
            {
                int roomID = Convert.ToInt32(comboBoxRoomID.SelectedValue);
                int bookingID = Convert.ToInt32(comboBoxBookingID.SelectedValue);
                string serviceType = comboBoxServiceType.SelectedItem.ToString();

                string query = "INSERT INTO RoomServices (RoomID, BookingID, ServiceType) VALUES (@RoomID, @BookingID, @ServiceType)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@RoomID", roomID);
                    command.Parameters.AddWithValue("@BookingID", bookingID);
                    command.Parameters.AddWithValue("@ServiceType", serviceType);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Услуга добавлена!");
                        this.Close(); // Закрываем форму после добавления
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка добавления услуги: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите номер комнаты, тип услуги и бронирование.");
            }
        }
    }
}