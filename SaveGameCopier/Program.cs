using System;
using System.IO;
using System.Security.Principal;

namespace SaveGameCopier.cs
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Please specify the user to copy games from:");
            var fromUser = Console.ReadLine();
            Console.WriteLine("Please specify the user to copy games to:");
            var toUser = Console.ReadLine();
            Console.WriteLine("Please choose which game's save files you would like copied:");
            Console.WriteLine("1 - Borderlands");
            Console.WriteLine("2 = Borderlands 2");
            string gameName = Console.ReadLine();
            Console.WriteLine("Backing up data...");

            gameName = gameName == "1" ? "Borderlands" : "Borderlands 2";

            string firstPath = $"C:\\Users\\{ fromUser }\\Documents\\My Games\\{ gameName }\\SaveData";
            string secondPath = $"C:\\Users\\{ toUser }\\Documents\\My Games\\{ gameName }\\SaveData";

            var firstDir = new DirectoryInfo(firstPath);
            var secondDir = new DirectoryInfo(secondPath);
            string firstBackupDir = firstDir + "\\Backup\\";
            string secondBackupDir = secondDir + "\\Backup\\";
            FileInfo[] firstFiles = firstDir.GetFiles();
            FileInfo[] secondFiles = secondDir.GetFiles();

            if (!Directory.Exists(firstBackupDir))
            {
                Directory.CreateDirectory(firstBackupDir);
            }

            if (!Directory.Exists(secondBackupDir))
            {
                Directory.CreateDirectory(secondBackupDir);
            }

            foreach (var file in firstFiles)
            {
                if (File.Exists($"{ firstBackupDir }{ file.Name }"))
                {
                    File.Delete($"{ firstBackupDir }{ file.Name }");
                }
                file.CopyTo($"{ firstBackupDir }{ file.Name }");
                Console.WriteLine($"Backed up { file.Name } to { firstBackupDir }.");
            }

            foreach (var file in secondFiles)
            {
                if (File.Exists($"{ secondBackupDir }{ file.Name }"))
                {
                    File.Delete($"{ secondBackupDir }{ file.Name }");
                }
                file.CopyTo($"{ secondBackupDir }{ file.Name }");
                Console.WriteLine($"Backed up { file.Name } to { secondBackupDir }.");
            }

            Console.WriteLine("Backup completed!");

            Console.ReadKey();
        }
    }
}