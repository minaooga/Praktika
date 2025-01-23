using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Gosti;
using System.Security.Cryptography;

namespace Gosti
{
    public partial class LoginForm : Form
    {
        string connectionString = "Data Source=LAPTOP-JK1JSQVJ\\MSSQLSERVER02;Initial Catalog=Rum;Integrated Security=True";

        public LoginForm()
        {
            InitializeComponent();
        }

        // Метод для хеширования пароля
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        // Метод для проверки логина и пароля
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Проверка на заполненность полей
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите имя пользователя и пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query;
            bool isSpecialUser = false;

            // Обработка исключений для admin и housekeeper
            if ((username == "admin" && password == "admin") || (username == "housekeeper" && password == "maid"))
            {
                query = "SELECT FullName, Role FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash";
                isSpecialUser = true; // Указываем, что пароли не будут хэшироваться
            }
            else
            {
                // Хешируем вводимый пароль для остальных пользователей
                password = HashPassword(password);
                query = "SELECT FullName, Role FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@PasswordHash", password); // Передаём пароль (хэшированный или нет)

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string fullName = reader["FullName"].ToString();
                        string role = reader["Role"].ToString();

                        // Проверка роли и открытие соответствующей формы
                        if (role == "Администратор")
                        {
                            MessageBox.Show($"Добро пожаловать, {fullName}!");

                            AdmForm admForm = new AdmForm(); // Открытие формы администратора
                            admForm.Show();
                            this.Hide();
                        }
                        else if (role == "Горничная")
                        {
                            MessageBox.Show($"Добро пожаловать, {fullName}!");

                            HousekeeperForm housekeeperForm = new HousekeeperForm(); // Открытие формы горничной
                            housekeeperForm.Show();
                            this.Hide();
                        }
                        else if (role == "Клиент")
                        {
                            MessageBox.Show($"Добро пожаловать, {fullName}!");

                            ClientForm clientForm = new ClientForm(fullName); // Открытие формы клиента
                            clientForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show($"Вы не авторизованы для входа в эту систему.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверное имя пользователя или пароль.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка входа: " + ex.Message);
                }
            }
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            // Открытие формы регистрации
            RegForm regForm = new RegForm();
            regForm.Show();
            this.Hide(); // Скрыть текущую форму (LoginForm)
        }
    }
}