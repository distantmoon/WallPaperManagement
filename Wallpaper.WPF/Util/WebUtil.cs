using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wallpaper.WPF.Util
{
    public class WebUtil
    {
        public static T GetResult<T>(string requestUrl, Dictionary<string,string> parameters = null)
        {
            StringBuilder postDataStringBuilder = new StringBuilder();
            string postString = string.Empty;
            if (parameters!=null)
            {
                foreach (var parameter in parameters)
                {
                    postDataStringBuilder.Append(string.Format("{0}={1}&", parameter.Key, parameter.Value));
                }
                postString = postDataStringBuilder.ToString().TrimEnd("&".ToCharArray());
            }
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    byte[] sendBytes = Encoding.UTF8.GetBytes(postString);
                    byte[] receiveBytes = client.UploadData(requestUrl, sendBytes);
                    string receiveString = Encoding.UTF8.GetString(receiveBytes);
                    IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
                    timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
                    var result = JsonConvert.DeserializeObject<T>(receiveString, timeConverter);
                    return result;
            }
        }
    }
}