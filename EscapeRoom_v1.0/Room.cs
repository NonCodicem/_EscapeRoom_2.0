using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace EscapeRoom_v1._0
{
    class Room : Actor
    {
        //public string Name { get; }
        //public string Description { get; set; }
        //public System.Windows.Media.ImageSource Source { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public List<Door> Doors { get; set; } = new List<Door>();
        public string image { get; set; }

        public Room(string nm, string desc) : base(nm, desc)
        {
            //Name = nm;
            //Description = desc;
        }

        public Uri SetImage(Room roomName)
        {
                        
            BitmapImage screenshot = new BitmapImage();
            Uri source = null;

            switch (roomName.Name)
            {
                case "Bedroom":
                    source = screenshot.UriSource = new Uri("ss-bedroom.PNG", UriKind.Relative);
                    break;

                default:
                    break;
            }

            return source;

        }



        
    }
}
