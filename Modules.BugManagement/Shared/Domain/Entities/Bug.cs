using Modules.BugManagement.Shared.Domain.Enums;
using Shared.Common.EntityHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.BugManagement.Shared.Domain.Entities
{
    public class Bug : BaseModel
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public BugStatus Status { get; set; } = BugStatus.Open;
        public string Priority { get; set; } = "Medium";
    }
}
