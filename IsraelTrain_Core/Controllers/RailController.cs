using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static System.Int32;
using static System.String;

namespace IsraelTrain_Core.Controllers
{
    [Route("/v01/")]
    [ApiController]
    public class RailController : ControllerBase
    {


        private static Regex regex = new Regex("^[0-9/: ]+$", RegexOptions.Compiled);
        public RailController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private readonly IConfiguration Configuration;

        [HttpGet]
        [Route("get")]
        public string Get()
        {
            return "anjdikans d";
        }



        [Route("GetQuery")]
        [HttpGet("{firstName}/{lastName}/{address}")]
        public string GetQuery(string id, string firstName, string lastName, string address)
        {
            return $"{firstName}:{lastName}:{address}";
        }

        
        [Route("schedulev2")]
        [HttpGet("{origin}/{destination}/{date}/{hours}")]
        public async Task<IActionResult> GetTimeScheduleAsync([FromQuery] string origin, [FromQuery] string destination, [FromQuery] string date, [FromQuery] string hours)
        {
            string password = Configuration["Password"];
            string userName = Configuration["SystemUserName"];
            string baseUrl = Configuration["BaseUrl"];
            string result = Empty;
            try
            {
                int systemId = 0;
                int originId = 0;
                int destinationId = 0;
                int hour = 0;
                string dateString = Empty;
                    //DateTime actualDateToCompare = DateTime.Now.AddDays(-1000);
                bool isErrorOccured = false;
                ArrayList list = new ArrayList();
                systemId = Parse(Configuration["SystemId"]);

                if (!Int32.TryParse(origin, out originId))
                    return Ok("Error: origin not correct");

                if (!Int32.TryParse(destination, out destinationId))
                    return Ok("Error: destination not correct");

                if (!Int32.TryParse(hours, out hour))
                    return Ok("Error: hour not correct");

                if (!String.IsNullOrEmpty(date))
                {
                    if (date.Length > 30 || !regex.IsMatch(date))
                    {
                        return Ok("Date Time Not Correct!");
                    }
                }
                else
                {
                    return Ok("Date Time Not Correct");
                }

                //try
                //{
                //    dateString = date;
                //    actualDateToCompare = DateTime.ParseExact(dateString.Substring(0, dateString.IndexOf(" ")).Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //    actualDateToCompare = actualDateToCompare.AddDays(1);
                //}
                //catch (Exception ex)
                //{
                //    isErrorOccured = true;
                //}
                var url = $"{baseUrl}WebServices/WsGetLuz.asmx/GetLuz?SystemId=1&SystemUserName={userName}&SystemPass={password}&Orign={originId}&Destination={destinationId}&Date={date}&Hours=0";

                var node = await GetAsync(url);
                if (!isErrorOccured)
                {
                    try
                    {
                        var nodes = node.SelectNodes("//Directs/Direct/train[starts-with(DepartureTime,'" + date + "')]");
                        foreach (XmlNode n in nodes)
                        {
                            try
                            {
                                node["Directs"].RemoveChild(n.ParentNode);
                            }
                            catch (Exception ex)
                            {
                                return Ok(ex.Message);
                                
                            }
                        }

                        nodes = node.SelectNodes("//Indirects/Indirect");
                        for (var i = nodes.Count - 1; i >= 0; i--)
                        {
                            try
                            {
                                if (nodes[i].HasChildNodes && nodes[i].FirstChild.SelectSingleNode("DepartureTime")
                                    .InnerXml
                                    .StartsWith(date)
                                )
                                    node["Indirects"].RemoveChild(nodes[i]);

                            }
                            catch (Exception ex)
                            {
                                return Ok(ex.Message);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        return Ok(ex.Message);
                    }
                }

                foreach (XmlNode rate in node.SelectNodes("Rates/Rate"))
                {
                    string _node = "<PassangerTypeTextEN>" + utilsTicketNPassenger.Get(enumXMListType.Passenger,
                                       int.Parse(rate["PassangerType"].InnerText), utilsTicketNPassenger.enumLang.ENG) +
                                   "</PassangerTypeTextEN>";
                    rate.InnerXml += _node;

                    _node = "<TicketTypeTextEN>" + utilsTicketNPassenger.Get(enumXMListType.Ticket,
                                int.Parse(rate["TicketType"].InnerText), utilsTicketNPassenger.enumLang.ENG) +
                            "</TicketTypeTextEN>";
                    rate.InnerXml += _node;

                    _node = "<PassangerTypeTextARB>" + utilsTicketNPassenger.Get(enumXMListType.Passenger,
                                int.Parse(rate["PassangerType"].InnerText), utilsTicketNPassenger.enumLang.ARR) +
                            "</PassangerTypeTextARB>";
                    rate.InnerXml += _node;

                    _node = "<TicketTypeTextARB>" + utilsTicketNPassenger.Get(enumXMListType.Ticket,
                                int.Parse(rate["TicketType"].InnerText), utilsTicketNPassenger.enumLang.ARR) +
                            "</TicketTypeTextARB>";
                    rate.InnerXml += _node;
                }
                result = JsonConvert.SerializeXmlNode(node);
                result = "{" + result.Remove(0, 1).Remove(result.Length - 2, 1) + "}";
                byte[] returnBytes = Encoding.UTF8.GetBytes(result);
                //return result;

                return Ok(result);
            }
            catch (Exception ex)
            {

                byte[] returnBytes = Encoding.UTF8.GetBytes("");
                Logger log = new Logger();
                log.LogToFile(ex.Message, "TimeScheduleLangServices.cs", ex.InnerException.ToString());
                return Ok(ex.Message);
                //return new MemoryStream(returnBytes);
            }
            
        }

        private async Task<XmlNode> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            XmlNode node = null;
            using HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            await using Stream stream = response.GetResponseStream();
            using StreamReader reader = new StreamReader(stream);
            var result = await reader.ReadToEndAsync();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            XmlNode newNode = doc.DocumentElement;
            return newNode;
        }
    }
}