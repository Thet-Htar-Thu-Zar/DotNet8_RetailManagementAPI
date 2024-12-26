namespace MODEL.ApplicationConfig
{
    public class Common
    {
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public bool ActiveFlag { get; set; } = true;

    }
}
