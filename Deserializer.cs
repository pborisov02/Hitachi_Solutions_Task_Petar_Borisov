using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hitachi_Solutions_Task
{
    public class Deserializer
    {
        public List<Day> Deserialize(string filePath, out int[] _temperatures, out int[] _wind, out int[] _humidity, out int[] _precipitation, bool isCustom)
        {
            //Here we store the custom params (if this option is selected)
            int[] customParams = new int[6];
            List<string> cloudsCustomParams = new List<string>();
            
            //Here we store the appropriate days
            List<Day> appropriateDays = new List<Day>();

            //Here we store the values we read from the file
            _temperatures = new int[15];
            _wind = new int[15];
            _humidity = new int[15];
            _precipitation = new int[15];
            bool[] _lightning = new bool[15];
            string[] _clouds = new string[15];
            
            //Here the user can give custom parameters to check for appropriate day
            if(isCustom)
            {
                Console.Write("Min temparature: ");
                int minTemperature = int.Parse(Console.ReadLine());
                Console.Write("Max temparature: ");
                int maxTemperature = int.Parse(Console.ReadLine());
                Console.Write("Max wind speed: ");
                int maxWind = int.Parse(Console.ReadLine());
                Console.Write("Max humidity: ");
                int maxHumidity = int.Parse(Console.ReadLine());
                Console.Write("Precipitation: ");
                int precipitation = int.Parse(Console.ReadLine());
                Console.Write("Lightning (Yes/No): ");
                int lightning = Console.ReadLine() == "Yes" ? 1 : 0;
                Console.Write("Not appropriate cloud conditions (separated with ','): ");
                string[] cloudConditions = Console.ReadLine().Split(',', StringSplitOptions.RemoveEmptyEntries);

                customParams[0] = minTemperature;
                customParams[1] = maxTemperature;
                customParams[2] = maxWind;
                customParams[3] = maxHumidity;
                customParams[4] = lightning;
                customParams[5] = precipitation;
                cloudsCustomParams.AddRange(cloudConditions);
            }

            //Here we read the values from the file
            int row = 0;
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (true)
                {
                    switch (row)
                    {
                        case 0:
                            reader.ReadLine();
                            row++;
                            break;
                        case 1:
                            _temperatures = reader.ReadLine().Split().Skip(2).Select(t => int.Parse(t)).ToArray();
                            row++;
                            break;
                        case 2:
                            _wind = reader.ReadLine().Split().Skip(2).Select(t => int.Parse(t)).ToArray();
                            row++;
                            break;
                        case 3:
                            _humidity = reader.ReadLine().Split().Skip(2).Select(t => int.Parse(t)).ToArray();
                            row++;
                            break;
                        case 4:
                            _precipitation = reader.ReadLine().Split().Skip(2).Select(t => int.Parse(t)).ToArray();
                            row++;
                            break;
                        case 5:
                            _lightning = reader.ReadLine().Split().Skip(1).Select(t => t == "Yes" ? t = "true" : t = "false").Select(t => bool.Parse(t)).ToArray();
                            row++;
                            break;
                        case 6:
                            _clouds = reader.ReadLine().Split().Skip(1).ToArray();
                            row++;
                            break;
                    }
                    if (row > 6)
                        break;
                }

            }

            //Here we try to create an appropriate day with the values from the file
            for (int i = 0; i < 15; i++)
            {
                try
                {
                    //With default params
                    if (!isCustom)
                    {
                        Day day = new Day(i + 1, _temperatures[i], _wind[i], _humidity[i], _precipitation[i], _lightning[i], _clouds[i]);
                        appropriateDays.Add(day);
                    }
                    //With custom params
                    else
                    {
                        Day day = new Day(i + 1, _temperatures[i], _wind[i], _humidity[i], _precipitation[i], _lightning[i], _clouds[i], customParams, cloudsCustomParams);
                        appropriateDays.Add(day);
                    }
                }
                catch(Exception e)
                {
                    continue;
                }
            }

            //We order the array by wind and then by humidity to get the most appropriate day
            return appropriateDays.OrderBy(d => d.Wind).ThenBy(d => d.Humidity).ToList();
        }
    }
}
