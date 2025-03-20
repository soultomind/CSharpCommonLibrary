using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Utilities
{
    public static class ImageUtility
    {
        /// <summary>
        /// <paramref name="srcImage"/> 이미지를 <paramref name="resizeImageResolution"/> 값 으로 리사이즈 합니다.
        /// </summary>
        /// <param name="srcImage">원본 이미지</param>
        /// <param name="srcImageResolution">원본 이미지 해상도(가로 세로 값 동일)</param>
        /// <param name="resizeImageResolution">리 사이즈 할 이미지 해상도(가로 세로 값 동일)</param>
        /// <returns></returns>
        public static Image ResizeImage(Image srcImage, int srcImageResolution, int resizeImageResolution)
        {
            return ResizeImage(srcImage, srcImageResolution, srcImageResolution, resizeImageResolution, resizeImageResolution);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcImage">원본 이미지</param>
        /// <param name="resizeHorizontalResolution">리 사이즈 할 이미지 가로 해상도</param>
        /// <param name="resizeVerticalResolution">리 사이즈 할 이미지 세로 해상도</param>
        /// <returns></returns>
        public static Image ResizeImage(Image srcImage, float resizeHorizontalResolution, float resizeVerticalResolution)
        {
            return ResizeImage(srcImage, srcImage.HorizontalResolution, srcImage.VerticalResolution, resizeHorizontalResolution, resizeVerticalResolution);
        }

        /// <summary>
        /// <paramref name="srcImage"/> 이미지를 <paramref name="resizeHorizontalResolution"/>,<paramref name="resizeVerticalResolution"/>  값 으로 리사이즈 합니다.
        /// </summary>
        /// <param name="srcImage">원본 이미지</param>
        /// <param name="srcHorizontalResolution">원본 이미지 가로 해상도</param>
        /// <param name="srcVerticalResolution">원본 이미지 세로 해상도</param>
        /// <param name="resizeHorizontalResolution">리 사이즈 할 이미지 가로 해상도</param>
        /// <param name="resizeVerticalResolution">리 사이즈 할 이미지 세로 해상도</param>
        /// <returns></returns>
        public static Image ResizeImage(Image srcImage, float srcHorizontalResolution, float srcVerticalResolution, float resizeHorizontalResolution, float resizeVerticalResolution)
        {
            float resizeWidth = (srcImage.Width * resizeHorizontalResolution) / srcHorizontalResolution;
            float resizeHeight = (srcImage.Height * resizeVerticalResolution) / srcVerticalResolution;

            Bitmap resizeImage = new Bitmap(srcImage, new Size((int)resizeWidth, (int)resizeHeight));
            return resizeImage;
        }
        /// <summary>
        /// 이미지(<paramref name="image"/>)를 <paramref name="imageFormat"/> 타입 형태의 Base64 문자열로 반환합니다.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="imageFormat"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.Runtime.InteropServices.ExternalException"></exception>
        public static string ToBase64String(Image image, ImageFormat imageFormat)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, imageFormat);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        /// <summary>
        /// <paramref name="base64String"/>(Base64) 이미지 데이터로부터 이미지를 반환합니다.
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static Image ToBase64StringImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
    }
}
