using System.Windows;
using Wallpaper.WPF.Views.WallPaper;

namespace Wallpaper.WPF
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainGrid.Children.Add(new PaperList());
        }

        private void Show_Custom(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new Views.ClientInfo.PaperList());
        }

        private void Show_WallPaper(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Add(new PaperList());
        }
    }
}