using System;
using System.Collections.Generic;
using System.Text;

namespace Interview.Data.Entities.Abstract
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; } 
        public int CreatedById { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedById { get; set; }

        public bool IsActive { get; set; }
    }
}
