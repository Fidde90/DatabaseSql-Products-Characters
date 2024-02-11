using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Services;
using System.Windows;

namespace DatabaseSql_Products_Characters.ViewModels
{
    public partial class EditPageViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProductService _productService;
        private readonly DescriptionService _descriptionService;
        private readonly SpecificationService _specificationService;
        private readonly MainPageViewModel _mainPageViewModel;

        public EditPageViewModel(IServiceProvider serviceProvider, ProductService productService, DescriptionService descriptionService, SpecificationService specificationService,MainPageViewModel mainPageViewModel)
        {
            _serviceProvider = serviceProvider;
            _productService = productService;
            _descriptionService = descriptionService;
            _specificationService = specificationService;
            _mainPageViewModel = mainPageViewModel;

            _editProduct = _productService.CurrentProduct;
        }

        [ObservableProperty]
        public Product _editProduct = new();

        [RelayCommand]
        public void Update_BtnClick()
        {
            var converted = _productService.ConvertTo_ProductEntity(EditProduct);
            _productService.UpdateProduct(converted);
            _descriptionService.UpdateDescription(EditProduct);
            _specificationService.UpdateSpecification(EditProduct);
            MessageBox.Show("Saved!");
        }

        [RelayCommand]
        public void BackToHomePage_BtnClick()
        {
            var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindowViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<MainPageViewModel>();
     
            _mainPageViewModel.FillListView();
        }
    }
}
