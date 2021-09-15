using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;

namespace Tjeker.Images
{
    public class ImagesFromVideo : IImages
    {
        private readonly string _videoPath;
        
        private readonly string _saveFolder;

        private readonly Func<double, double> _countResolve;

        public ImagesFromVideo(string videoPath, string saveFolder, int count)
            : this(videoPath, saveFolder, secondDuration => count)
        {
            
        }

        public ImagesFromVideo
            (string videoPath, string saveFolder, Func<double, double> countResolve)
        {
            _videoPath = videoPath;
            _saveFolder = saveFolder;
            _countResolve = countResolve;
        }

        public async Task<IEnumerable<string>> Save()
        {
            var video = new MediaFile {Filename = _videoPath};
            var paths = new List<string>();

            return await Task.Run(() =>
            {
                using (var engine = new Engine())
                {
                    engine.GetMetadata(video);
                    
                    var secondDuration = video.Metadata.Duration.TotalSeconds;
                    var frameCount = _countResolve.Invoke(secondDuration);
                    
                    for (var index = 1; index <= frameCount; index++)
                    {
                        var secondOffset = TimeSpan.FromSeconds(
                            secondDuration / frameCount * index
                        );
                        paths.Add(SaveImage(video, secondOffset, engine));
                    }
                }
                
                return paths;
            });
        }

        private string SaveImage(MediaFile video, TimeSpan timeOffset, Engine engine)
        {
            var output = new MediaFile
            {
                Filename = Path.Combine(_saveFolder, new RandomString().Value() + ".jpg")
            };

            var options = new ConversionOptions {Seek = timeOffset};
            engine.GetThumbnail(video, output, options);

            return output.Filename;
        }
    }
}