using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAToolKit.Models
{
    public class ItemToCopy
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public ItemToCopy()
        { }
        public ItemToCopy (string name, bool ischecked = false)
        {
            Name = name;
            IsChecked = ischecked;
        }
    }
}
