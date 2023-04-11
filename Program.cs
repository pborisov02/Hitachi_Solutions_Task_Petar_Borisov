using System.IO;
using System.Net;
using System.Reflection;

namespace Hitachi_Solutions_Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Here we store the appropriate days
            List<Day> days = new List<Day>();
            //We use this class to deserialize the .csv file
            Deserializer deserializer = new Deserializer();
            //We use this class to serialize the data into new .csv file
            Serializer serializer = new Serializer();
            //We use this class to send Emails via SMTP
            Sender sender = new Sender();
            
            //Here we store the values from the file, so later we can make aggregations
            int[] _temperatures = new int[15];
            int[] _wind = new int[15];
            int[] _humidity = new int[15];
            int[] _precipitation = new int[15];

            while (true)
            {
                string filePath;
                //First we give the user a chance to select custom criteria
                Console.WriteLine("Select option: ");
                Console.WriteLine("1. Use default criteria");
                Console.WriteLine("2. Use custom criteria");
                Console.WriteLine("0. Exit");
                string option = Console.ReadLine();
                if (option == "0")
                    return;
               
                //Here we check if the option is valid.
                if(option != "1" && option != "2" && option != "0")
                {
                    Console.WriteLine("Error! Invalid option.");
                    Console.WriteLine();
                    continue;
                }


                //We read the input needed in every case
                //In this do-while loop we check if the file exists 
                do
                {
                    Console.Write("File path: ");
                    filePath = Console.ReadLine();
                    if (!File.Exists(filePath))
                    {
                        Console.WriteLine("Error! File not found.");
                        Console.WriteLine();
                    }
                } while (!File.Exists(filePath));

                Console.Write("Sender Email: ");
                string ?senderEmail = Console.ReadLine();
                Console.Write("Sender password: ");
                string ?password = Console.ReadLine();
                Console.Write("Receiver email: ");
                string ?receiverEmail = Console.ReadLine();

                //Without custom criteria
                if (option == "1")
                {
                    //First we deserialize the file
                    days = deserializer.Deserialize(filePath, out _temperatures, out _wind, out _humidity, out _precipitation, false);
                }
                //With custom criteria
                else if (option == "2")
                {
                    days = deserializer.Deserialize(filePath, out _temperatures, out _wind, out _humidity, out _precipitation, true);
                }
                
                if (days.Count == 0) 
                {
                    //If there is no appropriate day found
                    serializer.SerializeWithNoAppropriateDay(_temperatures, _wind, _humidity, _precipitation);
                    sender.SendEmailWithNoAppropriateDay(senderEmail, password, receiverEmail);
                }
                else
                {
                    //If we have found an appropriate day
                    serializer.Serialize(_temperatures, _wind, _humidity, _precipitation, days[0]);
                    sender.SendEmail(senderEmail, password, receiverEmail, days[0]);
                }
                
            }
        }
    }
}