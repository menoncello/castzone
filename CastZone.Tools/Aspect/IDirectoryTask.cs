using System.Collections.Generic;

namespace CastZone.Tools.Aspect
{
    public interface IDirectoryTask
    {
        string GetCurrentDirectory();
        IEnumerable<string> GetFiles(string path, string mask);
    }
}