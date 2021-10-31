namespace ShopOnline.Core.Entities
{
    public class OrderDetailEntity
    {
        public int Price { get; set; }

        public int IdOrder { get; set; }
        public OrderEntity Order { get; set; }

        public int IdProduct { get; set; }
        public ProductEntity Product { get; set; }
    }
}
