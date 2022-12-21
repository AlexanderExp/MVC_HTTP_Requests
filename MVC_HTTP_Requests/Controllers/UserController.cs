using Microsoft.AspNetCore.Mvc;
using Peer7.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
namespace Peer7.Controllers
{
    /// <summary>
    /// Class of UserController.
    /// </summary>
    public class UserController : Controller
    {
        /// <summary>
        /// The List of all users.
        /// </summary>
        public static List<UserInfo> users = new List<UserInfo>();
        /// <summary>
        /// The POST method.
        /// </summary>
        /// <returns> Result of an operation. </returns>
        [HttpPost("create-list")]
        public IActionResult CreateUsers()
        {
            try
            {
                HandleUsersForPost();
            }
            catch (Exception)
            {

                return NotFound("At least one of the users is not correct OR \"Users.json\" is not empty");
            }
            try
            {
                HandleMessagesForPost();
            }
            catch (Exception)
            {

                return NotFound("At least one of the messages is not correct OR \"Messages.json\" is not empty");
            }
            return Ok();
        }

        /// <summary>
        /// Handles the Serialization of Messages file.
        /// </summary>
        public void HandleMessagesForPost()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<MessageInfo>));
            using (FileStream fs = new FileStream("Messages.json", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                if (fs.Length == 0)
                {
                    MessageController.messages = RandomGenerator.CreateMessagesList(users);
                    serializer.WriteObject(fs, MessageController.messages);
                }

            }
        }

        /// <summary>
        /// Handles the Serialization of Users file.
        /// </summary>
        public void HandleUsersForPost()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<UserInfo>));
            using (FileStream fs = new FileStream("Users.json", FileMode.OpenOrCreate))
            {
                if (fs.Length == 0)
                {
                    users = RandomGenerator.CreateUsersList();
                    serializer.WriteObject(fs, users);
                }
            }
        }
        /// <summary>
        /// The GET method (Gets single user by his Id).
        /// </summary>
        /// <returns> Result of an operation. </returns>
        [HttpGet("get-user-by-email")]
        public IActionResult GetUserById([FromQuery] string email)
        {
            var result = users.Where(x => x.Email == email).ToList();
            if (result.Count == 0)
            {
                return NotFound("not found");
            }
            return Ok(result.First());
        }


        /// <summary>
        /// The Get method (Gets the List of all Users).
        /// </summary>
        /// <returns> Result of an operation. </returns>
        [HttpGet("get-all-users")]
        public IActionResult GetAllUsers()
        {
            if (users.Count == 0)
            {
                return NotFound("not found");
            }
            return Ok(users);
        }
    }
}
