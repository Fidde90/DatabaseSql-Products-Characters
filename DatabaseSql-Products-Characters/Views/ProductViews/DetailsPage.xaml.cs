using DatabaseSql_Products_Characters.ViewModels;
using System.Windows.Controls;

namespace DatabaseSql_Products_Characters.Views
{
    public partial class DetailsPage : UserControl
    {
        public DetailsPage()
        {
            InitializeComponent();
        }
        public DetailsPage(DetailsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void TextBlock_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
