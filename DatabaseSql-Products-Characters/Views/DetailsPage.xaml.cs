using DatabaseWPFTest.ViewModels;
using System.Windows.Controls;

namespace DatabaseWPFTest.Views
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
