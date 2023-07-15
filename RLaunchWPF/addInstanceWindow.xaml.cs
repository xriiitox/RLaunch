using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;


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

        private async void AddGamesToList() {

            foreach (var file in Directory.GetFiles("availableGames")) {
                var game = Game.Load(file);
                var client = new WebClient();
                byte[] bitmap = client.DownloadData(game.Img.ToString());
                game.Img = BitmapFrame.Create(new MemoryStream(bitmap), BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                await GameListBox.Dispatcher.InvokeAsync(() => GameListBox.Items.Add(game));

            }
        }

        private void GameListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            var game = GameListBox.SelectedItem as Game;
            DescText.Text = game.Desc;
            byte[] bitmap = File.ReadAllBytes(@$"availableGames\exImgs\{game.Name.Replace(" ","")}.png");
            ExampleImg.Source = BitmapFrame.Create(new MemoryStream(bitmap), BitmapCreateOptions.None, BitmapCacheOption.OnLoad);

        }

        private void FunnyLabel_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("There was no reason to click that label.");
            MessageBox.Show("You have wasted your time.");

        }

        private async void DownloadInst_OnClick(object sender, RoutedEventArgs e)
        {
            if (GameListBox.SelectedItem == null) {
                MessageBox.Show("Please select a game!", "Error");
                return;
            }
            var game = (Game)GameListBox.SelectedItem;
            if (Directory.Exists($@"data\games\{game.Name.Replace(" ","")}{game.Ver}") 
                || File.Exists($@"data\meta\{game.Name.Replace(" ","")}{game.Ver}.json")) 
            {
                MessageBox.Show("Game already exists!", "Error");
                return;
            }

            using (var client = new WebClient())
            {
                client.DownloadFileCompleted   += (s, e) => MessageBox.Show("Game downloaded.");;
                client.DownloadProgressChanged += (s, e) => DownloadInst.Content = ($"Downloading {e.ProgressPercentage}%");
                await client.DownloadFileTaskAsync(new Uri(game.Src), @"data\games\game.zip");
            }

            ZipFile.ExtractToDirectory(@"data\games\game.zip", @$"data\games\{game.Name.Replace(" ", "")}{game.Ver}");

            File.Copy($@"availableGames\{game.Name.Replace(" ", "")}{game.Ver}.json", $@"data\meta\{game.Name.Replace(" ", "")}{game.Ver}.json");

            File.Delete(@"data\games\game.zip");

            DialogResult = true;
            Close();
        }
    }
}
