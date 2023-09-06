namespace veebipood.Models
{
    public class CartProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product prodId { get; set; }
        public int Quantity { get; set; }
    }
}
