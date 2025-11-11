using CitySimulation.Models.Base;

namespace CitySimulation.Models.ResourceExtraction_17
{
    /// <summary>
    /// МОДЕЛЬ: Оборудование для добычи ресурсов (Вариант 17)
    /// Описывает технику и инструменты для extraction
    /// </summary>
    public class ExtractionEquipment : ObservableObject
    {
        private string _name;
        private string _equipmentType;
        private double _efficiency;
        private double _maintenanceCost;
        private bool _isOperational;

        /// <summary>Название оборудования</summary>
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        /// <summary>Тип оборудования (буровая установка, экскаватор и т.д.)</summary>
        public string EquipmentType { get => _equipmentType; set => SetProperty(ref _equipmentType, value); }

        /// <summary>Эффективность работы (0.0 - 1.0)</summary>
        public double Efficiency { get => _efficiency; set => SetProperty(ref _efficiency, value); }

        /// <summary>Стоимость обслуживания в день</summary>
        public double MaintenanceCost { get => _maintenanceCost; set => SetProperty(ref _maintenanceCost, value); }

        /// <summary>Статус работоспособности</summary>
        public bool IsOperational { get => _isOperational; set => SetProperty(ref _isOperational, value); }
    }
}