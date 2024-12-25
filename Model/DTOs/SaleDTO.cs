namespace MODEL.DTOs
{
    public class CreateSaleDTO
    {
        public Guid ProductID { get; set; }
        public int? QuantitySold { get; set; }
        public string? CreatedBy { get; set; }

    }
    public class SaleSummary
    {
        public decimal TotalRevenue { get; set; } 
        public decimal TotalProfit { get; set; }
    }
}
