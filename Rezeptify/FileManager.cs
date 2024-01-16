using Rezeptify.AppComponents;

namespace Rezeptify
{
    public class FileManager : IFileManager
    {
        public string GetApplicationFolder()
        {
            if (!Directory.Exists(FileSystem.AppDataDirectory))  Directory.CreateDirectory(FileSystem.AppDataDirectory);
            return FileSystem.AppDataDirectory;
        }

        public string GetAppDataDir()
        {
            return FileSystem.AppDataDirectory;
        }
    }
}
