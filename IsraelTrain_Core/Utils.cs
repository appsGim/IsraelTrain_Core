using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using IsraelTrain_Core.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IsraelTrain_Core
{

    public class Utils
    {


        public Utils(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }

        public static string ApplicationKey(string key)
        {
            string value = string.Empty;
            try
            {
                value = Configuration[key];
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return value;
        }
    }


    public static class UtilStationsHandler
    {

        private static object locker = new object(), locker2 = new object();
        private static DateTime UpdateAt = new DateTime(DateTime.Now.AddDays(-10).Ticks);
        private static volatile int CacheMinutes = 10;

        public static volatile string stringStations = string.Empty, stringStations_lang = string.Empty;

        //private static bool Update()
        //{
        //    try
        //    {
        //        if (stringStations.Equals(string.Empty) || UpdateAt.AddMinutes(10) < DateTime.UtcNow)
        //        {

        //            lock (locker)
        //            {
        //                if (stringStations.Equals(string.Empty) || UpdateAt.AddMinutes(10) < DateTime.UtcNow)
        //                {

        //                    lock (locker2)
        //                    {
        //                        if (stringStations.Equals(string.Empty) || UpdateAt.AddMinutes(10) < DateTime.UtcNow)
        //                        {
        //                            #region MyRegion MAKE WEB REQUEST
        //                            using (WsGetStationsSoapClient client = new WsGetStationsSoapClient("WsGetStationsSoap"))
        //                            {

        //                                XmlNode node = client.GetStations(Utils.ApplicationKey("SystemID"), Utils.ApplicationKey("SystemUserName"),
        //                                                                                                    Utils.ApplicationKey("Password"));

        //                                #region MyRegion STATIONS
        //                                List<Station> stations = (from XmlNode station in node.ChildNodes select new Station(station)).ToList();
        //                                List<StationLang> lang_stations = (from XmlNode station in node.ChildNodes select new StationLang(station)).ToList();
        //                                foreach (Station station in stations)
        //                                {
        //                                    if (station.Handicap == "1")
        //                                    {
        //                                        station.Handicap = "2";
        //                                    }
        //                                    else if (station.Handicap == "2")
        //                                    {
        //                                        station.Handicap = "1";
        //                                    }

        //                                    if (station.Parking == "1")
        //                                    {
        //                                        station.Parking = "2";
        //                                    }
        //                                    else if (station.Parking == "2")
        //                                    {
        //                                        station.Parking = "1";
        //                                    }

        //                                    if (station.ParkingPay == "1")
        //                                    {
        //                                        station.ParkingPay = "2";
        //                                    }
        //                                    else if (station.ParkingPay == "2")
        //                                    {
        //                                        station.ParkingPay = "1";
        //                                    }

        //                                    //	if (station.InterChangeStation == "1")
        //                                    //	{
        //                                    //		station.InterChangeStation = "2";
        //                                    //	}
        //                                    //	else if (station.InterChangeStation == "2")
        //                                    //	{
        //                                    //		station.InterChangeStation = "1";
        //                                    //	}
        //                                }
        //                                #endregion
        //                                foreach (StationLang station in lang_stations)
        //                                {
        //                                    if (station.Handicap == "1")
        //                                    {
        //                                        station.Handicap = "2";
        //                                    }
        //                                    else if (station.Handicap == "2")
        //                                    {
        //                                        station.Handicap = "1";
        //                                    }

        //                                    if (station.Parking == "1")
        //                                    {
        //                                        station.Parking = "2";
        //                                    }
        //                                    else if (station.Parking == "2")
        //                                    {
        //                                        station.Parking = "1";
        //                                    }

        //                                    if (station.ParkingPay == "1")
        //                                    {
        //                                        station.ParkingPay = "2";
        //                                    }
        //                                    else if (station.ParkingPay == "2")
        //                                    {
        //                                        station.ParkingPay = "1";
        //                                    }

        //                                    //	if (station.InterChangeStation == "1")
        //                                    //	{
        //                                    //		station.InterChangeStation = "2";
        //                                    //	}
        //                                    //	else if (station.InterChangeStation == "2")
        //                                    //	{
        //                                    //		station.InterChangeStation = "1";
        //                                    //	}
        //                                }


        //                                string result = JsonConvert.SerializeObject(stations);
        //                                string langresult = JsonConvert.SerializeObject(lang_stations);
        //                                result = "{ \"Stations\" : { \"Station\" : [ " + result.Remove(0, 1).Remove(result.Length - 2, 1) + "]}}";
        //                                langresult = "{ \"Stations\" : { \"Station\" : [ " + langresult.Remove(0, 1).Remove(langresult.Length - 2, 1) + "]}}";
        //                                stringStations = result;
        //                                stringStations_lang = langresult;

        //                                UpdateAt = new DateTime(DateTime.UtcNow.Ticks);
        //                                return true;
        //                            }
        //                            #endregion
        //                        }
        //                    }

        //                }
        //            }

        //        }


        //        return true;
        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //    return false;
        //}

        //public static string getAllStations(bool langresult)
        //{
        //    if (stringStations.Equals(string.Empty) || UpdateAt.AddMinutes(10) < DateTime.UtcNow)
        //    {
        //        Update();
        //    }
        //    return langresult ? stringStations_lang : stringStations;
        //}


    }


    public enum enumXMListType { Ticket, Passenger }

    public  class utilsTicketNPassenger
    {

        public static IConfiguration Configuration { get; set; }

        public utilsTicketNPassenger(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        
        private static volatile Dictionary<int, string[]> TicketTypes = new Dictionary<int, string[]>(); // { get; set; }
        private static volatile Dictionary<int, string[]> PassengerTypes = new Dictionary<int, string[]>();// { get; set; }

        private static volatile Dictionary<int, string> StationDescription_ARB = new Dictionary<int, string>();

        private static volatile StationMappingList _StationMappingList = SetMappingList();// new StationMappingList();

        private static object locker = new object(), locker2 = new object();
        private static DateTime StartAt = new DateTime(DateTime.Now.AddDays(-10).Ticks);

        public static volatile int CacheMinutes = 30;

        /// <summary>
        /// TEST FOR ADMIN ONLY
        /// </summary>
        /// <returns></returns>
        //public static Dictionary<int, string[]> GetaAllTicketTypes()
        //{
        //    if (StartAt.AddMinutes(CacheMinutes) < DateTime.Now)
        //    {
        //        Update();
        //    }
        //    return TicketTypes;
        //}

        //public static Dictionary<int, string[]> GetaAllPassengerTypes()
        //{
        //    if (StartAt.AddMinutes(CacheMinutes) < DateTime.Now)
        //    {
        //        Update();
        //    }
        //    return PassengerTypes;
        //}

        //private static void Update()
        //{
        //    if (StartAt.AddMinutes(CacheMinutes) < DateTime.Now)
        //    {
        //        lock (locker)
        //        {
        //            #region MyRegion lock

        //            if (StartAt.AddMinutes(CacheMinutes) < DateTime.Now)
        //            {
        //                lock (locker2)
        //                {
        //                    if (StartAt.AddMinutes(CacheMinutes) < DateTime.Now)
        //                    {
        //                        _StationMappingList = SetMappingList();
        //                        CacheMinutes = int.Parse(Configuration["CacheMinutes"]);
        //                        #region MyRegion

        //                        string data = Configuration["TicketType"],
        //                                           data_continue = Configuration["TicketType_Continue"],
        //                                           data_continue_2 = Configuration["TicketType_Continue_2"];



        //                        JArray list = (JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(data),
        //                               list_continue = (JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(data_continue),
        //                               list_continue_2 = (JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(data_continue_2);


        //                        Dictionary<int, string[]> _TicketTypes = new Dictionary<int, string[]>();

        //                        foreach (JToken token in list)
        //                        {
        //                            int _id = int.Parse(token["id"].ToString());
        //                            string _valueEN = (string)token["value"];
        //                            string _valueARR = (string)token["valueARR"];
        //                            if (!_TicketTypes.ContainsKey(_id))
        //                            {
        //                                _TicketTypes.Add(_id, new string[2] { _valueEN, _valueARR });
        //                            }
        //                        }

        //                        foreach (JToken token in list_continue)
        //                        {
        //                            int _id = int.Parse(token["id"].ToString());
        //                            string _valueEN = (string)token["value"];
        //                            string _valueARR = (string)token["valueARR"];
        //                            if (!_TicketTypes.ContainsKey(_id))
        //                            {
        //                                _TicketTypes.Add(_id, new string[2] { _valueEN, _valueARR });
        //                            }
        //                        }

        //                        foreach (JToken token in list_continue_2)
        //                        {
        //                            int _id = int.Parse(token["id"].ToString());
        //                            string _valueEN = (string)token["value"];
        //                            string _valueARR = (string)token["valueARR"];
        //                            if (!_TicketTypes.ContainsKey(_id))
        //                            {
        //                                _TicketTypes.Add(_id, new string[2] { _valueEN, _valueARR });
        //                            }
        //                        }

        //                        TicketTypes = _TicketTypes;
        //                        _TicketTypes = null;

        //                        data = Configuration["PassengerType"];

        //                        list = (JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(data);

        //                        Dictionary<int, string[]> _PassengerTypes = new Dictionary<int, string[]>();

        //                        foreach (JToken token in list)
        //                        {
        //                            int _id = int.Parse(token["id"].ToString());
        //                            string _valueEN = (string)token["value"];
        //                            string _valueARR = (string)token["valueARR"];

        //                            if (!_PassengerTypes.ContainsKey(_id))
        //                            {
        //                                _PassengerTypes.Add(_id, new string[2] { _valueEN, _valueARR });
        //                            }
        //                        }

        //                        //SHURON
        //                        data = Configuration["StationDescription_ARB"];

        //                        list = (JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(data);

        //                        Dictionary<int, string> _StationDescription_ARB = new Dictionary<int, string>();

        //                        foreach (JToken token in list)
        //                        {
        //                            int _id = int.Parse(token["id"].ToString());
        //                            string _valueARB = (string)token["value"];

        //                            if (!_StationDescription_ARB.ContainsKey(_id))
        //                            {
        //                                _StationDescription_ARB.Add(_id, _valueARB);
        //                            }
        //                        }

        //                        StationDescription_ARB = _StationDescription_ARB;
        //                        _StationDescription_ARB = null;

        //                        _StationMappingList = SetMappingList();

        //                        PassengerTypes = _PassengerTypes;
        //                        _PassengerTypes = null;

        //                        StartAt = DateTime.Now;
        //                        #endregion
        //                    }
        //                }
        //            }

        //            #endregion
        //        }
        //    }
        //}

        private static string ArbDescriptionGet(int stationID)
        {
            if (StationDescription_ARB.Count == 0 || !StationDescription_ARB.ContainsKey(stationID)) return string.Empty;

            return StationDescription_ARB[stationID];
        }

        /// <summary>
        /// CALL CACHE
        /// </summary>
        /// <returns></returns>
        public static StationMappingList GetMappingList()
        {
            if (StartAt.AddMinutes(CacheMinutes) < DateTime.Now //StartAt.AddHours(3) < DateTime.Now 
                || _StationMappingList.Count == 0)
            {
             //   Update();
            }

            return _StationMappingList;
        }

        private static StationMappingList SetMappingList()
        {
            return new StationMappingList
        {
            new StationMapping {Name = "אשדוד עד הלום (מ' בר כוכבא)",
                                RailId = 5800,DescriptionArb=ArbDescriptionGet(5800), MapaId=54194, Latitude = 31.77401406, Longitude = 34.66607186,
                               EngName="Ashdod-Ad Halom (M.Bar Kochva)",ArbName="أشدود عاد هلوم "
            },
            new StationMapping {Name = "אשקלון",
                                RailId = 5900,DescriptionArb=ArbDescriptionGet(5900), MapaId = 54196, Latitude = 31.67673853, Longitude = 34.60485148,
                                EngName="Ashkelon",ArbName="آشكلون"
            },
            new StationMapping {Name = "באר יעקב",
                                RailId = 5300,DescriptionArb=ArbDescriptionGet(5300), MapaId =54197, Latitude = 31.93291329, Longitude = 34.8286168,
                                EngName="Be'er Ya'akov",ArbName="بئر يعقوڤ"
            },
            new StationMapping {Name = "באר שבע מרכז",
                                RailId = 7320,DescriptionArb=ArbDescriptionGet(7320), MapaId =54198, Latitude = 31.24328539, Longitude = 34.79839533,
                                EngName="Be'er Sheva-Center",ArbName="پئر السبع – مركز"
            },
            new StationMapping {Name = "באר שבע- צפון/אוניברסיטה",
                                RailId = 7300,DescriptionArb=ArbDescriptionGet(7300), MapaId =54199, Latitude = 31.26202196, Longitude = 34.80928381,
                                EngName="Be'er Sheva-North/University",ArbName="پئر السبع شمال – الجامعة"
            },
            new StationMapping {Name = "בית יהושע",
                                RailId = 3400,DescriptionArb=ArbDescriptionGet(3400), MapaId =54200, Latitude = 32.26248917, Longitude = 34.8601157,
                                EngName="Bet Yehoshu'a",ArbName="بيت يهوشوعا"
            },
            new StationMapping {Name = "בית שמש",
                                RailId = 6300,DescriptionArb=ArbDescriptionGet(6300),   MapaId = 54202, Latitude = 31.75780777, Longitude = 34.98951853,
                                EngName="Bet Shemesh",ArbName="بيت شيمش"
            },
            new StationMapping {Name = "בני ברק",
                                RailId = 4100, DescriptionArb=ArbDescriptionGet(4100),  MapaId = 54204, Latitude = 32.10309886, Longitude = 34.83014376,
                               EngName="Bne Brak",ArbName="بنيه براك"
            },
            new StationMapping {Name = "בנימינה",
                                RailId = 2800, DescriptionArb=ArbDescriptionGet(2800),       MapaId = 99339, Latitude = 32.51448467, Longitude = 34.94999134,
                                EngName="Binyamina",ArbName="بِِنيامينا"
            },
            new StationMapping {Name = "בת ים - יוספטל",
                                RailId = 4680,DescriptionArb=ArbDescriptionGet(4680), MapaId = 104146, Latitude = 32.01459119, Longitude = 34.76211643,
                                EngName="Bat Yam-Yoseftal",ArbName="نات يام - يوسفتال"
             },
            new StationMapping {Name = "בת ים - קוממיות",
                                RailId = 4690,DescriptionArb=ArbDescriptionGet(4690), MapaId = 104147, Latitude = 32.0009341, Longitude = 34.75948739,
                                EngName="Bat Yam-Komemiyut",ArbName="نات يام - قوميميوت"
            },
            new StationMapping {Name = "דימונה",
                                RailId = 7500,DescriptionArb=ArbDescriptionGet(7500), MapaId =79826, Latitude = 31.06914215, Longitude = 35.01187611,
                                EngName="Dimona",ArbName="ديمونا"
            },
            new StationMapping {Name = "הוד השרון סוקולוב",
                                RailId = 9200,DescriptionArb=ArbDescriptionGet(9200), MapaId = 99327, Latitude = 32.17027053, Longitude = 34.90155447,
                                EngName="Hod Hasharon-Sokolov",ArbName="هود هشارون-سوكولوف"
            },
            new StationMapping {Name = "הרצליה",
                                RailId = 3500,DescriptionArb=ArbDescriptionGet(3500), MapaId =54210, Latitude = 32.16380407, Longitude = 34.81844814,
                                EngName="Hertsliya",ArbName="هرتسليا" },
            new StationMapping {Name = "חדרה - מערב",
                                RailId = 3100,DescriptionArb=ArbDescriptionGet(3100), MapaId = 54211, Latitude = 32.43822138, Longitude = 34.89936548,
                                EngName="Hadera-West",ArbName="الخضيرة غرب"
            },
            new StationMapping {Name = "חולון - וולפסון",
                                RailId = 4660,DescriptionArb=ArbDescriptionGet(4660), MapaId = 104148, Latitude = 32.03541214, Longitude = 34.75970608,
                                EngName="Holon-Wolfson",ArbName="حولون - فولفسون"
            },
            new StationMapping {Name = "חוצות המפרץ",
                                RailId = 1300,DescriptionArb=ArbDescriptionGet(1300), MapaId = 54213, Latitude = 32.80943953, Longitude = 35.05452502,
                                EngName="Hutsot HaMifrats",ArbName="حوتسوت همِفراتس"
            },
            new StationMapping {Name = "חיפה - בת גלים",
                                RailId = 2200,DescriptionArb=ArbDescriptionGet(2200), MapaId = 54215, Latitude = 32.83061007, Longitude = 34.98187014,
                                EngName="Haifa-Bat Galim",ArbName="حيفا بات چَليم"
            },
            new StationMapping {Name = "חיפה - חוף הכרמל (ש' רזיאל)",
                                RailId = 2300,DescriptionArb=ArbDescriptionGet(2300), MapaId = 54212, Latitude = 32.79353924, Longitude = 34.95753144,
                                EngName="Haifa-Hof HaKarmel (Razi`el)",ArbName="حيفا حوف هكرمل (ش. رزيئيل)"
            },
            new StationMapping {Name = "חיפה - מרכז השמונה",
                                RailId = 2100,DescriptionArb=ArbDescriptionGet(2100), MapaId = 54214, Latitude = 32.82222905, Longitude = 34.9971028,
                                EngName="Haifa Center-HaShmona",ArbName="حيفا مركز - هشمونا"
            },
            new StationMapping {Name = "יבנה מזרח",
                                RailId = 5410,DescriptionArb=ArbDescriptionGet(5410), MapaId = 54216, Latitude = 31.86185992, Longitude = 34.744095,
                                EngName="Yavne-East",ArbName= "ياڤنيه-شرق"
            },
            new StationMapping {Name = "יבנה מערב",
                                RailId = 9000 ,DescriptionArb=ArbDescriptionGet(9000), Latitude = 31.89123559, Longitude = 34.73154036,
                                EngName="Yavne-West",ArbName="ياڤنيه-غرب"
            },
            new StationMapping {Name = "ירושלים - גן החיות התנכי",
                                RailId = 6500,DescriptionArb=ArbDescriptionGet(6500), MapaId = 54208, Latitude = 31.74495284, Longitude = 35.17813291,
                                EngName="Jerusalem-Biblical Zoo",ArbName="اورشليم – القدس – حديقة الحيوانات"
            },
            new StationMapping {Name = "ירושלים - מלחה",
                                RailId = 6700,DescriptionArb=ArbDescriptionGet(6700), MapaId = 54217, Latitude = 31.74781315, Longitude = 35.18816146,
                                EngName="Jerusalem-Malha",ArbName="اورشليم – القدس – المالحة"
            },
            new StationMapping {Name = "כפר חב\"ד",
                                RailId = 4800,DescriptionArb=ArbDescriptionGet(4800), MapaId = 54218, Latitude = 31.99319267, Longitude = 34.85301532,
                                EngName="Kfar Habad",ArbName="كفار حباد"
            },
            new StationMapping {Name = "כפר סבא נורדאו",
                                RailId = 8700,DescriptionArb=ArbDescriptionGet(8700), MapaId = 54209, Latitude = 32.16747612, Longitude = 34.91737666,
                                EngName="Kfar Sava-Nordau (A.Kostyuk)",ArbName="كفار سابا-نوردو(ا.كوسطيك)"
            },
            new StationMapping {Name = "מרכזית המפרץ",
                                RailId = 1220,DescriptionArb=ArbDescriptionGet(1220), MapaId = 54219, Latitude = 32.79389181, Longitude = 35.03715201,
                                EngName="HaMifrats Central Stat",ArbName="همفراتس المركزية"
            },
            new StationMapping {Name = "להבים - רהט",
                                RailId = 8550,DescriptionArb=ArbDescriptionGet(8550), MapaId = 79747, Latitude = 31.36986508, Longitude = 34.7982778,
                                EngName="Lehavim-Rahat",ArbName="ليهاڤيم - الرهط"
            },
            new StationMapping {Name = "לוד",
                                RailId = 5000, DescriptionArb=ArbDescriptionGet(5000),MapaId = 54220, Latitude = 31.94526774, Longitude = 34.8751583,
                                EngName="Lod",ArbName="اللد"
             },
            new StationMapping {Name = "לוד גני אביב",
                                RailId = 5150,DescriptionArb=ArbDescriptionGet(5150), MapaId = 99335, Latitude = 31.96700826, Longitude = 34.87871889,
                                EngName="Lod-Gane Aviv",ArbName="اللد – چاني آڤيڤ"
            },
            new StationMapping {Name = "מודיעין - מרכז",
                                RailId = 400,DescriptionArb=ArbDescriptionGet(400), MapaId = 90472, Latitude = 31.90119233, Longitude = 35.0057443,
                                EngName="Modi'in-Center",ArbName="موديعين مركز"
            },
            new StationMapping {Name = "פאתי מודיעין",
                                RailId = 300, DescriptionArb=ArbDescriptionGet(300),MapaId = 79823, Latitude = 31.89366159, Longitude = 34.96079059,
                                EngName="Pa'ate Modi'in",ArbName="پآتي (اطراف) موديعين"
            },
            new StationMapping {Name = "קריית מוצקין",
                                RailId = 800,DescriptionArb=ArbDescriptionGet(800), MapaId = 54235, Latitude = 32.83308122, Longitude = 35.06994745,
                                EngName="Kiryat Motzkin",ArbName="قِريات موتسكين"
            },
            new StationMapping {Name = "קריית מוצקין",
                                RailId = 1400,DescriptionArb=ArbDescriptionGet(1400), MapaId = 54235, Latitude = 32.83308122, Longitude = 35.06994745,
                                EngName="Kiryat Motzkin",ArbName="قِريات موتسكين"
            },
            new StationMapping {Name = "נהריה",
                                RailId = 1600,DescriptionArb=ArbDescriptionGet(1600), MapaId = 54221, Latitude = 33.00501673, Longitude = 35.09872242,
                                EngName="Nahariya",ArbName="نهاريا"
            },
            new StationMapping {Name = "נמל תעופה בן גוריון",
                                RailId = 8600,DescriptionArb=ArbDescriptionGet(8600), MapaId = 54222, Latitude = 32.00073373, Longitude = 34.87072874,
                                EngName="Ben Gurion Airport",ArbName="الميناء الجوي بن چوريون"
            },
            new StationMapping {Name = "נתניה",
                                RailId = 3300,DescriptionArb=ArbDescriptionGet(3300), MapaId = 54223, Latitude = 32.32003215, Longitude = 34.86924338,
                                EngName="Netanya",ArbName="نتانيا"
            },
            new StationMapping {Name = "עכו",
                                RailId = 1500,DescriptionArb=ArbDescriptionGet(1500), MapaId = 54224, Latitude = 32.92829346, Longitude = 35.08295984,
                                EngName="Ako",ArbName="عكا"
            },
            new StationMapping {Name = "עתלית",
                                RailId = 2500,DescriptionArb=ArbDescriptionGet(2500), MapaId = 54207, Latitude = 32.69289501, Longitude = 34.94043965,
                                EngName="Atlit",ArbName="عتليت"
            },
            new StationMapping {Name = "פתח תקווה - סגולה",
                                RailId = 4250,DescriptionArb=ArbDescriptionGet(4250), MapaId = 54227, Latitude = 32.1120646, Longitude = 34.90129706,
                                EngName="Petah Tikva-Segula",ArbName="پيتاح تيكڤا - سيچولا"
            },
            new StationMapping {Name = "פתח תקווה - קריית אריה",
                                RailId = 4170,DescriptionArb=ArbDescriptionGet(4170), MapaId = 90832, Latitude = 32.10620082, Longitude = 34.86317272,
                                EngName="Petah Tikva-Kiryat Arye",ArbName="بيتح تكڤا - كريات أريه"
            },
            new StationMapping {Name = "צומת חולון",
                                RailId = 4640,DescriptionArb=ArbDescriptionGet(4640), MapaId = 104149, Latitude = 32.03711211, Longitude = 34.77647898,
                                EngName="Holon Junction",ArbName="حولون - مفترق حولون"
            },
            new StationMapping {Name = "קיסריה - פרדס חנה",
                                RailId = 2820,DescriptionArb=ArbDescriptionGet(2820), MapaId = 54232, Latitude = 32.48536667, Longitude = 34.95406076,
                                EngName="Caesarea-Pardes Hana",ArbName="كيساريا – پردِس حانا"
            },
            new StationMapping {Name = "קריית  חיים",
                                RailId = 700,DescriptionArb=ArbDescriptionGet(700), MapaId = 54234, Latitude = 32.82487368, Longitude = 35.06429677,
                               EngName="Kiryat Hayim",ArbName="قِريات حاييم"

            },
            new StationMapping {Name = "קריית גת",
                                RailId = 7000,DescriptionArb=ArbDescriptionGet(7000), MapaId = 54233, Latitude = 31.60348136, Longitude = 34.77791873,
                                EngName="Kiryat Gat",ArbName="قِريات چات"
            },
            new StationMapping {Name = "ראש העין צפון",
                                RailId = 8800,DescriptionArb=ArbDescriptionGet(8800), MapaId = 54236, Latitude = 32.12081396, Longitude = 34.9347874,
                                EngName="Rosh Ha'Ayin-North",ArbName="روش هعاين شمال"
            },
            new StationMapping {Name = "ראשון לציון - הראשונים",
                                RailId = 9100,DescriptionArb=ArbDescriptionGet(9100), MapaId = 54237, Latitude = 31.94894702, Longitude = 34.8026769,
                                EngName="Rishon LeTsiyon-HaRishonim",ArbName="ريشون لتسيون - هريشونيم"
            },
            new StationMapping {Name = "ראשון לציון - משה דיין",
                                RailId = 9800,DescriptionArb=ArbDescriptionGet(9800), MapaId = 104150, Latitude = 31.98792906, Longitude = 34.75745031,
                                EngName="Rishon LeTsiyon-Moshe Dayan",ArbName="ريشون لِتْسِيون - موشيه ديان"
            },
            new StationMapping {Name = "רחובות (א' הדר)",
                                RailId = 5200,DescriptionArb=ArbDescriptionGet(5200), MapaId = 54238, Latitude = 31.90864822, Longitude = 34.80640457,
                                EngName="Rehovot (E. Hadar)",ArbName="رحوڤوت (ا. هدار)"
            },
            new StationMapping {Name = "רמלה",
                                RailId = 5010,DescriptionArb=ArbDescriptionGet(5100), MapaId = 54239, Latitude = 31.92883758, Longitude = 34.87725704,
                                EngName="Ramla",ArbName="الرملة"
            },
            new StationMapping {Name = "תל אביב - אוניברסיטה",
                                RailId = 3600,DescriptionArb=ArbDescriptionGet(3600), MapaId = 104151, Latitude = 32.10366119, Longitude = 34.80461316,
                                EngName="Tel Aviv-University",ArbName="تل ابيب - جامعة"
            },
            new StationMapping {Name = "תל אביב - ההגנה",
                                RailId = 4900,DescriptionArb=ArbDescriptionGet(4900), MapaId = 54243, Latitude = 32.05414521, Longitude = 34.78481667,
                                EngName="Tel Aviv-HaHagana",ArbName="تل ابيب – ههاچنا"
            },
            new StationMapping {Name = "תל אביב - השלום",
                                RailId = 4600,DescriptionArb=ArbDescriptionGet(4600), MapaId = 54242, Latitude = 32.07351575, Longitude = 34.79321156,
                                EngName="Tel Aviv-HaShalom",ArbName="تل ابيب - هشالوم"
            },
            new StationMapping {Name = "תל אביב - סבידור מרכז",
                                RailId = 3700,DescriptionArb=ArbDescriptionGet(3700), MapaId = 54240, Latitude = 32.0838827, Longitude = 34.79832291,
                                EngName="Tel Aviv-Savidor Center",ArbName="تل ابيب – ساڤيدور مركز"
            },
            new StationMapping {Name = "שדרות",
                                RailId = 9600,DescriptionArb=ArbDescriptionGet(9600), MapaId = 0, Latitude = 31.51523, Longitude = 34.586183,
                                EngName="Sderot",ArbName="سديروت"
            },
            new StationMapping {Name = "נתיבות",
                                RailId = 9650,DescriptionArb=ArbDescriptionGet(9650), MapaId = 0, Latitude = 31.410595, Longitude = 34.571191,
                                EngName="Netivot",ArbName="نتيفوت"
            },
            new StationMapping {Name = "אופקים",
                                RailId = 9700,DescriptionArb=ArbDescriptionGet(9700), MapaId = 0, Latitude = 31.321891, Longitude = 34.633948,
                                EngName="Ofakim",ArbName="وفاكيم"
            },
            new StationMapping {Name = "עפולה (ר.איתן)",
                                RailId = 1260,DescriptionArb=ArbDescriptionGet(1260), MapaId = 0, Latitude = 32.621368, Longitude = 35.294197,
                                EngName="Afula (R.Eitan)",ArbName="العفولة (ر. ايتان)"
            },
            new StationMapping {Name = "בית שאן",
                                RailId = 1280,DescriptionArb=ArbDescriptionGet(1280), MapaId = 0, Latitude = 32.515045, Longitude = 35.488278,
                                EngName="Beit She'an",ArbName="بيت شآن"
            },
            new StationMapping {Name = "מגדל העמק – כפר ברוך",
                                RailId = 1250,DescriptionArb=ArbDescriptionGet(1250), MapaId = 0, Latitude = 32.64816, Longitude = 35.208421,
                                EngName="Migdal Ha'Emek-Kfar Barukh",ArbName="مجدال هعيمك – كفار باروخ"
            },
            new StationMapping {Name = "נתניה – ספיר",
                                RailId = 3310,DescriptionArb=ArbDescriptionGet(3310), MapaId = 0, Latitude = 32.278789, Longitude = 34.865261,
                                EngName="Netanya-Sapir",ArbName="نتانيا - سبير"
            },
            new StationMapping {Name = "יקנעם – כפר יהושע",
                                RailId = 1240,DescriptionArb=ArbDescriptionGet(1240), MapaId = 0, Latitude = 32.681534, Longitude = 35.124919,
                                EngName="Yokne'am-Kfar Yehoshu'a",ArbName="يوكنعام – كفار يهوشوع"
            },
            new StationMapping {Name = "כרמיאל",
                                RailId = 1840,DescriptionArb=ArbDescriptionGet(1840), MapaId = 0, Latitude = 32.92458, Longitude = 35.29474,
                                EngName="Karmiel",ArbName="كرمئيل"
            },
            new StationMapping {Name = "אחיהוד",
                                RailId = 1820,DescriptionArb=ArbDescriptionGet(1820), MapaId = 0, Latitude = 32.91153, Longitude = 35.17403,
                                EngName="Ahihud",ArbName="احيهود"
            },
            new StationMapping {Name = "רעננה – דרום",
                                RailId = 2960,DescriptionArb=ArbDescriptionGet(2960), MapaId = 0, Latitude = 32.172592, Longitude = 34.886196,
                                EngName ="Raanana – South",ArbName="رعنانا – جنوب"
            },
            new StationMapping {Name = "רעננה – מערב",
                                RailId = 2940,DescriptionArb=ArbDescriptionGet(2940), MapaId = 0, Latitude = 32.180012, Longitude = 34.850805,
                                EngName ="Raanana – West",ArbName="رعنانا – غرب"
            }   ,
            new StationMapping {Name = "קריית מלאכי - יואב",
                RailId = 6150 ,DescriptionArb=ArbDescriptionGet(6150), MapaId = 0, Latitude = 34.823378, Longitude =34.823378,
                EngName ="Kiryat Malakhi –Yoav",ArbName="كريات ملاخي – يوآڤ"
            },
            new StationMapping {Name = "ירושלים – יצחק נבון",
                RailId = 680 ,DescriptionArb=ArbDescriptionGet(680), MapaId = 0, Latitude = 31.788155, Longitude =35.20251,
                EngName ="Jerusalem - Yitzhak Navon",ArbName="أورشليم – يتسحاق ناڤون"
            }
            ,
            new StationMapping {Name = "מזכרת בתיה",
                RailId = 6900 ,DescriptionArb=ArbDescriptionGet(6900), MapaId = 0, Latitude = 31.842165, Longitude =34.855981,
                EngName ="Mazkeret Batya",ArbName="مزكيرت باتيا"
            }

        };
        }

        /// <summary>
        /// CALL CACHE
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string Get(enumXMListType type, int id, enumLang lang)
        {
            //if (StartAt.AddHours(3) < DateTime.Now)
            if (StartAt.AddMinutes(CacheMinutes) < DateTime.Now)
            {
                //Update();
            }

            switch (type)
            {
                case enumXMListType.Passenger:
                    if (PassengerTypes.ContainsKey(id))
                    {
                        switch (lang)
                        {
                            case enumLang.ARR: return PassengerTypes[id][1];
                            default: return PassengerTypes[id][0];
                        }
                    }

                    return "";
                    break;
                case enumXMListType.Ticket:
                    if (TicketTypes.ContainsKey(id))
                    {
                        switch (lang)
                        {
                            case enumLang.ARR: return TicketTypes[id][1];
                            default: return TicketTypes[id][0];
                        }

                    }
                    return "";
                    break;

                default: return "";
            }

            return "";
        }

        public enum enumLang { HEB, ENG, ARR }

    }

    public class Logger
    {
        public string GetTempPath()
        {
            string path = System.Environment.GetEnvironmentVariable("TEMP");
            if (!path.EndsWith("\\")) path += "\\";
            return path;
        }

        public void LogToFile(string msg, string source, string innerException)
        {
            var fileName = string.Format("{0}train_{1}.log", GetTempPath(), DateTime.Now.ToString("yyyyMMddhh"));
            System.IO.StreamWriter sw = System.IO.File.AppendText(fileName);
            try
            {
                string logLine = System.String.Format(
                    "{0:G}: {1}.\n## INNER: {2}", System.DateTime.Now, msg, innerException);
                sw.WriteLine(logLine);
            }
            finally
            {
                sw.Close();
            }
        }
    }
}
