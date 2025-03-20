using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.IO
{
    public class ImageManager
    {
        public static IList<Image> GetImages(ImageFileCreateInfo imageFileCreateInfo)
        {
            IList<Image> list = new List<Image>();

            string[] imageFiles = Directory.GetFiles(
                imageFileCreateInfo.Directory,
                "*.*",
                SearchOption.AllDirectories)
            .Where(s => imageFileCreateInfo.FileExtension.Contains(Path.GetExtension(s).ToLower())).ToArray();

            Image image = null;
            foreach (string imageFile in imageFiles)
            {
                if (imageFileCreateInfo.FileCreateInfo.FileLock)
                {
                    image = Bitmap.FromFile(imageFile);
                }
                else
                {
                    byte[] imageBytes = null;
                    using (FileStream fs = new FileStream(imageFile, imageFileCreateInfo.FileCreateInfo.FileMode, imageFileCreateInfo.FileCreateInfo.FileAccess))
                    {
                        imageBytes = new byte[fs.Length];
                        fs.Read(imageBytes, 0, imageBytes.Length);
                        image = Image.FromStream(new MemoryStream(imageBytes));
                        imageBytes = null;
                    }
                }

                if (image != null && image.Width > 0 && image.Height > 0)
                {
                    list.Add(image);
                }
            }

            return list;
        }
    }
}
