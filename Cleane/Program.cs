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

            RecursiveDelete(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)), true);

            //IterateFolder(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            Console.ReadLine();
        }

        static void RecursiveDelete(DirectoryInfo baseDir, bool isRootDir)
        {
            if (!baseDir.Exists)
                return;

            foreach(var dir in baseDir.EnumerateDirectories())
            {
                if (!dir.Attributes.HasFlag(FileAttributes.Hidden)
                    && !dir.Attributes.HasFlag(FileAttributes.System))
                {
                    RecursiveDelete(dir, false);
                }
            }

            foreach(var file in baseDir.GetFiles())
            {
                bool isReadOnly = file.IsReadOnly;
                try
                {
                    file.IsReadOnly = false;
                    file.Delete();
                    Console.WriteLine($"[INFO MESSAGE] - File sucessfully deleted: {file.FullName}");
                }
                catch (Exception ex)
                {
                    if (isReadOnly == true && file.IsReadOnly == false)
                        file.IsReadOnly = true;
                    Console.WriteLine($"[ERROR MESSAGE] - {ex.Message}");
                }
            }

            if (!isRootDir)
            {
                try
                {
                    baseDir.Delete();
                    Console.WriteLine($"[INFO MESSAGE] - Directory deleted: {baseDir.FullName}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR MESSAGE] - {ex.Message}");
                }
            }
        }
    }
}
