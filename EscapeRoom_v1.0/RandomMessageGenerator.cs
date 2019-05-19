using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom_v1._0
{
    static class RandomMessageGenerator
    {
        static private string[] IsLockedMsg { get; } = new string[3] { "The item is locked.", "Try finding the right key.", "You need a key for this" };
        static private string[] IsWrongKeyMsg { get; } = new string[3] { "This key doen't fit.", "Find an other key to open this.", "This key wont do the trick." };
        static private string[] DoesNotWorkMsg { get; } = new string[3] { "This doesn't seem to work.", "This is not going to work.", "You need to try something else."};
        //private static List<string> IsLockedMsg { get; } = new List<string>();
        //private static List<string> IsWrongKeyMsg { get; } = new List<string>();
        //private static List<string> DoesNotWorkMsg { get; } = new List<string>();

        private static Random rmd = new Random();

        public static string GetRandomMsg(MessageType type)
        {
            string msg = "";
            int randomNumber = rmd.Next(0, 3);

            switch (type) 
            {
                case MessageType.IS_LOCKED:
                    msg = IsLockedMsg[randomNumber];
                    break;
                case MessageType.IS_WRONG_KEY:
                    msg = IsWrongKeyMsg[randomNumber];
                    break;
                case MessageType.DOES_NOT_WORK:
                    msg = DoesNotWorkMsg[randomNumber];
                    break;
                default:
                    msg = "That doesn't seem to work";
                    break;
            }
            return msg;

        }


    }
}
