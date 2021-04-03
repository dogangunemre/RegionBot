using IdentityModel.Client;
using Newtonsoft.Json;
using RegionBot.Const;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace RegionBot
{
    class Program
    {
        static void Main(string[] args)
        {


            try
            {
                RegionManager regionManager = new RegionManager();
                regionManager.Add();
            }
            catch (System.Exception ex)
            {

                Console.WriteLine(ex.GetBaseException().Message);
            }

            Console.ReadKey();

          
        }
    }
}
