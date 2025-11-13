using CitySimulation.ViewModels;
using System.Windows;
using System.Windows.Controls; 

namespace CitySimulation.Views.Logistics_19
{
    public partial class LogisticsView : UserControl
    {
        private LogisticsViewModel ViewModel { get; }

        public LogisticsView()
        {
            ViewModel = new LogisticsViewModel();
            DataContext = ViewModel;
            InitializeComponent();
            OrdersGrid.ItemsSource = ViewModel.Orders;
        }

        private void BtnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.CompanyName = TxtCompanyName.Text;
            ViewModel.PickupAddress = TxtPickupAddress.Text;
            ViewModel.DeliveryAddress = TxtDeliveryAddress.Text;
            ViewModel.Vehicle = TxtVehicle.Text;
            ViewModel.DriverName = TxtDriverName.Text;

            ViewModel.AddOrderCommand.Execute(null);
        }

        private void BtnSaveReport_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SaveReportCommand.Execute(null);
        }

        private void BtnLoadReport_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadReportCommand.Execute(null);
        }
    }
}