using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleane
{
    class Program
    {
        static void Main(string[] args)
        {
            ClearDesktop();

            try
            {
                IterateFolder(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
                Console.ReadLine();
            }
            catch (System.UnauthorizedAccessException)
            {
                Console.WriteLine($"Доступ запрещён:");
            }
        }

        static void ClearDesktop()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            foreach (string path in Directory.GetFiles(desktopPath))
            {
                string ext = Path.GetExtension(path);
                if (ext != ".lnk")
                {
                    File.Delete(path);
                }
            }
        }

        static void IterateFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                foreach (string dirPath in Directory.GetDirectories(folderPath))
                {
                    try
                    {
                        Console.WriteLine(dirPath);
                        IterateFolder(dirPath);
                    }
                    catch (System.UnauthorizedAccessException)
                    {
                        Console.WriteLine($"Доступ запрещён: {dirPath}");
                    }

                    //DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
                    //if (dirInfo.Name[0] == '.' || dirInfo.Attributes.HasFlag(FileAttributes.Hidden))
                    //    continue;

                    //foreach (string filePath in Directory.GetFiles(dirPath))
                    //{
                    //    string ext = Path.GetExtension(filePath);
                    //    if (ext != ".lnk")
                    //        File.Delete(filePath);
                    //}
                }
            }
        }
    }
}
