using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Common.Wrappers
{
    public class CategoryWrapper
    {
        public string CategoryName { get; set; }
        public int? ParentCategoryId { get; set; }

        #region Navigations

        public CategoryWrapper? ParentCategory { get; set; }

        #endregion
    }
}
