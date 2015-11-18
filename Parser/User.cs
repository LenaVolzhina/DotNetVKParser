using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class User : DownloadableData
    {
        UserDescription profile;

        public User(int userId)
        {
            methodName = "users.get";
            parameters = "user_id=" + userId.ToString() +
                "&fields=photo_id,sex,bdate,city,country,online,domain,site,universities,status,followers_count";
            ReloadInfo();
        }

        protected override void ParseData(){ }
    }
}
