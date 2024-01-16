using Android.App;
using Rezeptify.AppComponents;

namespace Rezeptify
{
    public class FileManager : IFileManager
    {
        public string GetApplicationFolder()
        {
            //string appdata_path = Path.Combine(FileSystem.AppDataDirectory, "Rezeptify");
            if (!Directory.Exists(FileSystem.AppDataDirectory))  Directory.CreateDirectory(FileSystem.AppDataDirectory);
            return FileSystem.AppDataDirectory;
        }

        public string GetAppDataDir()
        {
            return FileSystem.AppDataDirectory;
        }
    }
}
