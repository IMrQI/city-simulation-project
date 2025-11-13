using System;
using System.Collections.Generic;

namespace CitySimulation.Models
{
    public class LogisticsOrder
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string PickupAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public string Vehicle { get; set; }
        public string DriverName { get; set; }
        public DateTime DateCreated { get; set; }
        public string ReportPath { get; set; } // Путь к файлу отчета
    }
}