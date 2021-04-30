using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Net;
using System.Threading;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace WebApp
{
    // JSON API stuff
    public class Rates
    {
        public float PLN { get; set; }
        public float EUR { get; set; }
        public float GBP { get; set; }
        public float CHF { get; set; }
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

        public string getRates(string Date)
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://openexchangerates.org/api/historical/" + Date + ".json?app_id=dc18a6709e084d609d1efa99b2d6ad68");
                var json = web.DownloadString(url);
                return json;
            }
        }
    }

    public class ServerResponse
    {
        public string Base { get; set; }

        public int Timestamp { get; set; }

        public Rates Rates { get; set; }
    }

    // Database-related stuff
    public class RatesRecord : Rates
    {
        [Key]
        public DateTime Date { get; set; }

        public static RatesRecord FromRates(DateTime date, Rates r)
        {
            if (r == null)
            {
                throw new ArgumentNullException("r");
            }

            RatesRecord rec = new();
            rec.Date = date.Date;

            // Copy properties from r
            foreach (var prop in typeof(Rates).GetProperties())
            {
                var value = prop.GetValue(r);
                var myprop = rec.GetType().GetProperty(prop.Name);
                myprop.SetValue(rec, value, null);
            }

            return rec;
        }

        public override String ToString()
        {
            var strings = typeof(Rates).GetProperties().Select(p =>
                String.Format("{0}/{1} = {2}", p.Name, Program.BASE_CURRENCY, p.GetValue(this)));
            return String.Format("{0}: {1}", Date.ToString("d"), String.Join(",\t", strings));
        }
    }

    public class RatesHistory : DbContext
    {
        public virtual DbSet<RatesRecord> RatesRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./rates.sqlite");
        }
    }

    // App-related stuff
    public class RequestedAction
    {
        public enum RAType
        {
            FETCH,
            SHOW,
            SHOW_ALL,
        }

        public RAType Type { get; }
        public DateTime Start { get; }
        public DateTime? End { get; }

        public RequestedAction(RAType type, DateTime start, DateTime? end = null)
        {
            Type = type;
            Start = start.Date;
            if (end == null)
            {
                End = null;
            }
            else
            {
                End = ((DateTime)end).Date;
                if (End < Start)
                {
                    throw new ArgumentException("end date is smaller than start date");
                }
                else if (Type == RAType.FETCH && (End - Start).Value.Days + 1 > Program.MAX_DAYS_FETCH)
                {
                    throw new ArgumentException(String.Format(
                        "too big number of days to fetch, max is {0}",
                        Program.MAX_DAYS_FETCH));
                }
            }
        }

        public static RequestedAction ActionShowAll()
        {
            return new(RAType.SHOW_ALL, DateTime.Today);  // use dummy date
        }

        public IEnumerable<DateTime> Dates()
        {
            if (End == null)
            {
                yield return Start;
                yield break;
            }

            for (var day = Start; day <= End; day = day.AddDays(1))
            {
                yield return day;
            }
        }

        public override String ToString()
        {
            String type = "UNKNOWN_TYPE";
            switch (Type)
            {
                case RAType.FETCH: type = "FETCH"; break;
                case RAType.SHOW: type = "SHOW"; break;
                case RAType.SHOW_ALL: type = "SHOW_ALL"; break;
            }

            if (End == null)
            {
                return String.Format("RequestedAction({0}, {1})", type, Start.ToString("d"));
            }
            else
            {
                return String.Format("RequestedAction({0}, {1}, {2})", type, Start.ToString("d"), ((DateTime)End).ToString("d"));
            }
        }
    }
    public class Program
    {
        public static readonly String BASE_CURRENCY = "USD";
        public static readonly int MAX_DAYS_FETCH = 10;

        public static void PrintHelp()
        {
            Console.WriteLine("usage: program {{instruction}}");
            Console.WriteLine("instruction is one of:");
            Console.WriteLine("    help             -- display this help and exit");
            Console.WriteLine("    fetch date       -- fetch rates for given date");
            Console.WriteLine("    fetch start end  -- fetch rates for dates between start and end (both inclusive)");
            Console.WriteLine("    show date        -- show stored rates for given date");
            Console.WriteLine("    show start end   -- show stored rates dates between start and end (both inclusive)");
            Console.WriteLine("    show it          -- show previously fetched rates");
            Console.WriteLine("    show all         -- show all stored rates");
        }

        public static ICollection<RequestedAction> ParseArgs(string[] args)
        {
            int index = 0;
            List<RequestedAction> req_actions = new();

            try
            {
                while (index < args.Length)
                {
                    if (args[index] == "help")
                    {
                        PrintHelp();
                        return null;
                    }
                    else if (args[index] == "show" && args[index + 1] == "it")
                    {
                        if (req_actions.Count() == 0 || req_actions.Last().Type != RequestedAction.RAType.FETCH)
                        {
                            throw new ArgumentException(String.Format(
                                "'it' must reference arguments of previous fetch (in 'show', arg #{0})", index + 2));
                        }

                        req_actions.Add(new(RequestedAction.RAType.SHOW, req_actions.Last().Start, req_actions.Last().End));
                        index += 2;
                    }
                    else if (args[index] == "show" && args[index + 1] == "all")
                    {
                        req_actions.Add(RequestedAction.ActionShowAll());
                        index += 2;
                    }
                    else if (args[index] == "fetch" || args[index] == "show")
                    {
                        DateTime startDate;
                        DateTime? endDate = null;

                        // Parse first date arg, which is required
                        try
                        {
                            startDate = DateTime.Parse(args[index + 1]);
                        }
                        catch (FormatException)
                        {
                            throw new ArgumentException(String.Format(
                                "'{0}' is not a valid date (in '{1}', arg #{2})",
                                args[index + 1], args[index], index + 2));
                        }

                        // Parse second date arg, which is optional
                        if (index + 2 < args.Length)
                        {
                            try
                            {
                                endDate = DateTime.Parse(args[index + 2]);
                            }
                            catch (FormatException) { }
                        }

                        var req_type = (args[index] == "fetch") ? RequestedAction.RAType.FETCH : RequestedAction.RAType.SHOW;
                        try
                        {
                            req_actions.Add(new(req_type, startDate, endDate));
                        }
                        catch (ArgumentException e)
                        {
                            throw new ArgumentException(String.Format("{0} (in '{1}', arg#{2})", e.Message, args[index], index + 1));
                        }
                        index += 2 + (endDate != null ? 1 : 0);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("unknown action '{0}' (arg #{1})", args[index], index + 1));
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentException(String.Format("not enough arguments for action '{0}' (arg #{1})", args[index], index + 1));
            }

            return req_actions;
        }
        public static ServerResponse Fetch(DateTime date)
        {
            // Get currencies to fetch from Rates' declared properties and join with URL-quoted commas
            var symbols = String.Join("%2C", typeof(Rates).GetProperties().Select(p => p.Name));
            var url = String.Format(
                "https://openexchangerates.org/api/historical/{0}.json?app_id=dc18a6709e084d609d1efa99b2d6ad68&symbols={1}&base={2}",
                date.ToString("yyyy-MM-dd"), symbols, BASE_CURRENCY);
            using (WebClient web = new WebClient())
            {
                var response = web.DownloadString(url);
                return JsonConvert.DeserializeObject<ServerResponse>(response);
            }
        }

        public static RatesRecord GetRatesRecordFromHistory(RatesHistory context, DateTime date)
        {
            try
            {
                return context.RatesRecords.Where(r => r.Date == date.Date).First();
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }

        public static void Act(RequestedAction action, RatesHistory context)
        {
            if (action.Type == RequestedAction.RAType.FETCH)
            {
                var dates = action.Dates().ToList();
                var threads = new List<Thread>(dates.Count);

                foreach (var date in dates)
                {
                    if (GetRatesRecordFromHistory(context, date) == null)
                    {
                        var thread = new Thread(delegate () {
                            Console.WriteLine("Fetching data for {0}", date);
                            var resp = Fetch(date);
                            context.RatesRecords.Add(RatesRecord.FromRates(date, resp.Rates));
                        });
                        threads.Add(thread);
                        thread.Start();
                    }
                }

                foreach (var thread in threads)
                {
                    thread.Join();
                }

                context.SaveChanges();
            }
            else if (action.Type == RequestedAction.RAType.SHOW)
            {
                foreach (var date in action.Dates())
                {
                    var rates = GetRatesRecordFromHistory(context, date);
                    if (rates == null)
                    {
                        Console.WriteLine("{0}: no data", date.ToString("d"));
                    }
                    else
                    {
                        Console.WriteLine(rates);
                    }
                }
            }
            else if (action.Type == RequestedAction.RAType.SHOW_ALL)
            {
                foreach (var rates in context.RatesRecords.OrderBy(rr => rr.Date))
                {
                    Console.WriteLine(rates);
                }
            }
        }
        public static int Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", true);
            ICollection<RequestedAction> req_actions;

            try
            {
                req_actions = ParseArgs(args);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }

            if (req_actions == null)
            {
                return 1;
            }
            else if (req_actions.Count == 0)
            {
                Console.WriteLine("Nothing to do. See help with 'help' option.");
            }

            var context = new RatesHistory();
            context.Database.EnsureCreated();

            foreach (var action in req_actions)
            {
                // Console.WriteLine(action);
                Act(action, context);
            }

            CreateHostBuilder(args).Build().Run();
            return 0;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
