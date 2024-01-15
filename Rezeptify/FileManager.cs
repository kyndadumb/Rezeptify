using Rezeptify.AppComponents;

namespace Rezeptify
{
    public class FileManager : IFileManager
    {
        public string GetApplicationFolder()
        {
            return FileSystem.AppDataDirectory;
        }
    }
}
