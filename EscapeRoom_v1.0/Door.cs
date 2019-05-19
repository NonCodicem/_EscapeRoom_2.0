using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom_v1._0
{
    class Door :Actor
    {
        //public string Name { get; set; }
        public Room NextRoom { get; set;}
        public bool IsLocked { get; set; } = false;
        public Item Key { get; set; }

        public Door(string nm, string desc, Room nextRoom) : base(nm, desc)
        {
            //Name = nm;
            NextRoom = nextRoom;
        }

        public Door(string nm, string desc, Room nextRoom, bool lockedOrNot) : base(nm, desc)
            //:this(nm, nextRoom)
        {
            NextRoom = nextRoom;
            IsLocked = lockedOrNot;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
