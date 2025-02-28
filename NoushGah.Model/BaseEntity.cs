using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Model
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime UpdateDate { get; set; }
        public virtual int CreateUserId { get; set; }
        public virtual int UpdateUserId { get; set; }
        public virtual bool IsDeleted { get; set; }

    }
}
