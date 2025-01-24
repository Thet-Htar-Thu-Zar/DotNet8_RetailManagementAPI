using MODEL.ApplicationConfig;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MODEL.Entities
{
    [Table("Tbl_ATMUsers")]
    public class Users : Common
    {
        [Key]
        public Guid UserID { get; set; } = new Guid();
        public string? Password { get; set; }
        public string? UserName { get; set; }
        public decimal? Amount { get; set; }
        public string UserRole { get; set; }
        public bool? IsLocked { get; set; } = false;
    }
}
