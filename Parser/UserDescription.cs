using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Parser
{
    class UserDescription
    {
        // sex,photo_id,bdate,city,country,online,domain,universities,schools,status,last_seen,followers_count,counters
        // docs: https://vk.com/dev/fields
        // fields=photoId,sex,bdate,city,country,online,domain,site,universities,status,followers_count
        public int id;
        public string firstName;
        public string lastName;
        public bool deactivated = false;
        public bool hidden = false;
        public string photoId = null;
        public int sex = 0;         // 1 - f, 2 - m, 0 - unknown
        public string bdate = null;
        public string city = null;
        public string country = null;
        public bool online = false;
        public string domain = null;
        public string site = null;
        public List<string> universities;       // just names
        public string status = null;
        public int followersCount;


        public UserDescription(IEnumerable<System.Xml.Linq.XElement> elements)
        {
            universities = new List<string>();
            foreach (var elem in elements)
            {
                switch (elem.Name.ToString())
                {
                    case "uid":
                        id = int.Parse(elem.Value);
                        break;
                    case "first_name":
                        firstName = elem.Value;
                        break;
                    case "last_name":
                        lastName = elem.Value;
                        break;
                    case "deactivated":
                        deactivated = elem.Value == "1";
                        break;
                    case "hidden":
                        hidden = elem.Value == "1";
                        break;
                    case "photo_id":
                        photoId = elem.Value;
                        break;
                    case "sex":
                        sex = int.Parse(elem.Value);
                        break;
                    case "bdate":
                        bdate = elem.Value;
                        break;
                    case "city":
                        city = elem.Value;
                        break;
                    case "country":
                        country = elem.Value;
                        break;
                    case "online":
                        online = elem.Value == "1";
                        break;
                    case "domain":
                        domain = elem.Value;
                        break;
                    case "site":
                        site = elem.Value;
                        break;
                    case "universities":
                        var names = from item in elem.Descendants("university")
                                    select item.Element("name").Value;
                        foreach (string name in names)
                            universities.Add(name);                        
                        break;
                    case "status":
                        status = elem.Value;
                        break;
                    case "followers_count":
                        followersCount = int.Parse(elem.Value);
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine("unknown field " + elem.Name);
                        break;

                }
            }
            /*
                        {
                            firstName = item.Element("first_name").Value,
                            lastName = item.Element("last_name").Value,
                            hidden = (hidden != null) ? item.Element("hidden").Value : "0",
                        }*/
        }
    }
}
