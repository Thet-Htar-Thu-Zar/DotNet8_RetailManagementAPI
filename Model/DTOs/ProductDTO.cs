namespace MODEL.DTOs
{
    public class AddProductDTO
    {
        public string? ProductName { get; set;}
        public int? RemainingStock { get; set;}
        public decimal? ProductPrice { get; set;}
        public decimal? ProductProfit { get; set;}
        public string? CreatedBy { get; set; }
    }

    public class  UpdateDTO
    {
        public Guid ProductID { get; set; }
        public string? ProductName { get; set; } = null!;
        public int? RemainingStock { get; set; }
        public decimal? ProductPrice { get; set; }
        public decimal? ProductProfit { get; set; }
        public string? UpdatedBy { get;}
    }

    public class DeleteProductDTO
    {
        public Guid ProductID { get; set; }
    }
}
