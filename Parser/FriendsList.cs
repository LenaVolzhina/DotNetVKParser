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
    class FriendsList : DownloadableData
    {
        //http://api.vk.com/method/friends.get.xml?user_id=2569908&fields=photoId,sex,bdate,city,country,online,domain,site,universities,status,followers_count&count=10
        List<UserDescription> friends;

        public FriendsList(int userId)
        {
            methodName = "friends.get";
            parameters = "user_id=" + userId.ToString() +
                "&fields=photo_id,sex,bdate,city,country,online,domain,site,universities,status,followers_count";
            friends = new List<UserDescription>();
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
            friends = users.ToList();
        }

        public IEnumerator<UserDescription> GetEnumerator()
        {
            return friends.GetEnumerator();
        }
    }
}