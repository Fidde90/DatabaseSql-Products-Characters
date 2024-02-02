using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace DatabaseWPFTest.ViewModels
{
    public partial class DetailsViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProductService _productService;
        private readonly TracklistService _tracklistService;
        private readonly MainPageViewModel _mainPageViewModel;

        public DetailsViewModel(IServiceProvider serviceProvider, ProductService productService, TracklistService tracklistService, MainPageViewModel mainPageViewModel)
        {
            _serviceProvider = serviceProvider;
            _productService = productService;
            _tracklistService = tracklistService;
            _mainPageViewModel = mainPageViewModel;

            _detailsProduct = _productService.CurrentProduct;

            foreach (var track in _tracklistService.GetTracks(_detailsProduct.ArticleNumber))
                tracks.Add(track);
        }

        [ObservableProperty]
        private Product _detailsProduct = new Product();

        [ObservableProperty]
        private ObservableCollection<TracklistEntity> tracks = new ObservableCollection<TracklistEntity>();

        [RelayCommand]
        public void BackToHome_BtnClick()
        {
            var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindowViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<MainPageViewModel>();
            Tracks.Clear();
            _mainPageViewModel.FillListView();
        }
    }
}
