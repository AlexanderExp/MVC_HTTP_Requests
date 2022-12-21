using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Peer7.Models;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Peer7.Controllers
{
    /// <summary>
    /// Class for MessageController.
    /// </summary>
    public class MessageController : Controller
    {
        /// <summary>
        /// The list of all messages.
        /// </summary>
        public static List<MessageInfo> messages = new List<MessageInfo>();
        /// <summary>
        /// Gets messages that were send by User with {senderId} and received by User with {receiverId}.
        /// </summary>
        /// <param name="senderId"> The Id of the sender. </param>
        /// <param name="receiverId"> The Id of the receiver. </param>
        /// <returns> Result of an operation. </returns>
        [HttpGet("get-messages-by-SenderId-and-ReceiverId")]
        public IActionResult GetMessagesSenderReceiver([FromQuery] int senderId, [FromQuery] int receiverId)
        {
            var result = messages.Where(x => x.SenderId == senderId).Where(y => y.ReceiverId == receiverId).ToList();
            if (result.Count == 0)
            {
                return NotFound("not found");
            }
            return Ok(result);
        }

        /// <summary>
        /// Gets messages that were send by User with {senderId}.
        /// </summary>
        /// <param name="senderId"> The Id of the sender. </param>
        /// <returns> Result of an operation. </returns>
        [HttpGet("get-messages-by-SenderId")]
        public IActionResult GetMessagesSender([FromQuery] int senderId)
        {
            var result = messages.Where(x => x.SenderId == senderId).ToList();
            if (result.Count == 0)
            {
                return NotFound("not found");
            }
            return Ok(result);
        }

        /// <summary>
        /// Gets messages that were receiver by User with {receiverId}.
        /// </summary>
        /// <param name="receiverId"> The Id of the receiver. </param>
        /// <returns> Result of an operation. </returns>
        [HttpGet("get-messages-by-Receiver")]
        public IActionResult GetMessagesReceiver([FromQuery] int receiverId)
        {
            var result = messages.Where(x => x.ReceiverId == receiverId).ToList();
            if (result.Count == 0)
            {
                return NotFound("not found");
            }
            return Ok(result);
        }

        /// <summary>
        /// Adds new Message to the List of Messages.
        /// </summary>
        /// <param name="receiverId"> The Id of the receiver. </param>
        /// <param name="senderId"> The Id of the sender. </param>
        /// <param name="Subject"> Subject of the Message. </param>
        /// <param name="Message"> The Message. </param>
        /// <returns></returns>
        [HttpPost("add-message")]
        public IActionResult CreateNewMessage([FromQuery] int receiverId, [FromQuery] int senderId, [FromQuery] string Subject, [FromQuery] string Message)
        {
            if (receiverId < 0 || receiverId > (UserController.users.Count-1) || senderId < 0 || senderId > (UserController.users.Count - 1))
            {
                return NotFound("bad request");
            }
            if (Subject.Length > 15 || Message.Length > 500 || Message.Length < 2 || Subject.Length < 2)
            {
                return NotFound("Too long Message or Subject");
            }
            try
            {
                messages.Add(new MessageInfo() {SenderId = senderId, Message = Message, ReceiverId = receiverId, Subject = Subject });
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<MessageInfo>));
                using (FileStream fs = new FileStream("Messages.json", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    if (fs.Length == 0)
                    {
                        MessageController.messages = RandomGenerator.CreateMessagesList(UserController.users);
                        serializer.WriteObject(fs, MessageController.messages);
                    }

                }
            }
            catch (Exception)
            {
                return NotFound("bad request");
            }
            return Ok();

        }



        /// <summary>
        /// To make testing the program a bit easier.
        /// </summary>
        /// <returns> Result of an operation. </returns>
        [HttpGet("get-all-messages")]
        public IActionResult GetAllMessages()
        {
            return Ok(messages);
        }
    }
}
