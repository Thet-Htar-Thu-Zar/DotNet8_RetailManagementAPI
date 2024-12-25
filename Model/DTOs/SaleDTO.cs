namespace MODEL.DTOs
{
    public class CreateSaleDTO
    {
        public Guid ProductID { get; set; }
        public int? QuantitySold { get; set; }
        public string? CreatedBy { get; set; }

    }
}
