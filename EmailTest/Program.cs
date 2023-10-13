using EmailTest.Utilities;
using NUnitLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace EmailTest
{
    class Program
    {
        public static int Main(string[] args)
        {
            while (1==1)
            {
                string path = String.Empty;
                List<string> testList = new List<string>();

                if (args.Length > 0)
                {
                    if (System.IO.File.Exists(args[0]))
                    {
                        path = args[0];
                    }
                }

                if (String.IsNullOrEmpty(path))
                {
                    Console.WriteLine("Hello! Please enter the valid path to Your Config file or click enter to use a default file.");
                    path = Console.ReadLine();

                    while (1 == 1)
                    {
                        if (System.IO.File.Exists(path) || String.IsNullOrEmpty(path))
                            break;
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("The path: " + path + " is incorrect. Please enter the path to Your Config file or click enter to use a default file.");
                            path = Console.ReadLine();
                        }

                    }
                }

                if (!String.IsNullOrEmpty(path))
                {
                    setConfig(path);
                }

                if (ConfigurationHelper.Get<int>("RunAll") == 1)
                {
                    return new AutoRun().Execute(new string[] { });
                }

                if (ConfigurationHelper.Get<int>("RunEmailSend") == 1)
                {
                    testList.Add("--test=EmailTest.EmailTest.TestEmailSending");
                }

                if (ConfigurationHelper.Get<int>("RunEmailRecieve") == 1)
                {
                    testList.Add("--test=EmailTest.EmailTest.TestEmailReceiving");
                }

                if (testList.Count > 0)
                {
                    return new AutoRun().Execute(testList.ToArray());
                }

                Console.WriteLine("No tests were scheduled to run, please check Your config file and try again.");
            }
        }

        public static void setConfig(string path) 
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(path);

            foreach (XmlNode node in xml.SelectNodes("/configuration/appSettings/add"))
            {
                string key = node.Attributes["key"].Value;
                string value = node.Attributes["value"].Value;
                ConfigurationManager.AppSettings.Set(key, value);
            }
        }
    }
}
