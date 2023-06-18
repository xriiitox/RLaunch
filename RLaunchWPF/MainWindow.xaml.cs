using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RLaunchWPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        

        public MainWindow() {
            InitializeComponent();
            
            if (!Directory.Exists(@"data\meta")) {
                Directory.CreateDirectory(@"data\meta");
            }

            AddGamesToList();
        }

        private void AddInstance_OnClick(object sender, RoutedEventArgs e) {
            AddInstanceWindow a = new();
            if ((bool)a.ShowDialog()) {
                GameList.Items.Clear();
                AddGamesToList();
            }
            
        }

        private void Options_OnClick(object sender, RoutedEventArgs e) {
            OptionsWindow o = new();
            if ((bool)o.ShowDialog()) {
                GameList.Items.Clear();
                AddGamesToList();
            }
        }

        private void Refresh_OnClick(object sender, RoutedEventArgs e) {
            GameList.Items.Clear();
            AddGamesToList();
        }

        private void Play_OnClick(object sender, RoutedEventArgs e) {
            var g = (sender as Button).DataContext as Game;
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory + $@"{g.Dir}");
            Process game = new();
            game.StartInfo.FileName = g.Exe;
            game.Start();
            game.WaitForExit();
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void AddGamesToList() {

            foreach (var file in Directory.GetFiles(@"data\meta")) {
                var game = Game.Load(file);

                var bitmap = new WebClient().DownloadData(game.Img.ToString());
                game.Img = BitmapFrame.Create(new MemoryStream(bitmap), BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                GameList.Items.Add(game);

            }
        }

        private void NameColumnHeader_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Why are you going around clicking on things?");
        }

        private void Play_Column_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked me!");
        }
    }
}
