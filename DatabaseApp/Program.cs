using System;
using System.Net;
using System.IO;
using System.Data;
using Newtonsoft.Json;


namespace CurrencyApp
{
    public class rates
    {
        public float PLN;
        public float EUR;
        public float GBP;
        public float CHF;

    }
    public class Base
    {
        public float USD;
    }
    public class DownloadRates
    {
        public string rates { get; set; }

        public Base main { get; set; }

        public string getRates()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://openexchangerates.org/api/latest.json?app_id=dc18a6709e084d609d1efa99b2d6ad68");
                var json = web.DownloadString(url);
                return json;
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            DownloadRates myRates = new DownloadRates();
            DownloadRates deserializedProduct = JsonConvert.DeserializeObject<DownloadRates>(myRates.getRates());
            Console.WriteLine("PLN / USD: " + deserializedProduct.rates);
        }
    }
}