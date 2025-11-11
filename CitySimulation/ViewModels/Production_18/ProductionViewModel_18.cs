using CitySimulation.Infrastructure;
using CitySimulation.Models.Production_18;
using CitySimulation.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CitySimulation.ViewModels.Production_18
{
    /// <summary>
    /// VIEWMODEL: Управление производственными цехами (Вариант 18)
    /// Содержит логику для работы с цехами и производственными линиями
    /// </summary>
    public class ProductionViewModel : ViewModelBase
    {
        private ObservableCollection<Workshop> _workshops;
        private ObservableCollection<ProductionLine> _productionLines;
        private Workshop _selectedWorkshop;
        private ProductionLine _selectedProductionLine;
        private string _statusMessage;

        // Свойства для форм ввода
        private string _newWorkshopName;
        private string _newWorkshopAddress;
        private string _newWorkshopProductionType;
        private int _newWorkshopWorkerCapacity;
        private string _newProductionLineName;
        private string _newProductionLineProductType;
        private int _newProductionLineUnitsPerHour;

        public ProductionViewModel()
        {
            Workshops = new ObservableCollection<Workshop>();
            ProductionLines = new ObservableCollection<ProductionLine>();

            InitializeTestData();

            // Команды для цехов
            AddWorkshopCommand = new RelayCommand(ExecuteAddWorkshop, CanAddWorkshop);
            StartProductionCommand = new RelayCommand(ExecuteStartProduction, CanStartProduction);

            // Команды для производственных линий
            AddProductionLineCommand = new RelayCommand(ExecuteAddProductionLine, CanAddProductionLine);
            ToggleProductionLineCommand = new RelayCommand(ExecuteToggleProductionLine, CanToggleProductionLine);
        }

        private void InitializeTestData()
        {
            // Тестовые данные для цехов
            Workshops.Add(new Workshop
            {
                Name = "Цех металлообработки №1",
                Address = "ул. Промышленная, 15",
                ProductionType = "Металлические изделия",
                WorkerCapacity = 50,
                ProductionEfficiency = 0.85,
                DailyOutput = 1000,
                XCoordinate = 400,
                YCoordinate = 250
            });

            Workshops.Add(new Workshop
            {
                Name = "Деревообрабатывающий цех",
                Address = "ул. Лесная, 8",
                ProductionType = "Деревянные изделия",
                WorkerCapacity = 30,
                ProductionEfficiency = 0.75,
                DailyOutput = 500,
                XCoordinate = 500,
                YCoordinate = 300
            });

            // Тестовые данные для производственных линий
            ProductionLines.Add(new ProductionLine
            {
                Name = "Линия сборки станков",
                ProductType = "Промышленные станки",
                UnitsPerHour = 5,
                QualityRate = 0.92,
                IsActive = true
            });

            // Устанавливаем значения по умолчанию для форм
            NewWorkshopWorkerCapacity = 20;
            NewProductionLineUnitsPerHour = 10;
        }

        // Основные коллекции
        public ObservableCollection<Workshop> Workshops { get => _workshops; set => SetProperty(ref _workshops, value); }
        public ObservableCollection<ProductionLine> ProductionLines { get => _productionLines; set => SetProperty(ref _productionLines, value); }

        // Выбранные элементы
        public Workshop SelectedWorkshop { get => _selectedWorkshop; set => SetProperty(ref _selectedWorkshop, value); }
        public ProductionLine SelectedProductionLine { get => _selectedProductionLine; set => SetProperty(ref _selectedProductionLine, value); }
        public string StatusMessage { get => _statusMessage; set => SetProperty(ref _statusMessage, value); }

        // Свойства для формы цехов
        public string NewWorkshopName { get => _newWorkshopName; set => SetProperty(ref _newWorkshopName, value); }
        public string NewWorkshopAddress { get => _newWorkshopAddress; set => SetProperty(ref _newWorkshopAddress, value); }
        public string NewWorkshopProductionType { get => _newWorkshopProductionType; set => SetProperty(ref _newWorkshopProductionType, value); }
        public int NewWorkshopWorkerCapacity { get => _newWorkshopWorkerCapacity; set => SetProperty(ref _newWorkshopWorkerCapacity, value); }

        // Свойства для формы производственных линий
        public string NewProductionLineName { get => _newProductionLineName; set => SetProperty(ref _newProductionLineName, value); }
        public string NewProductionLineProductType { get => _newProductionLineProductType; set => SetProperty(ref _newProductionLineProductType, value); }
        public int NewProductionLineUnitsPerHour { get => _newProductionLineUnitsPerHour; set => SetProperty(ref _newProductionLineUnitsPerHour, value); }

        // Команды
        public ICommand AddWorkshopCommand { get; }
        public ICommand StartProductionCommand { get; }
        public ICommand AddProductionLineCommand { get; }
        public ICommand ToggleProductionLineCommand { get; }

        // Методы для цехов
        private void ExecuteAddWorkshop(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(NewWorkshopName) && !string.IsNullOrWhiteSpace(NewWorkshopAddress))
            {
                var newWorkshop = new Workshop
                {
                    Name = NewWorkshopName.Trim(),
                    Address = NewWorkshopAddress.Trim(),
                    ProductionType = NewWorkshopProductionType ?? "Общее производство",
                    WorkerCapacity = NewWorkshopWorkerCapacity,
                    ProductionEfficiency = 0.7, // Базовая эффективность
                    DailyOutput = NewWorkshopWorkerCapacity * 10, // Базовая производительность
                    XCoordinate = Workshops.Count * 120 + 100,
                    YCoordinate = Workshops.Count * 100 + 100
                };

                Workshops.Add(newWorkshop);
                StatusMessage = $"🏭 Добавлен новый цех: {newWorkshop.Name}";

                // Сбрасываем форму
                NewWorkshopName = "";
                NewWorkshopAddress = "";
            }
            else
            {
                StatusMessage = "❌ Заполните название и адрес цеха";
            }
        }

        private bool CanAddWorkshop(object parameter) =>
            !string.IsNullOrWhiteSpace(NewWorkshopName) && !string.IsNullOrWhiteSpace(NewWorkshopAddress);

        private void ExecuteStartProduction(object parameter)
        {
            if (SelectedWorkshop != null)
            {
                double production = SelectedWorkshop.DailyOutput * SelectedWorkshop.ProductionEfficiency;
                StatusMessage = $"⚙️ Запущено производство в цехе '{SelectedWorkshop.Name}'. " +
                              $"Выпущено: {production:F0} единиц продукции";

                // Улучшаем эффективность при регулярном использовании
                if (SelectedWorkshop.ProductionEfficiency < 0.95)
                    SelectedWorkshop.ProductionEfficiency += 0.01;
            }
            else
            {
                StatusMessage = "❌ Выберите цех для запуска производства";
            }
        }

        private bool CanStartProduction(object parameter) => SelectedWorkshop != null;

        // Методы для производственных линий
        private void ExecuteAddProductionLine(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(NewProductionLineName) && !string.IsNullOrWhiteSpace(NewProductionLineProductType))
            {
                var newProductionLine = new ProductionLine
                {
                    Name = NewProductionLineName.Trim(),
                    ProductType = NewProductionLineProductType.Trim(),
                    UnitsPerHour = NewProductionLineUnitsPerHour,
                    QualityRate = 0.85, // Базовая качество
                    IsActive = true
                };

                ProductionLines.Add(newProductionLine);
                StatusMessage = $"📦 Добавлена новая производственная линия: {newProductionLine.Name}";

                // Сбрасываем форму
                NewProductionLineName = "";
                NewProductionLineProductType = "";
            }
            else
            {
                StatusMessage = "❌ Заполните название и тип продукции для линии";
            }
        }

        private bool CanAddProductionLine(object parameter) =>
            !string.IsNullOrWhiteSpace(NewProductionLineName) && !string.IsNullOrWhiteSpace(NewProductionLineProductType);

        private void ExecuteToggleProductionLine(object parameter)
        {
            if (SelectedProductionLine != null)
            {
                SelectedProductionLine.IsActive = !SelectedProductionLine.IsActive;
                StatusMessage = $"🔧 Производственная линия '{SelectedProductionLine.Name}' " +
                              $"{(SelectedProductionLine.IsActive ? "АКТИВИРОВАНА" : "ОСТАНОВЛЕНА")}";
            }
            else
            {
                StatusMessage = "❌ Выберите производственную линию для управления";
            }
        }

        private bool CanToggleProductionLine(object parameter) => SelectedProductionLine != null;
    }
}