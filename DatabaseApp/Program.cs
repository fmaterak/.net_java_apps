using System;
using System.Net;
using System.IO;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
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

    public class RequestedAction
    {
        public enum RAType
        {
            FETCH,
            SHOW,
        }

        public readonly RAType Type;
        public readonly DateTime Start;
        public readonly DateTime? End;

        public RequestedAction(RAType type, DateTime start, DateTime? end = null) {
            Type = type;
            Start = start.Date;
            if (end == null) {
                End = null;
            } else {
                End = ((DateTime) end).Date;
                if (End < Start) {
                    throw new ArgumentException("end date is smaller than start date");
                }
                else if (Type == RAType.FETCH && (End - Start).Value.Days + 1 > Program.MAX_DAYS_FETCH) {
                    throw new ArgumentException(String.Format(
                        "too big number of days to fetch, max is {0}",
                        Program.MAX_DAYS_FETCH));
                }
            }
        }

        public override String ToString()
        {
            var type = Type == RAType.FETCH ? "FETCH" : "SHOW";
            if (End == null) {
                return String.Format("RequestedAction({0}, {1})", type, Start.ToString("d"));
            } else {
                return String.Format("RequestedAction({0}, {1}, {2})", type, Start.ToString("d"), ((DateTime) End).ToString("d"));
            }
        }
    }

    public class Program
    {
        public static readonly int MAX_DAYS_FETCH = 10;
        public static List<RequestedAction> ParseArgs(string[] args)
        {
            int index = 0;
            List<RequestedAction> req_actions = new();

            try {
                while (index < args.Length)
                {
                    if (args[index] == "show" && args[index+1] == "it") {
                        if (req_actions.Count() == 0 || req_actions.Last().Type != RequestedAction.RAType.FETCH) {
                            throw new ArgumentException(String.Format(
                                "'it' must reference arguments of previous fetch (in 'show', arg #{0})", index + 2));
                        }

                        req_actions.Add(new(RequestedAction.RAType.SHOW, req_actions.Last().Start, req_actions.Last().End));
                        index += 2;
                    }
                    else if (args[index] == "fetch" || args[index] == "show") {
                        DateTime startDate;
                        DateTime? endDate = null;

                        // Parse first date arg, which is required
                        try {
                            startDate = DateTime.Parse(args[index+1]);
                        }
                        catch (FormatException) {
                            throw new ArgumentException(String.Format(
                                "'{0}' is not a valid date (in '{1}', arg #{2})",
                                args[index+1], args[index], index + 2));
                        }

                        // Parse second date arg, which is optional
                        if (index + 2 < args.Length) {
                            try {
                                endDate = DateTime.Parse(args[index+2]);
                            }
                            catch (FormatException) { }
                        }

                        var req_type = (args[index] == "fetch") ? RequestedAction.RAType.FETCH : RequestedAction.RAType.SHOW;
                        try {
                            req_actions.Add(new(req_type, startDate, endDate));
                        }
                        catch (ArgumentException e) {
                            throw new ArgumentException(String.Format("{0} (in '{1}', arg#{2})", e.Message, args[index], index + 1));
                        }
                        index += 2 + (endDate != null ? 1 : 0);
                    }
                    else {
                        throw new ArgumentException(String.Format("unknown action '{0}' (arg #{1})", args[index], index + 1));
                    }
                }
            }
            catch (IndexOutOfRangeException) {
                throw new ArgumentException(String.Format("not enough arguments for action '{0}' (arg #{1})", args[index], index + 1));
            }

            return req_actions;
        }

        public static int Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", true);
            try {
                var req_actions = ParseArgs(args);
            }
            catch (ArgumentException e) {
                Console.WriteLine(e.Message);
                return 1;
            }
            foreach (var ra in ParseArgs(args)) {
                Console.WriteLine(ra);
            }

            return 0;
        }

        public static void NotMain(string[] args)
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
