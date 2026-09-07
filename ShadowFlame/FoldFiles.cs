using System;
using System.IO;

namespace ShadowFlame
{
    internal class FoldFiles
    {
        public static void OpenInExplorer(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
            {
                throw new ArgumentException("folderPath must be a valid existing directory.", nameof(folderPath));
            }
            System.Diagnostics.Process.Start("explorer.exe", folderPath);
        }
    }
}
