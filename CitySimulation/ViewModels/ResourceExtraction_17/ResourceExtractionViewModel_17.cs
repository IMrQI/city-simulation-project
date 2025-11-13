using CitySimulation.Infrastructure;
using CitySimulation.Models.ResourceExtraction_17;
using CitySimulation.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CitySimulation.ViewModels.ResourceExtraction_17
{
    /// <summary>
    /// VIEWMODEL: Управление добычей ресурсов (Вариант 17)
    /// Содержит логику для работы с месторождениями и оборудованием
    /// </summary>
    public class ResourceExtractionViewModel : ViewModelBase
    {
        private ObservableCollection<ResourceDeposit> _resourceDeposits;
        private ObservableCollection<ExtractionEquipment> _equipment;
        private ResourceDeposit _selectedDeposit;
        private ExtractionEquipment _selectedEquipment;
        private string _statusMessage;

        // Свойства для форм ввода
        private string _newDepositName;
        private string _newDepositResourceType;
        private double _newDepositCapacity;
        private string _newEquipmentName;
        private string _newEquipmentType;
        private double _newEquipmentEfficiency;

        public ResourceExtractionViewModel()
        {
            ResourceDeposits = new ObservableCollection<ResourceDeposit>();
            Equipment = new ObservableCollection<ExtractionEquipment>();

            InitializeTestData();

            // Команды для месторождений
            AddDepositCommand = new RelayCommand(ExecuteAddDeposit, CanAddDeposit);
            ExtractResourcesCommand = new RelayCommand(ExecuteExtractResources, CanExtractResources);

            // Команды для оборудования
            AddEquipmentCommand = new RelayCommand(ExecuteAddEquipment, CanAddEquipment);
            ToggleEquipmentCommand = new RelayCommand(ExecuteToggleEquipment, CanToggleEquipment);
        }

        private void InitializeTestData()
        {
            // Тестовые данные для месторождений
            ResourceDeposits.Add(new ResourceDeposit
            {
                Name = "Нефтяное месторождение №1",
                ResourceType = Enums.ResourceType.Oil,
                Capacity = 1000000,
                CurrentAmount = 800000,
                ExtractionRate = 500,
                XCoordinate = 150,
                YCoordinate = 200
            });

            ResourceDeposits.Add(new ResourceDeposit
            {
                Name = "Железный рудник",
                ResourceType = Enums.ResourceType.Iron,
                Capacity = 500000,
                CurrentAmount = 450000,
                ExtractionRate = 300,
                XCoordinate = 300,
                YCoordinate = 150
            });

            // Тестовые данные для оборудования
            Equipment.Add(new ExtractionEquipment
            {
                Name = "Буровая установка БУ-5000",
                EquipmentType = "Буровая",
                Efficiency = 0.85,
                MaintenanceCost = 15000,
                IsOperational = true
            });

            // Устанавливаем значения по умолчанию для форм
            NewDepositCapacity = 100000;
            NewEquipmentEfficiency = 0.8;
        }

        // Основные коллекции
        public ObservableCollection<ResourceDeposit> ResourceDeposits { get => _resourceDeposits; set => SetProperty(ref _resourceDeposits, value); }
        public ObservableCollection<ExtractionEquipment> Equipment { get => _equipment; set => SetProperty(ref _equipment, value); }

        // Выбранные элементы
        public ResourceDeposit SelectedDeposit { get => _selectedDeposit; set => SetProperty(ref _selectedDeposit, value); }
        public ExtractionEquipment SelectedEquipment { get => _selectedEquipment; set => SetProperty(ref _selectedEquipment, value); }
        public string StatusMessage { get => _statusMessage; set => SetProperty(ref _statusMessage, value); }

        // Свойства для формы месторождений
        public string NewDepositName { get => _newDepositName; set => SetProperty(ref _newDepositName, value); }
        public string NewDepositResourceType { get => _newDepositResourceType; set => SetProperty(ref _newDepositResourceType, value); }
        public double NewDepositCapacity { get => _newDepositCapacity; set => SetProperty(ref _newDepositCapacity, value); }

        // Свойства для формы оборудования
        public string NewEquipmentName { get => _newEquipmentName; set => SetProperty(ref _newEquipmentName, value); }
        public string NewEquipmentType { get => _newEquipmentType; set => SetProperty(ref _newEquipmentType, value); }
        public double NewEquipmentEfficiency { get => _newEquipmentEfficiency; set => SetProperty(ref _newEquipmentEfficiency, value); }

        // Команды
        public ICommand AddDepositCommand { get; }
        public ICommand ExtractResourcesCommand { get; }
        public ICommand AddEquipmentCommand { get; }
        public ICommand ToggleEquipmentCommand { get; }

        // Методы для месторождений
        private void ExecuteAddDeposit(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(NewDepositName) && NewDepositCapacity > 0)
            {
                var newDeposit = new ResourceDeposit
                {
                    Name = NewDepositName.Trim(),
                    ResourceType = ConvertStringToResourceType(NewDepositResourceType),
                    Capacity = NewDepositCapacity,
                    CurrentAmount = NewDepositCapacity,
                    ExtractionRate = NewDepositCapacity * 0.01, // 1% от вместимости
                    XCoordinate = ResourceDeposits.Count * 100 + 50,
                    YCoordinate = ResourceDeposits.Count * 80 + 50
                };

                ResourceDeposits.Add(newDeposit);
                StatusMessage = $"✅ Добавлено новое месторождение: {newDeposit.Name} ({newDeposit.ResourceType})";

                // Сбрасываем форму
                NewDepositName = "";
            }
            else
            {
                StatusMessage = "❌ Заполните название и вместимость месторождения";
            }
        }

        private bool CanAddDeposit(object parameter) =>
            !string.IsNullOrWhiteSpace(NewDepositName) && NewDepositCapacity > 0;

        private void ExecuteExtractResources(object parameter)
        {
            if (SelectedDeposit != null && SelectedDeposit.CurrentAmount > 0)
            {
                double extracted = SelectedDeposit.ExtractionRate;
                SelectedDeposit.CurrentAmount -= extracted;

                if (SelectedDeposit.CurrentAmount < 0)
                    SelectedDeposit.CurrentAmount = 0;

                StatusMessage = $"⛏️ Добыто {extracted} единиц {SelectedDeposit.ResourceType} из {SelectedDeposit.Name}";

                if (SelectedDeposit.CurrentAmount == 0)
                {
                    StatusMessage += " - МЕСТОРОЖДЕНИЕ ИСЧЕРПАНО!";
                }
            }
            else
            {
                StatusMessage = "❌ Выберите месторождение с ресурсами для добычи";
            }
        }

        private bool CanExtractResources(object parameter) =>
            SelectedDeposit != null && SelectedDeposit.CurrentAmount > 0;

        // Методы для оборудования
        private void ExecuteAddEquipment(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(NewEquipmentName) && !string.IsNullOrWhiteSpace(NewEquipmentType))
            {
                var newEquipment = new ExtractionEquipment
                {
                    Name = NewEquipmentName.Trim(),
                    EquipmentType = NewEquipmentType.Trim(),
                    Efficiency = NewEquipmentEfficiency,
                    MaintenanceCost = NewEquipmentEfficiency * 10000,
                    IsOperational = true
                };

                Equipment.Add(newEquipment);
                StatusMessage = $"✅ Добавлено новое оборудование: {newEquipment.Name}";

                // Сбрасываем форму
                NewEquipmentName = "";
                NewEquipmentType = "";
            }
            else
            {
                StatusMessage = "❌ Заполните название и тип оборудования";
            }
        }

        private bool CanAddEquipment(object parameter) =>
            !string.IsNullOrWhiteSpace(NewEquipmentName) && !string.IsNullOrWhiteSpace(NewEquipmentType);

        private void ExecuteToggleEquipment(object parameter)
        {
            if (SelectedEquipment != null)
            {
                SelectedEquipment.IsOperational = !SelectedEquipment.IsOperational;
                StatusMessage = $"🔧 Оборудование '{SelectedEquipment.Name}' " +
                              $"{(SelectedEquipment.IsOperational ? "ВКЛЮЧЕНО" : "ВЫКЛЮЧЕНО")}";
            }
            else
            {
                StatusMessage = "❌ Выберите оборудование для управления";
            }
        }

        private bool CanToggleEquipment(object parameter) => SelectedEquipment != null;

        // Вспомогательные методы
        private Enums.ResourceType ConvertStringToResourceType(string resource)
        {
            return resource switch
            {
                "Нефть" => Enums.ResourceType.Oil,
                "Газ" => Enums.ResourceType.Gas,
                "Железо" => Enums.ResourceType.Iron,
                "Медь" => Enums.ResourceType.Copper,
                "Дерево" => Enums.ResourceType.Wood,
                _ => Enums.ResourceType.Oil
            };
        }
    }
}