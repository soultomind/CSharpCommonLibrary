using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.IO
{
    public class FileCreateInfo
    {
        public bool FileLock { get; set; }
        public FileMode FileMode { get; set; }
        public FileAccess FileAccess { get; set; }

        public FileCreateInfo()
        {
            FileLock = false;
            FileMode = FileMode.Open;
            FileAccess = FileAccess.Read;
        }
    }
}
