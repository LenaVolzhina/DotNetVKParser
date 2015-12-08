using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Parser
{
    public class SearchEntity : DownloadableData
    {
        UserDescription profile;

        public SearchEntity(int userId)
        {
            methodName = "users.get";
            parameters = "user_id=" + userId.ToString() +
                "&fields=photo_id,sex,bdate,city,country,online,domain,site,universities,status,followers_count" +
                "counters,activities,interests,music,movies,tv,books,games,about,quotes";
            ReloadInfo();
        }

        protected override void ParseData()
        {
            var xml = XDocument.Load(new StringReader(data));

            profile = new UserDescription(xml.Root.Descendants("user").First().Elements());
        }

        public UserDescription getDescription()
        {
            return profile;
        }

        public FriendsList getFriends()
        {
            FriendsList friends = new FriendsList(profile.id);
            while (!friends.isReady)
                System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(0.01)).Wait();

            return friends;
        }

        public Search getSearch()
        {
            Search search = new Search(11);
            while (!search.isReady)
                System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(0.01)).Wait();

            return search;
        }

    }
}

