using Newtonsoft.Json.Linq;
using RLaunch;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace RLaunchWPF
{
    /// <summary>
    /// Interaction logic for AddInstanceWindow.xaml
    /// </summary>
    public partial class AddInstanceWindow : Window
    {
        public AddInstanceWindow()
        {
            InitializeComponent();

            AddGamesToList();
        }

        private void AddGamesToList() {

            foreach (var file in Directory.GetFiles("availableGames")) {
                var game = Game.Load(file);
                var client = new WebClient();
                byte[] bitmap = client.DownloadData(game.Img.ToString());
                game.Img = BitmapFrame.Create(new MemoryStream(bitmap), BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                GameListBox.Items.Add(game);

            }
        }

        private void GameListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            var game = GameListBox.SelectedItem as Game;
            DescText.Text = game.Desc;
            byte[] bitmap = new WebClient().DownloadData(game.ExImg.ToString());
            ExampleImg.Source = BitmapFrame.Create(new MemoryStream(bitmap), BitmapCreateOptions.None, BitmapCacheOption.OnLoad);

        }

        private void FunnyLabel_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("There was no reason to click that label.");
            MessageBox.Show("You have wasted your time.");

        }

        private void DownloadInst_OnClick(object sender, RoutedEventArgs e)
        {
            if (GameListBox.SelectedItem == null) {
                MessageBox.Show("Please select a game!");
                return;
            }
            var game = GameListBox.SelectedItem as Game;
            if (Directory.Exists($@"data\games\{game.Name}{game.Ver}"))
            {
                MessageBox.Show("Game already exists!");
                return;
            }
            
            var client = new WebClient();
            client.DownloadFile(game.Src, @"data\games\game.zip");

            Directory.CreateDirectory($@"data\games\{game.Name}{game.Ver}");
            ZipFile.ExtractToDirectory(@$"data\games\game.zip", @$"data\games\{game.Name}{game.Ver}");

            try {
                File.Copy(@$"availableGames\{game.Name}{game.Ver}.json", $@"data\meta\{game.Name}{game.Ver}.json");
            }
            catch (Exception) {
                MessageBox.Show("Game already exists!");
            }

            File.Delete(@"data\games\game.zip");
            MessageBox.Show("Downloaded Game!\nRemember to refresh!");
            
            this.Close();
        }
    }
}
