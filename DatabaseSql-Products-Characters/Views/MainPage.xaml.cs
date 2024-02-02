using DatabaseWPFTest.ViewModels;
using System.Windows.Controls;

namespace DatabaseWPFTest.Views
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
