namespace Domain.Entities
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Name { get; set; }
        
        public int TotalItems { get; set; }

        public decimal TotalPrice { get; set; }
        
       
    }
}
