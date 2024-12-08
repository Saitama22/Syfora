﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfGui {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow(MainViewModel viewModel) {
			InitializeComponent();
            DataContext = viewModel;
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e) {
            var viewModel = DataContext as MainViewModel;
            if (viewModel != null) {
                await viewModel.LoadDataAsync();
            }
        }
    }
}