using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Tjeker.Images;
using Tjeker.Processing;

namespace Tjeker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory() + @"\..\..\..\";
            var path = await new MergedImages(
                new ImagesFromVideo(
                    Path.Combine(currentDirectory, "sadness.mp4"),
                    Path.Combine(currentDirectory, "assets"),
                    3
                ),
                Path.Combine(currentDirectory, "assets")
            ).Save();
        }
    }
}