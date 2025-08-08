using BMI_client.Classes;
using LiveCharts;
using LiveCharts.Wpf;
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
        public SeriesCollection BMISeries { get; set; }
        public SeriesCollection WeightSeries { get; set; }
        public SeriesCollection HeightSeries { get; set; }
        public string[] Labels { get; set; }




        public StatsGraphicsPage()
        {
            InitializeComponent();
            LoadCharts();
        }

        public async void LoadCharts() // Async function for getting list of bmi, weight, height data and building graphic
        {
            var bmiValues = new ChartValues<double> { };  //BMI Data for chart
            var weightValues = new ChartValues<double> { };  //Weight Data for chart
            var heightValues = new ChartValues<double> { };  //Height Data for chart
            var dateValues = new List<string>();  //Date Data for chart



            var bmi_records = await GetBMIStats();  // List of objects of class BMIRecords

            foreach (var item in bmi_records)  // Loop for adding only bmi to bmiValues
            {
                bmiValues.Add(item.bmi);
            }

            foreach (var item in bmi_records)  // Loop for adding only weight to weightValues
            {
                weightValues.Add(item.weight);
            }

            foreach (var item in bmi_records)  // Loop for adding only height to heightValues
            {
                heightValues.Add(item.height);
            }

            foreach (var item in bmi_records)  // Loop for adding only adte to dateValues
            {
                dateValues.Add(item.date.ToString("dd.MM"));
            }

            BMISeries = new SeriesCollection  // Series Collection for building bmi chart
            {
                new LineSeries
                {
                    Title = "BMI",
                    Values = bmiValues,
                    PointGeometry = DefaultGeometries.Circle
                     
                }
             };

            WeightSeries = new SeriesCollection  // Series Collection for building weight chart
            {
                new LineSeries
                {
                    Title = "Weight",
                    Values = weightValues,
                    PointGeometry = DefaultGeometries.Circle

                }
             };

            HeightSeries = new SeriesCollection  // Series Collection for building height chart
            {
                new LineSeries
                {
                    Title = "Height",
                    Values = heightValues,
                    PointGeometry = DefaultGeometries.Circle

                }
             };

            Labels = dateValues.ToArray();
            // Bind the data context to this instance
            DataContext = this;
        }

        public async Task<List<BMIRecord>> GetBMIStats()  // Async API function for getting data from db
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
