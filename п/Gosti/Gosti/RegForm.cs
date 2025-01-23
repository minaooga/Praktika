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
using System.Security.Cryptography;
using Gosti;

namespace Gosti
{
    public partial class RegForm : Form
    {
        string connectionString = "Data Source=LAPTOP-JK1JSQVJ\\MSSQLSERVER02;Initial Catalog=Rum;Integrated Security=True";

        public RegForm()
        {
            InitializeComponent();
        }

        // Метод для хеширования пароля
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password); // Преобразуем пароль в байты
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);    // Хешируем

                // Конвертируем хешированные байты в строку (16-ричное представление)
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        // Метод для обработки кнопки регистрации
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string fullName = txtFullName.Text;

            // Проверка на совпадение паролей
            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают. Попробуйте снова.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка на заполненность всех полей
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Хешируем пароль
            string hashedPassword = HashPassword(password);

            // SQL-запрос для добавления нового пользователя
            string query = "INSERT INTO Users (Username, PasswordHash, FullName, Role) " +
                           "VALUES (@Username, @PasswordHash, @FullName, 'Клиент')";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@PasswordHash", hashedPassword); // Сохраняем хешированный пароль
                    command.Parameters.AddWithValue("@FullName", fullName);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Регистрация успешна!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Переход на форму авторизации
                    LoginForm loginForm = new LoginForm();
                    loginForm.Show();
                    this.Close(); // Закрываем текущую форму
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка регистрации: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Метод для регистрации пользователя (если потребуется в другом месте)
        public bool RegisterUser(string fullName, string username, string password, string role = "Клиент")
        {
            bool isRegistered = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Запрос на добавление нового пользователя
                string query = "INSERT INTO Users (FullName, Username, PasswordHash, Role) VALUES (@FullName, @Username, @PasswordHash, @Role)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FullName", fullName);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@PasswordHash", HashPassword(password)); // Хешируем пароль перед сохранением
                command.Parameters.AddWithValue("@Role", role);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    isRegistered = rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при регистрации: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return isRegistered;
        }
    }
}