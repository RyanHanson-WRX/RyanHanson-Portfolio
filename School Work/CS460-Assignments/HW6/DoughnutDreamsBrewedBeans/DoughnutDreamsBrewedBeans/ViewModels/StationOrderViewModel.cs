namespace DoughnutDreamsBrewedBeans.ViewModels
{
    public class StationOrderViewModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string DeliveryLocation { get; set; }
        public string Store { get; set; }
        public List<StationOrderItemViewModel> OrderItems { get; set; }
    }
}