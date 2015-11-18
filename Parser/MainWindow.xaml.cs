using System;
using System.Collections.Generic;
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

namespace Parser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            /*this.InitializeComponent();

            // loading friends list by user id
            // masha 1774688
            FriendsList list = new FriendsList(1);  // durov -- works fast!
            while (!list.isReady)
            {
                System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(0.01)).Wait();
            }
            // now list is ready
            // every friend in list has basic fields

            // loading user profile by user id
            User masha = new User(1774688);
            while (!masha.isReady)
                System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(0.01)).Wait();
            // now user is ready
            // user has also additional fields (if it is not hidden)
            UserDescription profile = masha.getDescription();
            System.Diagnostics.Debug.WriteLine(profile.about);

            // loading user's friend
            FriendsList mashas_friends = masha.getFriends();
            foreach (UserDescription friend in mashas_friends)
                System.Diagnostics.Debug.WriteLine(friend.lastName);*/
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
