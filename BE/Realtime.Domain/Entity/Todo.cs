using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Realtime.Domain.Entity
{
    public class Todo
    {
        public Guid Id {get; set;}
        [Description("Title")]
        [MaxLength(50)]
        public string Title {get; set;} = string.Empty;
        [MaxLength(300)]
        public string? Description {get; set;}
        [Description("Due Date")]
        public DateTime? DueDate {get; set;}
        [Description("Is Completed")]
        public bool IsCompleted {get; set;}
        [Description("Is Expired")]
        public bool IsExpired {get; set;}
        public DateTime? CreatedAt {get; set;}
        public DateTime? UpdatedAt {get; set;}
        [Description("userId")]
        public Guid UserId {get; set;}
    }
}