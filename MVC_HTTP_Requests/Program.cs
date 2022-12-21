using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Peer7.Controllers;
using Peer7.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Peer7
{
    /// <summary>
    /// Program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The Main method.
        /// </summary>
        /// <param name="args"> Unnesessary arguments. </param>
        public static void Main(string[] args)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<UserInfo>));

            using (FileStream fs = new FileStream("Users.json", FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    UserController.users = (List<UserInfo>)serializer.ReadObject(fs);
                    // Lets sort the Lis that was in input file (if the file in not empty).
                    UserController.users = UserController.users.OrderBy(s => s.Email).ToList();
                }

            }


            serializer = new DataContractJsonSerializer(typeof(List<MessageInfo>));
            using (FileStream fs = new FileStream("Messages.json", FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    MessageController.messages = (List<MessageInfo>)serializer.ReadObject(fs);
                }
            }



            CreateHostBuilder(args).Build().Run();
        }



        /// <summary>
        /// Method for building the Host Page.
        /// </summary>
        /// <param name="args"> Parametrs. </param>
        /// <returns> IHostBuilder. </returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
