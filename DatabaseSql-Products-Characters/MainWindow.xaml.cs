﻿using DatabaseSql_Products_Characters.ViewModels;
using System.Windows;

namespace DatabaseSql_Products_Characters
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}