using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace HomeSwitchHome.API.Utils
{
    public static class ImagesUtils
    {
        private static readonly string[] ACCEPTED_FILE_TYPES = new[] {".jpg", ".jpeg", ".png"};
        private static readonly int MBAllowed = 5 * 1024 * 1024; //5 mb

        public static bool IsValid(long fileLength)
        {
            return fileLength < MBAllowed;
        }

        public static bool IsValid(string fileName)
        {
            return ACCEPTED_FILE_TYPES.Contains(Path.GetExtension(fileName).ToLower());
        }

        public static Image GetReducedImage(int width, int height, Stream ResourceImage)
        {
            try
            {
                Image image = Image.FromStream(ResourceImage);

                // Figure out the ratio
                double ratioX = (double) width / (double) image.Width;
                double ratioY = (double) height / (double) image.Height;
                // use whichever multiplier is smaller
                double ratio = ratioX < ratioY ? ratioX : ratioY;

                int newHeight = Convert.ToInt32(image.Height * ratio);
                int newWidth = Convert.ToInt32(image.Width * ratio);

                Image thumb = image.GetThumbnailImage(newWidth, newHeight, () => false, IntPtr.Zero);

                return thumb;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}