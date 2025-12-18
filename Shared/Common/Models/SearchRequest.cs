using System.Text.Json.Serialization;

namespace Shared.Common.Models
{
    public record SearchRequest 
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        [JsonIgnore]
        public int Skip => (PageNumber - 1) * PageSize;
    }
}
