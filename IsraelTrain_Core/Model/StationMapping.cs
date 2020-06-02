using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace IsraelTrain_Core.Model
{

    [DataContract(Name = "stationmapping", Namespace = "")]
    public class StationMapping
    {
        [DataMember(Name = "railid", IsRequired = true, Order = 1)]
        public int RailId { get; set; }

        [DataMember(Name = "name", IsRequired = true, Order = 2)]
        public string Name { get; set; }

        [DataMember(Name = "mapaid", IsRequired = false, Order = 3)]
        public int? MapaId { get; set; }

        [DataMember(Name = "latitude", IsRequired = true, Order = 4)]
        public double? Latitude { get; set; }

        [DataMember(Name = "longitude", IsRequired = true, Order = 5)]
        public double? Longitude { get; set; }

        [DataMember(Name = "engname", IsRequired = true, Order = 6)]
        public string EngName { get; set; }

        [DataMember(Name = "arbname", IsRequired = true, Order = 7)]
        public string ArbName { get; set; }

        [DataMember(Name = "descarb", IsRequired = true, Order = 8)]
        public string DescriptionArb { get; set; }

        public StationMapping()
        {
        }
        public StationMapping(string name, int railId, int mapaId, double latitude, double longitude, string engName, string arrName)
        {
            setData(name, railId, mapaId, latitude, longitude, engName, arrName);
        }

        public void setData(string name, int railId, int mapaId, double latitude, double longitude, string engName, string arrName)
        {
            Name = name;
            RailId = railId;
            MapaId = mapaId;
            Latitude = latitude;
            Longitude = longitude;
            EngName = engName;
            ArbName = arrName;

        }

        public StationMapping(string name, string engName, int railId, string arrName)
        {
            setData(name, engName, railId, arrName);
        }

        public void setData(string name, string engName, int railId, string arrName)
        {
            Name = name;
            RailId = railId;
            EngName = engName;
            ArbName = arrName;
        }

        //   public static StationMapping Create()
        //{
        //	return new StationMapping {Name = "name", RailId = 12, MapaId = 12, Latitude = 12, Longitude = 12, EngName="engname"};
        //}
    }

    public class StationMappingList : List<StationMapping>
    {

        //public void Serialized()
        //{
        //    DataContractSerializer dcs = new DataContractSerializer(typeof(StationMapping));
        //    using (FileStream fs = new FileStream(Environment.CurrentDirectory + @"\App_Data\stations.xml", FileMode.Open, FileAccess.ReadWrite))
        //    {
        //        XmlDictionaryWriter xdw = XmlDictionaryWriter.CreateTextWriter(fs, Encoding.UTF8);
        //        dcs.WriteObject(xdw, this);
        //        fs.Flush();
        //    }
        //}
    }
}
