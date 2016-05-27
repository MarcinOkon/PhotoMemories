using ImageResizer;
using PhotoMemories.Code.ImageHoster.Picasa.DataModel;
using System;

namespace PhotoMemories.Code.ImageProcessing
{
    class WatermarkJob : ImageJob
    {
        public WatermarkJob(PicasaMedia media) : base(media.FilePath, media.FilePath, GetInstructions(media))
        {
        }

        private static Instructions GetInstructions(PicasaMedia media)
        {
            var watermarkText = string.Format($"{media.AlbumTitle}\r\n{GetYearsAgo(media)} - {media.Date.ToShortDateString()}");
            var instructionQuery = string.Format($"watermark=text;name={watermarkText}");
            return new Instructions(instructionQuery);
        }

        private static string GetYearsAgo(PicasaMedia media)
        {
            var yearsDelta = GetYearsDelta(media.Date);

            switch (yearsDelta)
            {
                case 0:
                    return "Dieses Jahr";
                case 1:
                    return "Vor 1 Jahr";
                default:
                    return string.Format($"Vor {yearsDelta} Jahren");
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
