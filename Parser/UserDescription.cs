using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Parser
{
    static class Constants
    {
        // не можем для каждого пользователя отдельно обращаться к серверу, чтобы выяснить название страны по коду,
        // поэтому захардкодим наиболее вероятные
        static public Dictionary<int, string> countries = new Dictionary<int, string>()
        {
            { 1, "Россия" },
            { 2, "Украина" },
            { 3, "Беларусь" },
            { 4, "Казахстан" },
            { 5, "Азербайджан" },
            { 6, "Армения" },
            { 7, "Грузия" },
            { 8, "Израиль" },
            { 9, "США" },
            { 10, "Канада" },
            { 11, "Кыргызстан" },
            { 12, "Латвия" },
            { 13, "Литва" },
            { 14, "Эстония" },
            { 15, "Молдова" },
            { 16, "Таджикистан" }
        };

        // то же для городов
        static public Dictionary<int, string> cities = new Dictionary<int, string>()
        {
            { 1, "Москва" },
            { 2, "Санкт-Петербург" },
            { 10, "Волгоград" },
            { 37, "Владивосток" },
            { 49, "Екатеринбург" },
            { 60, "Казань" },
            { 61, "Калининград" },
            { 72, "Краснодар" },
            { 73, "Красноярск" },
            { 95, "Нижний Новгород" },
            { 99, "Новосибирск" },
            { 104, "Омск" },
            { 110, "Пермь" },
            { 119, "Ростов-на-Дону" },
            { 123, "Самара" },
            { 151, "Уфа" },
            { 153, "Хабаровск" },
            { 158, "Челябинск" },
            { 223, "Донецк" },
            { 244, "Витебск" },
            { 280, "Харьков" },
            { 281, "Брест" },
            { 282, "Минск" },
            { 292, "Одесса" },
            { 314, "Киев" },
            { 375, "Мозырь" },
            { 377, "Николаев" },
            { 392, "Гомель" },
            { 444, "Чернигов" },
            { 455, "Мариуполь" },
            { 467, "Могилёв" },
            { 538, "Барановичи" },
            { 552, "Луганск" },
            { 610, "Пинск" },
            { 628, "Запорожье" },
            { 649, "Гродно" },
            { 650, "Днепропетровск" },
            { 761, "Винница" },
            { 916, "Кривой Рог" },
            { 1057, "Львов" },
            { 1107, "Бобруйск" },
            { 1299, "Новополоцк" },
            { 2179, "Горки" }
        };
    }

    public class UserDescription
    {
        // docs: https://vk.com/dev/fields
        // basic:
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public bool deactivated  { get; set; } = false;
        public bool hidden { get; set; } = false;
        public string photoId { get; set; } = null;
        public int sex { get; set; } = 0;         // 1 - f, 2 - m, 0 - unknown
        public string bdate { get; set; } = null;
        public string city { get; set; } = null;
        public string country { get; set; } = null;
        public bool online { get; set; } = false;
        public string domain { get; set; } = null;
        public string site { get; set; } = null;
        public List<string> universities { get; set; }      // just names
        public string status { get; set; } = null;
        public int followersCount { get; set; }

        // only for separate user:

        public string homePhone { get; set; } = null;
        public string skype { get; set; } = null;
        public string facebook { get; set; } = null;
        public string facebookName { get; set; } = null;
        public string instagramName { get; set; } = null;
        public string twitterName { get; set; } = null;

        public bool hasCounters { get; set; } = false;
        public int albumsNum { get; set; }
        public int videosNum { get; set; }
        public int audiosNum { get; set; }
        public int notesNum { get; set; }
        public int photosNum { get; set; }
        public int giftsNum { get; set; }


        public string activities { get; set; } = null;
        public string interests { get; set; } = null;
        public string music { get; set; } = null;
        public string movies { get; set; } = null;
        public string tv { get; set; } = null;
        public string books { get; set; } = null;
        public string games { get; set; } = null;
        public string about { get; set; } = null;
        public string quotes { get; set; } = null;



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
                        int city_code = int.Parse(elem.Value);
                        if (Constants.cities.ContainsKey(city_code))
                            city = Constants.cities[city_code];
                        break;
                    case "country":
                        int country_code = int.Parse(elem.Value);
                        if (Constants.countries.ContainsKey(country_code))
                            country = Constants.countries[country_code];
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

                    case "skype":
                        skype = elem.Value;
                        break;
                    case "facebook":
                        facebook = elem.Value;
                        break;
                    case "facebook_name":
                        facebookName = elem.Value;
                        break;
                    case "instagram":
                        instagramName = elem.Value;
                        break;
                    case "twitter":
                        twitterName = elem.Value;
                        break;
                    case "albums":
                        albumsNum = int.Parse(elem.Value);
                        break;
                    case "videos":
                        videosNum = int.Parse(elem.Value);
                        break;
                    case "audios":
                        audiosNum = int.Parse(elem.Value);
                        break;
                    case "notes":
                        notesNum = int.Parse(elem.Value);
                        break;
                    case "photos":
                        photosNum = int.Parse(elem.Value);
                        break;
                    case "gifts":
                        giftsNum = int.Parse(elem.Value);
                        break;
                    case "activities":
                        activities = elem.Value;
                        break;
                    case "interests":
                        interests = elem.Value;
                        break;
                    case "music":
                        music = elem.Value;
                        break;
                    case "movies":
                        movies = elem.Value;
                        break;
                    case "tv":
                        tv = elem.Value;
                        break;
                    case "books":
                        books = elem.Value;
                        break;
                    case "games":
                        games = elem.Value;
                        break;
                    case "about":
                        about = elem.Value;
                        break;
                    case "quotes":
                        quotes = elem.Value;
                        break;
                    default:
                        if ((elem.Name != "user_id") && (elem.Name != "lists") &&
                            (elem.Name != "online_mobile") && (elem.Name != "online_app"))
                            System.Diagnostics.Debug.WriteLine("unknown field " + elem.Name);
                        break;

                }
            }
        }


    }
}

