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
            //ClearDesktop();

            IterateFolder(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            Console.ReadLine();
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

        static void RemoveAllFiles(string dirPath)
        {
            foreach (string filePath in Directory.GetFiles(dirPath))
            {
                string ext = Path.GetExtension(filePath);
                if (ext != ".lnk")
                {
                    if (Path.GetFileName(filePath)[0] != '.')
                    {
                        try
                        {
                            File.Delete(filePath);
                            Console.WriteLine($"File deleted: {filePath}");
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            Console.WriteLine($"Доступ запрещён: {filePath}");
                        }

                    }
                }
            }
        }

        static void IterateFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                foreach (string dirPath in Directory.GetDirectories(folderPath))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
                    if (dirInfo.Name[0] == '.' || dirInfo.Attributes.HasFlag(FileAttributes.Hidden))
                        continue;

                    RemoveAllFiles(dirPath);
                    IterateFolder(dirPath);
                }
                RemoveAllFiles(folderPath);
            }
        }
    }
}
