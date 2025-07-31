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

        public void FindingBMI(object sender, RoutedEventArgs e) 
        {
            string s_height = Height.Text;
            string s_weight = Weight.Text;

            if (s_height == "")
            {
                MessageBox.Show("Введите рост!");
            }
            else if (s_weight == "")
            {
                MessageBox.Show("Введите вес!");
            }
            else
            {   
                double height = Convert.ToDouble(s_height);
                double weight = Convert.ToDouble(s_weight);

                result_BMI = Math.Round((weight / Math.Pow((height / 100.0), 2)), 2);

                ResultLabel.Content = string.Format(" Ваш индекс массы тела: {0}", result_BMI );
                SaveButton.Visibility = Visibility.Visible;
            }

        }
    }
}
