using CitySimulation.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CitySimulation.Views.AutomotiveIndustry_20
{
    public partial class AutomotiveIndustry : UserControl
    {
        private CarManufacturingViewModel ViewModel { get; }

        public AutomotiveIndustry()
        {
            ViewModel = new CarManufacturingViewModel();
            DataContext = ViewModel;
            InitializeComponent();
            OrdersGrid.ItemsSource = ViewModel.Orders;
        }

        private void BtnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            // Обновляем ViewModel с текущими значениями из TextBox
            ViewModel.FactoryAddress = GetActualText(TxtFactoryAddress, "Адрес завода");
            ViewModel.CompanyName = GetActualText(TxtCompanyName, "Компания");
            ViewModel.ProducedModels = GetActualText(TxtProducedModels, "Производимые модели");
            ViewModel.PartsSourceAddress = GetActualText(TxtPartsSourceAddress, "Адрес доставки деталей");
            ViewModel.DeliveryDestinationAddress = GetActualText(TxtDeliveryDestinationAddress, "Адрес доставки готовых машин");

            // Вызываем команду добавления заказа
            ViewModel.AddOrderCommand.Execute(null);
        }

        private void BtnSaveReport_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SaveReportCommand.Execute(null);
        }

        private void BtnLoadReport_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadReportCommand.Execute(null);
        }

        // Вспомогательный метод для получения реального текста (без placeholder'а)
        private string GetActualText(TextBox textBox, string placeholderText)
        {
            return textBox.Text == placeholderText ? string.Empty : textBox.Text;
        }

        // --- Обработчики событий для placeholder'ов ---

        private void TxtFactoryAddress_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Адрес завода")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TxtFactoryAddress_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Адрес завода";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void TxtFactoryAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Адрес завода")
            {
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void TxtCompanyName_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Компания")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TxtCompanyName_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Компания";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void TxtCompanyName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Компания")
            {
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void TxtProducedModels_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Производимые модели")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TxtProducedModels_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Производимые модели";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void TxtProducedModels_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Производимые модели")
            {
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void TxtPartsSourceAddress_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Адрес доставки деталей")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TxtPartsSourceAddress_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Адрес доставки деталей";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void TxtPartsSourceAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Адрес доставки деталей")
            {
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void TxtDeliveryDestinationAddress_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Адрес доставки готовых машин")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TxtDeliveryDestinationAddress_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Адрес доставки готовых машин";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void TxtDeliveryDestinationAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Адрес доставки готовых машин")
            {
                textBox.Foreground = Brushes.Gray;
            }
        }
    }
}