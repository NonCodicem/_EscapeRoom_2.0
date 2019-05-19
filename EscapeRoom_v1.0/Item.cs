using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom_v1._0
{
    class Item : Actor
    {

        
        public bool IsLocked { get; set; } = false;
        public bool IsPortable { get; set; } = true;
        public Item Key { get; set; }
        public Item HiddenItem { get; set; }

        public Item(string nm, string desc) :base(nm, desc)
        {
            //Name = nm;
            //Description = desc;
        }

        public Item(string nm, string desc, bool portable) 
            : this(nm, desc)
        {
            IsPortable = portable;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
