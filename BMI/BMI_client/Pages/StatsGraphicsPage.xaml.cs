using BMI_client.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Net.Http;
using System.Security.Policy;
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
    /// Логика взаимодействия для StatsGraphicsPage.xaml
    /// </summary>
    public partial class StatsGraphicsPage : Page
    {
        public StatsGraphicsPage()
        {
            InitializeComponent();
        }

        public async Task<List<BMIRecord>> GetBMIStats()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", SessionManager.AccessToken); // Token for id

            string  url = "http://127.0.0.1:8080/bmi_api/bmi/getList/";

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var bmi_records = JsonConvert.DeserializeObject<List<BMIRecord>>(json);

                return bmi_records;
            }

            else
            {
                MessageBox.Show("Ошибка получения данных: " + await response.Content.ReadAsStringAsync());
                return new List<BMIRecord>();
            }

        }
    }
}
