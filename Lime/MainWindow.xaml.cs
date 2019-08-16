using Lime.Properties;
using Newtonsoft.Json.Linq;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lime
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RenderOptions.SetBitmapScalingMode(UserTX, BitmapScalingMode.Fant);
            if (Settings.Default.qq != "")
            {
                UserName.Text = Settings.Default.name;
                var tx = Convert.FromBase64String(Settings.Default.tx).ToBitmapImage();
                UserTX.Background = new ImageBrush(tx);
            }
        }

        #region 窗口控制 最大化|最小化|关闭|拖动
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void CloseBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void MaxBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else WindowState = WindowState.Normal;
        }

        private void MinBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        #endregion
        #region 登录
        LoginWindow lw;
        private void UserName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lw = new LoginWindow(this);
            lw.Show();
        }
        public async void Login(string cdata)
        {
            lw.Close();

            string qq = TextHelper.XtoYGetTo(cdata, "Login:", "###", 0);
            string g_tk = TextHelper.XtoYGetTo(cdata, "g_tk[", "]sk", 0);
            string Cookie = TextHelper.XtoYGetTo(cdata, "Cookie[", "]END", 0);

            var sl = await HttpHelper.GetWebDatacAsync($"https://c.y.qq.com/rsc/fcgi-bin/fcg_get_profile_homepage.fcg?loginUin={qq}&hostUin=0&format=json&inCharset=utf8&outCharset=utf-8&notice=0&platform=yqq&needNewCode=0&cid=205360838&ct=20&userid={qq}&reqfrom=1&reqtype=0", Cookie);
            var sdc = JObject.Parse(sl)["data"]["creator"];

            var tx=await HttpHelper.HttpDownloadFileAsync(sdc["headpic"].ToString().Replace("http://", "https://"));
            string name = sdc["nick"].ToString();

            UserName.Text = name;
            UserTX.Background = new ImageBrush(tx);

            Settings.Default.name = name;
            Settings.Default.qq = qq;
            Settings.Default.Save();
        }
        #endregion

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ItemList.Width = ActualWidth;
            foreach (UserControl o in ItemList.Children)
            {
                o.Width = ActualWidth;
            }
        }

        private async void Send_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Input.Text != "") {
                var data = await RobotLib.TalkAsync(Input.Text, Settings.Default.qq);
                var U = new MeControl(Input.Text,UserTX.Background)
                {
                    Width = ActualWidth,
                    Opacity = 0
                };
                var Rb = new RobotControl(data.text)
                {
                    Width = ActualWidth,
                    Opacity = 0
                };
                ItemList.Children.Add(U);
                ItemList.Children.Add(Rb);
                var b = new DoubleAnimation(1, TimeSpan.FromSeconds(0.2));
                U.BeginAnimation(OpacityProperty, b);
                Rb.BeginAnimation(OpacityProperty, b);
                sv.ScrollToBottom();
                Input.Text = "";
            }
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Send_MouseDown(null, null);
        }
    }
}
