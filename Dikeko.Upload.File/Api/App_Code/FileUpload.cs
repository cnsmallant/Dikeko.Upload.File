using Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Api.App_Code
{
    /// <summary>
    /// 上传文件（如果遇到不明白的或者发现BUG请加入QQ群：Java .Net Go PHP UI群：574879752 直接@群主）
    /// </summary>
    public class FileUpload
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="folderName">文件夹</param>
        /// <returns></returns>
        public static FileResp UploadFile(string folderName)
        {
            try
            {
                FileResp fileResp;
                System.Web.HttpRequest request = System.Web.HttpContext.Current.Request;
                HttpFileCollection fileCollection = request.Files;
                // 判断是否有文件
                if (fileCollection.Count > 0)
                {
                    // 获取文件
                    HttpPostedFile httpPostedFile = fileCollection[0];
                    // 获取文件扩展名
                    string fileExtension = Path.GetExtension(httpPostedFile.FileName);

                    string strHashData = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    var Year = DateTime.Now.ToString("yyyy");
                    var Month = DateTime.Now.ToString("MM");
                    var Day = DateTime.Now.ToString("dd");
                    // 设置文件夹名称
                    string virtualPathDir = string.Format("/upload/file/{0}/{1}/{2}/{3}/", folderName, Year, Month, Day);
                    // 设置上传路径
                    string virtualPath = virtualPathDir + strHashData + fileExtension;
                    var pathDir = AppDomain.CurrentDomain.BaseDirectory + virtualPathDir;
                    var path = AppDomain.CurrentDomain.BaseDirectory + virtualPath;
                    // 如果目录不存在则要先创建
                    if (!Directory.Exists(pathDir))
                    {
                        Directory.CreateDirectory(pathDir);
                    }
                    httpPostedFile.SaveAs(path);
                    fileResp = new FileResp
                    {
                        FilePath = virtualPath,
                        FileError = "OK"
                    };
                }
                else
                {
                    fileResp = new FileResp
                    {
                        FilePath = string.Empty,
                        FileError = "请选择要上传的文件"
                    };
                }
                return fileResp;
            }
            catch (Exception e)
            {

                FileResp fileResp = new FileResp
                {
                    FilePath = string.Empty,
                    FileError = e.Message
                };

                return fileResp;
            }
        }
    }
}