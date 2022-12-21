using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Peer7.Models;

namespace Peer7
{
    /// <summary>
    /// Class for random generators.
    /// </summary>
    public class RandomGenerator
    {
        /// <summary>
        /// Creates random string for Name or Subject.
        /// </summary>
        /// <returns> Single String. </returns>
        public static string CreateRandomNameOrSubject()
        {
            Random rnd = new Random();
            // The length of the string. 
            int numOfLetters = rnd.Next(2, 16);
            string result = "";
            for (int i = 0; i < numOfLetters; i++)
            {
                result += ((char)rnd.Next(97, 123)).ToString();
            }
            return result;
        }


        /// <summary>
        /// Creates a message in Message object.
        /// </summary>
        /// <returns> Single String. </returns>
        public static string CreateRandomMessage()
        {
            Random rnd = new Random();
            // The length of the string. 
            int numOfLetters = rnd.Next(2, 501);
            string result = "";
            for (int i = 0; i < numOfLetters; i++)
            {
                result += ((char)rnd.Next(97, 123)).ToString();
            }
            return result;
        }
        /// <summary>
        /// Creates a random Email.
        /// </summary>
        /// <returns> Single string. </returns>
        public static string CreateRandomEmail()
        {
            Random rnd = new Random();
            // The length of the string. 
            int numOfLetters = rnd.Next(2, 16);
            string result = "";
            for (int i = 0; i < numOfLetters; i++)
            {
                result += ((char)rnd.Next(97, 123)).ToString();
            }
            result += "@";
            for (int i = 0; i < 4; i++)
            {
                result += ((char)rnd.Next(97, 123)).ToString();
            }
            result += ".";
            for (int i = 0; i < 2; i++)
            {
                result += ((char)rnd.Next(97, 123)).ToString();
            }
            return result;
        }
        /// <summary>
        /// Creates a List of random Messages.
        /// </summary>
        /// <param name="users"> The List of Users. </param>
        /// <returns> List of Messages. </returns>
        public static List<MessageInfo> CreateMessagesList(List<UserInfo> users)
        {
            Random rnd = new Random();
            // The amount of Messages can be a little bit less than the amount of Users OR a bit bigger.
            int numOfMessages = rnd.Next(users.Count - (int)users.Count % 10, users.Count +100);
            List<MessageInfo> result = new List<MessageInfo>();
            for (int i = 0; i < numOfMessages; i++)
            {
                // Someone can send a letter to himself (Realism).
                result.Add( new MessageInfo(){ Message = CreateRandomMessage(), ReceiverId = rnd.Next(0, users.Count), SenderId = rnd.Next(0, users.Count), Subject = CreateRandomNameOrSubject() });
            }
            return result;
        }


        /// <summary>
        /// Creates a List of random Users.
        /// </summary>
        /// <returns> List of Usrers. </returns>
        public static List<UserInfo> CreateUsersList()
        {
            Random rnd = new Random();
            List<UserInfo> users = new List<UserInfo>();
            // Lets try this interval for example.
            int numOfUsers = rnd.Next(1, 101);
            for (int i = 0; i < numOfUsers; i++)
            {

                users.Add(new UserInfo() { Email = RandomGenerator.CreateRandomEmail(), UserName = RandomGenerator.CreateRandomNameOrSubject() });
            }
            users = users.OrderBy(s => s.Email).ToList();
            return users;
        }

        internal static List<MessageInfo> CreateMessagesList(object users)
        {
            throw new NotImplementedException();
        }
    }
}
