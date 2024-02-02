using DatabaseWPFTest.ViewModels;
using System.Windows.Controls;

namespace DatabaseWPFTest.Views
{
    public partial class EditPage : UserControl
    {
        public EditPage()
        {
            InitializeComponent();
        }
        public EditPage(EditPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
