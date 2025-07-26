namespace Domain.Models.OrderModels
{
    public class OrderItem:BaseEntity<Guid>
    {
        public OrderItem()
        {

        }
        public OrderItem(int productId, string productName, string pictureUrl, decimal quntity, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
            Quntity = quntity;
            Price = price;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Quntity { get; set; }
        public decimal Price { get; set; }
    }
}