using System;
using System.IO;
using System.Linq;

namespace HomeSwitchHome.Infrastructure.Utils
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
    }
}