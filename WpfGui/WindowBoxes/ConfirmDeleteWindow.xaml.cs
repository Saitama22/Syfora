using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfGui.WindowBoxes
{
    /// <summary>
    /// Окно для подтвердения удаления
    /// </summary>
    public partial class ConfirmDeleteWindow : Window
    {
        public bool IsProcessing { get; set; }

        public ConfirmDeleteWindow()
        {
            InitializeComponent();
        }

        internal void AddError(string message) {
            ErrorTextBlock.Text = message;
            ErrorTextBlock.Visibility = Visibility.Visible;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
            ForceClose();
        }
        private void UpdateWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (IsProcessing) {
                e.Cancel = true;
                Visibility = Visibility.Hidden;
            }
        }
        private void ConfirmButton_Click(object sender, RoutedEventArgs e) {
            DialogResult = true;
            IsProcessing = true;
        }
        internal void ForceClose() {
            IsProcessing = false;
            Close();
        }
    }
}
