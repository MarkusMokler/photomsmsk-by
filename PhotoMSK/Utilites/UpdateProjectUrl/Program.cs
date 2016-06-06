using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDesk.Options;

namespace UpdateProjectUrl
{
    class Program
    {
        private string url;
        private bool show_help;

        private Program(string[] args)
        {

            InitArgs(args);

            using (var writer = new StreamWriter("c:\\photomsk\\conf.json"))
            {
                writer.Write("{url:'" + url + "'}");
            }

        }

        static void Main(string[] args)
        {
            new Program(args);
        }

        private void InitArgs(string[] args)
        {
            var p = new OptionSet
            {

                {"u|url=", "the {Url} of the site.", v => url = v},
            };

            try
            {
                p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("greet: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `greet --help' for more information.");
                return;
            }

            if (!show_help) return;
            ShowHelp(p);
        }
        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: greet [OPTIONS]+ message");
            Console.WriteLine("Greet a list of individuals with an optional message.");
            Console.WriteLine("If no message is specified, a generic greeting is used.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }

    }

    public class UrlConfiguration
    {
        public string Url { get; set; }
    }
}
