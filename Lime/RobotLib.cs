using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lime
{
    public class RobotLib
    {
        public static async Task<RobotData> TalkAsync(string text, string id)
        {
            try
            {
                JObject obj = JObject.Parse(await HttpHelper.GetWebAsync("http://www.tuling123.com/openapi/api?key=0651b32a3a6c8f54c7869b9e62872796&info=" + Uri.EscapeUriString(text) + "&userid=" + Uri.EscapeUriString(id)));
                if ((string)obj["code"] == "100000" || obj["code"].ToString() == "40002")
                    return new RobotData() { text = obj["text"].ToString(), url = "null" };
                else if ((string)obj["code"] == "200000")
                    return new RobotData() { text = obj["text"].ToString(), url = obj["url"].ToString() };
                else return new RobotData() { text = obj["text"].ToString(), url = "null" };
            }
            catch { return new RobotData() { text = "小萌机器人似乎遇到了些问题", url = "null" }; }
        }
    }
    public class RobotData
    {
        public string text;
        public string url;
    }
}
