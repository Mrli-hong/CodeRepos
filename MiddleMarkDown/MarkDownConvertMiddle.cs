using Markdig;
using Microsoft.Extensions.FileProviders;
using System.Text;
using UtfUnknown;

namespace MiddleMarkDown
{
    public class MarkDownConvertMiddle
    {
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment env;
        public MarkDownConvertMiddle(RequestDelegate next, IWebHostEnvironment env)
        {
            this.next = next;
            this.env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path.ToString();
            //返回应用程序所在路径
            //string file = env.ContentRootPath;
            //返回wwwroot所在路径
            string file = env.WebRootPath + path;
   
            if (File.Exists(file)&&file.IndexOf(".md")!=-1)
            {
                DetectionResult codeResult = CharsetDetector.DetectFromFile(file);
                string encodeName = codeResult?.Detected?.EncodingName ?? "UTF-8";

                string md = File.ReadAllText(file, Encoding.GetEncoding(encodeName));
                var result = Markdown.ToHtml(md);
                context.Response.ContentType = "text/html;charset=UTF-8";
                await context.Response.WriteAsync(result);
            }
            else
                await next.Invoke(context);
        }
    }
}
