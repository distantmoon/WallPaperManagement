using System.Windows.Controls;
using Wallpaper.WPF.Model;

namespace Wallpaper.WPF.Views.ClientInfo
{
    /// <summary>
    /// Interaction logic for List.xaml
    /// </summary>
    public partial class PaperList : UserControl
    {
        private readonly CustomPageInfo _wallPaperPageInfo = new CustomPageInfo();
        public PaperList()
        {
            InitializeComponent();
            this.DataContext = _wallPaperPageInfo;
        }
    }
}
