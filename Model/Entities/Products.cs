using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MODEL.ApplicationConfig;

namespace MODEL.Entities
{
    [Table("Tbl_Product")]
    public class Products : Common
    {
        [Key]
        public Guid ProductID { get; set; } = new Guid();
        public string? ProductName { get; set; } = null!;
        public int? RemainingStock { get; set; }
        public decimal? ProductPrice { get; set; }
        public decimal? ProductProfit { get; set; }
    }
}
