using ImageResizer;
using PhotoMemories.Code.ImageHoster.Picasa.DataModel;

namespace PhotoMemories.Code.ImageProcessing
{
    class ResizeJob : ImageJob
    {
        public ResizeJob(PicasaMedia media) : base(media.FilePath, media.FilePath, GetInstructions())
        {
        }

        private static Instructions GetInstructions()
        {
            var instructionQuery = "width=1024&height=768&bgcolor=black";
            return new Instructions(instructionQuery);
        }
    }
}
