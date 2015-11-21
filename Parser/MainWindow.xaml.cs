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
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            makesomeshit(Int32.Parse(textBox.Text));
        }

        private void makesomeshit(int id)
        {
            User masha = new User(id);
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
            User masha = new User(Int32.Parse(textBox.Text));
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
                makesomeshit(((UserDescription) item.DataContext).id);
            }
        }
    }
}
