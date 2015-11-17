using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Parser
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

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
                System.Diagnostics.Debug.WriteLine(friend.lastName);
        }
    }
}
