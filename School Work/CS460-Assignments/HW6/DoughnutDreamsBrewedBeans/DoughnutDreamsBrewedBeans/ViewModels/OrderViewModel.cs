
namespace DoughnutDreamsBrewedBeans.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Store { get; set;}
        public string CustomerName { get; set; }
        public string DeliveryLocation { get; set; }
        public string TotalPrice { get; set; }
        public string Complete { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
    }
}