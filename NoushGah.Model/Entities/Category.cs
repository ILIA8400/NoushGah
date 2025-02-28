using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Model.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public int? ParentCategoryId { get; set; }

        #region Navigations

        public Category? ParentCategory { get; set; }

        #endregion

    }
}
