using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Realtime.Domain.Entity
{
    public class TopicSQL
    {
        public Guid Id {get;set;}
        public Guid? TopicSQL2Id {get; set;}
        public TopicSQL2? TopicSQL2 {get; set;}
        public string NameSQL {get; set;} = string.Empty;
        public string? Description {get; set;}
        public bool? IsActive {get; set;}
        public bool? IsDeleted {get; set;}
        public DateTime? CreatedAt {get; set;}
        public DateTime? UpdatedAt {get; set;}
    }
}