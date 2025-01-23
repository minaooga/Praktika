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
using Gosti;

namespace Gosti
{
    public partial class HousekeeperForm : Form
    {
        string connectionString = "Data Source=LAPTOP-JK1JSQVJ\\MSSQLSERVER02;Initial Catalog=Rum;Integrated Security=True";

        public HousekeeperForm()
        {
            InitializeComponent();
            LoadRooms(); // Загружаем данные о номерах
            LoadRoomServices(); // Загружаем данные об услугах
        }

        // Метод для загрузки данных о номерах
        private void LoadRooms()
        {
            string query = "SELECT RoomID, RoomNumber, Status FROM Rooms";

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

        // Метод для загрузки данных об услугах для номеров
        private void LoadRoomServices()
        {
            string query = "SELECT ServiceID, ServiceType, RoomID FROM RoomServices";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                try
                {
                    connection.Open();
                    dataAdapter.Fill(dataTable);
                    dgvRoomServices.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки данных об услугах: " + ex.Message);
                }
            }
        }

        // Обработчик для обновления статуса комнаты
        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvRooms.SelectedRows.Count > 0 && comboBoxStatus.SelectedIndex != -1)
            {
                int roomID = Convert.ToInt32(dgvRooms.SelectedRows[0].Cells["RoomID"].Value);
                string newStatus = comboBoxStatus.SelectedItem.ToString();

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
                        MessageBox.Show("Статус комнаты обновлен!");
                        LoadRooms(); // Перезагружаем таблицу номеров
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка обновления статуса комнаты: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите комнату и статус.");
            }
        }

        // Обработчик для удаления услуги
        private void btnDeleteService_Click(object sender, EventArgs e)
        {
            if (dgvRoomServices.SelectedRows.Count > 0)
            {
                int serviceID = Convert.ToInt32(dgvRoomServices.SelectedRows[0].Cells["ServiceID"].Value);
                string query = "DELETE FROM RoomServices WHERE ServiceID = @ServiceID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ServiceID", serviceID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Услуга удалена!");
                        LoadRoomServices(); // Перезагружаем таблицу услуг
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка удаления услуги: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите услугу для удаления.");
            }
        }

        // Обработчик для добавления новой услуги
        private void btnAddService_Click(object sender, EventArgs e)
        {
            AddServiceForm addServiceForm = new AddServiceForm();
            addServiceForm.ShowDialog();
            LoadRoomServices();  // Обновляем таблицу RoomServices, чтобы отобразить добавленные услуги
        }
    }
}
