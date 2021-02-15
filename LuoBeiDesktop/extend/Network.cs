using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace LuoBeiDesktop.extend
{
    class Network
    {
        /// <summary>
        /// 判断网址是否存在
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool UrlIsExist(string url)
        {
            System.Uri u = null;
            try
            {
                u = new Uri(url);
            }
            catch { return false; }
            bool isExist = false;
            System.Net.HttpWebRequest r = System.Net.HttpWebRequest.Create(u) as System.Net.HttpWebRequest;
            r.Method = "HEAD";
            try
            {
                System.Net.HttpWebResponse s = r.GetResponse() as System.Net.HttpWebResponse;
                if (s.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    isExist = true;
                }
            }
            catch (System.Net.WebException x)
            {
                try
                {
                    isExist = ((x.Response as System.Net.HttpWebResponse).StatusCode != System.Net.HttpStatusCode.NotFound);
                }
                catch { isExist = (x.Status == System.Net.WebExceptionStatus.Success); }
            }
            return isExist;
        }
        /// <summary>
        /// 通过PING判断网络是否通畅
        /// </summary>
        /// <param name="strIpOrDName"></param>
        /// <returns></returns>
        public bool PingIpOrDomainName(string strIpOrDName)
        {
            try
            {
                Ping objPingSender = new Ping();
                PingOptions objPinOptions = new PingOptions();
                objPinOptions.DontFragment = true;
                string data = "";
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                int intTimeout = 120;
                PingReply objPinReply = objPingSender.Send(strIpOrDName, intTimeout, buffer, objPinOptions);
                string strInfo = objPinReply.Status.ToString();
                if (strInfo == "Success")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// POST请求与获取结果 
        /// </summary>
        /// <param name="Url">URL</param>
        /// <param name="Method">请求方式,GET,POST</param>
        /// <param name="DataStr">提交内容,格式UserName=admin&Password=123</param>
        /// <param name="ContentType">application/x-www-form-urlencoded,multipart/form-data,application/json,text/xml</param>
        /// <returns></returns>
        /// 
        public string Request(string Url, string Method = "POST", string DataStr = "", string ContentType = "application/x-www-form-urlencoded")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = Method;
            request.ContentType = ContentType;
            request.ContentLength = DataStr.Length;
            StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
            writer.Write(DataStr);
            writer.Flush();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码 
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            return retString;
        }

    }
}
