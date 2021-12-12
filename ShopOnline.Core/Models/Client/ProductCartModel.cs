using static ShopOnline.Core.Models.Enum.AppEnum;

namespace ShopOnline.Core.Models.Client
{
    public class ProductCartModel
    {
        public int Id { get; set; }
        public int IdProductDetail { get; set; }
        public string Name { get; set; }
        public int PriceVND { get; set; }
        public int PriceUSD { get; set; }
        public int TotalVND { get; set; }
        public int TotalUSD { get; set; }
        public string Pic { get; set; }
        public int Quantity { get; set; }
        public int SelectedQuantity { get; set; }
        public ProductSize Size { get; set; }
    }
}
