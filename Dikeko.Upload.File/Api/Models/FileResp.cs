using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    /// <summary>
    /// 上传文件（如果遇到不明白的或者发现BUG请加入QQ群：Java .Net Go PHP UI群：574879752 直接@群主）
    /// </summary>
    public class FileResp
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 上传错误
        /// </summary>
        public string FileError { get; set; }
    }
}