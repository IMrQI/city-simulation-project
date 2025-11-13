using CitySimulation.Models;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace CitySimulation.ViewModels
{
    public class CarManufacturingViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CarManufacturingOrder> Orders { get; set; }

        private string _factoryAddress;
        public string FactoryAddress
        {
            get => _factoryAddress;
            set
            {
                _factoryAddress = value;
                OnPropertyChanged(nameof(FactoryAddress));
            }
        }

        private string _companyName;
        public string CompanyName
        {
            get => _companyName;
            set
            {
                _companyName = value;
                OnPropertyChanged(nameof(CompanyName));
            }
        }

        private string _producedModels;
        public string ProducedModels
        {
            get => _producedModels;
            set
            {
                _producedModels = value;
                OnPropertyChanged(nameof(ProducedModels));
            }
        }

        private string _partsSourceAddress;
        public string PartsSourceAddress
        {
            get => _partsSourceAddress;
            set
            {
                _partsSourceAddress = value;
                OnPropertyChanged(nameof(PartsSourceAddress));
            }
        }

        private string _deliveryDestinationAddress;
        public string DeliveryDestinationAddress
        {
            get => _deliveryDestinationAddress;
            set
            {
                _deliveryDestinationAddress = value;
                OnPropertyChanged(nameof(DeliveryDestinationAddress));
            }
        }

        public ICommand AddOrderCommand { get; }
        public ICommand SaveReportCommand { get; }
        public ICommand LoadReportCommand { get; }

        public CarManufacturingViewModel()
        {
            Orders = new ObservableCollection<CarManufacturingOrder>();
            AddOrderCommand = new RelayCommand(AddOrder);
            SaveReportCommand = new RelayCommand(SaveReport);
            LoadReportCommand = new RelayCommand(LoadReport);
        }

        private void AddOrder(object obj)
        {
            if (string.IsNullOrWhiteSpace(FactoryAddress) || string.IsNullOrWhiteSpace(CompanyName) ||
                string.IsNullOrWhiteSpace(ProducedModels) || string.IsNullOrWhiteSpace(PartsSourceAddress) ||
                string.IsNullOrWhiteSpace(DeliveryDestinationAddress))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var order = new CarManufacturingOrder
            {
                Id = Orders.Count > 0 ? Orders[Orders.Count - 1].Id + 1 : 1,
                FactoryAddress = FactoryAddress,
                CompanyName = CompanyName,
                ProducedModels = ProducedModels,
                PartsSourceAddress = PartsSourceAddress,
                DeliveryDestinationAddress = DeliveryDestinationAddress,
                DateCreated = DateTime.Now
            };

            Orders.Add(order);

            // Очистка полей
            FactoryAddress = CompanyName = ProducedModels = PartsSourceAddress = DeliveryDestinationAddress = string.Empty;
        }

        private void SaveReport(object obj)
        {
            var order = Orders.Count > 0 ? Orders[Orders.Count - 1] : null;
            if (order == null)
            {
                MessageBox.Show("Нет заказов для сохранения отчета.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var saveFileDialog = new SaveFileDialog
            {
                FileName = $"Отчет_Автомобилестроение_{order.Id}.txt",
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, GenerateReport(order));
                order.ReportPath = saveFileDialog.FileName;
                MessageBox.Show($"Отчет сохранен в: {saveFileDialog.FileName}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void LoadReport(object obj)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var content = File.ReadAllText(openFileDialog.FileName);
                MessageBox.Show(content, "Содержимое отчета");
            }
        }

        private string GenerateReport(CarManufacturingOrder order)
        {
            return $"Отчет о заказе автомобилестроения #{order.Id}\n" +
                   $"Компания: {order.CompanyName}\n" +
                   $"Адрес завода: {order.FactoryAddress}\n" +
                   $"Производимые модели: {order.ProducedModels}\n" +
                   $"Адрес доставки деталей: {order.PartsSourceAddress}\n" +
                   $"Адрес доставки готовых машин: {order.DeliveryDestinationAddress}\n" +
                   $"Дата создания: {order.DateCreated:yyyy-MM-dd HH:mm:ss}\n";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}