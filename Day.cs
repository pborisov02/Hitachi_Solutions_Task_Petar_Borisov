using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hitachi_Solutions_Task
{
    public class Day
    {
        public Day(int id, int temparature, int wind, int humidity, int precipitation, bool lightning, string clouds)
        {
            if (temparature >= 2 
                && temparature <= 31
                && wind <= 10 
                && humidity < 60 
                && !lightning 
                && precipitation == 0 
                && clouds != "Cumulus" 
                && clouds != "Nimbus")
            {
                this.Id = id;
                this.Temperature = temparature;
                this.Wind = wind;
                this.Humidity = humidity;
                this.Precipitation = precipitation;
                this.Lightning = lightning;
                this.Clouds = clouds;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public Day(int id, int temparature, int wind, int humidity, int precipitation, bool lightning, string clouds, int[] customParams, List<string> cloudsParams)
        {
            if (temparature >= customParams[0]
                && temparature <= customParams[1]
                && wind <= customParams[2]
                && humidity < customParams[3]
                && lightning == Convert.ToBoolean(customParams[4])
                && precipitation == customParams[5]
                && !cloudsParams.Contains(clouds))
            {
                this.Id = id;
                this.Temperature = temparature;
                this.Wind = wind;
                this.Humidity = humidity;
                this.Precipitation = precipitation;
                this.Lightning = lightning;
                this.Clouds = clouds;
            }
            else
            {
                throw new ArgumentException();
            }
        }
        public int Id { get; }
        public int Temperature { get; }
        public int Wind { get; }
        public int Humidity { get; }
        public int Precipitation { get; }
        public bool Lightning { get;}
        public string Clouds { get;}
    }
}
