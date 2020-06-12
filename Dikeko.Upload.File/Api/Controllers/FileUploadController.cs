using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.App_Code;
using Newtonsoft.Json;

namespace Api.Controllers
{
    /// <summary>
    /// 上传文件（如果遇到不明白的或者发现BUG请加入QQ群：Java .Net Go PHP UI群：574879752 直接@群主）
    /// </summary>
    [RoutePrefix("api/file")]
    public class FileUploadController : ApiController
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("upload")]
        public dynamic FileUpload()
        {
            object obj;
            var result = App_Code.FileUpload.UploadFile("test");
            if (result.FileError == "OK")
            {
                obj = new
                {
                    code = 0,
                    msg = result.FileError,
                    data = result.FilePath
                };
            }
            else
            {
                obj = new
                {
                    code = 1,
                    msg = result.FileError,
                    data = result.FilePath
                };
            }
            return Json(obj);
        }
    }
}
