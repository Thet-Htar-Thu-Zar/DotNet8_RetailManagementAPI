namespace MODEL.DTOs
{
    public class CreateSaleDTO
    {
        public Guid ProductID { get; set; }
        public int? QuantitySold { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TotalProfit { get; set; }

    }
}
