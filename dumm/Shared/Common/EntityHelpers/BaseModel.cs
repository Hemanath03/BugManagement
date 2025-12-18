
namespace Shared.Common.EntityHelpers
{
    public class BaseModel 
    {
        public bool Archived { get; set; } = false;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? ArchivedBy { get; set; }
        public DateTime? ArchivedAt { get; set; }
    }
}
