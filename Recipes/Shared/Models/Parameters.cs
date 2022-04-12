using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Shared.Models
{
    public class Parameters
    {
        const int maxPageSize = 99999;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 48;
        public bool Pagination { get; set; } = true;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value > _pageSize ? maxPageSize : value;
            }
        }
    }
}
