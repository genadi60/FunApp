using System;
using System.Collections.Generic;
using System.Text;

namespace FunApp.Services.Models.Category
{
    public class AllCategoriesViewModel
    {
        public virtual IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
