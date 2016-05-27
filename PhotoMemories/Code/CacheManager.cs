using PhotoMemories.Code.ImageHoster.Picasa.DataModel;
using System.IO;

namespace PhotoMemories.Code
{
    public class CacheManager
    {
        const string cachePath = @"D:\PhotoMemories\Cache";

        public static bool CopiedFileFromCache(PicasaMedia media)
        {
            var cachedMediaPath = Path.Combine(cachePath, media.Id);
            var file = new FileInfo(cachedMediaPath);
            if (file.Exists)
            {
                new FileInfo(media.FilePath).Directory.Create();
                file.CopyTo(media.FilePath, true);
                return true;
            }
            return false;
        }
    }
}
