using Lime.Properties;
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

namespace Lime
{
    /// <summary>
    /// MeControl.xaml 的交互逻辑
    /// </summary>
    public partial class MeControl : UserControl
    {
        public MeControl(string text,Brush tx)
        {
            InitializeComponent();
            Te.Text = text;
            //这步是为了保证头像清晰
            RenderOptions.SetBitmapScalingMode(bd, BitmapScalingMode.Fant);
            bd.Background = tx;
            UName.Text = Settings.Default.name;
        }
    }
}
