using GooglePhotosUploader.Code.DataModel;
using ImageResizer;
using ImageResizer.Configuration;
using ImageResizer.Plugins.Watermark;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace GooglePhotosUploader.Code
{
    class ImageProcessor
    {
        public static void ResizeImages(IEnumerable<PicasaMedia> mediaList)
        {
            Config config = SetConfig();

            foreach (var media in mediaList)
            {
                ResizeImage(media, config);
            }
        }

        private static Config SetConfig()
        {
            var config = new Config();
            var watermark = new WatermarkPlugin();
            watermark.Install(config);
            watermark.NamedWatermarks["text"] = new Layer[] { GetTextLayer() };
            return config;
        }

        private static TextLayer GetTextLayer()
        {
            return new TextLayer
            {
                Text = "#{name}",
                Align = ContentAlignment.BottomLeft,
                GlowColor = Color.Black,
                GlowWidth = 5,
                TextColor = Color.White,
                Width = new DistanceUnit(30, DistanceUnit.Units.Percentage),
                Fill = true,
                Font = "Roboto"
            };
        }

        private static void ResizeImage(PicasaMedia media, Config config)
        {
            if (File.Exists(media.FilePath))
            {
                config.Build(GetResizeJob(media));
                config.Build(GetWatermarkJob(media));
            }

        }

        private static ImageJob GetWatermarkJob(PicasaMedia media)
        {
            var yearsDelta = GetYearsDelta(media.Date);
            var watermarkText = string.Format("{0}\r\n{1} - {2}", media.AlbumTitle, GetYearsAgo(yearsDelta), media.Date.ToShortDateString());
            var instructionQuery = string.Format("watermark=text;name={0}", watermarkText);
            var instruction = new Instructions(instructionQuery);
            return new ImageJob(media.FilePath, media.FilePath, instruction);
        }

        private static ImageJob GetResizeJob(PicasaMedia media)
        {
            var instructionQuery = "width=1024&height=768&bgcolor=black";
            var instruction = new Instructions(instructionQuery);
            return new ImageJob(media.FilePath, media.FilePath, instruction);
        }

        private static string GetYearsAgo(int years)
        {
            switch (years)
            {
                case 0:
                    return "Dieses Jahr";
                case 1:
                    return "Vor 1 Jahr";
                default:
                    return string.Format("Vor {0} Jahren", years);
            }
        }

        private static int GetYearsDelta(DateTime date)
        {
            var difference = DateTime.Now - date;
            var rounded = Math.Round(difference.TotalDays / 365);
            return (int)rounded;
        }
    }
}
