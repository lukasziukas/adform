namespace Dtos
{
    public class InvoiceDetailsDto
    {
       public string ProductCategory { get;set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Amount { get; set; }

        public decimal TotalAmount { get; set; } 
    }
}
