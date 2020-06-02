using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IsraelTrain_Core.Model
{


    public static class UtilMapping
    {

        public static string GetEngName(int stationID)
        {
            return (from o in stationMappingList where o.RailId == stationID select o.EngName).FirstOrDefault();
        }

        public static string GetArrName(int stationID)
        {
            return (from o in stationMappingList where o.RailId == stationID select o.ArbName).FirstOrDefault();
        }

        public static string DescriptionAr(int stationID)
        {
            return (from o in stationMappingList where o.RailId == stationID select o.DescriptionArb).FirstOrDefault();
        }

        public static StationMappingList stationMappingList = utilsTicketNPassenger.GetMappingList();
    }


    public class StationLang : Station
    {
        [DataMember(Name = "DescriptionArb", IsRequired = true, Order = 10)]
        public string DescriptionArb { get; set; }
        [DataMember(Name = "DescriptionArb", IsRequired = true, Order = 11)]
        public string ArbName { get; set; }
        public StationLang()
        {
            Location = new Geolocation();
        }
        public StationLang(XmlNode node) : base()
        {

            setStation(node);
            DescriptionArb = UtilMapping.DescriptionAr(int.Parse(StationId));
            ArbName = UtilMapping.GetArrName(int.Parse(StationId));
        }
    }

    public class Station
    {
        public string StationId { get; set; }
        public string DescriptionHe { get; set; }
        public string DescriptionEn { get; set; }
        public string InterChangeStation { get; set; }
        public string StationKind { get; set; }
        public string Parking { get; set; }
        public string ParkingPay { get; set; }
        public string Handicap { get; set; }
        public string EngName { get; set; }


        public Geolocation Location { get; set; }

        public Station()
        {
            Location = new Geolocation();
        }

        public Station(XmlNode node)
        {
            setStation(node);

        }

        public void setStation(XmlNode node)
        {
            if (node["StationId"] != null)
                StationId = node["StationId"].InnerText;
            if (node["DescriptionHe"] != null)
                DescriptionHe = node["DescriptionHe"].InnerText;
            if (node["DescriptionEn"] != null)
                DescriptionEn = node["DescriptionEn"].InnerText;
            if (node["InterChangeStation"] != null)
                InterChangeStation = node["InterChangeStation"].InnerText;
            if (node["StationKind"] != null)
                StationKind = node["StationKind"].InnerText;
            if (node["Parking"] != null)
                Parking = node["Parking"].InnerText;
            if (node["ParkingPay"] != null)
                ParkingPay = node["ParkingPay"].InnerText;
            if (node["Handicap"] != null)
                Handicap = node["Handicap"].InnerText;
            try
            {
                EngName = UtilMapping.GetEngName(int.Parse(StationId));
            }
            catch (Exception ex)
            {

            }
            try
            {
                Location = _locations[StationId];
            }
            catch (Exception ex)
            {

            }
        }

        private readonly Dictionary<string, Geolocation> _locations = new Dictionary<string, Geolocation>
    {
            {"300", new Geolocation{ Latitude = 31.89366159, Longitude = 34.96079059 }},
            {"400", new Geolocation{ Latitude = 31.90119233, Longitude = 35.0057443 }},
            {"700", new Geolocation{ Latitude = 32.82487368, Longitude = 35.06429677 }},
            {"1220", new Geolocation{ Latitude = 32.79389181, Longitude = 35.03715201 }},
            {"1240", new Geolocation{ Latitude = 32.681534, Longitude = 35.124919 }},
            {"1250", new Geolocation{ Latitude = 32.64816, Longitude = 35.208421 }},
            {"1260", new Geolocation{ Latitude = 32.621368, Longitude = 35.294197 }},
            {"1280", new Geolocation{ Latitude = 32.515045, Longitude = 35.488278 }},
            {"1300", new Geolocation{ Latitude = 32.80943953, Longitude = 35.05452502 }},
            {"1400", new Geolocation{ Latitude = 32.83308122, Longitude = 35.06994745 }},
            {"1500", new Geolocation{ Latitude = 32.92829346, Longitude = 35.08295984 }},
            {"1600", new Geolocation{ Latitude = 33.00501673, Longitude = 35.09872242 }},
            {"1820", new Geolocation{ Latitude = 32.91153, Longitude = 35.17403 }},
            {"1840", new Geolocation{ Latitude = 32.92458, Longitude = 35.29474 }},
            {"2100", new Geolocation{ Latitude = 32.82222905, Longitude = 34.9971028 }},
            {"2200", new Geolocation{ Latitude = 32.83061007, Longitude = 34.98187014 }},
            {"2300", new Geolocation{ Latitude = 32.79353924, Longitude = 34.95753144 }},
            {"2500", new Geolocation{ Latitude = 32.69289501, Longitude = 34.94043965 }},
            {"2800", new Geolocation{ Latitude = 32.51448467, Longitude = 34.94999134 }},
            {"2820", new Geolocation{ Latitude = 32.48536667, Longitude = 34.95406076 }},
            {"2940", new Geolocation{ Latitude = 32.180012, Longitude = 34.850805 }},
            {"2960", new Geolocation{ Latitude = 32.172592, Longitude = 34.886196 }},
            {"3100", new Geolocation{ Latitude = 32.43822138, Longitude = 34.89936548 }},
            {"3300", new Geolocation{ Latitude = 32.32003215, Longitude = 34.86924338 }},
            {"3310", new Geolocation{ Latitude = 32.278789, Longitude = 34.865261 }},
            {"3400", new Geolocation{ Latitude = 32.26248917, Longitude = 34.8601157 }},
            {"3500", new Geolocation{ Latitude = 32.16380407, Longitude = 34.81844814 }},
            {"3600", new Geolocation{ Latitude = 32.10366119, Longitude = 34.80461316 }},
            {"3700", new Geolocation{ Latitude = 32.0838827, Longitude = 34.79832291 }},
            {"4100", new Geolocation{ Latitude = 32.10309886, Longitude = 34.83014376 }},
            {"4170", new Geolocation{ Latitude = 32.10620082, Longitude = 34.86317272 }},
            {"4250", new Geolocation{ Latitude = 32.1120646, Longitude = 34.90129706 }},
            {"4600", new Geolocation{ Latitude = 32.07351575, Longitude = 34.79321156 }},
            {"4640", new Geolocation{ Latitude = 32.03711211, Longitude = 34.77647898 }},
            {"4660", new Geolocation{ Latitude = 32.03541214, Longitude = 34.75970608 }},
            {"4680", new Geolocation{ Latitude = 32.01459119, Longitude = 34.76211643 }},
            {"4690", new Geolocation{ Latitude = 32.0009341, Longitude = 34.75948739 }},
            {"4800", new Geolocation{ Latitude = 31.99319267, Longitude = 34.85301532 }},
            {"4900", new Geolocation{ Latitude = 32.05414521, Longitude = 34.78481667 }},
            {"5000", new Geolocation{ Latitude = 31.94526774, Longitude = 34.8751583 }},
            {"5010", new Geolocation{ Latitude = 31.92883758, Longitude = 34.87725704 }},
            {"5150", new Geolocation{ Latitude = 31.96700826, Longitude = 34.87871889 }},
            {"5200", new Geolocation{ Latitude = 31.90864822, Longitude = 34.80640457 }},
            {"5300", new Geolocation{ Latitude = 31.93291329, Longitude = 34.8286168 }},
            {"5410", new Geolocation{ Latitude = 31.86185992, Longitude = 34.744095 }},
            {"5800", new Geolocation{ Latitude = 31.77401406, Longitude = 34.66607186 }},
            {"5900", new Geolocation{ Latitude = 31.67673853, Longitude = 34.60485148 }},
            {"6300", new Geolocation{ Latitude = 31.75780777, Longitude = 34.98951853 }},
            {"6500", new Geolocation{ Latitude = 31.74495284, Longitude = 35.17813291 }},
            {"6700", new Geolocation{ Latitude = 31.74781315, Longitude = 35.18816146 }},
            {"7000", new Geolocation{ Latitude = 31.60348136, Longitude = 34.77791873 }},
            {"7300", new Geolocation{ Latitude = 31.26202196, Longitude = 34.80928381 }},
            {"7320", new Geolocation{ Latitude = 31.24328539, Longitude = 34.79839533 }},
            {"7500", new Geolocation{ Latitude = 31.06914215, Longitude = 35.01187611 }},
            {"8550", new Geolocation{ Latitude = 31.36986508, Longitude = 34.7982778 }},
            {"8600", new Geolocation{ Latitude = 32.00073373, Longitude = 34.87072874 }},
            {"8700", new Geolocation{ Latitude = 32.16747612, Longitude = 34.91737666 }},
            {"8800", new Geolocation{ Latitude = 32.12081396, Longitude = 34.9347874 }},
            {"9000", new Geolocation{ Latitude = 31.89123559, Longitude = 34.73154036 }},
            {"9100", new Geolocation{ Latitude = 31.94894702, Longitude = 34.8026769 }},
            {"9200", new Geolocation{ Latitude = 32.17027053, Longitude = 34.90155447 }},
            {"9600", new Geolocation{ Latitude = 31.51523, Longitude = 34.586183 }},
            {"9650", new Geolocation{ Latitude = 31.410595, Longitude = 34.571191 }},
            {"9700", new Geolocation{ Latitude = 31.321891, Longitude = 34.633948 }},
            {"9800", new Geolocation{ Latitude = 31.98792906, Longitude = 34.75745031 }},
            {"6150", new Geolocation{ Latitude = 31.755331, Longitude = 34.823378 }},
            {"680", new Geolocation{ Latitude = 31.788155, Longitude = 35.20251 }}
            ,{"6900", new Geolocation{ Latitude = 31.842165, Longitude = 34.855981 }}
    };

    }

    public class Geolocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
