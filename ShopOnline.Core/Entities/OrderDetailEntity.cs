namespace ShopOnline.Core.Entities
{
    public class OrderDetailEntity
    {
        public int IdOrder { get; set; }
        public OrderEntity Order { get; set; }

        public int IdProduct { get; set; }
        public ProductEntity Product { get; set; }

        public int Price { get; set; }
    }
}
