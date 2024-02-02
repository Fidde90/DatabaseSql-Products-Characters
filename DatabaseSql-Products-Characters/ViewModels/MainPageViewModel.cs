using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DatabaseWPFTest.Models;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;

namespace DatabaseWPFTest.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProductService _productService;
        private readonly GenreService _genreService;
        private readonly CategoryService _categoryService;
        private readonly ProductRepository _productRepository;
        private readonly TracklistService _tracklistService;
        private readonly DescriptionService _descriptionService;
        private readonly SpecificationService _specificationService;

        public MainPageViewModel(IServiceProvider serviceProvider, ProductService productService, GenreService genreService, CategoryService categoryService, 
            ProductRepository productRepository, TracklistService tracklistService, DescriptionService descriptionService, SpecificationService specificationService)
        {
            _serviceProvider = serviceProvider;
            _productService = productService;
            _genreService = genreService;
            _categoryService = categoryService;
            _productRepository = productRepository;
            _tracklistService = tracklistService;
            _descriptionService = descriptionService;
            _specificationService = specificationService;

            Fill_Category_Dropdown();
            Fill_Genre_Dropdown();
            FillListView();
        }

        [ObservableProperty]
        ObservableCollection<Product> productList = [];

        [ObservableProperty]
        ObservableCollection<TracklistEntity> trackList = [];

        [ObservableProperty]
        ObservableCollection<string> genreList = [];

        [ObservableProperty]
        ObservableCollection<string> categoryList = [];

        [ObservableProperty]
        ProductForm form = new();

        [ObservableProperty]
        AddTracksForm addTrackform = new();

        [ObservableProperty]
        string _searchValue = null!;

        [RelayCommand]
        public void Add_BtnCLick()
        {
            Product product = new Product
            {
                ArticleNumber = Form.ArticleNumber,
                Artist = Form.Artist,
                Title = Form.Title,
                Price = Form.Price,
                Quantity = Form.Quantity,
                Genre = Form.Genre,
                Category = Form.Category,
                About = Form.About,
                ReleseDate = Form.ReleseDate,
                RecordedDate = Form.RecordedDate,
                Country = Form.Country,
                Label = Form.Label
            };

            var added = _productService.CreateProduct(product, [.. TrackList]);

            if (added)
            {
                Form = new ProductForm();
                AddTrackform = new AddTracksForm();
                FillListView();
            }
            else
                MessageBox.Show("either the product already exists or something went wrong");
            TrackList.Clear();
        }

        [RelayCommand]
        private void FindOne_BtnClick()
        {
            var product = _productService.GetProduct(SearchValue);

            if (product != null)
            {
                ProductList.Clear();
                ProductList.Add(product);
                SearchValue = string.Empty;
            }
        }

        [RelayCommand]
        public void FillListView()
        {
            ProductList.Clear();
            var products = _productRepository.GetAllFromDB();

            foreach (var entity in products)
                ProductList.Add(entity);
        }

        [RelayCommand]
        private void Edit_BtnClick(Product listviewProduct)
        {
            var specification = _specificationService.GetSpecification(listviewProduct.ArticleNumber);
            var description = _descriptionService.GetDescription(listviewProduct.ArticleNumber);

            Product editProduct = new Product
            {
                ArticleNumber = listviewProduct.ArticleNumber,
                Artist = listviewProduct.Artist,
                Title = listviewProduct.Title,
                Price = listviewProduct.Price,
                Genre = listviewProduct.Genre,
                Category = listviewProduct.Category,
                Quantity = listviewProduct.Quantity,
                About = description?.About ?? string.Empty,
                Label = specification?.Label ?? string.Empty,
                RecordedDate = specification?.RecordedDate ?? string.Empty,
                ReleseDate = specification?.ReleseDate ?? string.Empty,
                Country = specification?.Country ?? string.Empty
            };

            _productService.CurrentProduct = editProduct;

            var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindowViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<EditPageViewModel>();
        }

        [RelayCommand]
        private void Details_BtnClick(Product listviewProduct)
        {
            var specification = _specificationService.GetSpecification(listviewProduct.ArticleNumber);
            var description = _descriptionService.GetDescription(listviewProduct.ArticleNumber);

            Product editProduct = new Product
            {
                ArticleNumber = listviewProduct.ArticleNumber,
                Artist = listviewProduct.Artist,
                Title = listviewProduct.Title,
                Price = listviewProduct.Price,
                Genre = listviewProduct.Genre,
                Category = listviewProduct.Category,
                Quantity = listviewProduct.Quantity,
                About = description?.About ?? string.Empty,
                Label = specification?.Label ?? string.Empty,
                RecordedDate = specification?.RecordedDate ?? string.Empty,
                ReleseDate = specification?.ReleseDate ?? string.Empty,
                Country = specification?.Country ?? string.Empty
            };

            _productService.CurrentProduct = editProduct;

            var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindowViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<DetailsViewModel>();
        }

        [RelayCommand]
        private void Delete_BtnClick(Product listviewProduct)
        {
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete item: {listviewProduct.Title}?", "Wait!", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                _productService.DeleteProduct(listviewProduct.ArticleNumber);
                FillListView();
            }  
        }

        [RelayCommand]
        private void AddTrack_BtnClick()
        {
            TrackList.Add(new TracklistEntity
            {
                ArticleNumber = Form.ArticleNumber,
                Title = AddTrackform.SongTitle
            });
            AddTrackform = new AddTracksForm();
        }

        private void Fill_Category_Dropdown()
        {
            CategoryList.Clear();
            var categories = _categoryService.GetAllCategories();

            if (categories != null)
                for (int i = 0; i < categories.Count; i++)
                    CategoryList.Add(categories[i].CategoryName);
            else
                CategoryList.Add("Empty");
        }

        private void Fill_Genre_Dropdown()
        {
            GenreList.Clear();
            var genres = _genreService.GetAllGenres();

            if (genres != null)
                for (int x = 0; x < genres.Count; x++)
                    GenreList.Add(genres[x].GenreName);
            else
                GenreList.Add("Empty");
        }
    }
}
