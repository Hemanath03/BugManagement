using Modules.BugManagement.Shared.Domain.Enums;

namespace Modules.BugManagement.Bugs.ViewModels
{
   public record BugView
    {
        public long Id { get; set; }
        public string Title { get; set; } 
        public string? Description { get; set; }
        public BugStatus Status { get; set; } 
        public string Priority { get; set; }
    }
    public static class BugViewExtensions
    {
        public static BugView ToView(this Shared.Domain.Entities.Bug bug)
        {
            return new BugView
            {
                Id = bug.Id,
                Title = bug.Title,
                Description = bug.Description,
                Status = bug.Status,
                Priority = bug.Priority
            };
        }
    }
}
