using Core.Entities.Concretes;
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
        public static string CarImageIdDoesntExists="Car Image id doesnt exists";
        public static string EmailExists="Email already exists";
        public static string UserRegistered="User registered";
        public static string AccessTokenCreated="Access Token has been created";
        public static string UserNotFound="User couldnt found";
        public static string InvalidPassword="Invalid password";
        public static string LoginSuccess="Successfully logged in";
        public static string AuthorizationDenied="Authorization is denied";
    }
}
