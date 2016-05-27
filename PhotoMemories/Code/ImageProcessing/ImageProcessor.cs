using ImageResizer.Configuration;
using PhotoMemories.Code.ImageHoster.Picasa.DataModel;
using System.Collections.Generic;
using System.IO;

namespace PhotoMemories.Code.ImageProcessing
{
    class ImageProcessor
    {
        public static void ProcessImages(IEnumerable<PicasaMedia> mediaList)
        {
            var config = new ImageProcessorConfig();

            foreach (var media in mediaList)
            {
                ProcessImage(media, config);
            }
        }


        private static void ProcessImage(PicasaMedia media, Config config)
        {
            if (File.Exists(media.FilePath))
            {
                config.Build(new ResizeJob(media));
                config.Build(new WatermarkJob(media));
            }
        }
    }
}
