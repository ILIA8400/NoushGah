using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Model.Entities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
        public virtual string? CreatedUserId { get; set; }
        public virtual string? UpdatedUserId { get; set; }
        public virtual bool IsDeleted { get; set; }

    }
}
