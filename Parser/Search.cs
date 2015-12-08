using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Parser
{
    public class Search : DownloadableData
    {
        //получение токена (его надо менять каждый день)
        //https://oauth.vk.com/authorize?client_id=5174921&display=page&redirect_uri=https://oauth.vk.com/blank.html&response_type=token&v=5.40
        //https://api.vk.com/method/users.search.xml?city=11&count=1000&v=5.25&access_token=bf0ee6f7182116e53432d4ff9f8ca6961a5a180f95ca504c7291275b51382ee19b02ab7ed2eb7ea6a4c9f

        string access_token = "bf0ee6f7182116e53432d4ff9f8ca6961a5a180f95ca504c7291275b51382ee19b02ab7ed2eb7ea6a4c9f";
        public List<UserDescription> search { get; set; }

        public Search(int city)
        {
            methodName = "users.search";
            parameters = "city=" + city +
                "&count=1000&v=5.25&" + "access_token=" + access_token;
            search = new List<UserDescription>();
            ReloadInfo();
        }

        protected override void ParseData()
        {
            // Loading from a file, you can also load from a stream
            var xml = XDocument.Load(new StringReader(data));

            // Query the data and write out a subset of contacts
            var users = from item in xml.Root.Descendants("user")
                        let elements = item.Elements()
                        select new UserDescription(elements);
            search = users.ToList();
        }

        public IEnumerator<UserDescription> GetEnumerator()
        {
            return search.GetEnumerator();
        }
    }
}
