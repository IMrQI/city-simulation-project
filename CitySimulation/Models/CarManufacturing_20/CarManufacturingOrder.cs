using System;

namespace CitySimulation.Models
{
    public class CarManufacturingOrder
    {
        public int Id { get; set; }
        public string FactoryAddress { get; set; }
        public string CompanyName { get; set; }
        public string ProducedModels { get; set; }
        public string PartsSourceAddress { get; set; }
        public string DeliveryDestinationAddress { get; set; }
        public DateTime DateCreated { get; set; }
        public string ReportPath { get; set; } // Путь к файлу отчета
    }
}