using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peer7.Models
{
    /// <summary>
    /// Class for Messages.
    /// </summary>
    public class MessageInfo
    {
        /// <summary>
        /// The subject of the message.
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// The message itself.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Id of the sender.
        /// </summary>
        public int SenderId { get; set; }
        /// <summary>
        /// Id of the Receiver.
        /// </summary>
        public int ReceiverId { get; set; }
    }
}
