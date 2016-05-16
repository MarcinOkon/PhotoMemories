using ImageResizer.Configuration;
using ImageResizer.Plugins.Watermark;
using System.Drawing;

namespace PhotoMemories.Code.ImageProcessing
{
    public class ImageProcessorConfig : Config
    {
        public ImageProcessorConfig()
        {
            Initialize();
        }

        private void Initialize()
        {
            var watermark = new WatermarkPlugin();
            watermark.Install(this);
            watermark.NamedWatermarks["text"] = new Layer[] { GetTextLayer() };
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
    }
}
