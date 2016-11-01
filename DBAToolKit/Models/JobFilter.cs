using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAToolKit.Models
{
    public class JobFilter
    {
        public string CategoryName { get; set; }
        public bool IsChecked { get; set; }

        public JobFilter()
        {
        }
        public JobFilter(string categoryName, bool isChecked)
        {
            CategoryName = categoryName;
            IsChecked = isChecked;
        }
    }
}
