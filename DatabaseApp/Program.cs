using System;
using System.Net;
using System.IO;
using System.Data;
using Newtonsoft.Json;


namespace CurrencyApp
{
    public class Rates
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
    public class Timestamp
    {
        public string timestamp;
    }
    public class DownloadRates
    {
        public string Base { get; set; }

        public string Timestamp { get; set; }

        public Rates Rates { get; set; }

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
            Console.WriteLine("Baza: " + deserializedProduct.Base);
            Console.WriteLine("Znacznik czasu: " + deserializedProduct.Timestamp);
            Console.WriteLine("USD / PLN: " + deserializedProduct.Rates.PLN);
            Console.WriteLine("USD / EUR: " + deserializedProduct.Rates.EUR);
            Console.WriteLine("USD / GBP: " + deserializedProduct.Rates.GBP);
            Console.WriteLine("USD / CHF: " + deserializedProduct.Rates.CHF);

        }
    }
}