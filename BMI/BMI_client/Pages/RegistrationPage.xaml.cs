using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using BMI_client.Classes;

namespace BMI_client.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void ToAutorisationPage(object sender, RoutedEventArgs e) // To AutorisationPage
        {
            NavigationService.Navigate(new LoginPage());
        }

        private async void RegistrationUser(object sender, RoutedEventArgs e)
        {
            await RegisterUser();
        }

        public async Task RegisterUser() // Api to make new user
        {
            var httpClient = new HttpClient();
            var url = "http://127.0.0.1:8080/user_api/register/";

            string username = Username.Text;
            string password = Password.Text;

            if (username == "" || username == " " || password == "" || password == " ")
            {
                ErrorLabel.Content = "Заполните все поля!!!";
            }
            else if (password.Length < 5)
            {
                ErrorLabel.Content = "Пароль должен быть минимум из 5 символов!!!";
            }
            else
            {
                var requestBody = new
                {
                    username = username,
                    password = password,
                };

                string json = JsonConvert.SerializeObject(requestBody);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var responce = await httpClient.PostAsync(url, content);

                if (responce.IsSuccessStatusCode)
                {
                    MessageBox.Show("Пользователь успешно зарегестрирован");

                    string responseJson = await responce.Content.ReadAsStringAsync();

                    // Десериализуем JSON с токенами
                    var tokenData = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseJson);


                    SessionManager.AccessToken = tokenData["access"];
                    SessionManager.RefreshToken = tokenData["refresh"];
                    SessionManager.UserName = username;

                    NavigationService.Navigate(new BMITrackingPage());
                }
                else
                {
                    string error = await responce.Content.ReadAsStringAsync();
                    MessageBox.Show("Ошибка регистрации: " + error);
                }

            }

        }
    }
}
