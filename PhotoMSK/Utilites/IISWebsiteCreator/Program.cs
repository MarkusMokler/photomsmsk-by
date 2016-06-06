using System;
using System.IO;
using System.Linq;
using Microsoft.Web.Administration;
using NDesk.Options;

namespace IISWebsiteCreator
{
    class Program
    {

        public ServerManager iisManager { get; set; }
        bool show_help = false;
        string siteName;
        string vDir;
        string path;
        string port = "8080";

        private Program(string[] args)
        {
            InitArgs(args);

            var targetPath = "c:\\inetpub\\" + siteName + "\\" + vDir;

            CopyFiles(path, targetPath);

            path = targetPath;

            iisManager = new ServerManager();


            var id = CreateSite();

            CreateVDir(id);

            if (id.State == ObjectState.Stopped)
                id.Start();

        }
        private void InitArgs(string[] args)
        {
            var p = new OptionSet
            {

                {"s|siteName=", "the {NAME} of the site.", v => siteName = v},
                {"vd|virtualDirectory=", "virtual directory for site", v => vDir = v},
                {"port=", "port for site", v => port= v},
                {"p|path=", "path to site", v => path = v},
                {"h|help", "show this message and exit", v => show_help = v != null},
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
        Site CreateSite()
        {
            var site = iisManager.Sites.SingleOrDefault(x => x.Name == siteName);

            if (site != null)
                return site;
            iisManager.Sites.Add(siteName, "http", "*:" + port + ":" + siteName, path);
            iisManager.CommitChanges();
            return iisManager.Sites.SingleOrDefault(x => x.Name == siteName);

        }
        void CreateVDir(Site site)
        {
            var vdir = site.Applications.FirstOrDefault(x => x.Path.Equals("/" + vDir));//.SelectMany(x => x.VirtualDirectories).ToList();

            if (vdir == null)
            {
                site.Applications.Add("/" + vDir, path);
                iisManager.CommitChanges();
            }
        }

        private void CopyFiles(string sourcePath, string targetPath)
        {
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            if (Directory.Exists(sourcePath))
            {
                var dir = new DirectoryInfo(sourcePath);
                var files = dir.GetFiles();
                foreach (var file in files)
                {
                    string temppath = Path.Combine(targetPath, file.Name);
                    try
                    {
                        File.Copy(file.FullName, temppath, true);

                    }
                    catch (Exception exception)
                    {
                        Console.Write(exception.StackTrace);
                    }
                }

                foreach (DirectoryInfo subdir in dir.GetDirectories())
                {
                    string temppath = Path.Combine(targetPath, subdir.Name);
                    CopyFiles(subdir.FullName, temppath);
                }
            }
            else
            {
                Console.WriteLine("Source path does not exist!");
            }
        }


        static void Main(string[] args)
        {
            new Program(args);
        }

    }
}