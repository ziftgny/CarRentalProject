using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string InvalidValue = "You have entered an invalid value";
        public static string CarNameMinChar = "Car names must have minimum 3 characters";
        public static string CarNotDelivered = "Car is not delivered to the customer";
        public static string CarIdDoesntExists = "Car id does not exists";
        public static string MaxCarImage= "You can't add more than 5 images for a single car";
        internal static string CarImageIdDoesntExists="Car Image id doesnt exists";
    }
}
