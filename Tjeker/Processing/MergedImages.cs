using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;

namespace Tjeker.Processing
{
    public class MergedImages : IImage
    {
        private readonly IImages _images;
        
        private readonly string _path;

        public MergedImages(IImages images, string folder)
            : this(images, folder, new RandomString().Value() + ".jpg")
        {

        }

        public MergedImages(IImages images, string folder, string name)
        {
            _images = images;
            _path = Path.Combine(folder, name);
        }
        
        public async Task<string> Save()
        {
            var paths = await _images.Save();

            var plot = Plot(paths);
            Draw(paths, plot);
            plot.Save(_path);

            return _path;
        }

        private void Draw(IEnumerable<string> imagePaths, Bitmap plot)
        {
            var leftOffset = 0;
            var graphics = Graphics.FromImage(plot);
            
            foreach (var path in imagePaths)
            {
                using (var image = Image.FromFile(path))
                {
                    graphics.DrawImage(image, new Point(leftOffset, 0));
                    leftOffset += image.Width;
                }
            }
            
            graphics.Dispose();
        }

        private Bitmap Plot(IEnumerable<string> imagePaths)
        {
            var maxHeight = 0;
            var width = 0;
            foreach (var path in imagePaths)
            {
                using (var image = Image.FromFile(path))
                {
                    if (image.Height > maxHeight)
                    {
                        maxHeight = image.Height;
                    }

                    width += image.Width;
                }
            }

            return new Bitmap(width, maxHeight);
        }
    }
}