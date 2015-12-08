using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace Parser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            profile(Int32.Parse(textBox.Text));
        }

        private void profile(int id)
        {
            SearchEntity masha = new SearchEntity(id);
            while (!masha.isReady)
                System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(0.01)).Wait();
            UserDescription profile = masha.getDescription();
            FriendsList mashas_friends = masha.getFriends();
            List<UserDescription> descriptions = new List<UserDescription>();
            descriptions.Add(profile);
            List<string> strings = new List<string>();
            strings.Add(profile.firstName);
            this.listBox1.ItemsSource = descriptions;
            this.listBox1.Visibility = System.Windows.Visibility.Visible;
            this.listBox2.Visibility = System.Windows.Visibility.Hidden;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            SearchEntity masha = new SearchEntity(Int32.Parse(textBox.Text));
            while (!masha.isReady)
                System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(0.01)).Wait();
            // now user is ready
            // user has also additional fields (if it is not hidden)
            UserDescription profile = masha.getDescription();

            // loading user's friend
            FriendsList mashas_friends = masha.getFriends();
            this.listBox1.Visibility = System.Windows.Visibility.Hidden;
            this.listBox2.Visibility = System.Windows.Visibility.Visible;
            this.listBox2.ItemsSource = mashas_friends.friends;    
        }

        private void listBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(listBox2, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {
                var tmp = ((UserDescription)item.DataContext).id;
                textBox.Text = tmp.ToString();
                profile(tmp);
            }
        }

        private Levels friendsList(SearchEntity masha, int i)
        {
            if (i <= 3)
            {
                Levels levels1 = new Levels(masha.getDescription(), new List<Levels>());
                foreach (UserDescription user in masha.getFriends())
                {
                    SearchEntity tmp = new SearchEntity(user.id);
                    while (!tmp.isReady)
                        System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(0.01)).Wait();
                    levels1.friends.Add(friendsList(tmp, i + 1));
                }
                return levels1;            
            }
            else
                return new Levels(masha.getDescription(), new List<Levels>());       
        }

        public string SerializeObject(object obj)
        {
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                serializer.Serialize(ms, obj);
                ms.Position = 0;
                xmlDoc.Load(ms);
                return xmlDoc.InnerXml;
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Search town = new Search(Int32.Parse(textBox2.Text));
            while (!town.isReady)
                System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(0.01)).Wait();
            Random random = new Random();
            int randomNumber = random.Next(0, 500);
            Search search = town;
            int id = search.search[randomNumber].id;
            SearchEntity masha = new SearchEntity(id);
            while (!masha.isReady)
                System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(0.01)).Wait();
            List<UserDescription> mashas_friends = masha.getFriends().friends;
            List<Levels> levelsList = new List<Levels>();
            Levels levels = friendsList(masha, 0);
            XmlDocument doc = new XmlDocument();
            string str = SerializeObject(levels);
            File.WriteAllText("MyFile.xml", str);
        }
    }
}
