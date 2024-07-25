using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string MaintenanceTime = "Maintenance time";
        //USERS
        public static string UserListed = "All user are listed ";
        public static string UserUpdated = "User is updated";
        public static string UserDeleted = "User is deleted";
        public static string UserAdded = "Users added succesfully ";
        /// 
        public static string? AuthorizationDenied = "Authorization denied";
        public static string UserRegistered = "User registered";
        public static string UserNotFound = "User not found";
        public static string PasswordError = "Password error";
        public static string SuccessfulLogin = "Successful login";
        public static string UserAlreadyExists = "User already exists";
        public static string AccessTokenCreated = "Access Token Created";
        internal static List<StudentNoteDto> NotesListed;
    }
}
