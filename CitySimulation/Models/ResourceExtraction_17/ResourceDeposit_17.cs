using CitySimulation.Models.Base;

namespace CitySimulation.Models.ResourceExtraction_17
{
    /// <summary>
    /// МОДЕЛЬ: Месторождение природных ресурсов (Вариант 17)
    /// Содержит информацию о залежах ресурсов и их добыче
    /// </summary>
    public class ResourceDeposit : ObservableObject
    {
        private string _name;
        private Enums.ResourceType _resourceType;
        private double _capacity;
        private double _currentAmount;
        private double _extractionRate;
        private double _xCoordinate;
        private double _yCoordinate;

        /// <summary>Название месторождения</summary>
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        /// <summary>Тип добываемого ресурса</summary>
        public Enums.ResourceType ResourceType { get => _resourceType; set => SetProperty(ref _resourceType, value); }

        /// <summary>Общая вместимость месторождения</summary>
        public double Capacity { get => _capacity; set => SetProperty(ref _capacity, value); }

        /// <summary>Текущее количество ресурса</summary>
        public double CurrentAmount { get => _currentAmount; set => SetProperty(ref _currentAmount, value); }

        /// <summary>Скорость добычи (единиц в день)</summary>
        public double ExtractionRate { get => _extractionRate; set => SetProperty(ref _extractionRate, value); }

        /// <summary>Координата X на карте</summary>
        public double XCoordinate { get => _xCoordinate; set => SetProperty(ref _xCoordinate, value); }

        /// <summary>Координата Y на карте</summary>
        public double YCoordinate { get => _yCoordinate; set => SetProperty(ref _yCoordinate, value); }
    }
}