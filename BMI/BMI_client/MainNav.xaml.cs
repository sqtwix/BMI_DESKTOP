using System.Windows.Navigation;

namespace BMI_client
{
    public partial class MainNav : NavigationWindow
    {
        public MainNav()
        {
            InitializeComponent();  // эта строка вызывает ошибку, если XAML не скомпилировался
        }
    }
}
