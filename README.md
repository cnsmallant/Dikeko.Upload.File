# Asp.Net WebApi 上传文件方法

#  实现功能：

1.原生js调用api上传

2.jq ajax调用api上传

# 后端

## Model

```cs
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
```

![点击并拖拽以移动](data:image/gif;base64,R0lGODlhAQABAPABAP///wAAACH5BAEKAAAALAAAAAABAAEAAAICRAEAOw==)

## 上传方法

```cs
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
```

![点击并拖拽以移动](data:image/gif;base64,R0lGODlhAQABAPABAP///wAAACH5BAEKAAAALAAAAAABAAEAAAICRAEAOw==)

# API端

```cs
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
```

![点击并拖拽以移动](data:image/gif;base64,R0lGODlhAQABAPABAP///wAAACH5BAEKAAAALAAAAAABAAEAAAICRAEAOw==)

# 前端

```html
<!DOCTYPE html>
<html lang="zh">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>上传测试</title>
    <style>
        pre {
            outline: 1px solid #ccc;
        }

        .string {
            color: green;
        }

        .number {
            color: darkorange;
        }

        .boolean {
            color: blue;
        }

        .null {
            color: magenta;
        }

        .key {
            color: red;
        }
    </style>
</head>

<body>
    <!--如果遇到不明白的或者发现BUG请加入QQ群：Java .Net Go PHP UI群：574879752 直接@群主-->
    <p><input type="file" id="upfile"></p>
    <p><input type="button" id="upJS" value="用原生JS上传"></p>
    <p><input type="button" id="upJQuery" value="用jQuery上传"></p>

    <pre id="txt"></pre>

    <script src="/js/jquery-3.4.1.min.js"></script>

    <script>
        // 原生JS版
        document.getElementById("upJS").onclick = function () { /* FormData 是表单数据类 */
            var fd = new FormData();
            var ajax = new XMLHttpRequest();
            fd.append("upload", 1); /* 把文件添加到表单里 */
            fd.append("upfile", document.getElementById("upfile").files[0]);
            ajax.open("post", "http://localhost:20861/api/file/upload/", true);
            ajax.onload = function () {
                document.getElementById("txt").innerHTML = jsonShowFn(ajax.responseText);
                console.log(ajax.responseText);
            };
            ajax.send(fd);
        }
        // jQuery版
        $('#upJQuery').on('click', function () {
            var fd = new FormData();
            fd.append("upload", 1);
            fd.append("upfile", $("#upfile").get(0).files[0]);
            $.ajax({
                url: "http://localhost:20861/api/file/upload/",
                type: "POST",
                processData: false,
                contentType: false,
                data: fd,
                success: function (d) {
                    $("#txt").html(jsonShowFn(JSON.stringify(d)));
                    console.log(d);
                }
            });
        });

        //显示json
        function jsonShowFn(json) {
            if (!json.match("^\{(.+:.+,*){1,}\}$")) {
                return json           //判断是否是json数据，不是直接返回
            }

            if (typeof json != 'string') {
                json = JSON.stringify(json, undefined, 2);
            }
            json = json.replace(/&/g, '&').replace(/</g, '<').replace(/>/g, '>');
            return json.replace(/("(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\"])*"(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)/g, function (match) {
                var cls = 'number';
                if (/^"/.test(match)) {
                    if (/:$/.test(match)) {
                        cls = 'key';
                    } else {
                        cls = 'string';
                    }
                } else if (/true|false/.test(match)) {
                    cls = 'boolean';
                } else if (/null/.test(match)) {
                    cls = 'null';
                }
                return '<span class="' + cls + '">' + match + '</span>';
            });
        }
    </script>
</body>

</html>
```

