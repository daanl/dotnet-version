using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace dotnet_version
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || string.Equals(args[0], "-help", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Usage: <FILE_PATH> <VERSION_STRING>");
                return;
            }

            var projectFilePath = Directory.GetCurrentDirectory() + args[0];

            if (!File.Exists(projectFilePath))
            {
                Console.WriteLine($"Project file path: {projectFilePath} doesn't exists");
                return;
            }

            var version = args[1];

            if (string.IsNullOrWhiteSpace(version))
            {
                Console.WriteLine("Version not specified");
                return;
            }

            var content = File.ReadAllText(projectFilePath);
            dynamic projectFile = JsonConvert.DeserializeObject(content);

            projectFile.version = args[1];


            var projectJsonFile = JsonConvert.SerializeObject(projectFile, Formatting.Indented);
            File.WriteAllText(projectFilePath, projectJsonFile);

            Console.WriteLine($"Successfully updated: {projectFilePath}");
        }
    }
}
