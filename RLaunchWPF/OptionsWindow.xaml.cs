using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Media.Imaging;


namespace RLaunchWPF {
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window {
        public OptionsWindow() {
            InitializeComponent();

            AddGamesToList();
        }

        private void AddGamesToList() {

            foreach (var file in Directory.GetFiles(@"data\meta")) {
                var game = Game.Load(file);
                var client = new WebClient();
                byte[] bitmap = client.DownloadData(game.Img.ToString());
                game.Img = BitmapFrame.Create(new MemoryStream(bitmap), BitmapCreateOptions.None,
                    BitmapCacheOption.OnLoad);
                OptionsListBox.Items.Add(game);

            }
        }

        private void RemoveInst_OnClick(object sender, RoutedEventArgs e) {
            
            if (OptionsListBox.SelectedItem == null) {
                MessageBox.Show("No game was selected.", "Error");
                return;
            }
            
            var game = (Game)OptionsListBox.SelectedItem;
            
            var confirmation = MessageBox.Show($"Are you sure you want to delete {game.Name}? This cannot be undone.", "Confirm",
                MessageBoxButton.YesNo);

            if (confirmation == MessageBoxResult.Yes) {
                Directory.Delete(@"data\games\" + game.Name.Replace(" ", "") + game.Ver, true);
                File.Delete(@"data\meta\" + game.Name.Replace(" ", "") + game.Ver + ".json");
            }
            
            MessageBox.Show("Game deleted successfully.", "Success");
            
            OptionsListBox.Items.Clear();
            AddGamesToList();
            DialogResult = true;
        }
    }
}