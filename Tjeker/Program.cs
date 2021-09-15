using System;
using System.IO;
using System.Threading.Tasks;
using Tjeker.Images;

namespace Tjeker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory() + @"\..\..\..\";
            var images = await new ImagesFromVideo(
                Path.Combine(currentDirectory, "sadness.mp4"),
                Path.Combine(currentDirectory, "assets"),
                10
            ).Save();
            foreach (var image in images)
            {
                Console.WriteLine(image);
            }
        }
    }
}