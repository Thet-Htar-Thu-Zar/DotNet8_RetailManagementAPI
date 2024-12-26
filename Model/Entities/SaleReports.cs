using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entities
{
    [Table("Tbl_Sale")]
    public class SaleReports
    {
        [Key]
        public Guid SaleID { get; set; } = new Guid();
        public Guid ProductID { get; set; }
        public int QuantitySold { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalProfit { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public bool ActiveFlag { get; set; } = true;
    }
}
