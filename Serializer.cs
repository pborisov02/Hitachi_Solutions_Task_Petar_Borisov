using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hitachi_Solutions_Task
{
    public class Serializer
    {
        //malDay = mostApropriateLaunchDay
        //Here we serialize the data into a new .csv file when there is an appropriate day found
        public void Serialize(int[] _temperatures, int[] _wind, int[] _humidity,  int[] _precipitation, Day malDay)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("Aggregated values\tAverage value\tMax value\tMin value\tMedian value\tMost appropriate launch day parameter value");
            sb.AppendLine($"Temperature (C)\t{_temperatures.Average():f2}\t{_temperatures.Max():f2}\t{_temperatures.Min():f2}\t{GetMedianValue(_temperatures):f2}\t{malDay.Temperature}");
            sb.AppendLine($"Wind (m/s)\t{_wind.Average():f2}\t{_wind.Max():f2}\t{_wind.Min():f2}\t{GetMedianValue(_wind):f2}\t{malDay.Wind}");
            sb.AppendLine($"Humidity (%)\t{_humidity.Average():f2}\t{_humidity.Max():f2}\t{_humidity.Min():f2}\t{GetMedianValue(_humidity):f2}\t{malDay.Humidity}");
            sb.AppendLine($"Precipitation (%)\t{_precipitation.Average():f2}\t{_precipitation.Max():f2}\t{_precipitation.Min():f2}\t{GetMedianValue(_precipitation):f2}\t{malDay.Precipitation}");
            sb.AppendLine($"Lightning\t \t \t \t \tNo");
            sb.AppendLine($"Clouds\t \t \t \t \t{malDay.Clouds}");

            File.WriteAllTextAsync("WeatherReport.csv", sb.ToString().TrimEnd());
        }

        //Here we serialize the data into a new .csv file when there is no appropriate day found
        public void SerializeWithNoAppropriateDay(int[] _temperatures, int[] _wind, int[] _humidity, int[] _precipitation)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Aggregated values\tAverage value\tMax value\tMin value\tMedian value");
            sb.AppendLine($"Temperature (C)\t{_temperatures.Average():f2}\t{_temperatures.Max():f2}\t{_temperatures.Min():f2}\t{GetMedianValue(_temperatures):f2}");
            sb.AppendLine($"Wind (m/s)\t{_wind.Average():f2}\t{_wind.Max():f2}\t{_wind.Min():f2}\t{GetMedianValue(_wind):f2}");
            sb.AppendLine($"Humidity (%)\t{_humidity.Average():f2}\t{_humidity.Max():f2}\t{_humidity.Min():f2}\t{GetMedianValue(_humidity):f2}");
            sb.AppendLine($"Precipitation (%)\t{_precipitation.Average():f2}\t{_precipitation.Max():f2}\t{_precipitation.Min():f2}\t{GetMedianValue(_precipitation):f2}");
            sb.AppendLine($"Lightning\t \t \t \t ");
            sb.AppendLine($"Clouds\t \t \t \t ");

            File.WriteAllText("WeatherReport.csv", sb.ToString().TrimEnd());
        }



        //Calculates the median value
        public double GetMedianValue(int[] array)
        {
            int[] arr = array.OrderBy(x => x).ToArray();
            int size = arr.Length;
            int mid = size / 2;
            double median = (size % 2 != 0) ? (double)arr[mid] : ((double)arr[mid] + (double)arr[mid - 1]) / 2;
            return median;
        }
    }
}
