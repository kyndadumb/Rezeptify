using Rezeptify.VM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
