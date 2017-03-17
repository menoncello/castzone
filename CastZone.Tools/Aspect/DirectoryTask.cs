using System;
using System.Collections.Generic;
using System.IO;

namespace CastZone.Tools.Aspect
{
    public class DirectoryTask : IDirectoryTask
    {
        public string GetCurrentDirectory() =>
            Directory.GetCurrentDirectory();

        public IEnumerable<string> GetFiles(string path, string mask) =>
            Directory.GetFiles(path, mask);
    }
}