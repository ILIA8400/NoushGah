using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Model.Entities
{
    public class InvoiceItem : BaseEntity
    {
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }

        #region Navigations

        public Product Product { get; set; }
        public Invoice Invoice { get; set; }

        #endregion
    }
}
