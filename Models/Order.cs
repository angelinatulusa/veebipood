namespace veebipood.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime created { get; set; }
        public double TotalSum { get; set; }
        public bool Paid { get; set; }
        public ICollection<CartProduct> CartProduct { get; set; }
        public Person Person { get; set; }
    }
}
