using System.Windows.Controls;
using Wallpaper.WPF.Model;

namespace Wallpaper.WPF.Views.WallPaper
{
    /// <summary>
    ///     Interaction logic for List.xaml
    /// </summary>
    public partial class PaperList : UserControl
    {
        private readonly WallPaperPageInfo _wallPaperPageInfo = new WallPaperPageInfo();
         public PaperList()
        {
            InitializeComponent();
            this.DataContext = _wallPaperPageInfo;
        }
    }
}