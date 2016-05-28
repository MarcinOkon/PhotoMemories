using ImageResizer.Configuration;
using PhotoMemories.Code.ImageHoster.Picasa.DataModel;
using System.Collections.Generic;
using System.IO;

namespace PhotoMemories.Code.ImageProcessing
{
    class ImageProcessor
    {
        public static void ProcessImage(PicasaMedia media, string filePath, Config config)
        {
            if (File.Exists(filePath))
            {
                config.Build(new ResizeJob(media, filePath));
                config.Build(new WatermarkJob(media, filePath));
            }
        }
    }
}
