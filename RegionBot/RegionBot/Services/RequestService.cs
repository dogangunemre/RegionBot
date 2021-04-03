using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RegionBot.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RegionBot
{
    public class RequestService
    {

        public List<Region> regions;

        public List<Region>  GetRegions(string method, string jsonData, string listType)
        {


            WebRequest request = WebRequest.Create(
         "https://turk.net/service/AddressServ.svc/" + method);

            string Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJTYWxlS2V5IjoiMGI2NGY1ZGYtNjQzNS00ZmYzLTg3NWItMTI2OThmOTljM2E5In0.1EMJIBussNkldzTQLW8BzdNB8QzD3UQe6jg01ByKd-0";
            request.Method = "PUT";
            request.PreAuthenticate = true;
            request.Headers.Add("Token", Token);
            byte[] postBytes = Encoding.UTF8.GetBytes(jsonData);
            request.ContentType = "application/json; charset=UTF-8";
            request.ContentLength = postBytes.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);


            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            JObject json = JObject.Parse(reader.ReadToEnd());
            var regionList = json[$"{listType}"].Value<JArray>();
            List<Region> regions = regionList.ToObject<List<Region>>();
        

            response.Close();
            return regions;

        }


    }

     
 }

