using System.Windows;
using Wallpaper.WPF.Model;

namespace Wallpaper.WPF
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly WallPaperPageInfo _wallPaperPageInfo = new WallPaperPageInfo();
      

        public MainWindow()
        {
            InitializeComponent();
            this.WallPaperTab.DataContext = _wallPaperPageInfo;
        }
    }
}