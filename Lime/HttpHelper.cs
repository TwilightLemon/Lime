using Lime.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Lime
{
    public class HttpHelper
    {
        public static async Task<string> GetWebAsync(string url)
        {
            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
            var o = await hwr.GetResponseAsync();
            StreamReader sr = new StreamReader(o.GetResponseStream(), Encoding.UTF8);
            var st = await sr.ReadToEndAsync();
            sr.Dispose();
            return st;
        }
        public static async Task<BitmapImage> HttpDownloadFileAsync(string url)
        {
            HttpWebRequest hwr = WebRequest.Create(url) as HttpWebRequest;
            hwr.Accept="text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            hwr.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
            hwr.Headers.Add("Cache-Control", "max-age=0");
            hwr.KeepAlive = true;
            hwr.Referer = url;
            hwr.Headers.Add("Upgrade-Insecure-Requests", "1");
            hwr.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";
            hwr.Timeout = 20000;
            HttpWebResponse response = await hwr.GetResponseAsync() as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();
            byte[] bArr = new byte[response.ContentLength];
            await responseStream.ReadAsync(bArr, 0, bArr.Length);
            responseStream.Close();
            Settings.Default.tx = Convert.ToBase64String(bArr);
            return bArr.ToBitmapImage();
        }
        public static async Task<string> GetWebDatacAsync(string url, string Cookie)
        {
            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
            hwr.Timeout = 20000;
            hwr.KeepAlive = true;
            hwr.Headers.Add(HttpRequestHeader.CacheControl, "max-age=0");
            hwr.Headers.Add(HttpRequestHeader.Upgrade, "1");
            hwr.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.110 Safari/537.36";
            hwr.Accept = "*/*";
            hwr.Referer = "https://y.qq.com/portal/player.html";
            hwr.Host = "c.y.qq.com";
            hwr.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
            hwr.Headers.Add(HttpRequestHeader.Cookie, Cookie);
            var o = await hwr.GetResponseAsync();
            StreamReader sr = new StreamReader(o.GetResponseStream(), Encoding.UTF8);
            var st = await sr.ReadToEndAsync();
            sr.Dispose();
            return st;
        }
    }
}
