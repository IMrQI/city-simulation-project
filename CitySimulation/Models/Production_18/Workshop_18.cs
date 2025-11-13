using CitySimulation.Models.Base;

namespace CitySimulation.Models.Production_18
{
    /// <summary>
    /// МОДЕЛЬ: Производственный цех (Вариант 18)
    /// Основная единица производственной инфраструктуры
    /// </summary>
    public class Workshop : Building
    {
        private string _productionType;
        private int _workerCapacity;
        private double _productionEfficiency;
        private double _dailyOutput;

        /// <summary>Тип производимой продукции</summary>
        public string ProductionType { get => _productionType; set => SetProperty(ref _productionType, value); }

        /// <summary>Вместимость рабочих</summary>
        public int WorkerCapacity { get => _workerCapacity; set => SetProperty(ref _workerCapacity, value); }

        /// <summary>Эффективность производства (0.0 - 1.0)</summary>
        public double ProductionEfficiency { get => _productionEfficiency; set => SetProperty(ref _productionEfficiency, value); }

        /// <summary>Суточный объем производства</summary>
        public double DailyOutput { get => _dailyOutput; set => SetProperty(ref _dailyOutput, value); }
    }
}