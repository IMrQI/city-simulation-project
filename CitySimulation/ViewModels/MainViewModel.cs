using CitySimulation.ViewModels.Base;
using CitySimulation.ViewModels.ForeignRelations;
using CitySimulation.ViewModels.EmergencyService;
using CitySimulation.ViewModels.ResourceExtraction_17;
using CitySimulation.ViewModels.Production_18;

namespace CitySimulation.ViewModels
{
    /// <summary>
    /// ГЛАВНЫЙ VIEWMODEL: Объединяет все модули приложения
    /// Содержит ссылки на ViewModel'и всех вариантов задач
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        // Существующие ViewModel'и
        public ForeignRelationsViewModel ForeignRelationsVM { get; }
        public EmergencyServiceViewModel EmergencyServiceVM { get; }

        // Новые ViewModel'и для вариантов 17 и 18
        public ResourceExtractionViewModel ResourceExtractionVM_17 { get; }
        public ProductionViewModel ProductionVM_18 { get; }

        public MainViewModel()
        {
            // Инициализация всех ViewModel'ей
            ForeignRelationsVM = new ForeignRelationsViewModel();
            EmergencyServiceVM = new EmergencyServiceViewModel();
            ResourceExtractionVM_17 = new ResourceExtractionViewModel(); // Вариант 17
            ProductionVM_18 = new ProductionViewModel(); // Вариант 18
        }
    }
}