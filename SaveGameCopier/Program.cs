using System;
using System.IO;
using System.Linq;

namespace SaveGameCopier
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Please specify the user to copy games from:");
            string fromUser = Console.ReadLine();
            Console.WriteLine("Please specify the user to copy games to:");
            string toUser = Console.ReadLine();
            Console.WriteLine("Please choose which game's save files you would like copied:");
            Console.WriteLine("1 - Borderlands");
            Console.WriteLine("2 - Borderlands 2");
            Console.WriteLine("3 - Rocket League");
            int.TryParse(Console.ReadLine(), out int gameSelection);

            var pathDef = new PathDefinitions();
            string savePath = pathDef._definitions.FirstOrDefault(p => p.Key == (PathDefinitions.GameNames)gameSelection).Value;
            if (savePath == null)
            {
                Console.WriteLine("The name provided does not match the currently supported games. Please try again.");
                Console.ReadKey();
                Environment.Exit(13);
            }

            string fromPath = $"C:\\Users\\{ fromUser }\\Documents\\My Games\\{ savePath }";
            string toPath = $"C:\\Users\\{ toUser }\\Documents\\My Games\\{ savePath }";

            var fromDir = new DirectoryInfo(fromPath);
            var toDir = new DirectoryInfo(toPath);

            string fromBackupDir = fromDir + "\\Backup\\";
            string toBackupDir = toDir + "\\Backup\\";

            if (!Directory.Exists(fromDir.ToString()))
            {
                Console.WriteLine("The save folder does not exist, or the incorrect game was chosen. Please try again.");
                Console.ReadKey();
                Environment.Exit(13);
            }
            FileInfo[] fromFiles = fromDir.GetFiles("*", SearchOption.AllDirectories);

            if (!Directory.Exists(toDir.ToString()))
            {
                Directory.CreateDirectory(toDir.ToString());
            }
            FileInfo[] toFiles = toDir.GetFiles("*", SearchOption.AllDirectories);

            if (!Directory.Exists(fromBackupDir))
            {
                Directory.CreateDirectory(fromBackupDir);
            }

            if (!Directory.Exists(toBackupDir))
            {
                Directory.CreateDirectory(toBackupDir);
            }

            Console.WriteLine("Backing up data...");

            foreach (var file in toFiles)
            {
                if (File.Exists($"{ toBackupDir }{ file.Name }"))
                {
                    File.Delete($"{ toBackupDir }{ file.Name }");
                }
                file.CopyTo($"{ toBackupDir }{ file.Name }");
                Console.WriteLine($"Backed up { file.Name } to { toBackupDir }.");
            }

            foreach (var file in fromFiles)
            {
                if (File.Exists($"{ fromBackupDir }{ file.Name }"))
                {
                    File.Delete($"{ fromBackupDir }{ file.Name }");
                }
                file.CopyTo($"{ fromBackupDir }{ file.Name }");
                Console.WriteLine($"Backed up { file.Name } to { fromBackupDir }.");

                if (File.Exists($"{ toDir }{ file.Name }"))
                {
                    File.Delete($"{ toDir }{ file.Name }");
                }
                file.CopyTo($"{ toDir }{ file.Name }");
                Console.WriteLine($"Copied { file.Name } from { fromDir } to { toDir }.");
            }

            Console.WriteLine("Operation complete.");

            Console.ReadKey();
        }
    }
}