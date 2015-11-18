using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public abstract class DownloadableData
    {
        // https://api.vk.com/method/'''METHOD_NAME'''?'''PARAMETERS'''&access_token='''ACCESS_TOKEN'''

        protected string data { get; set; }
        protected string linkToServer = "http://api.vk.com/method/";
        protected string link;

        protected string methodName;
        protected string parameters = null;

        public bool isReady = false;

        protected void ReloadInfo()
        {
            link = linkToServer + methodName + ".xml";
            if (parameters != null)
                link += "?" + parameters;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(link);
            request.BeginGetResponse(FinishData, request);
        }

        protected void FinishData(IAsyncResult result)
        {

            HttpWebRequest request = result.AsyncState as HttpWebRequest;
            WebResponse response = request.EndGetResponse(result);
            using (StreamReader httpWebStreamReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")))
            {
                data = httpWebStreamReader.ReadToEnd();
            }
            System.Diagnostics.Debug.WriteLine("started " + link);
            ParseData();
            System.Diagnostics.Debug.WriteLine("done with " + link);
            isReady = true;
        }


        abstract protected void ParseData();

    }
}
