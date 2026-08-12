namespace E_CommerceManagementSystemEfCore.Entities
{
    public class OrderItem
    {
        public int OrderId { get; set; }

        public Order Order { get; set; } = new();


        public int ProductId { get; set; }

        public Product Product { get; set; } = new();


        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}