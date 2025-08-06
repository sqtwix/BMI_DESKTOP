using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMI_client.Classes;
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
using System.Net.Http;
using Newtonsoft.Json;

namespace BMI_client.Pages
{
    /// <summary>
    /// Логика взаимодействия для BMITrackingPage.xaml
    /// </summary>
    public partial class BMITrackingPage : Page
    {
        public BMITrackingPage()
        {
            InitializeComponent();

            NameLabel.Content = SessionManager.UserName;
        }

        double result_BMI; // global var for containing bmi value

        public void ToAuth(object sender, RoutedEventArgs e) // To login page
        {
            NavigationService.Navigate(new LoginPage());
        }

        public void ToStatsGraphs(object sender, RoutedEventArgs e)  // To Stats page
        {
            NavigationService.Navigate(new StatsGraphicsPage());
        }

        public async void SaveBMI(object sender, RoutedEventArgs e)
        {
            string weight = Weight.Text;
            string height = Height.Text;
            string bmi = Convert.ToString(result_BMI);

            if (weight == "") { MessageBox.Show("Заполните поле веса"); }
            else if (height == "") { MessageBox.Show("Заполните поле роста"); }
            else {await SaveStatsAPI(weight, height, bmi); }
        }

       
        public void FindingBMI(object sender, RoutedEventArgs e) 
        {
            string s_height = Height.Text;
            string s_weight = Weight.Text;

            if (s_height == "") { MessageBox.Show("Введите рост!"); }
            else if (s_weight == "") { MessageBox.Show("Введите вес!"); }
            else
            {   
                double height = Convert.ToDouble(s_height);
                double weight = Convert.ToDouble(s_weight);

                result_BMI = Math.Round((weight / Math.Pow((height / 100.0), 2)), 2);

                ResultLabel.Content = string.Format(" Ваш индекс массы тела: {0}", result_BMI );
                SaveButton.Visibility = Visibility.Visible;
            }

        }

        public async Task SaveStatsAPI(string weightStr, string heightStr, string bmiStr) // API for saving weight, height, bmi
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", SessionManager.AccessToken); // Token for id

            var url = "http://127.0.0.1:8080/bmi_api/bmi/"; // url of local server


            // Преобразуем строки в числа
            if (!float.TryParse(weightStr, out float weight) ||
                !float.TryParse(heightStr, out float height) ||
                !float.TryParse(bmiStr, out float bmi))
            {
                MessageBox.Show("Некорректные числовые значения!");
                return;
            }

            var loginData = new
            {
                weight = weight,
                height = height,
                bmi = bmi
            };

            string json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var responce = await httpClient.PostAsync(url, content);

            if (responce.IsSuccessStatusCode)
            {
                MessageBox.Show("Данные успешно сохраненны");
            }
            else
            {
                var error = await responce.Content.ReadAsStringAsync();
                MessageBox.Show("Ошибка"+error);
            }
        }

    }
}
