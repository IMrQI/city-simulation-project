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
    public class LogisticsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<LogisticsOrder> Orders { get; set; }

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

        private string _pickupAddress;
        public string PickupAddress
        {
            get => _pickupAddress;
            set
            {
                _pickupAddress = value;
                OnPropertyChanged(nameof(PickupAddress));
            }
        }

        private string _deliveryAddress;
        public string DeliveryAddress
        {
            get => _deliveryAddress;
            set
            {
                _deliveryAddress = value;
                OnPropertyChanged(nameof(DeliveryAddress));
            }
        }

        private string _vehicle;
        public string Vehicle
        {
            get => _vehicle;
            set
            {
                _vehicle = value;
                OnPropertyChanged(nameof(Vehicle));
            }
        }

        private string _driverName;
        public string DriverName
        {
            get => _driverName;
            set
            {
                _driverName = value;
                OnPropertyChanged(nameof(DriverName));
            }
        }

        public ICommand AddOrderCommand { get; }
        public ICommand SaveReportCommand { get; }
        public ICommand LoadReportCommand { get; }

        public LogisticsViewModel()
        {
            Orders = new ObservableCollection<LogisticsOrder>();
            AddOrderCommand = new RelayCommand(AddOrder);
            SaveReportCommand = new RelayCommand(SaveReport);
            LoadReportCommand = new RelayCommand(LoadReport);
        }

        private void AddOrder(object obj)
        {
            if (string.IsNullOrWhiteSpace(CompanyName) || string.IsNullOrWhiteSpace(PickupAddress) ||
                string.IsNullOrWhiteSpace(DeliveryAddress) || string.IsNullOrWhiteSpace(Vehicle) ||
                string.IsNullOrWhiteSpace(DriverName))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var order = new LogisticsOrder
            {
                Id = Orders.Count > 0 ? Orders[Orders.Count - 1].Id + 1 : 1,
                CompanyName = CompanyName,
                PickupAddress = PickupAddress,
                DeliveryAddress = DeliveryAddress,
                Vehicle = Vehicle,
                DriverName = DriverName,
                DateCreated = DateTime.Now
            };

            Orders.Add(order);

            // Очистка полей
            CompanyName = PickupAddress = DeliveryAddress = Vehicle = DriverName = string.Empty;
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
                FileName = $"Отчет_Заказ_{order.Id}.txt",
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

        private string GenerateReport(LogisticsOrder order)
        {
            return $"Отчет о заказе #{order.Id}\n" +
                   $"Компания: {order.CompanyName}\n" +
                   $"Адрес забора: {order.PickupAddress}\n" +
                   $"Адрес доставки: {order.DeliveryAddress}\n" +
                   $"Машина: {order.Vehicle}\n" +
                   $"Перевозчик: {order.DriverName}\n" +
                   $"Дата создания: {order.DateCreated:yyyy-MM-dd HH:mm:ss}\n";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }

    // Вспомогательный класс для команд
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
        public event EventHandler CanExecuteChanged;
    }
}