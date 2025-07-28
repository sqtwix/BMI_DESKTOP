using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BMI_client.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public void ToRegistrationPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

        public async void  Login(object sender, RoutedEventArgs e)
        {
            await AuthUser();
        }

        public async Task AuthUser() // Api to make new user
        {
            var httpClient = new HttpClient();
            var url = "http://127.0.0.1:8080/user_api/login/";

            string username = Username.Text;
            string password = Password.Text;

            if (username == "" || username == " " || password == "" || password == " ")
            {
                //ErrorLabel.Content = "Заполните все поля!!!";
            }
           
            else
            {
                var loginData = new
                {
                    username = username,
                    password = password,
                };

                string json = JsonConvert.SerializeObject(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var responce = await httpClient.PostAsync(url, content);

                if (responce.IsSuccessStatusCode)
                {
                    string responseJson = await responce.Content.ReadAsStringAsync();
                    NavigationService.Navigate(new BMITrackingPage());
                }
                else
                {
                    string error = await responce.Content.ReadAsStringAsync();
                    MessageBox.Show("Ошибка авторизации: " + error);
                }

            }
        }

        }
    }
