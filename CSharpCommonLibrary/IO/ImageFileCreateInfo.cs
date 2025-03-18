using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.IO
{
    public class ImageFileCreateInfo
    {
        /// <summary>
        /// 생성할 이미지가 있는 디렉토리
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// 생성할 이미지 확장자
        /// <para>*.jpg,*.png 형태로 값 명시</para>
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// 파일 생성 방법
        /// </summary>
        public FileCreateInfo FileCreateInfo { get; set; }

        public ImageFileCreateInfo(string directory, string fileExtension)
        {
            Directory = directory;
            FileExtension = fileExtension;
        }

        public ImageFileCreateInfo(string directory, string fileExtension, FileCreateInfo fileCreateInfo)
            : this(directory, fileExtension)
        {
            FileCreateInfo = fileCreateInfo;
        }
    }
}
