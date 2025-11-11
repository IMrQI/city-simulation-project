using CitySimulation.Models.Base;

namespace CitySimulation.Models.Production_18
{
    /// <summary>
    /// МОДЕЛЬ: Производственная линия (Вариант 18)
    /// Отвечает за конкретный процесс производства
    /// </summary>
    public class ProductionLine : ObservableObject
    {
        private string _name;
        private string _productType;
        private int _unitsPerHour;
        private double _qualityRate;
        private bool _isActive;

        /// <summary>Название производственной линии</summary>
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        /// <summary>Тип производимого продукта</summary>
        public string ProductType { get => _productType; set => SetProperty(ref _productType, value); }

        /// <summary>Производительность (единиц в час)</summary>
        public int UnitsPerHour { get => _unitsPerHour; set => SetProperty(ref _unitsPerHour, value); }

        /// <summary>Уровень качества продукции (0.0 - 1.0)</summary>
        public double QualityRate { get => _qualityRate; set => SetProperty(ref _qualityRate, value); }

        /// <summary>Статус активности линии</summary>
        public bool IsActive { get => _isActive; set => SetProperty(ref _isActive, value); }
    }
}