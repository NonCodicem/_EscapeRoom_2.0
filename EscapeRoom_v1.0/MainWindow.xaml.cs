using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EscapeRoom_v1._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

        public enum MessageType { IS_LOCKED, IS_WRONG_KEY, DOES_NOT_WORK}

    public partial class MainWindow : Window
    {
        Room currentRoom;

        public MainWindow()
        {
            InitializeComponent();

            //define Bedroom
            Room Bedroom = new Room("Bedroom", "I seem to be in a medium sized bedroom. " +
                "There is a locker to the left, a nice rug on the" +
                "floor, and a bed to the right. ");

                       
            //define items
            Item key1 = new Item("Small silver key", "A small silver key, " +
                "makes me think of one I hadat highschool. ");
            Item key2 = new Item("Large key", "A large key. Could this be the way out?");

            Item locker = new Item("Locker", "A locker. I wonder what's inside.", false);
            locker.HiddenItem = key2;
            locker.IsLocked = true;
            locker.Key = key1;

            Item bed = new Item("Bed", "Just a bed. I am not tired now.", false);
            bed.HiddenItem = key1;

            Item chair = new Item("Chair", "Just a chair. I do not want to sit rigth now", false);
            Item poster = new Item("Poster", "A flashy poster.");

            Bedroom.Items.Add(new Item("Floor mat", "A bit ragged floor mat, " +
                "but still one of the most popular designs. "));
            Bedroom.Items.Add(bed);
            Bedroom.Items.Add(locker);
            Bedroom.Items.Add(chair);
            Bedroom.Items.Add(poster);

            //define Livingroom
            Room Livingroom = new Room("Livingroom", "Seems to be the living room.");

            Item clock = new Item("Clock", "A clock. Makes me aware of the amount of time I have spent in this room.", false);

            Livingroom.Items.Add(clock);

            //define Computerroom
            Room Computerroom = new Room("Computerroom", "Hey, wow. A computer!");

            Item computer = new Item("Computer", "A computer. Lets try to turn it on.", false);

            Computerroom.Items.Add(computer);

            //define doors
            Door bedroomToLivingDoor = new Door("a green door", " A discription", Livingroom, true)
            // alternatief voor: bedroomToLiving.Key = key2;
            {
                Key = key2
            };
            Door LivingDoorToBedroom = new Door("Door to bedroom", " A discription", Bedroom, false);
            Bedroom.Doors.Add(bedroomToLivingDoor);
            Livingroom.Doors.Add(LivingDoorToBedroom);

            Door LivingToComputerroomDoor = new Door("Door to the computer room.", " A discription", Computerroom, false);
            Livingroom.Doors.Add(LivingToComputerroomDoor);

            Door ComputerroomToLivingDoor = new Door("Door to living room", " A discription", Livingroom, false);
            Computerroom.Doors.Add(ComputerroomToLivingDoor);
            //define picture
            //imgRoom.Source = currentRoom.SetImage(currentRoom);

            currentRoom = Bedroom;
            lblMessage.Content = "I am awake, but cannot remember who I am. Must have been a hell of a partu last night ...";
            txtRoomDesc.Text = currentRoom.Description;
            UpdateUI();
                        
        }

        private void UpdateUI()
        {

            lstRoomItems.Items.Clear();
            foreach (Item itm in currentRoom.Items)
            {
                lstRoomItems.Items.Add(itm);
            }
            lstDoors.Items.Clear();
            foreach (Door itm in currentRoom.Doors)
            {
                lstDoors.Items.Add(itm);
            }

            //update image
            //maak een property van het type string in de klaase room. maak een constructor voor room met een derde parameter zijnde de string naar image
            //maak een folder in visual studio zelf en sleep de screenshots daar naartoe. in de upodate UI neem je de syntax van google op ==> new bitmap ... en je gebruikt currentroom.image (eventueel met folder naam ervvor.
        }

        private void LstRoomItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnCheck.IsEnabled = lstRoomItems.SelectedValue != null;
            btnPickUp.IsEnabled = lstRoomItems.SelectedValue != null;
            btnUseOne.IsEnabled = lstRoomItems.SelectedValue != null && lstMyItems.SelectedValue != null;

        }

        private void LstMyItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnCheck.IsEnabled = lstRoomItems.SelectedValue != null;
            btnPickUp.IsEnabled = lstRoomItems.SelectedValue != null;
            btnUseOne.IsEnabled = lstRoomItems.SelectedValue != null && lstMyItems.SelectedValue != null;
        }

        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {
            //find item to check
            Item roomitem = (Item)lstRoomItems.SelectedItem;

            //is it locked 
            if (roomitem.IsLocked)
            {
                lblMessage.Content = RandomMessageGenerator.GetRandomMsg(MessageType.IS_LOCKED);
                //lblMessage.Content = $"{roomitem.Description} It is firmly locked!";
                return;
            }

            //does it contain a hidden item
            Item foundItem = roomitem.HiddenItem;
            if (foundItem != null)
            {
                lblMessage.Content = $"Oh, look I found a {foundItem.Name} .";
                lstMyItems.Items.Add(foundItem);
                roomitem.HiddenItem = null;
                return;
            }

            //just another item; show description
            lblMessage.Content = roomitem.Description;
        }

        private void BtnUseOne_Click(object sender, RoutedEventArgs e)
        {
            //find both items
            Item myItem = (Item)lstMyItems.SelectedItem;
            Item roomItem = (Item)lstRoomItems.SelectedItem;
            
            //item doesnt fit
            if (roomItem.Key != myItem)
            {
                lblMessage.Content = RandomMessageGenerator.GetRandomMsg(MessageType.IS_WRONG_KEY);
                //lblMessage.Content = "That doesn't seem to work.";
                return;
            }

            //item fits; other item unlocked
            roomItem.IsLocked = false;
            roomItem.Key = null;
            lstMyItems.Items.Remove(myItem);
            lblMessage.Content = $"You just unlocked {roomItem.Name} !";
        }

        private void BtnPickUp_Click(object sender, RoutedEventArgs e)
        {
            //find selected item
            Item selItem = (Item)lstRoomItems.SelectedItem;

            //check if item is portable
            if (!selItem.IsPortable)
            {
                lblMessage.Content = $"The item {selItem.Name} is not something you can carry around.";
                return;
            }

            //add item to the item list
            lblMessage.Content = $"You just picked up {selItem.Name} .";
            lstMyItems.Items.Add(selItem);
            lstRoomItems.Items.Remove(selItem);
            currentRoom.Items.Remove(selItem);
        }

        private void btnDrop_Click(object sender, RoutedEventArgs e)
        {
            //find selected item
            Item dropItem = (Item)lstMyItems.SelectedItem;

            //remove item from the item list
            lblMessage.Content = $"You just droped {dropItem.Name} .";
            lstMyItems.Items.Remove(dropItem);
            lstRoomItems.Items.Add(dropItem);
            currentRoom.Items.Add(dropItem);
        }

        private void btnOpenWith_Click(object sender, RoutedEventArgs e)
        {
            //find both items
            Item myItem = (Item)lstMyItems.SelectedItem;
            Door door = (Door)lstDoors.SelectedItem;

            //item doesnt fit
            if (door.Key != myItem)
            {
                lblMessage.Content = RandomMessageGenerator.GetRandomMsg(MessageType.IS_WRONG_KEY);
                //lblMessage.Content = "That doesn't seem to work.";
                return;
            }

            //item fits; other item unlocked
            door.IsLocked = false;
            door.Key = null;
            lstMyItems.Items.Remove(myItem);
            lblMessage.Content = $"You just unlocked {door.Name} !";
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            Door door = (Door)lstDoors.SelectedItem;

            if (door.IsLocked)
            {
                lblMessage.Content = RandomMessageGenerator.GetRandomMsg(MessageType.IS_LOCKED);
                return;
            }
            currentRoom = door.NextRoom;
            lblMessage.Content = "I found an other room!";
            txtRoomDesc.Text = currentRoom.Description;
            UpdateUI();
        }
    }
}
